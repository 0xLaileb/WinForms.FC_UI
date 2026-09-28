using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using Timer = System.Windows.Forms.Timer;

namespace FC_UI.Controls;

[ToolboxBitmap(typeof(CheckBox))]
[Description("Allows the user to enable or disable the corresponding option.")]
public partial class FSwitchBox : FControlBase
{
    #region Fields

    private const float ToggleAnimationStep = 0.2F;

    // 0 = knob at the "off" position, 1 = knob at the "on" position.
    private float _knobPosition;
    private readonly Timer _toggleAnimationTimer = new() { Interval = 15 };

    #endregion

    #region Properties

    public delegate void CheckedChangedHandler();

    [Category("FC_UI")]
    [Description("Occurs on every Checked property change.")]
    public event CheckedChangedHandler CheckedChanged = delegate { };

    [Category("FSwitchBox")]
    [Description("Enable/Disable")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool Checked
    {
        get;
        set
        {
            if (field == value) return;
            field = value;

            if (EnableToggleAnimation && IsHandleCreated && Visible) _toggleAnimationTimer.Start();
            else _knobPosition = value ? 1F : 0F;

            CheckedChanged();
            AccessibilityNotifyClients(AccessibleEvents.StateChange, -1);
            Invalidate(true);
        }
    }

    [Category("FSwitchBox")]
    [Description("Inner circle color")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color ColorValue
    {
        get;
        set { field = value; Invalidate(true); }
    }

    // --- Gradient Fill ---

    [Category("LinearGradient")]
    [Description("Enable/Disable value gradient")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool UseGradientFill
    {
        get;
        set { field = value; Invalidate(true); }
    }

    [Category("LinearGradient")]
    [Description("Value gradient color #1")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color GradientFillColor1
    {
        get;
        set { field = value; Invalidate(true); }
    }

    [Category("LinearGradient")]
    [Description("Value gradient color #2")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color GradientFillColor2
    {
        get;
        set { field = value; Invalidate(true); }
    }

    // --- Effects ---

    [Category("Effects")]
    [Description("Slide the knob instead of jumping when Checked changes")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool EnableToggleAnimation { get; set; }

    [Category("Effects")]
    [Description("Enable/Disable hover overlay effect")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool EnableHoverEffect
    {
        get;
        set { field = value; Invalidate(); }
    }

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

    // --- Style ---

    [Category("FSwitchBox")]
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
                    Size = new Size(35, 20);
                    BackColor = Color.Transparent;
                    ForeColor = Color.FromArgb(245, 245, 245);
                    Checked = false;
                    Rgb = false;
                    ShowBackground = true;
                    Rounding = true;
                    CornerRadius = 90;
                    ColorValue = Color.FromArgb(29, 200, 238);
                    BackgroundColor = Color.FromArgb(37, 52, 68);
                    RgbUpdateInterval = 300;
                    ShowBorder = true;
                    BorderWidth = 2F;
                    BorderColor = Color.FromArgb(29, 200, 238);
                    UseGradientBackground = false;
                    GradientColor1 = Color.FromArgb(37, 52, 68);
                    GradientColor2 = Color.FromArgb(41, 63, 86);
                    UseGradientFill = false;
                    GradientFillColor1 = Color.FromArgb(28, 200, 238);
                    GradientFillColor2 = Color.FromArgb(100, 208, 232);
                    Lighting = false;
                    LightingColor = Color.FromArgb(29, 200, 238);
                    LightingAlpha = 50;
                    LightingWidth = 10;
                    UseGradientBorder = false;
                    GradientBorderColor1 = Color.FromArgb(37, 52, 68);
                    GradientBorderColor2 = Color.FromArgb(41, 63, 86);
                    EnableToggleAnimation = true;
                    EnableHoverEffect = true;
                    HoverEffectOpacity = 20;
                    HoverEffectColor = Color.White;
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
                    ColorValue = HelpEngine.RandomColor(HelpEngine.RandomInt(0, 255));
                    break;
            }
            Invalidate(true);
        }
    }

    #endregion

    #region Initialization

    public FSwitchBox()
    {
        ControlStyle = ControlStyleMode.Default;
        ControlStyle = ControlStyleMode.Custom;
        _toggleAnimationTimer.Tick += (_, _) => StepToggleAnimation();
        UpdateGeometry();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _toggleAnimationTimer.Stop();
            _toggleAnimationTimer.Dispose();
        }
        base.Dispose(disposing);
    }

    #endregion

    #region Events

    protected override void OnMouseClick(MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left) Checked = !Checked;
        base.OnMouseClick(e);
    }

    protected override void OnKeyUp(KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Space && e.Modifiers == Keys.None)
        {
            Checked = !Checked;
            e.Handled = true;
        }
        base.OnKeyUp(e);
    }

    #endregion

    #region Accessibility

    protected override AccessibleRole DefaultAccessibleRole => AccessibleRole.CheckButton;

    protected override AccessibleStates AccessibleStateFlags => Checked ? AccessibleStates.Checked : AccessibleStates.None;

    protected override string AccessibleDefaultActionText => Checked ? "Turn off" : "Turn on";

    protected override void DoAccessibleDefaultAction() => Checked = !Checked;

    #endregion

    #region Animation

    internal bool IsToggleAnimationRunning => _toggleAnimationTimer.Enabled;

    internal float KnobPosition => _knobPosition;

    internal void StepToggleAnimation()
    {
        var target = Checked ? 1F : 0F;
        _knobPosition = _knobPosition < target
            ? Math.Min(target, _knobPosition + ToggleAnimationStep)
            : Math.Max(target, _knobPosition - ToggleAnimationStep);

        if (_knobPosition == target) _toggleAnimationTimer.Stop();
        Refresh();
    }

    #endregion

    #region Drawing

    protected override void PaintControl(Graphics graphics)
    {
        var roundingValue = PrepareGeometry(ControlSize.Height);

        DrawBorder(graphics, roundingValue);

        var state = ClipToContent(graphics, roundingValue, (int)(2 + BorderWidth));
        FillBackground(graphics);
        DrawToggle(graphics);
        if (EnableHoverEffect && IsHovered)
        {
            using SolidBrush brush = new(Color.FromArgb(HoverEffectOpacity, HoverEffectColor));
            graphics.FillPath(brush, ShapePath);
        }
        graphics.Restore(state);

        DrawInnerFocusCue(graphics, roundingValue);
    }

    private void DrawToggle(Graphics graphics)
    {
        var offsetX = RegionRect.Width / 10;
        var offsetY = RegionRect.Height / 6;
        var knobSize = RegionRect.Height - offsetY * 2;
        var offX = RegionRect.X + offsetX;
        var onX = RegionRect.X + RegionRect.Width - offsetX - knobSize;
        var t = _knobPosition;

        RectangleF toggleRect = new(offX + (onX - offX) * t, RegionRect.Y + offsetY, knobSize, knobSize);

        var fill1 = GetRgbOrColor(UseGradientFill ? GradientFillColor1 : ColorValue);
        var fill2 = UseGradientFill ? (Rgb ? DrawEngine.GetRgbColor(Hue + 20) : GradientFillColor2) : fill1;

        // The "off" knob is drawn at half brightness (and translucent without a gradient).
        var offAlpha = UseGradientFill ? 255 : 100;
        using Brush brush = UseGradientFill
            ? new LinearGradientBrush(RegionRect, Blend(Dim(fill1, offAlpha), fill1, t), Blend(Dim(fill2, offAlpha), fill2, t), 360)
            : new SolidBrush(Blend(Dim(fill1, offAlpha), fill1, t));
        graphics.FillEllipse(brush, toggleRect);
    }

    private static Color Dim(Color color, int alpha) =>
        Color.FromArgb(alpha, (int)(color.R * 0.5F), (int)(color.G * 0.5F), (int)(color.B * 0.5F));

    private static Color Blend(Color from, Color to, float t) => Color.FromArgb(
        (int)(from.A + (to.A - from.A) * t),
        (int)(from.R + (to.R - from.R) * t),
        (int)(from.G + (to.G - from.G) * t),
        (int)(from.B + (to.B - from.B) * t));

    #endregion
}
