using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using Timer = System.Windows.Forms.Timer;

namespace FC_UI.Controls;

[ToolboxBitmap(typeof(Button))]
[Description("Raises an event when clicked.")]
[DefaultEvent("Click")]
public partial class FButton : FControlBase, IButtonControl
{
    #region Fields

    private const int ImageTextGap = 6;

    private Point _clickLocation;
    private readonly StringFormat _textFormat = new();
    private int _animationSize;

    #endregion

    #region Properties

    [Category("FButton")]
    [Description("Control text")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public string DisplayText
    {
        get;
        set { field = value; Invalidate(true); }
    } = string.Empty;

    // --- Image ---

    [Category("FButton")]
    [Description("Image displayed on the button (not disposed by the button)")]
    [DefaultValue(null)]
    public Image? Image
    {
        get;
        set { field = value; Invalidate(); }
    }

    [Category("FButton")]
    [Description("Image size; empty uses the image size scaled down to fit the button")]
    public Size ImageSize
    {
        get;
        set
        {
            if (value.Width < 0 || value.Height < 0) return;
            field = value;
            Invalidate();
        }
    }

    [Category("FButton")]
    [Description("Position of the image relative to the text")]
    [DefaultValue(TextImageRelation.ImageBeforeText)]
    public TextImageRelation TextImageRelation
    {
        get;
        set { field = value; Invalidate(); }
    } = TextImageRelation.ImageBeforeText;

    [Category("Behavior")]
    [Description("Dialog result assigned to the parent form when the button is clicked")]
    [DefaultValue(DialogResult.None)]
    public DialogResult DialogResult { get; set; }

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
        set
        {
            if (value > 0) _clickAnimationTimer.Interval = value;
        }
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
                    Size = new Size(130, 50);
                    BackColor = Color.Transparent;
                    ForeColor = Color.FromArgb(245, 245, 245);
                    DisplayText = "FButton";
                    Rgb = false;
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
            Invalidate(true);
        }
    }

    #endregion

    #region Initialization

    public FButton()
    {
        // Like Button: two quick clicks are two Click events, not a DoubleClick.
        SetStyle(ControlStyles.StandardDoubleClick, false);

        ControlStyle = ControlStyleMode.Default;
        ControlStyle = ControlStyleMode.Custom;

        _textFormat.Alignment = StringAlignment.Center;
        _textFormat.LineAlignment = StringAlignment.Center;
        _clickAnimationTimer.Tick += (_, _) => StepClickAnimation();

        UpdateGeometry();
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

    private bool ShouldSerializeImageSize() => !ImageSize.IsEmpty;

    private void ResetImageSize() => ImageSize = Size.Empty;

    #endregion

    #region IButtonControl

    /// <summary>
    /// Called by the parent form when the button becomes or stops being its default (Enter) button.
    /// </summary>
    public void NotifyDefault(bool value)
    {
        // No separate look for the default button: Enter handling is done by the form.
    }

    /// <summary>
    /// Raises <see cref="Control.Click"/> as if the user clicked the button.
    /// </summary>
    public void PerformClick()
    {
        if (!CanSelect) return;

        // Like Button.PerformClick: do not click when validation of the focused control is cancelled.
        if (Parent?.GetContainerControl() is ContainerControl container && !container.Validate()) return;

        StartClickAnimation(new Point(Width / 2, Height / 2));
        OnClick(EventArgs.Empty);
    }

    protected override void OnClick(EventArgs e)
    {
        if (DialogResult != DialogResult.None && FindForm() is { } form) form.DialogResult = DialogResult;
        base.OnClick(e);
    }

    #endregion

    #region Events

    protected override void OnMouseLeave(EventArgs e)
    {
        _clickAnimationTimer.Stop();
        _animationSize = 0;
        base.OnMouseLeave(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left) StartClickAnimation(e.Location);
        base.OnMouseUp(e);
    }

    // A focused button handles Enter itself; FButton is a ContainerControl, so the form would not make it the default button.
    protected override bool IsInputKey(Keys keyData) => keyData == Keys.Enter || base.IsInputKey(keyData);

    protected override void OnKeyDown(KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter && e.Modifiers == Keys.None)
        {
            PerformClick();
            e.Handled = true;
        }
        base.OnKeyDown(e);
    }

    protected override void OnKeyUp(KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Space && e.Modifiers == Keys.None)
        {
            PerformClick();
            e.Handled = true;
        }
        base.OnKeyUp(e);
    }

    #endregion

    #region Accessibility

    protected override AccessibleRole DefaultAccessibleRole => AccessibleRole.PushButton;

    protected override string? AccessibleText => DisplayText;

    protected override string AccessibleDefaultActionText => "Press";

    protected override void DoAccessibleDefaultAction() => PerformClick();

    #endregion

    #region Animation

    internal bool IsClickAnimationRunning => _clickAnimationTimer.Enabled;

    private int ClickAnimationMaxSize => Math.Max(ControlSize.Width, ControlSize.Height) * 2;

    private void StartClickAnimation(Point location)
    {
        _clickAnimationTimer.Stop();
        if (!EnableClickEffect) return;

        _clickLocation = location;
        _animationSize = 2;
        _clickAnimationTimer.Start();
    }

    internal void StepClickAnimation()
    {
        _animationSize += 20;

        // Stop once the ripple has covered the control instead of repainting forever.
        if (_animationSize >= ClickAnimationMaxSize) _clickAnimationTimer.Stop();

        Refresh();
    }

    #endregion

    #region Drawing

    protected override void PaintControl(Graphics graphics)
    {
        var roundingValue = PrepareGeometry(Height);

        DrawBorder(graphics, roundingValue);

        var state = ClipToContent(graphics, roundingValue, 1);
        FillBackground(graphics);
        if (EnableClickEffect) DrawClickAnimation(graphics);
        if (EnableHoverEffect && IsHovered) DrawHoverOverlay(graphics);
        graphics.Restore(state);

        DrawContent(graphics);
        DrawInnerFocusCue(graphics, roundingValue);
    }

    private void DrawContent(Graphics graphics)
    {
        using SolidBrush brush = new(ForeColor);

        if (Image is null)
        {
            graphics.DrawString(DisplayText, Font, brush, RegionRect, _textFormat);
            return;
        }

        var bounds = Rectangle.Inflate(RegionRect, -6, -4);
        var textSize = string.IsNullOrEmpty(DisplayText) ? SizeF.Empty : graphics.MeasureString(DisplayText, Font);
        var imageSize = ResolveImageSize(bounds);
        var (imageBounds, textBounds) = LayoutImageAndText(bounds, imageSize, textSize, TextImageRelation,
            textSize.IsEmpty ? 0 : ImageTextGap);

        var state = graphics.Save();
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.DrawImage(Image, imageBounds);
        graphics.Restore(state);

        if (!textSize.IsEmpty) graphics.DrawString(DisplayText, Font, brush, textBounds, _textFormat);
    }

    private Size ResolveImageSize(Rectangle bounds)
    {
        if (!ImageSize.IsEmpty) return ImageSize;
        if (Image is null || Image.Width <= 0 || Image.Height <= 0) return Size.Empty;

        var scale = Math.Min(1F, Math.Min((float)bounds.Width / Image.Width, (float)bounds.Height / Image.Height));
        return new Size(Math.Max(1, (int)(Image.Width * scale)), Math.Max(1, (int)(Image.Height * scale)));
    }

    /// <summary>
    /// Places the image and the text inside <paramref name="bounds"/>, centering the combined block.
    /// </summary>
    internal static (Rectangle Image, RectangleF Text) LayoutImageAndText(
        Rectangle bounds, Size imageSize, SizeF textSize, TextImageRelation relation, int gap)
    {
        var centerX = bounds.X + bounds.Width / 2F;
        var centerY = bounds.Y + bounds.Height / 2F;

        switch (relation)
        {
            case TextImageRelation.ImageBeforeText:
            case TextImageRelation.TextBeforeImage:
            {
                var textWidth = Math.Min(textSize.Width, Math.Max(0, bounds.Width - imageSize.Width - gap));
                var left = centerX - (imageSize.Width + gap + textWidth) / 2;
                var imageFirst = relation == TextImageRelation.ImageBeforeText;
                var imageX = imageFirst ? left : left + textWidth + gap;
                var textX = imageFirst ? left + imageSize.Width + gap : left;
                return (
                    new Rectangle((int)imageX, (int)(centerY - imageSize.Height / 2F), imageSize.Width, imageSize.Height),
                    new RectangleF(textX, bounds.Y, textWidth, bounds.Height));
            }
            case TextImageRelation.ImageAboveText:
            case TextImageRelation.TextAboveImage:
            {
                var textHeight = Math.Min(textSize.Height, Math.Max(0, bounds.Height - imageSize.Height - gap));
                var top = centerY - (imageSize.Height + gap + textHeight) / 2;
                var imageFirst = relation == TextImageRelation.ImageAboveText;
                var imageY = imageFirst ? top : top + textHeight + gap;
                var textY = imageFirst ? top + imageSize.Height + gap : top;
                return (
                    new Rectangle((int)(centerX - imageSize.Width / 2F), (int)imageY, imageSize.Width, imageSize.Height),
                    new RectangleF(bounds.X, textY, bounds.Width, textHeight));
            }
            default:
                return (
                    new Rectangle((int)(centerX - imageSize.Width / 2F), (int)(centerY - imageSize.Height / 2F),
                        imageSize.Width, imageSize.Height),
                    bounds);
        }
    }

    private void DrawClickAnimation(Graphics graphics)
    {
        if (_animationSize >= ClickAnimationMaxSize) return;

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

    private void DrawHoverOverlay(Graphics graphics)
    {
        using SolidBrush brush = new(Color.FromArgb(HoverEffectOpacity, HoverEffectColor));
        graphics.FillPath(brush, ShapePath);
    }

    #endregion
}
