using System.ComponentModel;
using System.Drawing.Drawing2D;
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

    protected float _hue;
    protected Rectangle _regionRect = new();
    protected GraphicsPath _shapePath = new();
    protected Size _controlSize = new();
    private EventHandler? _rgbTickHandler;
    private EventHandler? _globalRgbTickHandler;

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
        set => _rgbTimer.Interval = value;
    }

    private bool _isRgbEnabled;

    [Description("Enable/Disable RGB mode")]
    [DefaultValue(false)]
    public bool RGB
    {
        get => _isRgbEnabled;
        set
        {
            _isRgbEnabled = value;

            _rgbTimer.Stop();
            if (_rgbTickHandler is not null)
            {
                _rgbTimer.Tick -= _rgbTickHandler;
                _rgbTickHandler = null;
            }
            if (_globalRgbTickHandler is not null)
            {
                DrawEngine.GlobalRgbTimer.Tick -= _globalRgbTickHandler;
                _globalRgbTickHandler = null;
            }

            if (_isRgbEnabled)
            {
                if (DrawEngine.GlobalRgbTimer.Enabled)
                {
                    // HsvToRgb already uses s_globalHue when GlobalRgbTimer is active;
                    // subscribe for repaints only.
                    _globalRgbTickHandler = (sender, args) => Refresh();
                    DrawEngine.GlobalRgbTimer.Tick += _globalRgbTickHandler;
                }
                else
                {
                    _rgbTickHandler = (sender, args) =>
                    {
                        _hue += 4;
                        if (_hue >= 360) _hue = 0;
                        Refresh();
                    };
                    _rgbTimer.Tick += _rgbTickHandler;
                    _rgbTimer.Start();
                }
            }
            else
            {
                Refresh();
            }
        }
    }

    // --- Rounding ---

    [Description("Enable/Disable corner rounding")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool Rounding
    {
        get;
        set { field = value; Refresh(); }
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
                Refresh();
            }
        }
    }

    // --- Background ---

    [Description("Enable/Disable background fill")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool ShowBackground
    {
        get;
        set { field = value; Refresh(); }
    }

    [Description("Background color")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color BackgroundColor
    {
        get;
        set { field = value; Refresh(); }
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
            OnSizeChanged(EventArgs.Empty);
            Refresh();
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
            field = value;
            OnSizeChanged(EventArgs.Empty);
            Refresh();
        }
    }

    [Category("BorderStyle")]
    [Description("Border color")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color BorderColor
    {
        get;
        set { field = value; Refresh(); }
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
            OnSizeChanged(EventArgs.Empty);
            Refresh();
        }
    }

    [Category("Lighting")]
    [Description("Lighting / shadow color")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color LightingColor
    {
        get;
        set { field = value; Refresh(); }
    }

    [Category("Lighting")]
    [Description("Lighting max alpha")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int LightingAlpha
    {
        get;
        set { field = value; Refresh(); }
    }

    [Category("Lighting")]
    [Description("Lighting pen width")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int LightingWidth
    {
        get;
        set
        {
            field = value;
            OnSizeChanged(EventArgs.Empty);
            Refresh();
        }
    }

    // --- Linear Gradient Background ---

    [Category("LinearGradient")]
    [Description("Enable/Disable background gradient")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool UseGradientBackground
    {
        get;
        set { field = value; Refresh(); }
    }

    [Category("LinearGradient")]
    [Description("Background gradient color #1")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color GradientColor1
    {
        get;
        set { field = value; Refresh(); }
    }

    [Category("LinearGradient")]
    [Description("Background gradient color #2")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color GradientColor2
    {
        get;
        set { field = value; Refresh(); }
    }

    // --- Linear Gradient Border ---

    [Category("LinearGradient")]
    [Description("Enable/Disable border gradient")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool UseGradientBorder
    {
        get;
        set { field = value; Refresh(); }
    }

    [Category("LinearGradient")]
    [Description("Border gradient color #1")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color GradientBorderColor1
    {
        get;
        set { field = value; Refresh(); }
    }

    [Category("LinearGradient")]
    [Description("Border gradient color #2")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color GradientBorderColor2
    {
        get;
        set { field = value; Refresh(); }
    }

    // --- Graphics Quality ---

    private SmoothingMode _smoothingMode;

    [Description("Graphics smoothing mode")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public SmoothingMode SmoothingMode
    {
        get => _smoothingMode;
        set
        {
            if (value != SmoothingMode.Invalid) _smoothingMode = value;
            Refresh();
        }
    }

    [Description("Graphics text rendering hint")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public TextRenderingHint TextRenderingHint
    {
        get;
        set { field = value; Refresh(); }
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
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _rgbTimer.Stop();
            if (_rgbTickHandler is not null)
            {
                _rgbTimer.Tick -= _rgbTickHandler;
                _rgbTickHandler = null;
            }
            if (_globalRgbTickHandler is not null)
            {
                DrawEngine.GlobalRgbTimer.Tick -= _globalRgbTickHandler;
                _globalRgbTickHandler = null;
            }
            _rgbTimer.Dispose();
            _shapePath.Dispose();
        }
        base.Dispose(disposing);
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
        int margin = (int)((ShowBorder ? BorderWidth : 0) + (Lighting ? LightingWidth / 4 : 0));
        _controlSize = new(Width - margin * 2, Height - margin * 2);
        _regionRect = new(margin, margin, _controlSize.Width, _controlSize.Height);
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
        RGB ? DrawEngine.HsvToRgb(_hue, 1f, 1f) : fallback;

    /// <summary>
    /// Prepares geometry: updates shape path, creates region.
    /// Returns the computed rounding value.
    /// </summary>
    protected float PrepareGeometry(float referenceHeight)
    {
        float roundingValue = CalculateRoundingValue(referenceHeight);

        _shapePath?.Dispose();
        _shapePath = DrawEngine.CreateRoundedPath(_regionRect, roundingValue);

        using GraphicsPath regionPath = DrawEngine.CreateRoundedPath(new(0, 0, Width, Height), roundingValue);
        Region?.Dispose();
        Region = new Region(regionPath);

        return roundingValue;
    }

    /// <summary>
    /// Renders the border layer (shadow + border) into a bitmap.
    /// </summary>
    protected Bitmap RenderBorderLayer(float roundingValue)
    {
        Bitmap bitmap = new(Width, Height);
        using Graphics graphics = HelpEngine.GetGraphics(bitmap, SmoothingMode, TextRenderingHint);

        if (Lighting)
        {
            using GraphicsPath shadowPath = DrawEngine.CreateRoundedPath(_regionRect, roundingValue);
            DrawEngine.DrawBlurredShadow(graphics, LightingColor, shadowPath, LightingAlpha, LightingWidth);
        }

        if (BorderWidth != 0 && ShowBorder)
        {
            if (UseGradientBorder)
            {
                using LinearGradientBrush brush = new(_regionRect, GradientBorderColor1, GradientBorderColor2, 360);
                using Pen pen = new(brush, BorderWidth) { LineJoin = LineJoin.Round, DashCap = DashCap.Round };
                graphics.DrawPath(pen, _shapePath);
            }
            else
            {
                using Pen pen = new(GetRgbOrColor(BorderColor), BorderWidth) { LineJoin = LineJoin.Round, DashCap = DashCap.Round };
                graphics.DrawPath(pen, _shapePath);
            }
        }

        return bitmap;
    }

    /// <summary>
    /// Renders the content layer (background fill) into a bitmap with optional clipping.
    /// </summary>
    protected Bitmap RenderContentLayer(float roundingValue)
    {
        Bitmap bitmap = new(Width, Height);
        using Graphics graphics = HelpEngine.GetGraphics(bitmap, SmoothingMode, TextRenderingHint);

        // Clip region
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

        return bitmap;
    }

    /// <summary>
    /// Convenience: renders both layers to the form graphics.
    /// Subclasses can override to add extra content to the content layer.
    /// </summary>
    protected void DrawLayeredBackground(Graphics formGraphics, float roundingValue)
    {
        using Bitmap borderLayer = RenderBorderLayer(roundingValue);
        formGraphics.DrawImage(borderLayer, PointF.Empty);

        using Bitmap contentLayer = RenderContentLayer(roundingValue);
        formGraphics.DrawImage(contentLayer, PointF.Empty);
    }

    #endregion
}
