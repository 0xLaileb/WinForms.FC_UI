using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using Timer = System.Windows.Forms.Timer;

namespace FC_UI.Controls;

[ToolboxBitmap(typeof(RadioButton))]
[Description("Allows the user to select a single option from a group of choices.")]
public partial class FRadioButton : FControlBase
{
    #region Fields

    // Geometry is designed for the default 45 px height and scaled proportionally to Height.
    private const int DesignHeight = 45;
    private const int DesignBoxSize = 21;
    private const int DesignEffectSize = 40;

    private int _animationSize;
    private float _scale = 1F;

    #endregion

    #region Properties

    public delegate void CheckedChangedHandler();

    [Category("FC_UI")]
    [Description("Occurs on every Checked property change.")]
    public event CheckedChangedHandler CheckedChanged = delegate { };

    [Category("FRadioButton")]
    [Description("Enable/Disable checked status. Checking a button unchecks the other AutoCheck buttons in the same container.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool Checked
    {
        get;
        set
        {
            if (field == value) return;
            field = value;
            if (value && AutoCheck) UncheckSiblings();
            CheckedChanged();
            AccessibilityNotifyClients(AccessibleEvents.StateChange, -1);
            Invalidate(true);
        }
    }

    [Category("FRadioButton")]
    [Description("Check the button on click and uncheck the other AutoCheck buttons in the same container")]
    [DefaultValue(true)]
    public bool AutoCheck { get; set; } = true;

    [Category("FRadioButton")]
    [Description("Inner circle size offset (must be even, default = 8)")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int SizeChecked
    {
        get;
        set { if (value % 2 == 0) { field = value; Invalidate(true); } }
    }

    [Category("FRadioButton")]
    [Description("Control text")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public string DisplayText
    {
        get;
        set { field = value; Invalidate(true); }
    } = string.Empty;

    [Category("FRadioButton")]
    [Description("Checkmark color")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color ColorChecked
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
    [Description("Click animation color")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color ClickEffectColor
    {
        get;
        set { field = value; Invalidate(true); }
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

    [Category("FRadioButton")]
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
                    SizeChecked = 8;
                    DisplayText = "FRadioButton";
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
            Invalidate(true);
        }
    }

    #endregion

    #region Initialization

    public FRadioButton()
    {
        ControlStyle = ControlStyleMode.Default;
        ControlStyle = ControlStyleMode.Custom;
        _clickAnimationTimer.Tick += (_, _) => StepClickAnimation();
        UpdateGeometry();
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
            cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
            return cp;
        }
    }

    #endregion

    #region Events

    protected override void OnMouseClick(MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left) ActivateByUser();
        base.OnMouseClick(e);
    }

    // Arrow keys move within the group; a lone button leaves them to normal focus navigation.
    protected override bool IsInputKey(Keys keyData) =>
        (keyData is Keys.Up or Keys.Down or Keys.Left or Keys.Right && GetGroupSibling(1) is not null)
        || base.IsInputKey(keyData);

    protected override void OnKeyDown(KeyEventArgs e)
    {
        var step = e.KeyCode switch
        {
            Keys.Up or Keys.Left => -1,
            Keys.Down or Keys.Right => 1,
            _ => 0
        };

        if (step != 0 && e.Modifiers == Keys.None && GetGroupSibling(step) is { } next)
        {
            next.Focus();
            next.ActivateByUser();
            e.Handled = true;
        }
        base.OnKeyDown(e);
    }

    protected override void OnKeyUp(KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Space && e.Modifiers == Keys.None)
        {
            ActivateByUser();
            e.Handled = true;
        }
        base.OnKeyUp(e);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        _clickAnimationTimer.Stop();
        _animationSize = 0;
        base.OnMouseLeave(e);
    }

    /// <summary>
    /// Handles a user activation (click, Space, arrow keys, accessibility action).
    /// </summary>
    private void ActivateByUser()
    {
        if (!AutoCheck || Checked) return;

        Checked = true;
        _clickAnimationTimer.Stop();
        _animationSize = RegionRect.Width;
        _clickAnimationTimer.Start();
    }

    private void UncheckSiblings()
    {
        if (Parent is null) return;

        foreach (var sibling in Parent.Controls.OfType<FRadioButton>())
        {
            if (sibling != this && sibling.AutoCheck) sibling.Checked = false;
        }
    }

    private FRadioButton? GetGroupSibling(int step)
    {
        if (Parent is null) return null;

        var group = Parent.Controls.OfType<FRadioButton>()
            .Where(button => button == this || (button.AutoCheck && button.Visible && button.Enabled))
            .OrderBy(button => button.TabIndex)
            .ToList();
        if (group.Count < 2) return null;

        var index = group.IndexOf(this);
        return group[(index + step + group.Count) % group.Count];
    }

    protected override void UpdateGeometry()
    {
        _scale = Math.Max(0.4F, (float)Height / DesignHeight);
        var boxSize = Math.Max(8, (int)Math.Round(DesignBoxSize * _scale));
        RegionRect = new Rectangle((int)Math.Round(15 * _scale), Height / 2 - (boxSize + 3) / 2, boxSize, boxSize);
        ControlSize = RegionRect.Size;
    }

    #endregion

    #region Accessibility

    protected override AccessibleRole DefaultAccessibleRole => AccessibleRole.RadioButton;

    protected override string? AccessibleText => DisplayText;

    protected override AccessibleStates AccessibleStateFlags => Checked ? AccessibleStates.Checked : AccessibleStates.None;

    protected override string AccessibleDefaultActionText => "Select";

    protected override void DoAccessibleDefaultAction() => ActivateByUser();

    #endregion

    #region Animation

    internal bool IsClickAnimationRunning => _clickAnimationTimer.Enabled;

    private int ClickAnimationMaxSize => (int)Math.Round(DesignEffectSize * _scale);

    internal void StepClickAnimation()
    {
        _animationSize += Math.Max(1, (int)Math.Round(_scale));

        // Stop once the ripple reaches its final size instead of repainting forever.
        if (_animationSize >= ClickAnimationMaxSize) _clickAnimationTimer.Stop();

        Refresh();
    }

    #endregion

    #region Drawing

    protected override void PaintControl(Graphics graphics)
    {
        var roundingValue = CalculateRoundingValue(RegionRect.Height);

        ShapePath.Dispose();
        ShapePath = DrawEngine.CreateRoundedPath(RegionRect, roundingValue);
        UpdateRegion(roundingValue);

        DrawBorder(graphics, roundingValue);

        if (EnableClickEffect && _animationSize < ClickAnimationMaxSize)
            DrawEffectCircle(graphics, _animationSize, ClickEffectOpacity, ClickEffectColor);
        if (EnableHoverEffect && IsHovered)
            DrawEffectCircle(graphics, ClickAnimationMaxSize, HoverEffectOpacity, HoverEffectColor);

        FillBackground(graphics);
        if (Checked) DrawCheckMark(graphics);

        var textX = RegionRect.Right + (int)Math.Round(10 * _scale);
        var textY = Height / 2 - Font.Height / 2;
        using SolidBrush brush = new(ForeColor);
        graphics.DrawString(DisplayText, Font, brush, textX, textY);

        var textWidth = (int)Math.Ceiling(graphics.MeasureString(DisplayText, Font).Width);
        DrawFocusCue(graphics, new Rectangle(textX - 2, textY - 1, textWidth + 2, Font.Height + 2), 0.1F);
    }

    private void DrawCheckMark(Graphics graphics)
    {
        var inset = (int)Math.Round(SizeChecked * _scale / 2);
        var checkRect = Rectangle.Inflate(RegionRect, -inset, -inset);
        if (checkRect.Width <= 0 || checkRect.Height <= 0) return;

        using var checkPath = DrawEngine.CreateRoundedPath(checkRect, Rounding ? checkRect.Height / 100F * CornerRadius : 0.1F);

        using Brush brush = UseGradientFill
            ? new LinearGradientBrush(RegionRect,
                GetRgbOrColor(GradientFillColor1),
                Rgb ? DrawEngine.GetRgbColor(Hue + 20) : GradientFillColor2,
                360)
            : new SolidBrush(GetRgbOrColor(ColorChecked));
        graphics.FillPath(brush, checkPath);
    }

    private void DrawEffectCircle(Graphics graphics, int size, int opacity, Color color)
    {
        if (size <= 0) return;

        var centerX = RegionRect.X + RegionRect.Width / 2F;
        var centerY = RegionRect.Y + RegionRect.Height / 2F;
        using SolidBrush brush = new(Color.FromArgb(opacity, color));
        graphics.FillEllipse(brush, centerX - size / 2F, centerY - size / 2F, size, size);
    }

    #endregion
}
