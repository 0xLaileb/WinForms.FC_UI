using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using Timer = System.Windows.Forms.Timer;

namespace FC_UI.Controls;

[ToolboxBitmap(typeof(VScrollBar))]
[Description("Provides horizontal/vertical content scrolling capability (use event subscription).")]
[DefaultEvent("ValueChanged")]
public partial class FScrollBar : UserControl
{
    #region VARIABLES
    private float h = 0;
    private Rectangle rectangle_region = new();
    private Rectangle rectangle_value = new();
    private GraphicsPath graphicsPath = new();
    private Size size_fscrollbar = new();
    private EventHandler? _rgbTickHandler;
    [Category("FC_UI")]
    [Description("Occurs on every Value property change.")]
    public event EventHandler ValueChanged;
    public enum Style
    {
        Default,
        Custom,
        Random
    }
    #endregion

    #region SETTINGS
    private int tmp_value_scroll;
    [Category("Value")]
    [Description("Value")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int Value
    {
        get => tmp_value_scroll;
        set
        {
            if (tmp_value_scroll == value)
                return;
            tmp_value_scroll = value;
            Refresh();
            OnScroll();
        }
    }
    //
    private Orientation tmp_orientation;
    [Category("Value")]
    [Description("System.Windows.Forms.Orientation")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Orientation OrientationValue
    {
        get => tmp_orientation;
        set
        {
            if (value == Orientation.Vertical)
            {
                Size = new(Size.Width, Size.Height);
                if (RoundingInt != 0) RoundingInt /= 10;
            }
            else
            {
                Size = new(Size.Height, Size.Width);
                if (RoundingInt != 0) RoundingInt *= 10;
            }
            tmp_orientation = value;
            Refresh();
        }
    }
    //
    [Category("Value")]
    [Description("Scroll step amount")]
    [DefaultValue(1)]
    public int SmallStep { get; set; }
    //
    private int tmp_thumbSize;
    [Category("Value")]
    [Description("Thumb size")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int ThumbSize
    {
        get => tmp_thumbSize;
        set
        {
            tmp_thumbSize = value;
            Refresh();
        }
    }
    //
    private int tmp_value_maximum;
    [Category("Value")]
    [Description("MAX value")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int Maximum
    {
        get => tmp_value_maximum;
        set
        {
            if (value > Minimum)
            {
                tmp_value_maximum = value;
                Value = 0;
                Refresh();
            }
        }
    }
    //
    private int tmp_value_minimum;
    [Category("Value")]
    [Description("MIN value")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int Minimum
    {
        get => tmp_value_minimum;
        set
        {
            if (value < Maximum)
            {
                tmp_value_minimum = value;
                Value = 0;
                Refresh();
            }
        }
    }
    //
    private bool tmp_rgb_status;
    [Category("FScrollBar")]
    [Description("Enable/Disable RGB")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool RGB
    {
        get => tmp_rgb_status;
        set
        {
            tmp_rgb_status = value;

            // Unsubscribe old handler to prevent event handler leak
            timer_rgb.Stop();
            if (_rgbTickHandler is not null)
            {
                timer_rgb.Tick -= _rgbTickHandler;
                _rgbTickHandler = null;
            }

            if (tmp_rgb_status)
            {
                if (!DrawEngine.timer_global_rgb.Enabled)
                {
                    _rgbTickHandler = (sender, args) =>
                    {
                        h += 4;
                        if (h >= 360) h = 0;
                        Refresh();
                    };
                    timer_rgb.Tick += _rgbTickHandler;
                    timer_rgb.Start();
                }
            }
            else
            {
                Refresh();
            }
        }
    }
    //
    private readonly Timer timer_rgb = new() { Interval = 300 };
    [Category("Timers")]
    [Description("RGB mode update speed (triggers repaint)")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int Timer_RGB
    {
        get => timer_rgb.Interval;
        set => timer_rgb.Interval = value;
    }
    //
    private Color tmp_color_fscrollbar;
    [Category("FScrollBar")]
    [Description("Fill color")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color ColorScrollBar
    {
        get => tmp_color_fscrollbar;
        set
        {
            tmp_color_fscrollbar = value;
            Refresh();
        }
    }
    //
    private int tmp_color_fscrollbar_transparency;
    [Category("Value")]
    [Description("Fill transparency")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int ColorScrollBar_Transparency
    {
        get => tmp_color_fscrollbar_transparency;
        set
        {
            if (value >= 10 && value <= 255)
            {
                tmp_color_fscrollbar_transparency = value;
                Refresh();
            }
        }
    }
    //
    private bool tmp_rounding_status;
    [Category("FScrollBar")]
    [Description("Enable/Disable rounding")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool Rounding
    {
        get => tmp_rounding_status;
        set
        {
            tmp_rounding_status = value;
            Refresh();
        }
    }
    //
    private int tmp_rounding_int;
    [Category("FScrollBar")]
    [Description("Rounding percentage")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int RoundingInt
    {
        get => tmp_rounding_int;
        set
        {
            if (value >= 0 && value <= 100)
            {
                tmp_rounding_int = value;
                Refresh();
            }
        }
    }
    //
    private Color tmp_color_background;
    [Category("FScrollBar")]
    [Description("Background color")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color ColorBackground
    {
        get => tmp_color_background;
        set
        {
            tmp_color_background = value;
            Refresh();
        }
    }
    //
    private bool tmp_background;
    [Category("FScrollBar")]
    [Description("Enable/Disable background")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool Background
    {
        get => tmp_background;
        set
        {
            tmp_background = value;
            Refresh();
        }
    }
    //
    private bool tmp_background_pen;
    [Category("BorderStyle")]
    [Description("Enable/Disable border")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool BackgroundPen
    {
        get => tmp_background_pen;
        set
        {
            tmp_background_pen = value;
            OnSizeChanged(null);
            Refresh();
        }
    }
    //
    private float tmp_background_width_pen;
    [Category("BorderStyle")]
    [Description("Border width")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public float Background_WidthPen
    {
        get => tmp_background_width_pen;
        set
        {
            tmp_background_width_pen = value;
            OnSizeChanged(null);
            Refresh();
        }
    }
    //
    private Color tmp_color_background_pen;
    [Category("BorderStyle")]
    [Description("Border color")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color ColorBackground_Pen
    {
        get => tmp_color_background_pen;
        set
        {
            tmp_color_background_pen = value;
            Refresh();
        }
    }
    //
    private bool tmp_lighting;
    [Category("Lighting")]
    [Description("Enable/Disable lighting")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool Lighting
    {
        get => tmp_lighting;
        set
        {
            tmp_lighting = value;
            OnSizeChanged(null);
            Refresh();
        }
    }
    //
    private Color tmp_color_lighting;
    [Category("Lighting")]
    [Description("Lighting / shadow color")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color ColorLighting
    {
        get => tmp_color_lighting;
        set
        {
            tmp_color_lighting = value;
            Refresh();
        }
    }
    //
    private int tmp_alpha;
    [Category("Lighting")]
    [Description("")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int Alpha
    {
        get => tmp_alpha;
        set
        {
            tmp_alpha = value;
            Refresh();
        }
    }
    //
    private int tmp_pen_width;
    [Category("Lighting")]
    [Description("")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int PenWidth
    {
        get => tmp_pen_width;
        set
        {
            tmp_pen_width = value;
            OnSizeChanged(null);
            Refresh();
        }
    }
    //
    private bool tmp_lineargradient_background_status;
    [Category("LinearGradient")]
    [Description("Enable/Disable background gradient")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool LinearGradient_Background
    {
        get => tmp_lineargradient_background_status;
        set
        {
            tmp_lineargradient_background_status = value;
            Refresh();
        }
    }
    //
    private Color tmp_color_1_for_gradient_background;
    [Category("LinearGradient")]
    [Description("Background gradient color #1")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color ColorBackground_1
    {
        get => tmp_color_1_for_gradient_background;
        set
        {
            tmp_color_1_for_gradient_background = value;
            Refresh();
        }
    }
    //
    private Color tmp_color_2_for_gradient_background;
    [Category("LinearGradient")]
    [Description("Background gradient color #2")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color ColorBackground_2
    {
        get => tmp_color_2_for_gradient_background;
        set
        {
            tmp_color_2_for_gradient_background = value;
            Refresh();
        }
    }
    //
    private bool tmp_lineargradient_value_status;
    [Category("LinearGradient")]
    [Description("Enable/Disable thumb gradient")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool LinearGradient_Value
    {
        get => tmp_lineargradient_value_status;
        set
        {
            tmp_lineargradient_value_status = value;
            Refresh();
        }
    }
    //
    private Color tmp_color_1_for_gradient_value;
    [Category("LinearGradient")]
    [Description("Thumb gradient color #1")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color ColorBackground_Value_1
    {
        get => tmp_color_1_for_gradient_value;
        set
        {
            tmp_color_1_for_gradient_value = value;
            Refresh();
        }
    }
    //
    private Color tmp_color_2_for_gradient_value;
    [Category("LinearGradient")]
    [Description("Thumb gradient color #2")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color ColorBackground_Value_2
    {
        get => tmp_color_2_for_gradient_value;
        set
        {
            tmp_color_2_for_gradient_value = value;
            Refresh();
        }
    }
    //
    private bool tmp_lineargradient_pen_status;
    [Category("LinearGradient")]
    [Description("Enable/Disable border gradient")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool LinearGradientPen
    {
        get => tmp_lineargradient_pen_status;
        set
        {
            tmp_lineargradient_pen_status = value;
            Refresh();
        }
    }
    //
    private Color tmp_color_1_for_gradient_pen;
    [Category("LinearGradient")]
    [Description("Border gradient color #1")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color ColorPen_1
    {
        get => tmp_color_1_for_gradient_pen;
        set
        {
            tmp_color_1_for_gradient_pen = value;
            Refresh();
        }
    }
    //
    private Color tmp_color_2_for_gradient_pen;
    [Category("LinearGradient")]
    [Description("Border gradient color #2")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color ColorPen_2
    {
        get => tmp_color_2_for_gradient_pen;
        set
        {
            tmp_color_2_for_gradient_pen = value;
            Refresh();
        }
    }
    //
    private SmoothingMode tmp_smoothing_mode;
    [Category("FScrollBar")]
    [Description("Graphics smoothing mode")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public SmoothingMode SmoothingMode
    {
        get => tmp_smoothing_mode;
        set
        {
            if (value != SmoothingMode.Invalid) tmp_smoothing_mode = value;
            Refresh();
        }
    }
    //
    private TextRenderingHint tmp_text_rendering_hint;
    [Category("FScrollBar")]
    [Description("Graphics text rendering hint")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public TextRenderingHint TextRenderingHint
    {
        get => tmp_text_rendering_hint;
        set
        {
            tmp_text_rendering_hint = value;
            Refresh();
        }
    }
    //
    private Style tmp_fscrollbar_style = Style.Default;
    [Category("FScrollBar")]
    [Description("Control style")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Style FScrollBarStyle
    {
        get => tmp_fscrollbar_style;
        set
        {
            tmp_fscrollbar_style = value;
            switch (tmp_fscrollbar_style)
            {
                case Style.Default:
                    Size = new(26, 300);
                    BackColor = Color.Transparent;
                    ForeColor = Color.FromArgb(245, 245, 245);

                    Value = 0;
                    Minimum = 0;
                    Maximum = 100;
                    OrientationValue = Orientation.Vertical;
                    ThumbSize = 60;
                    SmallStep = 1;
                    RGB = false;
                    Background = true;
                    BackgroundPen = true;
                    Background_WidthPen = 3F;
                    Rounding = true;
                    RoundingInt = 7;
                    Timer_RGB = 300;
                    ColorScrollBar = Color.FromArgb(29, 200, 238);
                    ColorScrollBar_Transparency = 255;
                    ColorBackground = Color.FromArgb(37, 52, 68);
                    ColorBackground_Pen = Color.FromArgb(29, 200, 238);
                    Lighting = false;
                    ColorLighting = Color.FromArgb(29, 200, 238);
                    Alpha = 50;
                    PenWidth = 10;
                    LinearGradient_Background = false;
                    ColorBackground_1 = Color.FromArgb(37, 52, 68);
                    ColorBackground_2 = Color.FromArgb(41, 63, 86);
                    LinearGradientPen = false;
                    ColorPen_1 = Color.FromArgb(37, 52, 68);
                    ColorPen_2 = Color.FromArgb(41, 63, 86);
                    LinearGradient_Value = false;
                    ColorBackground_Value_1 = Color.FromArgb(28, 200, 238);
                    ColorBackground_Value_2 = Color.FromArgb(100, 208, 232);
                    SmoothingMode = SmoothingMode.HighQuality;
                    TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                    break;
                case Style.Custom:
                    break;
                case Style.Random:
                    HelpEngine.GetRandom random = new();
                    Background = random.Bool();
                    Rounding = random.Bool();
                    if (Rounding) RoundingInt = random.Int(2, 10);
                    if (Background) ColorBackground = random.ColorArgb(random.Int(0, 255));
                    BackgroundPen = random.Bool();
                    if (BackgroundPen)
                    {
                        Background_WidthPen = random.Float(1, 3);
                        ColorBackground_Pen = random.ColorArgb(random.Int(0, 255));
                    }
                    Lighting = random.Bool();
                    if (Lighting) ColorLighting = random.ColorArgb();
                    LinearGradient_Background = random.Bool();
                    if (LinearGradient_Background)
                    {
                        ColorBackground_1 = random.ColorArgb();
                        ColorBackground_2 = random.ColorArgb();
                    }
                    LinearGradient_Value = random.Bool();
                    if (LinearGradient_Value)
                    {
                        ColorBackground_Value_1 = random.ColorArgb();
                        ColorBackground_Value_2 = random.ColorArgb();
                    }
                    LinearGradientPen = random.Bool();
                    if (LinearGradientPen)
                    {
                        ColorPen_1 = random.ColorArgb();
                        ColorPen_2 = random.ColorArgb();
                    }
                    ColorScrollBar = random.ColorArgb();
                    ColorScrollBar_Transparency = random.Int(0, 255);
                    break;
            }
            Refresh();
        }
    }
    #endregion

    #region INITIALIZATION
    public FScrollBar()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
        DoubleBuffered = true;

        Tag = "FC_UI";
        FScrollBarStyle = Style.Default;
        FScrollBarStyle = Style.Custom;

        OnSizeChanged(null);
    }
    #endregion

    #region EVENTS
    protected override void OnPaint(PaintEventArgs e)
    {
        try
        {
            Settings_Load(e.Graphics);
            Draw_Background(e.Graphics);
        }
        catch (Exception er) { HelpEngine.MSB_Error($"[{Name}] Error: \n{er}"); }
    }
    public virtual void OnScroll(ScrollEventType type = ScrollEventType.ThumbPosition)
    {
        ValueChanged?.Invoke(this, EventArgs.Empty);
    }
    protected override void OnMouseDown(MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left) MouseScroll(e);
    }
    protected override void OnMouseMove(MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left) MouseScroll(e);
    }
    protected override void OnSizeChanged(EventArgs e)
    {
        int tmp = (int)((BackgroundPen ? Background_WidthPen : 0) + (Lighting ? (PenWidth) / 4 : 0));
        size_fscrollbar = new(Width - tmp * 2, Height - tmp * 2);
        rectangle_region = new(tmp, tmp, size_fscrollbar.Width, size_fscrollbar.Height);
    }
    private void MouseScroll(MouseEventArgs e)
    {
        int value = Value;

        switch (OrientationValue)
        {
            case Orientation.Vertical:
                if (e.Y < 0) value -= SmallStep;
                else if (e.Y > rectangle_region.Height) value += SmallStep;
                else value = Maximum * (e.Y - ThumbSize / 2) / (rectangle_region.Height - ThumbSize);
                rectangle_value = new(
                rectangle_region.X,
                rectangle_region.Y + (Value * (rectangle_region.Height - ThumbSize) / Maximum),
                rectangle_region.Width,
                ThumbSize);
                break;
            case Orientation.Horizontal:
                if (e.X < 0) value -= SmallStep;
                else if (e.X > rectangle_region.Width) value += SmallStep;
                else value = Maximum * (e.X - ThumbSize / 2) / (rectangle_region.Width - ThumbSize);
                rectangle_value = new(
                rectangle_region.X + (Value * (rectangle_region.Width - ThumbSize) / Maximum),
                rectangle_region.Y,
                ThumbSize,
                rectangle_region.Height);
                break;
        }
        Value = Math.Max(0, Math.Min(Maximum, value));
    }

    #endregion

    #region DRAWING METHODS
    private void Settings_Load(Graphics graphics)
    {
        BackColor = Color.Transparent;

        graphics.SmoothingMode = SmoothingMode;
        graphics.TextRenderingHint = TextRenderingHint;
    }
    private void Draw_Background(Graphics graphics_form)
    {
        float roundingValue;
        void BaseLoading()
        {
            //Rounding
            roundingValue = 0.1F;
            if (Rounding && RoundingInt > 0)
            {
                roundingValue = Height / 100F * RoundingInt;
            }
            //RoundedRectangle
            graphicsPath = DrawEngine.RoundedRectangle(rectangle_region, roundingValue);

            //Region
            using GraphicsPath regionPath = DrawEngine.RoundedRectangle(new Rectangle(
            0, 0,
            Width, Height),
            roundingValue);
            Region = new Region(regionPath);
        }
        Bitmap Layer_1()
        {
            Bitmap bitmap = new(Width, Height);
            using Graphics graphics = HelpEngine.GetGraphics(ref bitmap, SmoothingMode, TextRenderingHint);

            //Shadow
            if (Lighting)
            {
                using GraphicsPath shadowPath = DrawEngine.RoundedRectangle(rectangle_region, roundingValue);
                DrawEngine.DrawBlurred(graphics, ColorLighting, shadowPath, Alpha, PenWidth);
            }

            //Background border
            if (Background_WidthPen != 0 && BackgroundPen)
            {
                if (LinearGradientPen)
                {
                    using LinearGradientBrush penBrush = new(rectangle_region, ColorPen_1, ColorPen_2, 360);
                    using Pen pen = new(penBrush, Background_WidthPen);
                    pen.LineJoin = LineJoin.Round;
                    pen.DashCap = DashCap.Round;
                    graphics.DrawPath(pen, graphicsPath);
                }
                else
                {
                    using Pen pen = new(RGB ? DrawEngine.HSV_To_RGB(h, 1f, 1f) : ColorBackground_Pen, Background_WidthPen);
                    pen.LineJoin = LineJoin.Round;
                    pen.DashCap = DashCap.Round;
                    graphics.DrawPath(pen, graphicsPath);
                }
            }

            return bitmap;
        }
        Bitmap Layer_2()
        {
            Bitmap bitmap = new(Width, Height);
            using Graphics graphics = HelpEngine.GetGraphics(ref bitmap, SmoothingMode, TextRenderingHint);

            //Region_Clip
            using GraphicsPath clipPath = DrawEngine.RoundedRectangle(new Rectangle(
                rectangle_region.X - (int)(2 + Background_WidthPen),
                rectangle_region.Y - (int)(2 + Background_WidthPen),
                rectangle_region.Width + (int)(2 + Background_WidthPen) * 2,
                rectangle_region.Height + (int)(2 + Background_WidthPen) * 2), Rounding ? roundingValue : 0.1F);
            graphics.Clip = new Region(clipPath);

            //Background
            if (Background)
            {
                if (LinearGradient_Background)
                {
                    using LinearGradientBrush brush = new(rectangle_region, ColorBackground_1, ColorBackground_2, 360);
                    graphics.FillPath(brush, graphicsPath);
                }
                else
                {
                    using SolidBrush brush = new(ColorBackground);
                    graphics.FillPath(brush, graphicsPath);
                }
            }

            //Additional
            Draw_Bar(graphics, roundingValue);

            return bitmap;
        }

        BaseLoading();
        using Bitmap layer1 = Layer_1();
        graphics_form.DrawImage(layer1, new PointF(0, 0));
        using Bitmap layer2 = Layer_2();
        graphics_form.DrawImage(layer2, new PointF(0, 0));
    }
    private void Draw_Bar(Graphics graphics, float roundingValue)
    {
        if (Maximum <= 0) return;
        rectangle_value = new(2, 2, rectangle_region.Width, ThumbSize);

        switch (OrientationValue)
        {
            case Orientation.Vertical:
                rectangle_value = new(
                rectangle_region.X,
                rectangle_region.Y + (Value * (rectangle_region.Height - ThumbSize) / Maximum),
                rectangle_region.Width,
                ThumbSize);
                break;
            case Orientation.Horizontal:
                rectangle_value = new(
                rectangle_region.X + (Value * (rectangle_region.Width - ThumbSize) / Maximum),
                rectangle_region.Y,
                ThumbSize,
                rectangle_region.Height);
                break;
        }

        int tmp = 1;
        rectangle_value.X -= tmp;
        rectangle_value.Y -= tmp;
        rectangle_value.Width += tmp * 2;
        rectangle_value.Height += tmp * 2;
        roundingValue += tmp * 2;

        using GraphicsPath barPath = DrawEngine.RoundedRectangle(rectangle_value, roundingValue);
        if (LinearGradient_Value)
        {
            using LinearGradientBrush brush = new(rectangle_region,
                Color.FromArgb(ColorScrollBar_Transparency, RGB ? DrawEngine.HSV_To_RGB(h, 1f, 1f) : ColorBackground_Value_1),
                Color.FromArgb(ColorScrollBar_Transparency, RGB ? DrawEngine.HSV_To_RGB(h + 20, 1f, 1f) : ColorBackground_Value_2), 360);
            graphics.FillPath(brush, barPath);
        }
        else
        {
            using SolidBrush brush = new(
                Color.FromArgb(ColorScrollBar_Transparency, RGB ? DrawEngine.HSV_To_RGB(h, 1f, 1f) : ColorScrollBar));
            graphics.FillPath(brush, barPath);
        }
    }
    #endregion
}
