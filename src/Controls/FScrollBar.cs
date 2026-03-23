using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace FC_UI.Controls;

[ToolboxBitmap(typeof(VScrollBar))]
[Description("Provides horizontal/vertical content scrolling capability (use event subscription).")]
[DefaultEvent("ValueChanged")]
public partial class FScrollBar : FControlBase
{
    #region Fields

    private Rectangle _thumbRect = new();

    #endregion

    #region Properties

    [Category("FC_UI")]
    [Description("Occurs on every Value property change.")]
    public event EventHandler? ValueChanged;

    [Category("Value")]
    [Description("Current value")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int Value
    {
        get;
        set
        {
            if (field == value) return;
            field = value;
            Refresh();
            OnScroll();
        }
    }

    [Category("Value")]
    [Description("Orientation")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Orientation Orientation
    {
        get;
        set
        {
            if (value == System.Windows.Forms.Orientation.Vertical)
            {
                Size = new(Size.Width, Size.Height);
                if (CornerRadius != 0) CornerRadius /= 10;
            }
            else
            {
                Size = new(Size.Height, Size.Width);
                if (CornerRadius != 0) CornerRadius *= 10;
            }
            field = value;
            Refresh();
        }
    }

    [Category("Value")]
    [Description("Scroll step amount")]
    [DefaultValue(1)]
    public int SmallStep { get; set; }

    [Category("Value")]
    [Description("Thumb size")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int ThumbSize
    {
        get;
        set { field = value; Refresh(); }
    }

    [Category("Value")]
    [Description("Maximum value")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int Maximum
    {
        get;
        set
        {
            if (value > Minimum)
            {
                field = value;
                Value = 0;
                Refresh();
            }
        }
    }

    [Category("Value")]
    [Description("Minimum value")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int Minimum
    {
        get;
        set
        {
            if (value < Maximum)
            {
                field = value;
                Value = 0;
                Refresh();
            }
        }
    }

    [Category("FScrollBar")]
    [Description("Thumb color")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color ThumbColor
    {
        get;
        set { field = value; Refresh(); }
    }

    [Category("Value")]
    [Description("Thumb opacity (10-255)")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int ThumbOpacity
    {
        get;
        set
        {
            if (value is >= 10 and <= 255)
            {
                field = value;
                Refresh();
            }
        }
    }

    // --- Gradient Fill ---

    [Category("LinearGradient")]
    [Description("Enable/Disable thumb gradient")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool UseGradientFill
    {
        get;
        set { field = value; Refresh(); }
    }

    [Category("LinearGradient")]
    [Description("Thumb gradient color #1")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color GradientFillColor1
    {
        get;
        set { field = value; Refresh(); }
    }

    [Category("LinearGradient")]
    [Description("Thumb gradient color #2")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color GradientFillColor2
    {
        get;
        set { field = value; Refresh(); }
    }

    // --- Style ---

    [Category("FScrollBar")]
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
                    Size = new(26, 300);
                    BackColor = Color.Transparent;
                    ForeColor = Color.FromArgb(245, 245, 245);
                    Value = 0;
                    Minimum = 0;
                    Maximum = 100;
                    Orientation = System.Windows.Forms.Orientation.Vertical;
                    ThumbSize = 60;
                    SmallStep = 1;
                    RGB = false;
                    ShowBackground = true;
                    ShowBorder = true;
                    BorderWidth = 3F;
                    Rounding = true;
                    CornerRadius = 7;
                    RgbUpdateInterval = 300;
                    ThumbColor = Color.FromArgb(29, 200, 238);
                    ThumbOpacity = 255;
                    BackgroundColor = Color.FromArgb(37, 52, 68);
                    BorderColor = Color.FromArgb(29, 200, 238);
                    Lighting = false;
                    LightingColor = Color.FromArgb(29, 200, 238);
                    LightingAlpha = 50;
                    LightingWidth = 10;
                    UseGradientBackground = false;
                    GradientColor1 = Color.FromArgb(37, 52, 68);
                    GradientColor2 = Color.FromArgb(41, 63, 86);
                    UseGradientBorder = false;
                    GradientBorderColor1 = Color.FromArgb(37, 52, 68);
                    GradientBorderColor2 = Color.FromArgb(41, 63, 86);
                    UseGradientFill = false;
                    GradientFillColor1 = Color.FromArgb(28, 200, 238);
                    GradientFillColor2 = Color.FromArgb(100, 208, 232);
                    SmoothingMode = SmoothingMode.HighQuality;
                    TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                    break;
                case ControlStyleMode.Custom:
                    break;
                case ControlStyleMode.Random:
                    ShowBackground = HelpEngine.RandomBool();
                    Rounding = HelpEngine.RandomBool();
                    if (Rounding) CornerRadius = HelpEngine.RandomInt(2, 10);
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
                    UseGradientFill = HelpEngine.RandomBool();
                    if (UseGradientFill)
                    {
                        GradientFillColor1 = HelpEngine.RandomColor();
                        GradientFillColor2 = HelpEngine.RandomColor();
                    }
                    UseGradientBorder = HelpEngine.RandomBool();
                    if (UseGradientBorder)
                    {
                        GradientBorderColor1 = HelpEngine.RandomColor();
                        GradientBorderColor2 = HelpEngine.RandomColor();
                    }
                    ThumbColor = HelpEngine.RandomColor();
                    ThumbOpacity = HelpEngine.RandomInt(10, 255);
                    break;
            }
            Refresh();
        }
    }

    #endregion

    #region Initialization

    public FScrollBar()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
        DoubleBuffered = true;

        ControlStyle = ControlStyleMode.Default;
        ControlStyle = ControlStyleMode.Custom;

        OnSizeChanged(EventArgs.Empty);
    }

    #endregion

    #region Events

    protected override void OnPaint(PaintEventArgs e)
    {
        try
        {
            ApplyGraphicsSettings(e.Graphics);
            DrawBackground(e.Graphics);
        }
        catch (Exception ex) { System.Diagnostics.Debug.WriteLine($"[{Name}] OnPaint error: {ex}"); }
    }

    public virtual void OnScroll(ScrollEventType type = ScrollEventType.ThumbPosition)
    {
        ValueChanged?.Invoke(this, EventArgs.Empty);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left) HandleMouseScroll(e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left) HandleMouseScroll(e);
    }

    protected override void OnSizeChanged(EventArgs e)
    {
        RecalculateRegion();
    }

    private void HandleMouseScroll(MouseEventArgs e)
    {
        int newValue = Value;

        switch (Orientation)
        {
            case System.Windows.Forms.Orientation.Vertical:
                if (e.Y < 0) newValue -= SmallStep;
                else if (e.Y > _regionRect.Height) newValue += SmallStep;
                else
                {
                    int range = _regionRect.Height - ThumbSize;
                    if (range > 0) newValue = Maximum * (e.Y - ThumbSize / 2) / range;
                }
                break;
            case System.Windows.Forms.Orientation.Horizontal:
                if (e.X < 0) newValue -= SmallStep;
                else if (e.X > _regionRect.Width) newValue += SmallStep;
                else
                {
                    int range = _regionRect.Width - ThumbSize;
                    if (range > 0) newValue = Maximum * (e.X - ThumbSize / 2) / range;
                }
                break;
        }
        Value = Math.Max(0, Math.Min(Maximum, newValue));
    }

    #endregion

    #region Drawing

    private void DrawBackground(Graphics formGraphics)
    {
        float roundingValue = PrepareGeometry(Height);

        // Border layer
        using Bitmap borderLayer = RenderBorderLayer(roundingValue);
        formGraphics.DrawImage(borderLayer, PointF.Empty);

        // Content layer
        Bitmap contentBitmap = new(Width, Height);
        using (Graphics g = HelpEngine.GetGraphics(contentBitmap, SmoothingMode, TextRenderingHint))
        {
            using GraphicsPath clipPath = DrawEngine.CreateRoundedPath(new(
                _regionRect.X - (int)(2 + BorderWidth),
                _regionRect.Y - (int)(2 + BorderWidth),
                _regionRect.Width + (int)(2 + BorderWidth) * 2,
                _regionRect.Height + (int)(2 + BorderWidth) * 2), Rounding ? roundingValue : 0.1F);
            using Region clipRegion = new(clipPath);
            g.Clip = clipRegion;

            if (ShowBackground)
            {
                if (UseGradientBackground)
                {
                    using LinearGradientBrush brush = new(_regionRect, GradientColor1, GradientColor2, 360);
                    g.FillPath(brush, _shapePath);
                }
                else
                {
                    using SolidBrush brush = new(BackgroundColor);
                    g.FillPath(brush, _shapePath);
                }
            }

            DrawThumb(g, roundingValue);
        }
        using (contentBitmap) formGraphics.DrawImage(contentBitmap, PointF.Empty);
    }

    private void DrawThumb(Graphics graphics, float roundingValue)
    {
        if (Maximum <= 0) return;
        _thumbRect = new(2, 2, _regionRect.Width, ThumbSize);

        switch (Orientation)
        {
            case System.Windows.Forms.Orientation.Vertical:
            {
                int vRange = _regionRect.Height - ThumbSize;
                _thumbRect = new(
                    _regionRect.X,
                    _regionRect.Y + (vRange > 0 ? Value * vRange / Maximum : 0),
                    _regionRect.Width,
                    ThumbSize);
                break;
            }
            case System.Windows.Forms.Orientation.Horizontal:
            {
                int hRange = _regionRect.Width - ThumbSize;
                _thumbRect = new(
                    _regionRect.X + (hRange > 0 ? Value * hRange / Maximum : 0),
                    _regionRect.Y,
                    ThumbSize,
                    _regionRect.Height);
                break;
            }
        }

        const int offset = 1;
        _thumbRect.X -= offset;
        _thumbRect.Y -= offset;
        _thumbRect.Width += offset * 2;
        _thumbRect.Height += offset * 2;
        roundingValue += offset * 2;

        using GraphicsPath thumbPath = DrawEngine.CreateRoundedPath(_thumbRect, roundingValue);

        if (UseGradientFill)
        {
            using LinearGradientBrush brush = new(_regionRect,
                Color.FromArgb(ThumbOpacity, GetRgbOrColor(GradientFillColor1)),
                Color.FromArgb(ThumbOpacity, RGB ? DrawEngine.HsvToRgb(_hue + 20, 1f, 1f) : GradientFillColor2),
                360);
            graphics.FillPath(brush, thumbPath);
        }
        else
        {
            using SolidBrush brush = new(Color.FromArgb(ThumbOpacity, GetRgbOrColor(ThumbColor)));
            graphics.FillPath(brush, thumbPath);
        }
    }

    #endregion
}
