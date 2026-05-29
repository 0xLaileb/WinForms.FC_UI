using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using Timer = System.Windows.Forms.Timer;

namespace FC_UI.Controls;

[ToolboxBitmap(typeof(CheckBox))]
[Description("Allows the user to select or deselect the corresponding option.")]
public partial class FCheckBox : FControlBase
{
    #region Fields

    private int _animationSize;
    private bool _isMouseHovered;
    private Size _checkboxSize;
    private EventHandler? _effectTickHandler;

    #endregion

    #region Properties

    public delegate void CheckedChangedHandler();

    [Category("FC_UI")]
    [Description("Occurs on every Checked property change.")]
    public event CheckedChangedHandler CheckedChanged = delegate { };

    [Category("FCheckBox")]
    [Description("Enable/Disable checked status")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool Checked
    {
        get;
        set
        {
            if (field == value) return;
            field = value;
            CheckedChanged();
            Refresh();
        }
    }

    [Category("FCheckBox")]
    [Description("Control text")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public string DisplayText
    {
        get;
        set { field = value; Refresh(); }
    } = string.Empty;

    [Category("FCheckBox")]
    [Description("Checkmark color")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color ColorChecked
    {
        get;
        set { field = value; Refresh(); }
    }

    // --- Effects ---

    [Category("Effects")]
    [Description("Click animation color")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color ClickEffectColor
    {
        get;
        set { field = value; Refresh(); }
    }

    [Category("Effects")]
    [DefaultValue(true)]
    [Description("Enable/Disable circle effect on hover/activation")]
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

    private readonly Timer _clickAnimationTimer = new() { Interval = 1 };

    [Category("Timers")]
    [Description("Click effect animation speed")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int ClickEffectInterval
    {
        get => _clickAnimationTimer.Interval;
        set
        {
            if (value > 0) _clickAnimationTimer.Interval = value;
        }
    }

    // --- Style ---

    [Category("FCheckBox")]
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
                    Size = new Size(140, 45);
                    BackColor = Color.Transparent;
                    ForeColor = Color.FromArgb(245, 245, 245);
                    Checked = false;
                    DisplayText = "FCheckBox";
                    Rgb = false;
                    ShowBackground = true;
                    Rounding = true;
                    CornerRadius = 100;
                    ClickEffectColor = Color.FromArgb(29, 200, 238);
                    BackgroundColor = Color.FromArgb(37, 52, 68);
                    ShowBorder = true;
                    BorderWidth = 2F;
                    BorderColor = Color.FromArgb(29, 200, 238);
                    ColorChecked = Color.FromArgb(29, 200, 238);
                    EnableClickEffect = true;
                    ClickEffectOpacity = 25;
                    EnableHoverEffect = true;
                    HoverEffectOpacity = 15;
                    HoverEffectColor = Color.White;
                    ClickEffectInterval = 1;
                    RgbUpdateInterval = 300;
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
                        ColorChecked = HelpEngine.RandomColor(HelpEngine.RandomInt(0, 255));
                    }
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

    public FCheckBox()
    {
        ControlStyle = ControlStyleMode.Default;
        ControlStyle = ControlStyleMode.Custom;
        OnSizeChanged(EventArgs.Empty);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _clickAnimationTimer.Stop();
            _clickAnimationTimer.Dispose();
        }
        base.Dispose(disposing);
    }

    protected override CreateParams CreateParams
    {
        get
        {
            var cp = base.CreateParams;
            cp.ExStyle |= 0x02000000; // WS_CLIPCHILDREN
            return cp;
        }
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
        }
        catch (Exception ex) { System.Diagnostics.Debug.WriteLine($"[{Name}] OnPaint error: {ex}"); }
    }

    protected override void OnMouseClick(MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Left) return;

        Checked = !Checked;

        _clickAnimationTimer.Stop();
        if (_effectTickHandler is not null)
        {
            _clickAnimationTimer.Tick -= _effectTickHandler;
            _effectTickHandler = null;
        }

        _animationSize = _checkboxSize.Width;

        if (Checked)
        {
            _effectTickHandler = (_, _) =>
            {
                _animationSize += 1;
                Refresh();
            };
            _clickAnimationTimer.Tick += _effectTickHandler;
            _clickAnimationTimer.Start();
        }
        else Refresh();
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

    protected override void OnSizeChanged(EventArgs e)
    {
        Size = Size with { Height = 45 };
        _checkboxSize = new Size(21, 21);
        RegionRect = new Rectangle(15, Size.Height / 2 - 12, _checkboxSize.Width, _checkboxSize.Height);
    }

    #endregion

    #region Drawing

    private void DrawBackground(Graphics formGraphics)
    {
        var roundingValue = 0.1F;

        // Prepare geometry
        if (Rounding && CornerRadius > 0)
            roundingValue = _checkboxSize.Height / 100F * CornerRadius;

        ShapePath?.Dispose();
        ShapePath = DrawEngine.CreateRoundedPath(RegionRect, roundingValue);

        using var regionPath = DrawEngine.CreateRoundedPath(new Rectangle(0, 0, Width, Height), roundingValue);
        Region?.Dispose();
        Region = new Region(regionPath);

        // Layer 1: Border
        Bitmap borderBitmap = new(Width, Height);
        using (var graphics = HelpEngine.GetGraphics(borderBitmap, SmoothingMode, TextRenderingHint))
        {
            if (BorderWidth != 0 && ShowBorder)
            {
                if (UseGradientBorder)
                {
                    using LinearGradientBrush brush = new(RegionRect, GradientBorderColor1, GradientBorderColor2, 360);
                    
                    using Pen pen = new(brush, BorderWidth);
                    pen.LineJoin = LineJoin.Round;
                    pen.DashCap = DashCap.Round;
                    
                    graphics.DrawPath(pen, ShapePath);
                }
                else
                {
                    using Pen pen = new(GetRgbOrColor(BorderColor), BorderWidth);
                    pen.LineJoin = LineJoin.Round;
                    pen.DashCap = DashCap.Round;
                    graphics.DrawPath(pen, ShapePath);
                }
            }
        }
        using (borderBitmap) formGraphics.DrawImage(borderBitmap, PointF.Empty);

        // Layer 2: Content
        Bitmap contentBitmap = new(Width, Height);
        using (var graphics = HelpEngine.GetGraphics(contentBitmap, SmoothingMode, TextRenderingHint))
        {
            if (EnableClickEffect) DrawClickAnimation(graphics);
            if (EnableHoverEffect && _isMouseHovered) DrawHoverCircleOverlay(graphics);

            if (ShowBackground)
            {
                if (UseGradientBackground)
                {
                    using LinearGradientBrush brush = new(RegionRect, GradientColor1, GradientColor2, 360);
                    graphics.FillPath(brush, ShapePath);
                }
                else
                {
                    using SolidBrush brush = new(BackgroundColor);
                    graphics.FillPath(brush, ShapePath);
                }
            }

            if (Checked) DrawCheckMark(graphics);
        }
        using (contentBitmap) formGraphics.DrawImage(contentBitmap, PointF.Empty);
    }

    private void DrawText(Graphics graphics)
    {
        using SolidBrush brush = new(ForeColor);
        graphics.DrawString(
            DisplayText, Font, brush,
            new Rectangle((int)(25 + ShapePath.GetBounds().Width), Size.Height / 2 - Font.Height / 2, 0, 0));
    }

    private void DrawCheckMark(Graphics graphics)
    {
        using Font checkFont = new("Segoe MDL2 Assets", 10F, FontStyle.Regular);
        using SolidBrush brush = new(GetRgbOrColor(ColorChecked));
        graphics.DrawString("\uE73E", checkFont, brush,
            new Rectangle(15 + 3, Size.Height / 2 - 25 / 2 + 5, 0, 0));
    }

    private void DrawClickAnimation(Graphics graphics)
    {
        const int maxSize = 40;
        if (_animationSize < maxSize)
        {
            Rectangle circleRect = new(
                15 + 25 / 2 - _animationSize / 2 - 2,
                Size.Height / 2 - _animationSize / 2 - 2,
                _animationSize, _animationSize);

            if (circleRect is { Width: > 0, Height: > 0 })
            {
                using SolidBrush brush = new(Color.FromArgb(ClickEffectOpacity, ClickEffectColor));
                graphics.FillEllipse(brush, circleRect);
            }
        }
    }

    private void DrawHoverCircleOverlay(Graphics graphics)
    {
        const int circleSize = 40;
        Rectangle circleRect = new(
            15 + 25 / 2 - circleSize / 2 - 2,
            Size.Height / 2 - circleSize / 2 - 2,
            circleSize, circleSize);

        if (circleRect is { Width: > 0, Height: > 0 })
        {
            using SolidBrush brush = new(Color.FromArgb(HoverEffectOpacity, HoverEffectColor));
            graphics.FillEllipse(brush, circleRect);
        }
    }

    #endregion
}
