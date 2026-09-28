using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace FC_UI.Controls;

[ToolboxBitmap(typeof(ColorDialog))]
[Description("HSV color wheel picker with a brightness slider.")]
[DefaultEvent("ColorChanged")]
public partial class ZColorPicker : FControlBase
{
    #region Fields

    private enum DragTarget
    {
        None,
        Wheel,
        Brightness
    }

    // Proportions of the original 185x210 layout.
    private const float BarWidthRatio = 17F / 179F;
    private const float BarHeightRatio = 107F / 150F;
    private const int WheelBarGap = 12;
    private const int ContentPadding = 3;

    private float _hue;
    private float _saturation;
    private float _brightness = 1F;
    private bool _updatingFromHsv;
    private DragTarget _dragTarget;

    private Bitmap? _wheelBitmap;

    #endregion

    #region Properties

    public delegate void ColorChangedHandler(Color color);

    [Category("FC_UI")]
    [Description("Occurs when the selected color changes.")]
    public event ColorChangedHandler ColorChanged = delegate { };

    [Category("ZColorPicker")]
    [Description("Selected color (Color.Empty = nothing selected)")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color SelectedColor
    {
        get;
        set
        {
            if (field == value) return;
            field = value;

            if (!_updatingFromHsv && !value.IsEmpty)
            {
                (_hue, _saturation, _brightness) = DrawEngine.RgbToHsv(value);
            }

            AccessibilityNotifyClients(AccessibleEvents.ValueChange, -1);
            Invalidate();
            ColorChanged(value);
        }
    }

    [Category("ZColorPicker")]
    [Description("Show the RGB and HEX values under the wheel")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool ShowColorInfo
    {
        get;
        set
        {
            field = value;
            UpdateGeometry();
            Invalidate();
        }
    }

    [Category("ZColorPicker")]
    [Description("Color of the RGB/HEX text")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color InfoTextColor
    {
        get;
        set { field = value; Invalidate(); }
    }

    [Category("ZColorPicker")]
    [Description("Selection marker color")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color MarkerColor
    {
        get;
        set { field = value; Invalidate(); }
    }

    // --- Style ---

    [Category("ZColorPicker")]
    [Description("Control style")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public ControlStyleMode ControlStyle
    {
        get;
        set
        {
            field = value;
            switch (field)
            {
                case ControlStyleMode.Default:
                    Size = new Size(185, 210);
                    BackColor = Color.Transparent;
                    ForeColor = Color.FromArgb(245, 245, 245);
                    InfoTextColor = Color.WhiteSmoke;
                    MarkerColor = Color.DarkGray;
                    ShowColorInfo = true;
                    Rgb = false;
                    ShowBackground = false;
                    Rounding = true;
                    CornerRadius = 10;
                    BackgroundColor = Color.FromArgb(37, 52, 68);
                    RgbUpdateInterval = 300;
                    ShowBorder = false;
                    BorderWidth = 2F;
                    BorderColor = Color.FromArgb(29, 200, 238);
                    Lighting = false;
                    LightingColor = Color.FromArgb(29, 200, 238);
                    LightingAlpha = 20;
                    LightingWidth = 15;
                    UseGradientBackground = false;
                    GradientColor1 = Color.FromArgb(37, 52, 68);
                    GradientColor2 = Color.FromArgb(41, 63, 86);
                    UseGradientBorder = false;
                    GradientBorderColor1 = Color.FromArgb(37, 52, 68);
                    GradientBorderColor2 = Color.FromArgb(41, 63, 86);
                    SmoothingMode = SmoothingMode.HighQuality;
                    TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                    Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular);
                    break;
                case ControlStyleMode.Custom:
                    break;
                case ControlStyleMode.Random:
                    ShowBackground = HelpEngine.RandomBool();
                    Rounding = HelpEngine.RandomBool();
                    if (Rounding) CornerRadius = HelpEngine.RandomInt(5, 30);
                    if (ShowBackground) BackgroundColor = HelpEngine.RandomColor(HelpEngine.RandomInt(0, 255));
                    ShowBorder = HelpEngine.RandomBool();
                    if (ShowBorder)
                    {
                        BorderWidth = HelpEngine.RandomFloat(1, 3);
                        BorderColor = HelpEngine.RandomColor(HelpEngine.RandomInt(0, 255));
                    }
                    Lighting = HelpEngine.RandomBool();
                    if (Lighting) LightingColor = HelpEngine.RandomColor();
                    UseGradientBackground = HelpEngine.RandomBool();
                    if (UseGradientBackground)
                    {
                        GradientColor1 = HelpEngine.RandomColor();
                        GradientColor2 = HelpEngine.RandomColor();
                    }
                    UseGradientBorder = HelpEngine.RandomBool();
                    if (UseGradientBorder)
                    {
                        GradientBorderColor1 = HelpEngine.RandomColor();
                        GradientBorderColor2 = HelpEngine.RandomColor();
                    }
                    MarkerColor = HelpEngine.RandomColor();
                    break;
            }
            Invalidate(true);
        }
    }

    #endregion

    #region Initialization

    public ZColorPicker()
    {
        ControlStyle = ControlStyleMode.Default;
        ControlStyle = ControlStyleMode.Custom;
        UpdateGeometry();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _wheelBitmap?.Dispose();
            _wheelBitmap = null;
        }
        base.Dispose(disposing);
    }

    #endregion

    #region Layout

    internal Rectangle WheelBounds { get; private set; }

    internal Rectangle BrightnessBarBounds { get; private set; }

    internal Rectangle PreviewBounds { get; private set; }

    internal Point InfoLocation { get; private set; }

    protected override void UpdateGeometry()
    {
        RecalculateRegion();

        var content = Rectangle.Inflate(RegionRect, -ContentPadding, -ContentPadding);
        var infoHeight = ShowColorInfo ? Font.Height * 2 + 20 : 0;
        var barWidth = Math.Max(8, (int)Math.Round(content.Width * BarWidthRatio));
        var wheelSize = Math.Max(0, Math.Min(content.Width - barWidth - WheelBarGap, content.Height - infoHeight));

        WheelBounds = new Rectangle(content.X, content.Y, wheelSize, wheelSize);
        var barHeight = (int)Math.Round(wheelSize * BarHeightRatio);
        BrightnessBarBounds = new Rectangle(WheelBounds.Right + WheelBarGap, content.Y, barWidth, barHeight);
        PreviewBounds = new Rectangle(BrightnessBarBounds.X, BrightnessBarBounds.Bottom + 6, barWidth,
            Math.Max(0, WheelBounds.Bottom - BrightnessBarBounds.Bottom - 6));
        InfoLocation = new Point(WheelBounds.X + (int)(wheelSize * 0.293F), WheelBounds.Bottom + 9);

        if (_wheelBitmap is not null && _wheelBitmap.Width != wheelSize)
        {
            _wheelBitmap.Dispose();
            _wheelBitmap = null;
        }
    }

    protected override void OnFontChanged(EventArgs e)
    {
        UpdateGeometry();
        base.OnFontChanged(e);
    }

    #endregion

    #region Events

    protected override void OnMouseDown(MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            Focus();
            _dragTarget = WheelBounds.Contains(e.Location) ? DragTarget.Wheel
                : Rectangle.Inflate(BrightnessBarBounds, 2, 2).Contains(e.Location) ? DragTarget.Brightness
                : DragTarget.None;
            UpdateFromPointer(e.Location);
        }
        base.OnMouseDown(e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left) UpdateFromPointer(e.Location);
        base.OnMouseMove(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            UpdateFromPointer(e.Location);
            _dragTarget = DragTarget.None;
        }
        base.OnMouseUp(e);
    }

    protected override bool IsInputKey(Keys keyData) =>
        keyData is Keys.Up or Keys.Down or Keys.Left or Keys.Right || base.IsInputKey(keyData);

    protected override void OnKeyDown(KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.Left: SetHsv(_hue - 5, Math.Max(_saturation, 0.05F), _brightness); e.Handled = true; break;
            case Keys.Right: SetHsv(_hue + 5, Math.Max(_saturation, 0.05F), _brightness); e.Handled = true; break;
            case Keys.Up: SetHsv(_hue, _saturation, _brightness + 0.05F); e.Handled = true; break;
            case Keys.Down: SetHsv(_hue, _saturation, _brightness - 0.05F); e.Handled = true; break;
        }
        base.OnKeyDown(e);
    }

    private void UpdateFromPointer(Point location)
    {
        switch (_dragTarget)
        {
            case DragTarget.Wheel:
            {
                var radius = WheelBounds.Width / 2F;
                if (radius <= 0) return;
                var dx = location.X - (WheelBounds.X + radius);
                var dy = location.Y - (WheelBounds.Y + radius);
                var hue = MathF.Atan2(dy, dx) * 180F / MathF.PI;
                SetHsv(hue, MathF.Sqrt(dx * dx + dy * dy) / radius, _brightness);
                break;
            }
            case DragTarget.Brightness:
            {
                var bar = BrightnessBarBounds;
                if (bar.Height <= 0) return;
                SetHsv(_hue, _saturation, (float)(bar.Bottom - location.Y) / bar.Height);
                break;
            }
        }
    }

    /// <summary>
    /// Selects the color with the given hue (degrees), saturation and brightness (0..1, clamped).
    /// </summary>
    internal void SetHsv(float hue, float saturation, float brightness)
    {
        hue %= 360;
        if (hue < 0) hue += 360;
        _hue = hue;
        _saturation = Math.Clamp(saturation, 0F, 1F);
        _brightness = Math.Clamp(brightness, 0F, 1F);

        _updatingFromHsv = true;
        try
        {
            SelectedColor = DrawEngine.HsvToRgb(_hue, _saturation, _brightness);
        }
        finally
        {
            _updatingFromHsv = false;
        }
        Invalidate();
    }

    internal (float Hue, float Saturation, float Brightness) Hsv => (_hue, _saturation, _brightness);

    #endregion

    #region Accessibility

    protected override string AccessibleText => "Color picker";

    protected override string AccessibleValueText => FormatHex(SelectedColor);

    #endregion

    #region Drawing

    protected override void PaintControl(Graphics graphics)
    {
        var roundingValue = PrepareGeometry(Math.Min(Width, Height));

        DrawBorder(graphics, roundingValue);
        var state = ClipToContent(graphics, roundingValue, 1);
        FillBackground(graphics);
        graphics.Restore(state);

        if (WheelBounds.Width > 1)
        {
            graphics.DrawImage(GetWheelBitmap(), WheelBounds.Location);
            DrawWheelMarker(graphics);
        }

        DrawBrightnessBar(graphics);

        if (!SelectedColor.IsEmpty && PreviewBounds is { Width: > 0, Height: > 0 })
        {
            using SolidBrush previewBrush = new(SelectedColor);
            graphics.FillRectangle(previewBrush, PreviewBounds);
        }

        if (ShowColorInfo)
        {
            using SolidBrush textBrush = new(InfoTextColor);
            var lineHeight = Font.Height + 4;
            var rgb = SelectedColor.IsEmpty ? "none" : $"{SelectedColor.R}, {SelectedColor.G}, {SelectedColor.B}";
            graphics.DrawString($"RGB: {rgb}", Font, textBrush, InfoLocation);
            graphics.DrawString($"HEX: {FormatHex(SelectedColor)}", Font, textBrush,
                InfoLocation.X, InfoLocation.Y + lineHeight);
        }

        DrawFocusCue(graphics, Rectangle.Inflate(WheelBounds, 2, 2), WheelBounds.Width + 4, InfoTextColor);
    }

    private void DrawWheelMarker(Graphics graphics)
    {
        var radius = WheelBounds.Width / 2F;
        var angle = _hue * MathF.PI / 180F;
        var x = WheelBounds.X + radius + MathF.Cos(angle) * _saturation * radius;
        var y = WheelBounds.Y + radius + MathF.Sin(angle) * _saturation * radius;

        using Pen pen = new(MarkerColor, 1F);
        graphics.DrawEllipse(pen, x - 4, y - 4, 8, 8);
    }

    private void DrawBrightnessBar(Graphics graphics)
    {
        var bar = BrightnessBarBounds;
        if (bar.Width <= 0 || bar.Height <= 0) return;

        // The bar always shows the full-brightness hue so the marker position matches the gradient.
        var top = DrawEngine.HsvToRgb(_hue, _saturation, 1F);
        using LinearGradientBrush gradient = new(
            new Point(bar.X, bar.Y - 1), new Point(bar.X, bar.Bottom + 1), top, Color.Black);
        graphics.FillRectangle(gradient, bar);

        using SolidBrush markerBrush = new(MarkerColor);
        graphics.FillRectangle(markerBrush, bar.X, bar.Y + bar.Height * (1 - _brightness) - 2, bar.Width, 4);
    }

    private Bitmap GetWheelBitmap()
    {
        var size = WheelBounds.Width;
        if (_wheelBitmap is not null && _wheelBitmap.Width == size) return _wheelBitmap;

        _wheelBitmap?.Dispose();
        _wheelBitmap = CreateWheelBitmap(size);
        return _wheelBitmap;
    }

    /// <summary>
    /// Renders the hue/saturation wheel pixel by pixel so the picked color always matches the pixel under the cursor.
    /// </summary>
    internal static Bitmap CreateWheelBitmap(int size)
    {
        Bitmap bitmap = new(size, size, PixelFormat.Format32bppArgb);
        var data = bitmap.LockBits(new Rectangle(0, 0, size, size), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
        try
        {
            var pixels = new int[size * size];
            var radius = size / 2F;

            for (var y = 0; y < size; y++)
            for (var x = 0; x < size; x++)
            {
                var dx = x + 0.5F - radius;
                var dy = y + 0.5F - radius;
                var distance = MathF.Sqrt(dx * dx + dy * dy);

                // One-pixel antialiased edge.
                var coverage = Math.Clamp(radius - distance + 0.5F, 0F, 1F);
                if (coverage <= 0) continue;

                var hue = MathF.Atan2(dy, dx) * 180F / MathF.PI;
                var color = DrawEngine.HsvToRgb(hue, Math.Min(1F, distance / radius), 1F);
                pixels[y * size + x] = Color.FromArgb((int)(coverage * 255), color).ToArgb();
            }

            for (var row = 0; row < size; row++)
                Marshal.Copy(pixels, row * size, data.Scan0 + row * data.Stride, size);
        }
        finally
        {
            bitmap.UnlockBits(data);
        }
        return bitmap;
    }

    private static string FormatHex(Color color) =>
        color.IsEmpty ? "none" : $"#{color.R:X2}{color.G:X2}{color.B:X2}";

    #endregion
}
