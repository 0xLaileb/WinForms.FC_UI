using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace FC_UI.Controls;

[ToolboxBitmap(typeof(TextBox))]
[Description("Allows user text input with multi-line editing and password character masking.")]
public partial class FTextBox : FControlBase
{
    #region Fields

    private readonly StringFormat _textFormat = new();
    private Font? _cachedFont;
    private int _lastFontHeight = -1;
    private bool _updatingDisplayText;

    public TextBox InnerTextBox { get; } = new();

    #endregion

    #region Properties

    public delegate void TextChangedHandler();

    [Category("FC_UI")]
    [Description("Occurs when the Text property value changes.")]
    public new event TextChangedHandler TextChanged = delegate { };

    [Category("FTextBox")]
    [Description("Control text")]
    [DefaultValue("FTextBox")]
    public string DisplayText
    {
        get => InnerTextBox.Text;
        set
        {
            if (InnerTextBox.Text == value) return;

            _updatingDisplayText = true;
            try
            {
                InnerTextBox.Text = value;
            }
            finally
            {
                _updatingDisplayText = false;
            }

            TextChanged();
        }
    }

    [Category("FTextBox")]
    [DefaultValue(false)]
    [Description("Enable/Disable password display mode")]
    public bool Password
    {
        get;
        set
        {
            field = value;
            UpdateTextBox(true);
        }
    }

    [Category("FTextBox")]
    [DefaultValue('●')]
    [Description("Password masking character")]
    public char PasswordChar
    {
        get;
        set
        {
            field = value;
            UpdateTextBox(true);
        }
    }

    // --- Style ---

    [Category("FTextBox")]
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
                    Size = new Size(200, 40);
                    BackColor = Color.Transparent;
                    ForeColor = Color.FromArgb(245, 245, 245);
                    DisplayText = "FTextBox";
                    Rgb = false;
                    Password = false;
                    PasswordChar = '●';
                    Rounding = true;
                    CornerRadius = 60;
                    ShowBorder = true;
                    BorderWidth = 3F;
                    BorderColor = Color.FromArgb(29, 200, 238);
                    BackgroundColor = Color.FromArgb(37, 52, 68);
                    RgbUpdateInterval = 300;
                    Lighting = false;
                    LightingColor = Color.FromArgb(29, 200, 238);
                    LightingAlpha = 20;
                    LightingWidth = 15;
                    UseGradientBorder = false;
                    GradientBorderColor1 = Color.FromArgb(29, 200, 238);
                    GradientBorderColor2 = Color.FromArgb(37, 52, 68);
                    SmoothingMode = SmoothingMode.HighQuality;
                    TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                    Font = HelpEngine.GetDefaultFont();
                    break;
                case ControlStyleMode.Custom:
                    break;
                case ControlStyleMode.Random:
                    BackgroundColor = HelpEngine.RandomColor();
                    Password = HelpEngine.RandomBool();
                    Rounding = HelpEngine.RandomBool();
                    if (Rounding) CornerRadius = HelpEngine.RandomInt(5, 90);
                    ShowBorder = HelpEngine.RandomBool();
                    if (ShowBorder)
                    {
                        BorderWidth = HelpEngine.RandomFloat(1, 3);
                        BorderColor = HelpEngine.RandomColor(HelpEngine.RandomInt(0, 255));
                    }
                    Lighting = HelpEngine.RandomBool();
                    if (Lighting) LightingColor = HelpEngine.RandomColor();
                    UseGradientBorder = HelpEngine.RandomBool();
                    if (UseGradientBorder)
                    {
                        GradientBorderColor1 = HelpEngine.RandomColor();
                        GradientBorderColor2 = HelpEngine.RandomColor();
                    }
                    break;
            }
            Refresh();
        }
    }

    #endregion

    #region Initialization

    public FTextBox()
    {
        ControlStyle = ControlStyleMode.Default;
        ControlStyle = ControlStyleMode.Custom;

        _textFormat.Alignment = StringAlignment.Center;
        _textFormat.LineAlignment = StringAlignment.Center;

        InnerTextBox.Text = @"Text";
        InnerTextBox.TextChanged += (_, _) =>
        {
            if (!_updatingDisplayText) TextChanged();
        };
        UpdateTextBox(false);
        Controls.Add(InnerTextBox);

        OnSizeChanged(EventArgs.Empty);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _textFormat.Dispose();
            _cachedFont?.Dispose();
        }
        base.Dispose(disposing);
    }

    public void UpdateTextBox(bool visible)
    {
        InnerTextBox.Visible = visible;
        InnerTextBox.Size = new Size((int)(ControlSize.Width - CornerRadius / 2 - BorderWidth / 2), ControlSize.Height / 2);
        InnerTextBox.Location = new Point(Width / 2 - InnerTextBox.Size.Width / 2, Height / 2 - InnerTextBox.Size.Height / 2);
        if (BackgroundColor.Name != "Transparent") InnerTextBox.BackColor = BackgroundColor;
        InnerTextBox.ForeColor = Color.WhiteSmoke;
        InnerTextBox.BorderStyle = BorderStyle.None;
        if (Height != _lastFontHeight)
        {
            var fontSize = Math.Max(1f, Height / 4f);
            _cachedFont?.Dispose();
            _cachedFont = new Font(Font.Name, fontSize, Font.Style);
            Font = _cachedFont;
            _lastFontHeight = Height;
        }
        InnerTextBox.Font = Font;
        InnerTextBox.TextAlign = HorizontalAlignment.Center;
        InnerTextBox.MaxLength = 10000;
        InnerTextBox.PasswordChar = Password ? PasswordChar : '\0';
    }

    #endregion

    #region Events

    protected override void OnPaint(PaintEventArgs e)
    {
        try
        {
            ApplyGraphicsSettings(e.Graphics);
            DrawBackground(e.Graphics);
            UpdateTextBox(true);
        }
        catch (Exception ex) { System.Diagnostics.Debug.WriteLine($"[{Name}] OnPaint error: {ex}"); }
    }

    protected override void OnSizeChanged(EventArgs e)
    {
        RecalculateRegion();
    }

    #endregion

    #region Drawing

    private void DrawBackground(Graphics formGraphics)
    {
        var roundingValue = CalculateRoundingValue(Height);
        using var shapePath = DrawEngine.CreateRoundedPath(RegionRect, roundingValue);
        using var regionPath = DrawEngine.CreateRoundedPath(new Rectangle(0, 0, Width, Height), roundingValue);
        Region?.Dispose();
        Region = new Region(regionPath);

        // Border layer
        Bitmap borderBitmap = new(Width, Height);
        using (var g = HelpEngine.GetGraphics(borderBitmap, SmoothingMode, TextRenderingHint))
        {
            if (Lighting)
            {
                using var shadowPath = DrawEngine.CreateRoundedPath(RegionRect, roundingValue);
                DrawEngine.DrawBlurredShadow(g, LightingColor, shadowPath, LightingAlpha, LightingWidth);
            }

            if (BorderWidth != 0 && ShowBorder)
            {
                if (UseGradientBorder)
                {
                    using var brush = new LinearGradientBrush(RegionRect, GradientBorderColor1, GradientBorderColor2, 360);
                    
                    using var pen = new Pen(brush, BorderWidth);
                    pen.LineJoin = LineJoin.Round;
                    pen.DashCap = DashCap.Round;
                    
                    g.DrawPath(pen, shapePath);
                }
                else
                {
                    using var pen = new Pen(GetRgbOrColor(BorderColor), BorderWidth);
                    pen.LineJoin = LineJoin.Round;
                    pen.DashCap = DashCap.Round;
                    
                    g.DrawPath(pen, shapePath);
                }
            }
        }
        using (borderBitmap) formGraphics.DrawImage(borderBitmap, PointF.Empty);

        // Content layer
        Bitmap contentBitmap = new(Width, Height);
        using (var g = HelpEngine.GetGraphics(contentBitmap, SmoothingMode, TextRenderingHint))
        {
            using var clipPath = DrawEngine.CreateRoundedPath(new Rectangle(
                RegionRect.X - (int)(2 + BorderWidth),
                RegionRect.Y - (int)(2 + BorderWidth),
                RegionRect.Width + (int)(2 + BorderWidth) * 2,
                RegionRect.Height + (int)(2 + BorderWidth) * 2), Rounding ? roundingValue : 0.1F);
            using var clipRegion = new Region(clipPath);
            g.Clip = clipRegion;

            using var brush = new SolidBrush(BackgroundColor);
            g.FillPath(brush, shapePath);
        }
        using (contentBitmap) formGraphics.DrawImage(contentBitmap, PointF.Empty);
    }

    #endregion
}
