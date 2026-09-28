using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace FC_UI.Controls;

[ToolboxBitmap(typeof(VScrollBar))]
[Description("Provides horizontal/vertical content scrolling capability (use event subscription).")]
[DefaultEvent("ValueChanged")]
public partial class FScrollBar : FControlBase, ISupportInitialize
{
    #region Fields

    private bool _initializing;
    private bool _isDragging;
    private int _dragOffset;
    private bool _isThumbHovered;
    private int _wheelDelta;

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
            var clampedValue = Math.Clamp(value, Minimum, Maximum);
            if (field == clampedValue) return;
            field = clampedValue;
            AccessibilityNotifyClients(AccessibleEvents.ValueChange, -1);
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
            // Re-applying the current orientation must not swap the size or rescale the radius again.
            if (field == value) return;
            field = value;

            // Designer code restores the final Size and CornerRadius itself (between BeginInit and EndInit).
            if (!_initializing)
            {
                Size = new Size(Size.Height, Size.Width);
                if (CornerRadius != 0)
                {
                    // CornerRadius is a percentage of the height, which changes with the orientation.
                    // Out-of-range results are ignored by the CornerRadius setter.
                    CornerRadius = value == Orientation.Vertical ? CornerRadius / 10 : CornerRadius * 10;
                }
            }
            Invalidate(true);
        }
    }

    [Category("Value")]
    [Description("Value change for arrow keys and the mouse wheel")]
    [DefaultValue(1)]
    public int SmallStep { get; set; }

    [Category("Value")]
    [Description("Value change for Page Up / Page Down")]
    [DefaultValue(10)]
    public int LargeStep { get; set; } = 10;

    [Category("Value")]
    [Description("Thumb size")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int ThumbSize
    {
        get;
        set { field = value; Invalidate(true); }
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

    [Category("FScrollBar")]
    [Description("Thumb color")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color ThumbColor
    {
        get;
        set { field = value; Invalidate(true); }
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
                Invalidate(true);
            }
        }
    }

    // --- Effects ---

    [Category("Effects")]
    [Description("Highlight the thumb while it is hovered or dragged")]
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

    // --- Gradient Fill ---

    [Category("LinearGradient")]
    [Description("Enable/Disable thumb gradient")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool UseGradientFill
    {
        get;
        set { field = value; Invalidate(true); }
    }

    [Category("LinearGradient")]
    [Description("Thumb gradient color #1")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color GradientFillColor1
    {
        get;
        set { field = value; Invalidate(true); }
    }

    [Category("LinearGradient")]
    [Description("Thumb gradient color #2")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color GradientFillColor2
    {
        get;
        set { field = value; Invalidate(true); }
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
                    // Orientation first: switching it swaps the size set below.
                    Orientation = Orientation.Vertical;
                    Size = new Size(26, 300);
                    BackColor = Color.Transparent;
                    ForeColor = Color.FromArgb(245, 245, 245);
                    Value = 0;
                    Minimum = 0;
                    Maximum = 100;
                    ThumbSize = 60;
                    SmallStep = 1;
                    LargeStep = 10;
                    Rgb = false;
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
                    EnableHoverEffect = true;
                    HoverEffectOpacity = 50;
                    HoverEffectColor = Color.White;
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
            Invalidate(true);
        }
    }

    #endregion

    #region Initialization

    public FScrollBar()
    {
        ControlStyle = ControlStyleMode.Default;
        ControlStyle = ControlStyleMode.Custom;

        UpdateGeometry();
    }

    /// <summary>
    /// Suspends the automatic size swap and corner-radius scaling of <see cref="Orientation"/> while designer code loads.
    /// </summary>
    public void BeginInit() => _initializing = true;

    public void EndInit()
    {
        _initializing = false;
        UpdateGeometry();
        Invalidate(true);
    }

    #endregion

    #region Events

    public virtual void OnScroll(ScrollEventType type = ScrollEventType.ThumbPosition)
    {
        ValueChanged?.Invoke(this, EventArgs.Empty);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            Focus();
            var thumb = GetThumbBounds();
            var position = AxisPosition(e.Location);
            var thumbStart = Orientation == Orientation.Vertical ? thumb.Y : thumb.X;

            // Grabbing the thumb keeps the grab point under the cursor; clicking the track centers the thumb there.
            _dragOffset = thumb.Contains(e.Location) ? position - thumbStart : ThumbSize / 2;
            _isDragging = true;
            ScrollToPosition(position);
        }
        base.OnMouseDown(e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        if (_isDragging && e.Button == MouseButtons.Left) ScrollToPosition(AxisPosition(e.Location));

        var thumbHovered = GetThumbBounds().Contains(e.Location);
        if (thumbHovered != _isThumbHovered)
        {
            _isThumbHovered = thumbHovered;
            Invalidate();
        }
        base.OnMouseMove(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left) _isDragging = false;
        base.OnMouseUp(e);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        _isThumbHovered = false;
        base.OnMouseLeave(e);
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
        // Touchpads and high-resolution wheels send fractions of a notch; accumulate them.
        _wheelDelta += e.Delta;
        var notches = _wheelDelta / SystemInformation.MouseWheelScrollDelta;
        if (notches != 0)
        {
            _wheelDelta -= notches * SystemInformation.MouseWheelScrollDelta;
            var lines = SystemInformation.MouseWheelScrollLines;
            var step = lines > 0 ? SmallStep * lines : LargeStep;
            ChangeValue(-(long)notches * step);
        }

        // Keep the parent (for example an AutoScroll panel) from scrolling as well.
        if (e is HandledMouseEventArgs handled) handled.Handled = true;
        base.OnMouseWheel(e);
    }

    protected override bool IsInputKey(Keys keyData) => keyData switch
    {
        Keys.Up or Keys.Down or Keys.Left or Keys.Right or Keys.PageUp or Keys.PageDown or Keys.Home or Keys.End => true,
        _ => base.IsInputKey(keyData)
    };

    protected override void OnKeyDown(KeyEventArgs e)
    {
        if (e.Modifiers == Keys.None)
        {
            e.Handled = true;
            switch (e.KeyCode)
            {
                case Keys.Up or Keys.Left: ChangeValue(-SmallStep); break;
                case Keys.Down or Keys.Right: ChangeValue(SmallStep); break;
                case Keys.PageUp: ChangeValue(-LargeStep); break;
                case Keys.PageDown: ChangeValue(LargeStep); break;
                case Keys.Home: Value = Minimum; break;
                case Keys.End: Value = Maximum; break;
                default: e.Handled = false; break;
            }
        }
        base.OnKeyDown(e);
    }

    private void ChangeValue(long delta)
    {
        Value = (int)Math.Clamp(Value + delta, Minimum, Maximum);
    }

    private int AxisPosition(Point point) => Orientation == Orientation.Vertical ? point.Y : point.X;

    private void ScrollToPosition(int position)
    {
        var trackStart = Orientation == Orientation.Vertical ? RegionRect.Y : RegionRect.X;
        var trackLength = (Orientation == Orientation.Vertical ? RegionRect.Height : RegionRect.Width) - ThumbSize;
        if (trackLength <= 0) return;

        var offset = Math.Clamp(position - _dragOffset - trackStart, 0, trackLength);
        Value = (int)(Minimum + (long)(Maximum - Minimum) * offset / trackLength);
    }

    #endregion

    #region Accessibility

    protected override AccessibleRole DefaultAccessibleRole => AccessibleRole.ScrollBar;

    protected override string AccessibleValueText => Value.ToString();

    #endregion

    #region Drawing

    /// <summary>
    /// Thumb bounds in client coordinates.
    /// </summary>
    internal Rectangle GetThumbBounds()
    {
        var valueRange = Maximum - Minimum;
        var valueOffset = (long)(Value - Minimum);

        if (Orientation == Orientation.Vertical)
        {
            var track = RegionRect.Height - ThumbSize;
            var y = track > 0 && valueRange > 0 ? (int)(valueOffset * track / valueRange) : 0;
            return new Rectangle(RegionRect.X, RegionRect.Y + y, RegionRect.Width, ThumbSize);
        }

        var hTrack = RegionRect.Width - ThumbSize;
        var x = hTrack > 0 && valueRange > 0 ? (int)(valueOffset * hTrack / valueRange) : 0;
        return new Rectangle(RegionRect.X + x, RegionRect.Y, ThumbSize, RegionRect.Height);
    }

    protected override void PaintControl(Graphics graphics)
    {
        var roundingValue = PrepareGeometry(Height);

        DrawBorder(graphics, roundingValue);

        var state = ClipToContent(graphics, roundingValue, (int)(2 + BorderWidth));
        FillBackground(graphics);
        DrawThumb(graphics, roundingValue);
        graphics.Restore(state);

        DrawInnerFocusCue(graphics, roundingValue);
    }

    private void DrawThumb(Graphics graphics, float roundingValue)
    {
        if (Maximum - Minimum <= 0) return;

        const int offset = 1;
        var thumbRect = Rectangle.Inflate(GetThumbBounds(), offset, offset);
        using var thumbPath = DrawEngine.CreateRoundedPath(thumbRect, roundingValue + offset * 2);

        using Brush brush = UseGradientFill
            ? new LinearGradientBrush(RegionRect,
                Color.FromArgb(ThumbOpacity, GetRgbOrColor(GradientFillColor1)),
                Color.FromArgb(ThumbOpacity, Rgb ? DrawEngine.GetRgbColor(Hue + 20) : GradientFillColor2),
                360)
            : new SolidBrush(Color.FromArgb(ThumbOpacity, GetRgbOrColor(ThumbColor)));
        graphics.FillPath(brush, thumbPath);

        if (EnableHoverEffect && (_isThumbHovered || _isDragging))
        {
            using SolidBrush hoverBrush = new(Color.FromArgb(HoverEffectOpacity, HoverEffectColor));
            graphics.FillPath(hoverBrush, thumbPath);
        }
    }

    #endregion
}
