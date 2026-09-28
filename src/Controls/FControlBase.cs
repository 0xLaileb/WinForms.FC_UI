using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using Timer = System.Windows.Forms.Timer;

namespace FC_UI.Controls;

/// <summary>
/// Base class for all FC_UI custom controls. Provides shared rendering properties,
/// RGB timer management, border/lighting/gradient support, and common drawing infrastructure.
/// </summary>
public abstract class FControlBase : UserControl
{
    #region Style

    public enum ControlStyleMode
    {
        Default,
        Custom,
        Random
    }

    #endregion

    #region Fields

    protected float Hue;
    protected Rectangle RegionRect;
    protected GraphicsPath ShapePath = new();
    protected Size ControlSize;

    private (Size Size, Rectangle Rect, float Rounding) _regionKey;

    // Grayscale at half opacity for Enabled = false.
    private static readonly ColorMatrix DisabledColorMatrix = new(
    [
        [0.30F, 0.30F, 0.30F, 0, 0],
        [0.59F, 0.59F, 0.59F, 0, 0],
        [0.11F, 0.11F, 0.11F, 0, 0],
        [0, 0, 0, 0.5F, 0],
        [0, 0, 0, 0, 1]
    ]);

    #endregion

    #region Shared Properties

    // --- RGB ---

    private readonly Timer _rgbTimer = new() { Interval = 300 };

    [Category("Timers")]
    [Description("RGB mode update speed (triggers repaint)")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int RgbUpdateInterval
    {
        get => _rgbTimer.Interval;
        set
        {
            if (value > 0) _rgbTimer.Interval = value;
        }
    }

    [Description("Enable/Disable RGB mode")]
    [DefaultValue(false)]
    public bool Rgb
    {
        get;
        set
        {
            field = value;

            DrawEngine.GlobalRgbTick -= OnGlobalRgbTick;

            if (field)
            {
                // Both subscriptions stay active so the control keeps animating
                // when global RGB mode is switched on or off after this point.
                DrawEngine.GlobalRgbTick += OnGlobalRgbTick;
                _rgbTimer.Start();
            }
            else
            {
                _rgbTimer.Stop();
                Invalidate(true);
            }
        }
    }

    internal bool IsRgbTimerRunning => _rgbTimer.Enabled;

    // --- Rounding ---

    [Description("Enable/Disable corner rounding")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool Rounding
    {
        get;
        set { field = value; Invalidate(true); }
    }

    [Description("Corner radius percentage (0-100)")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int CornerRadius
    {
        get;
        set
        {
            if (value is >= 0 and <= 100)
            {
                field = value;
                Invalidate(true);
            }
        }
    }

    // --- Background ---

    [Description("Enable/Disable background fill")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool ShowBackground
    {
        get;
        set { field = value; Invalidate(true); }
    }

    [Description("Background color")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color BackgroundColor
    {
        get;
        set { field = value; Invalidate(true); }
    }

    // --- Border ---

    [Category("BorderStyle")]
    [Description("Enable/Disable border")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool ShowBorder
    {
        get;
        set
        {
            field = value;
            UpdateGeometry();
            Invalidate(true);
        }
    }

    [Category("BorderStyle")]
    [Description("Border width")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public float BorderWidth
    {
        get;
        set
        {
            if (value < 0) return;
            field = value;
            UpdateGeometry();
            Invalidate(true);
        }
    }

    [Category("BorderStyle")]
    [Description("Border color")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color BorderColor
    {
        get;
        set { field = value; Invalidate(true); }
    }

    // --- Lighting ---

    [Category("Lighting")]
    [Description("Enable/Disable lighting / shadow effect")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool Lighting
    {
        get;
        set
        {
            field = value;
            UpdateGeometry();
            Invalidate(true);
        }
    }

    [Category("Lighting")]
    [Description("Lighting / shadow color")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color LightingColor
    {
        get;
        set { field = value; Invalidate(true); }
    }

    [Category("Lighting")]
    [Description("Lighting max alpha")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int LightingAlpha
    {
        get;
        set
        {
            if (value is >= 0 and <= 255)
            {
                field = value;
                Invalidate(true);
            }
        }
    }

    [Category("Lighting")]
    [Description("Lighting pen width")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int LightingWidth
    {
        get;
        set
        {
            if (value < 0) return;
            field = value;
            UpdateGeometry();
            Invalidate(true);
        }
    }

    // --- Linear Gradient Background ---

    [Category("LinearGradient")]
    [Description("Enable/Disable background gradient")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool UseGradientBackground
    {
        get;
        set { field = value; Invalidate(true); }
    }

    [Category("LinearGradient")]
    [Description("Background gradient color #1")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color GradientColor1
    {
        get;
        set { field = value; Invalidate(true); }
    }

    [Category("LinearGradient")]
    [Description("Background gradient color #2")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color GradientColor2
    {
        get;
        set { field = value; Invalidate(true); }
    }

    // --- Linear Gradient Border ---

    [Category("LinearGradient")]
    [Description("Enable/Disable border gradient")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool UseGradientBorder
    {
        get;
        set { field = value; Invalidate(true); }
    }

    [Category("LinearGradient")]
    [Description("Border gradient color #1")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color GradientBorderColor1
    {
        get;
        set { field = value; Invalidate(true); }
    }

    [Category("LinearGradient")]
    [Description("Border gradient color #2")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color GradientBorderColor2
    {
        get;
        set { field = value; Invalidate(true); }
    }

    // --- Graphics Quality ---

    [Description("Graphics smoothing mode")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public SmoothingMode SmoothingMode
    {
        get;
        set
        {
            if (value != SmoothingMode.Invalid) field = value;
            Invalidate(true);
        }
    }

    [Description("Graphics text rendering hint")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public TextRenderingHint TextRenderingHint
    {
        get;
        set { field = value; Invalidate(true); }
    }

    #endregion

    #region Constructor

    protected FControlBase()
    {
        SetStyle(
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.UserPaint |
            ControlStyles.ResizeRedraw |
            ControlStyles.Selectable |
            ControlStyles.SupportsTransparentBackColor |
            ControlStyles.StandardDoubleClick,
            true);
        DoubleBuffered = true;
        Tag = "FC_UI";
        _rgbTimer.Tick += OnRgbTimerTick;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _rgbTimer.Stop();
            _rgbTimer.Tick -= OnRgbTimerTick;
            DrawEngine.GlobalRgbTick -= OnGlobalRgbTick;
            _rgbTimer.Dispose();
            ShapePath.Dispose();
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
            if (Enabled) PaintControl(e.Graphics);
            else PaintDisabled(e.Graphics);
        }
        catch (Exception ex)
        {
            // A failing paint must not take down the host form; keep the error visible in Release builds.
            Trace.WriteLine($"[{Name}] OnPaint error: {ex}");
        }

        base.OnPaint(e);
    }

    /// <summary>
    /// Draws the control. Called from <see cref="OnPaint"/>; disabled controls are drawn through a grayscale filter.
    /// </summary>
    protected virtual void PaintControl(Graphics graphics) { }

    protected override void OnSizeChanged(EventArgs e)
    {
        UpdateGeometry();
        base.OnSizeChanged(e);
    }

    /// <summary>
    /// Recalculates layout-dependent geometry after a size, border, or lighting change.
    /// </summary>
    protected virtual void UpdateGeometry() => RecalculateRegion();

    private void OnRgbTimerTick(object? sender, EventArgs e)
    {
        // While global RGB mode runs, the shared timer advances the hue and repaints.
        if (DrawEngine.GlobalRgbTimer.Enabled) return;

        Hue += 4;
        if (Hue >= 360) Hue = 0;
        Refresh();
    }

    private void OnGlobalRgbTick(object? sender, EventArgs e) => Refresh();

    /// <summary>
    /// True while the mouse pointer is over the control.
    /// </summary>
    protected bool IsHovered { get; private set; }

    protected override void OnMouseEnter(EventArgs e)
    {
        IsHovered = true;
        Invalidate();
        base.OnMouseEnter(e);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        IsHovered = false;
        Invalidate();
        base.OnMouseLeave(e);
    }

    protected override void OnGotFocus(EventArgs e)
    {
        Invalidate();
        base.OnGotFocus(e);
    }

    protected override void OnLostFocus(EventArgs e)
    {
        Invalidate();
        base.OnLostFocus(e);
    }

    #endregion

    #region Accessibility

    protected override AccessibleObject CreateAccessibilityInstance() => new FControlAccessibleObject(this);

    /// <summary>
    /// Accessible role reported when <see cref="Control.AccessibleRole"/> is left at <see cref="AccessibleRole.Default"/>.
    /// </summary>
    protected virtual AccessibleRole DefaultAccessibleRole => AccessibleRole.Client;

    /// <summary>
    /// Accessible name reported when <see cref="Control.AccessibleName"/> is not set.
    /// </summary>
    protected virtual string? AccessibleText => null;

    /// <summary>
    /// Extra accessible states such as <see cref="AccessibleStates.Checked"/>.
    /// </summary>
    protected virtual AccessibleStates AccessibleStateFlags => AccessibleStates.None;

    /// <summary>
    /// Accessible value, for example the current progress.
    /// </summary>
    protected virtual string? AccessibleValueText => null;

    /// <summary>
    /// Name of the default accessible action; <c>null</c> keeps the standard behavior.
    /// </summary>
    protected virtual string? AccessibleDefaultActionText => null;

    /// <summary>
    /// Performs the default accessible action named by <see cref="AccessibleDefaultActionText"/>.
    /// </summary>
    protected virtual void DoAccessibleDefaultAction() { }

    private sealed class FControlAccessibleObject(FControlBase owner) : ControlAccessibleObject(owner)
    {
        public override AccessibleRole Role =>
            owner.AccessibleRole != AccessibleRole.Default ? owner.AccessibleRole : owner.DefaultAccessibleRole;

        public override string? Name => owner.AccessibleName ?? owner.AccessibleText ?? base.Name;

        public override AccessibleStates State => base.State | owner.AccessibleStateFlags;

        public override string? Value => owner.AccessibleValueText ?? base.Value;

        public override string? DefaultAction => owner.AccessibleDefaultActionText ?? base.DefaultAction;

        public override void DoDefaultAction()
        {
            if (owner.AccessibleDefaultActionText is null) base.DoDefaultAction();
            else owner.DoAccessibleDefaultAction();
        }
    }

    #endregion

    #region Shared Methods

    /// <summary>
    /// Applies smoothing and text rendering settings to the graphics surface.
    /// </summary>
    protected void ApplyGraphicsSettings(Graphics graphics)
    {
        BackColor = Color.Transparent;
        graphics.SmoothingMode = SmoothingMode;
        graphics.TextRenderingHint = TextRenderingHint;
    }

    /// <summary>
    /// Recalculates the region rectangle and control size based on border/lighting settings.
    /// </summary>
    protected void RecalculateRegion()
    {
        var margin = (int)((ShowBorder ? BorderWidth : 0) + (Lighting ? LightingWidth / 4 : 0));
        ControlSize = new Size(Width - margin * 2, Height - margin * 2);
        RegionRect = new Rectangle(margin, margin, ControlSize.Width, ControlSize.Height);
    }

    /// <summary>
    /// Calculates the rounding value based on the reference height and current settings.
    /// </summary>
    protected float CalculateRoundingValue(float referenceHeight)
    {
        if (Rounding && CornerRadius > 0)
            return referenceHeight / 100F * CornerRadius;
        return 0.1F;
    }

    /// <summary>
    /// Returns the current RGB color or the provided fallback color.
    /// </summary>
    protected Color GetRgbOrColor(Color fallback) =>
        Rgb ? DrawEngine.GetRgbColor(Hue) : fallback;

    /// <summary>
    /// Prepares geometry: updates shape path, creates region.
    /// Returns the computed rounding value.
    /// </summary>
    protected float PrepareGeometry(float referenceHeight)
    {
        var roundingValue = CalculateRoundingValue(referenceHeight);

        ShapePath.Dispose();
        ShapePath = DrawEngine.CreateRoundedPath(RegionRect, roundingValue);
        UpdateRegion(roundingValue);

        return roundingValue;
    }

    /// <summary>
    /// Clips the window to a rounded rectangle; the native region is only replaced when the geometry changes.
    /// </summary>
    protected void UpdateRegion(float roundingValue)
    {
        var key = (Size, RegionRect, roundingValue);
        if (Region is not null && _regionKey == key) return;

        using var regionPath = DrawEngine.CreateRoundedPath(new Rectangle(0, 0, Width, Height), roundingValue);
        var oldRegion = Region;
        Region = new Region(regionPath);
        oldRegion?.Dispose();
        _regionKey = key;
    }

    /// <summary>
    /// Draws the lighting (shadow) and the border of <see cref="ShapePath"/>.
    /// </summary>
    /// <param name="graphics">Target surface.</param>
    /// <param name="roundingValue">Corner rounding of <see cref="RegionRect"/>.</param>
    /// <param name="borderColorOverride">Solid border color to use instead of the configured border (for example a focus highlight).</param>
    protected void DrawBorder(Graphics graphics, float roundingValue, Color? borderColorOverride = null)
    {
        if (Lighting)
        {
            using var shadowPath = DrawEngine.CreateRoundedPath(RegionRect, roundingValue);
            DrawEngine.DrawBlurredShadow(graphics, LightingColor, shadowPath, LightingAlpha, LightingWidth);
        }

        if (BorderWidth == 0 || !ShowBorder) return;

        using Brush brush = borderColorOverride is { } overrideColor
            ? new SolidBrush(overrideColor)
            : UseGradientBorder
                ? new LinearGradientBrush(RegionRect, GradientBorderColor1, GradientBorderColor2, 360)
                : new SolidBrush(GetRgbOrColor(BorderColor));
        using Pen pen = new(brush, BorderWidth);
        pen.LineJoin = LineJoin.Round;
        pen.DashCap = DashCap.Round;

        graphics.DrawPath(pen, ShapePath);
    }

    /// <summary>
    /// Clips <paramref name="graphics"/> to the rounded content area grown by <paramref name="offset"/> pixels.
    /// Restore the returned state after drawing the content.
    /// </summary>
    protected GraphicsState ClipToContent(Graphics graphics, float roundingValue, int offset)
    {
        var state = graphics.Save();
        using var clipPath = DrawEngine.CreateRoundedPath(
            Rectangle.Inflate(RegionRect, offset, offset),
            Rounding ? roundingValue : 0.1F);
        graphics.SetClip(clipPath, CombineMode.Intersect);
        return state;
    }

    /// <summary>
    /// Fills <see cref="ShapePath"/> with the solid or gradient background when <see cref="ShowBackground"/> is on.
    /// </summary>
    protected void FillBackground(Graphics graphics)
    {
        if (!ShowBackground) return;

        using Brush brush = UseGradientBackground
            ? new LinearGradientBrush(RegionRect, GradientColor1, GradientColor2, 360)
            : new SolidBrush(BackgroundColor);
        graphics.FillPath(brush, ShapePath);
    }

    /// <summary>
    /// Draws a dotted keyboard-focus outline along <paramref name="bounds"/> when focus cues are shown.
    /// </summary>
    /// <param name="graphics">Target surface.</param>
    /// <param name="bounds">Outline rectangle.</param>
    /// <param name="roundingValue">Corner rounding of the outline.</param>
    /// <param name="color">Outline color; defaults to <see cref="Control.ForeColor"/>.</param>
    protected void DrawFocusCue(Graphics graphics, Rectangle bounds, float roundingValue, Color? color = null)
    {
        if (!Focused || !ShowFocusCues || bounds.Width <= 0 || bounds.Height <= 0) return;

        using var path = DrawEngine.CreateRoundedPath(bounds, Math.Max(0.1F, roundingValue));
        using Pen pen = new(Color.FromArgb(170, color ?? ForeColor)) { DashStyle = DashStyle.Dot };
        graphics.DrawPath(pen, path);
    }

    /// <summary>
    /// Focus cue drawn inside the border of <see cref="RegionRect"/>.
    /// </summary>
    protected void DrawInnerFocusCue(Graphics graphics, float roundingValue)
    {
        var inset = (int)Math.Ceiling((ShowBorder ? BorderWidth : 0) / 2) + 3;
        DrawFocusCue(graphics, Rectangle.Inflate(RegionRect, -inset, -inset), roundingValue - inset * 2);
    }

    /// <summary>
    /// Renders the border layer (shadow + border) into a bitmap.
    /// </summary>
    protected Bitmap RenderBorderLayer(float roundingValue)
    {
        Bitmap bitmap = new(Width, Height);
        using var graphics = HelpEngine.GetGraphics(bitmap, SmoothingMode, TextRenderingHint);
        DrawBorder(graphics, roundingValue);
        return bitmap;
    }

    /// <summary>
    /// Renders the content layer (background fill) into a bitmap with optional clipping.
    /// </summary>
    protected Bitmap RenderContentLayer(float roundingValue)
    {
        Bitmap bitmap = new(Width, Height);
        using var graphics = HelpEngine.GetGraphics(bitmap, SmoothingMode, TextRenderingHint);
        ClipToContent(graphics, roundingValue, 1);
        FillBackground(graphics);
        return bitmap;
    }

    /// <summary>
    /// Convenience: renders both layers to the form graphics.
    /// </summary>
    protected void DrawLayeredBackground(Graphics formGraphics, float roundingValue)
    {
        DrawBorder(formGraphics, roundingValue);
        var state = ClipToContent(formGraphics, roundingValue, 1);
        FillBackground(formGraphics);
        formGraphics.Restore(state);
    }

    private void PaintDisabled(Graphics target)
    {
        if (Width <= 0 || Height <= 0) return;

        // ClearType needs an opaque surface, so the offscreen pass uses grayscale antialiasing.
        using Bitmap bitmap = new(Width, Height);
        using (var graphics = HelpEngine.GetGraphics(bitmap, SmoothingMode, TextRenderingHint.AntiAliasGridFit))
            PaintControl(graphics);

        using ImageAttributes attributes = new();
        attributes.SetColorMatrix(DisabledColorMatrix);
        target.DrawImage(bitmap, new Rectangle(Point.Empty, bitmap.Size), 0, 0, bitmap.Width, bitmap.Height,
            GraphicsUnit.Pixel, attributes);
    }

    #endregion
}
