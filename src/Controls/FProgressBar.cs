using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using Timer = System.Windows.Forms.Timer;

namespace FC_UI.Controls;

[ToolboxBitmap(typeof(ProgressBar))]
[Description("Displays an operation progress indicator.")]
public partial class FProgressBar : FControlBase
{
    #region Fields

    private readonly StringFormat _textFormat = new();
    private readonly Timer _valueAnimationTimer = new() { Interval = 15 };

    // Value currently drawn; trails Value while the value animation runs.
    private double _displayedValue;

    #endregion

    #region Properties

    [Category("Value")]
    [Description("Current value")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int Value
    {
        get;
        set
        {
            if (value <= Maximum && value >= Minimum)
            {
                field = value;
                AccessibilityNotifyClients(AccessibleEvents.ValueChange, -1);

                if (EnableValueAnimation && IsHandleCreated && Visible)
                {
                    _valueAnimationTimer.Start();
                    return;
                }

                _displayedValue = value;
                // Synchronous repaint keeps progress visible when Value is updated from a busy UI-thread loop.
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
                if (Value < field) Value = field;
                Invalidate(true);
            }
        }
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
                if (Value > field) Value = field;
                Invalidate(true);
            }
        }
    }

    [Category("Value")]
    [Description("Value at which drawing starts (use when Rounding is true to fix small-value artifacts)")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int StartDrawingValue
    {
        get;
        set { field = value; Invalidate(true); }
    }

    [Category("FProgressBar")]
    [Description("Enable/Disable progress text")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool ProgressText
    {
        get;
        set { field = value; Invalidate(true); }
    }

    [Category("FProgressBar")]
    [Description("Progress fill color")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color FillColor
    {
        get;
        set { field = value; Invalidate(true); }
    }

    [Category("Value")]
    [Description("Fill transparency (5-255)")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int FillOpacity
    {
        get;
        set
        {
            if (value is >= 5 and <= 255)
            {
                field = value;
                Invalidate(true);
            }
        }
    }

    [Category("Effects")]
    [Description("Animate the fill smoothly towards a new Value")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool EnableValueAnimation { get; set; }

    // --- Gradient Fill ---

    [Category("LinearGradient")]
    [Description("Enable/Disable fill gradient")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool UseGradientFill
    {
        get;
        set { field = value; Invalidate(true); }
    }

    [Category("LinearGradient")]
    [Description("Fill gradient color #1")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color GradientFillColor1
    {
        get;
        set { field = value; Invalidate(true); }
    }

    [Category("LinearGradient")]
    [Description("Fill gradient color #2")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color GradientFillColor2
    {
        get;
        set { field = value; Invalidate(true); }
    }

    // --- Style ---

    [Category("FProgressBar")]
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
                    Size = new Size(300, 34);
                    BackColor = Color.Transparent;
                    ForeColor = Color.FromArgb(245, 245, 245);
                    Value = 0;
                    Minimum = 0;
                    Maximum = 100;
                    StartDrawingValue = 0;
                    ProgressText = true;
                    Rgb = false;
                    ShowBackground = true;
                    Rounding = true;
                    CornerRadius = 70;
                    BackgroundColor = Color.FromArgb(37, 52, 68);
                    RgbUpdateInterval = 300;
                    ShowBorder = true;
                    BorderWidth = 3F;
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
                    FillOpacity = 200;
                    FillColor = Color.FromArgb(29, 200, 238);
                    EnableValueAnimation = false;
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
                    ProgressText = HelpEngine.RandomBool();
                    FillColor = HelpEngine.RandomColor();
                    FillOpacity = HelpEngine.RandomInt(5, 255);
                    break;
            }
            Invalidate(true);
        }
    }

    #endregion

    #region Initialization

    public FProgressBar()
    {
        // A progress bar only displays state, so it is skipped by Tab navigation.
        SetStyle(ControlStyles.Selectable, false);
        TabStop = false;

        ControlStyle = ControlStyleMode.Default;
        ControlStyle = ControlStyleMode.Custom;

        _textFormat.Alignment = StringAlignment.Center;
        _textFormat.LineAlignment = StringAlignment.Center;
        _valueAnimationTimer.Tick += (_, _) => StepValueAnimation();

        UpdateGeometry();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _textFormat.Dispose();
            _valueAnimationTimer.Stop();
            _valueAnimationTimer.Dispose();
        }
        base.Dispose(disposing);
    }

    #endregion

    #region Accessibility

    protected override AccessibleRole DefaultAccessibleRole => AccessibleRole.ProgressBar;

    protected override string AccessibleValueText => $"{Percent}%";

    #endregion

    #region Animation

    internal bool IsValueAnimationRunning => _valueAnimationTimer.Enabled;

    internal double DisplayedValue => _displayedValue;

    internal void StepValueAnimation()
    {
        var delta = Value - _displayedValue;
        var minStep = Math.Max(1, Maximum - Minimum) / 200.0;

        // Ease out: cover 25% of the remaining distance per tick, finishing with a small fixed step.
        if (Math.Abs(delta) <= minStep)
        {
            _displayedValue = Value;
            _valueAnimationTimer.Stop();
        }
        else
        {
            _displayedValue += Math.Sign(delta) * Math.Max(minStep, Math.Abs(delta) * 0.25);
        }

        Refresh();
    }

    #endregion

    #region Drawing

    private int Percent
    {
        get
        {
            var range = Maximum - Minimum;
            return range > 0 ? (int)Math.Round((double)(Value - Minimum) / range * 100) : 0;
        }
    }

    protected override void PaintControl(Graphics graphics)
    {
        var roundingValue = PrepareGeometry(Height);

        DrawBorder(graphics, roundingValue);

        var state = ClipToContent(graphics, roundingValue, (int)(2 + BorderWidth));
        FillBackground(graphics);
        if (Value >= StartDrawingValue) DrawProgressFill(graphics, roundingValue);
        graphics.Restore(state);

        if (ProgressText)
        {
            using SolidBrush brush = new(ForeColor);
            graphics.DrawString($"{Percent}%", Font, brush, RegionRect, _textFormat);
        }
    }

    private void DrawProgressFill(Graphics graphics, float roundingValue)
    {
        var range = Maximum - Minimum;
        if (range <= 0 || _displayedValue <= Minimum) return;

        var ratio = Math.Clamp((_displayedValue - Minimum) / range, 0, 1);
        var valueRect = RegionRect with { Width = Convert.ToInt32(ShapePath.GetBounds().Width * ratio) };

        const int offset = 1;
        valueRect.Inflate(offset, offset);
        roundingValue += offset * 2;

        var valueRounding = Math.Min(roundingValue, Math.Min(valueRect.Width, valueRect.Height) / 2f);
        if (valueRounding < 0.5f) valueRounding = 0.1f;

        using var valuePath = DrawEngine.CreateRoundedPath(valueRect, valueRounding);

        using Brush brush = UseGradientFill
            ? new LinearGradientBrush(valueRect,
                Color.FromArgb(FillOpacity, GetRgbOrColor(GradientFillColor1)),
                Color.FromArgb(FillOpacity, Rgb ? DrawEngine.GetRgbColor(Hue + 20) : GradientFillColor2),
                360)
            : new SolidBrush(Color.FromArgb(FillOpacity, GetRgbOrColor(FillColor)));
        graphics.FillPath(brush, valuePath);
    }

    #endregion
}
