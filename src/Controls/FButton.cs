using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using Timer = System.Windows.Forms.Timer;

namespace FC_UI.Controls;

[ToolboxBitmap(typeof(Button))]
[Description("Raises an event when clicked.")]
[DefaultEvent("Click")]
public partial class FButton : FControlBase
{
    #region Fields

    private Point _clickLocation = new();
    private readonly StringFormat _textFormat = new();
    private int _animationSize;
    private bool _isMouseHovered;
    private EventHandler? _effectTickHandler;

    #endregion

    #region Properties

    [Category("FButton")]
    [Description("Control text")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public string DisplayText
    {
        get;
        set { field = value; Refresh(); }
    } = string.Empty;

    // --- Effects ---

    [Category("Effects")]
    [Description("Click animation color")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color ClickEffectColor { get; set; }

    [Category("Effects")]
    [Description("Enable/Disable circle effect on click")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool EnableClickEffect { get; set; }

    [Category("Effects")]
    [Description("Click effect opacity (1-255)")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int ClickEffectOpacity
    {
        get;
        set { if (value is > 0 and <= 255) field = value; }
    }

    [Category("Effects")]
    [Description("Enable/Disable hover overlay effect")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool EnableHoverEffect { get; set; }

    [Category("Effects")]
    [Description("Hover effect opacity (1-255)")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int HoverEffectOpacity
    {
        get;
        set { if (value is > 0 and <= 255) field = value; }
    }

    [Category("Effects")]
    [Description("Hover effect color")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color HoverEffectColor { get; set; }

    // --- Timers ---

    private readonly Timer _clickAnimationTimer = new();

    [Category("Timers")]
    [Description("Click effect animation speed")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int ClickEffectInterval
    {
        get => _clickAnimationTimer.Interval;
        set => _clickAnimationTimer.Interval = value;
    }

    // --- Style ---

    [Category("FButton")]
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
                    Size = new(130, 50);
                    BackColor = Color.Transparent;
                    ForeColor = Color.FromArgb(245, 245, 245);
                    DisplayText = "FButton";
                    RGB = false;
                    ShowBackground = true;
                    Rounding = true;
                    CornerRadius = 70;
                    ClickEffectColor = Color.FromArgb(29, 200, 238);
                    BackgroundColor = Color.FromArgb(37, 52, 68);
                    EnableClickEffect = true;
                    ClickEffectOpacity = 25;
                    EnableHoverEffect = true;
                    HoverEffectOpacity = 20;
                    HoverEffectColor = Color.White;
                    ClickEffectInterval = 5;
                    RgbUpdateInterval = 300;
                    ShowBorder = true;
                    BorderWidth = 4F;
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
                    Font = HelpEngine.GetDefaultFont();
                    break;
                case ControlStyleMode.Custom:
                    break;
                case ControlStyleMode.Random:
                    ShowBackground = HelpEngine.RandomBool();
                    Rounding = HelpEngine.RandomBool();
                    if (Rounding) CornerRadius = HelpEngine.RandomInt(5, 90);
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
                    break;
            }
            Refresh();
        }
    }

    #endregion

    #region Initialization

    public FButton()
    {
        ControlStyle = ControlStyleMode.Default;
        ControlStyle = ControlStyleMode.Custom;

        _textFormat.Alignment = StringAlignment.Center;
        _textFormat.LineAlignment = StringAlignment.Center;

        OnSizeChanged(EventArgs.Empty);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _textFormat.Dispose();
            _clickAnimationTimer.Stop();
            _clickAnimationTimer.Dispose();
        }
        base.Dispose(disposing);
    }

    #endregion

    #region Events

    protected override void OnPaint(PaintEventArgs e)
    {
        try
        {
            ApplyGraphicsSettings(e.Graphics);
            DrawBackground(e.Graphics);
            DrawText(e.Graphics);

            _shapePath.ClearMarkers();
        }
        catch (Exception ex) { System.Diagnostics.Debug.WriteLine($"[{Name}] OnPaint error: {ex}"); }
    }

    protected override void OnMouseEnter(EventArgs e)
    {
        _isMouseHovered = true;
        Refresh();
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        _clickAnimationTimer.Stop();
        _isMouseHovered = false;
        _animationSize = 0;
        Refresh();
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        _clickAnimationTimer.Stop();
        if (_effectTickHandler is not null)
        {
            _clickAnimationTimer.Tick -= _effectTickHandler;
            _effectTickHandler = null;
        }

        if (e.Button == MouseButtons.Left && EnableClickEffect)
        {
            _clickLocation = e.Location;
            _animationSize = 2;

            _effectTickHandler = (sender, args) =>
            {
                _animationSize += 20;
                Refresh();
            };
            _clickAnimationTimer.Tick += _effectTickHandler;
            _clickAnimationTimer.Start();
        }
    }

    protected override void OnSizeChanged(EventArgs e)
    {
        RecalculateRegion();
    }

    #endregion

    #region Drawing

    private void DrawBackground(Graphics formGraphics)
    {
        float roundingValue = PrepareGeometry(Height);

        using Bitmap borderLayer = RenderBorderLayer(roundingValue);
        formGraphics.DrawImage(borderLayer, PointF.Empty);

        // Content layer with effects
        using Bitmap contentBitmap = new(Width, Height);
        using (Graphics graphics = HelpEngine.GetGraphics(contentBitmap, SmoothingMode, TextRenderingHint))
        {
            int offset = 1;
            using GraphicsPath clipPath = DrawEngine.CreateRoundedPath(new(
                _regionRect.X - offset,
                _regionRect.Y - offset,
                _regionRect.Width + offset * 2,
                _regionRect.Height + offset * 2), Rounding ? roundingValue : 0.1F);
            using Region clipRegion = new(clipPath);
            graphics.Clip = clipRegion;

            if (ShowBackground)
            {
                if (UseGradientBackground)
                {
                    using LinearGradientBrush brush = new(_regionRect, GradientColor1, GradientColor2, 360);
                    graphics.FillPath(brush, _shapePath);
                }
                else
                {
                    using SolidBrush brush = new(BackgroundColor);
                    graphics.FillPath(brush, _shapePath);
                }
            }

            if (EnableClickEffect) DrawClickAnimation(graphics);
            if (EnableHoverEffect && _isMouseHovered) DrawHoverOverlay(graphics);
        }
        formGraphics.DrawImage(contentBitmap, PointF.Empty);
    }

    private void DrawText(Graphics graphics)
    {
        using SolidBrush brush = new(ForeColor);
        graphics.DrawString(DisplayText, Font, brush, _regionRect, _textFormat);
    }

    private void DrawClickAnimation(Graphics graphics)
    {
        int maxDimension = _controlSize.Width >= _controlSize.Height
            ? _controlSize.Width * 2
            : _controlSize.Height * 2;

        if (_animationSize < maxDimension)
        {
            Rectangle circleRect = new(
                _clickLocation.X - _animationSize / 2,
                _clickLocation.Y - _animationSize / 2,
                _animationSize, _animationSize);

            if (circleRect is { Width: > 0, Height: > 0 })
            {
                using SolidBrush brush = new(Color.FromArgb(ClickEffectOpacity, ClickEffectColor));
                graphics.FillEllipse(brush, circleRect);
            }
        }
    }

    private void DrawHoverOverlay(Graphics graphics)
    {
        using SolidBrush brush = new(Color.FromArgb(HoverEffectOpacity, HoverEffectColor));
        graphics.FillPath(brush, _shapePath);
    }

    #endregion
}
