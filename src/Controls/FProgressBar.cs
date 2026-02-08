using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using Timer = System.Windows.Forms.Timer;

namespace FC_UI.Controls;

[ToolboxBitmap(typeof(ProgressBar))]
[Description("Displays an operation progress indicator.")]
public partial class FProgressBar : UserControl
{
    #region VARIABLES
    private float h = 0;
    private Rectangle rectangle_region = new();
    private Rectangle rectangle_value = new();
    private GraphicsPath graphicsPath = new();
    private Size size_fprogressbar = new();
    private readonly StringFormat stringFormat = new();
    private int value_progressbar_for_draw = 0;
    private EventHandler? _rgbTickHandler;
    public enum Style
    {
        Default,
        Custom,
        Random
    }
    #endregion

    #region SETTINGS
    private int tmp_value_progressbar;
    [Category("Value")]
    [Description("Value [%]")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int Value
    {
        get => tmp_value_progressbar;
        set
        {
            if (value <= Maximum && value >= Minimum)
            {
                tmp_value_progressbar = value;
                Refresh();
            }
        }
    }
    //
    private int tmp_min_progressbar;
    [Category("Value")]
    [Description("MIN value")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int Minimum
    {
        get => tmp_min_progressbar;
        set
        {
            if (value < Maximum)
            {
                tmp_min_progressbar = value;
                Refresh();
            }
        }
    }
    //
    private int tmp_max_progressbar;
    [Category("Value")]
    [Description("MAX value")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int Maximum
    {
        get => tmp_max_progressbar;
        set
        {
            if (value > Minimum)
            {
                tmp_max_progressbar = value;
                Refresh();
            }
        }
    }
    //
    private int tmp_start_drawing_value;
    [Category("Value")]
    [Description(
    "Value at which drawing starts (if (Value >= value) Drawing).\n" +
    "(use when Rounding == true to fix artifacts at small values)"
    )]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int StartDrawingValue
    {
        get => tmp_start_drawing_value;
        set
        {
            tmp_start_drawing_value = value;
            Refresh();
        }
    }
    //
    private bool progress_text;
    [Category("FProgressBar")]
    [Description("Enable/Disable text")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool ProgressText
    {
        get => progress_text;
        set
        {
            progress_text = value;
            Refresh();
        }
    }
    //
    private bool tmp_rgb_status;
    [Category("FProgressBar")]
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
    private Color tmp_color_progressbar;
    [Category("FProgressBar")]
    [Description("ProgressBar fill color")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color ColorProgressBar
    {
        get => tmp_color_progressbar;
        set
        {
            tmp_color_progressbar = value;
            Refresh();
        }
    }
    //
    private int tmp_color_progressbar_transparency;
    [Category("Value")]
    [Description("Fill transparency")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int ColorValue_Transparency
    {
        get => tmp_color_progressbar_transparency;
        set
        {
            if (value >= 5 && value <= 255)
            {
                tmp_color_progressbar_transparency = value;
                Refresh();
            }
        }
    }
    //
    private bool tmp_rounding_status;
    [Category("FProgressBar")]
    [Description("Enable/Disable progressbar rounding")]
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
    [Category("FProgressBar")]
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
    [Category("FProgressBar")]
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
    [Category("FProgressBar")]
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
    private Color tmp_color_Background_pen;
    [Category("BorderStyle")]
    [Description("Border color")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color ColorBackground_Pen
    {
        get => tmp_color_Background_pen;
        set
        {
            tmp_color_Background_pen = value;
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
    private bool tmp_lineargradient_Background_status;
    [Category("LinearGradient")]
    [Description("Enable/Disable background gradient")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool LinearGradient_Background
    {
        get => tmp_lineargradient_Background_status;
        set
        {
            tmp_lineargradient_Background_status = value;
            Refresh();
        }
    }
    //
    private Color tmp_color_1_for_gradient_Background;
    [Category("LinearGradient")]
    [Description("Background gradient color #1")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color ColorBackground_1
    {
        get => tmp_color_1_for_gradient_Background;
        set
        {
            tmp_color_1_for_gradient_Background = value;
            Refresh();
        }
    }
    //
    private Color tmp_color_2_for_gradient_Background;
    [Category("LinearGradient")]
    [Description("Background gradient color #2")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color ColorBackground_2
    {
        get => tmp_color_2_for_gradient_Background;
        set
        {
            tmp_color_2_for_gradient_Background = value;
            Refresh();
        }
    }
    //
    private bool tmp_lineargradient_value_status;
    [Category("LinearGradient")]
    [Description("Enable/Disable fill gradient")]
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
    [Description("Fill gradient color #1")]
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
    [Description("Fill gradient color #2")]
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
    [Category("FProgressBar")]
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
    [Category("FProgressBar")]
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
    private Style tmp_fprogressbar_style = Style.Default;
    [Category("FProgressBar")]
    [Description("Control style")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Style FProgressBarStyle
    {
        get => tmp_fprogressbar_style;
        set
        {
            tmp_fprogressbar_style = value;
            switch (tmp_fprogressbar_style)
            {
                case Style.Default:
                    Size = new(300, 34);
                    BackColor = Color.Transparent;
                    ForeColor = Color.FromArgb(245, 245, 245);

                    Value = 0;
                    Minimum = 0;
                    Maximum = 100;
                    StartDrawingValue = 0; //6
                    ProgressText = true;
                    RGB = false;
                    Background = true;
                    Rounding = true;
                    RoundingInt = 70;
                    ColorBackground = Color.FromArgb(37, 52, 68);
                    Timer_RGB = 300;
                    BackgroundPen = true;
                    Background_WidthPen = 3F;
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
                    ColorValue_Transparency = 200;
                    ColorProgressBar = Color.FromArgb(29, 200, 238);
                    SmoothingMode = SmoothingMode.HighQuality;
                    TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                    Font = HelpEngine.GetDefaultFont();
                    break;
                case Style.Custom:
                    break;
                case Style.Random:
                    HelpEngine.GetRandom random = new();
                    Background = random.Bool();
                    Rounding = random.Bool();
                    if (Rounding) RoundingInt = random.Int(5, 90);
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
                    ProgressText = random.Bool();
                    ColorProgressBar = random.ColorArgb();
                    ColorValue_Transparency = random.Int(0, 255);
                    break;
            }
            Refresh();
        }
    }
    #endregion

    #region INITIALIZATION
    public FProgressBar()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.ResizeRedraw |
        ControlStyles.Selectable | ControlStyles.SupportsTransparentBackColor | ControlStyles.StandardDoubleClick, true);
        DoubleBuffered = true;

        Tag = "FC_UI";
        FProgressBarStyle = Style.Default;
        FProgressBarStyle = Style.Custom;

        stringFormat.Alignment = StringAlignment.Center;
        stringFormat.LineAlignment = StringAlignment.Center;

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
            if (progress_text) Draw_Text(e.Graphics);
        }
        catch (Exception er) { HelpEngine.MSB_Error($"[{Name}] Error: \n{er}"); }
    }
    protected override void OnSizeChanged(EventArgs e)
    {
        int tmp = (int)((BackgroundPen ? Background_WidthPen : 0) + (Lighting ? (PenWidth) / 4 : 0));
        size_fprogressbar = new(Width - tmp * 2, Height - tmp * 2);
        rectangle_region = new(tmp, tmp, size_fprogressbar.Width, size_fprogressbar.Height);
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
            if (Value >= StartDrawingValue) Draw_Value(graphics, roundingValue);

            return bitmap;
        }

        BaseLoading();
        using Bitmap layer1 = Layer_1();
        graphics_form.DrawImage(layer1, new PointF(0, 0));
        using Bitmap layer2 = Layer_2();
        graphics_form.DrawImage(layer2, new PointF(0, 0));
    }
    private void Draw_Text(Graphics graphics)
    {
        using SolidBrush textBrush = new(ForeColor);
        graphics.DrawString(
            ((Value / (Maximum / 100))).ToString() + "%",
            Font,
            textBrush,
            new Rectangle(rectangle_region.X, rectangle_region.Y, rectangle_region.Width, rectangle_region.Height),
            stringFormat);
    }
    private void Draw_Value(Graphics graphics, float roundingValue)
    {
        if (Value != 0)
        {
            value_progressbar_for_draw = Convert.ToInt32(((graphicsPath.GetBounds().Width / 100) * (Value / (Maximum / 100))));
            rectangle_value = new(
                rectangle_region.X,
                rectangle_region.Y,
                value_progressbar_for_draw,
                rectangle_region.Height);

            int tmp = 1;
            rectangle_value.X -= tmp;
            rectangle_value.Y -= tmp;
            rectangle_value.Width += tmp * 2;
            rectangle_value.Height += tmp * 2;
            roundingValue += tmp * 2;

            using GraphicsPath graphicsPath_value = DrawEngine.RoundedRectangle(rectangle_value, roundingValue);
            if (LinearGradient_Value)
            {
                using LinearGradientBrush brush = new(rectangle_value,
                    Color.FromArgb(ColorValue_Transparency, RGB ? DrawEngine.HSV_To_RGB(h, 1f, 1f) : ColorBackground_Value_1),
                    Color.FromArgb(ColorValue_Transparency, RGB ? DrawEngine.HSV_To_RGB(h + 20, 1f, 1f) : ColorBackground_Value_2), 360);
                graphics.FillPath(brush, graphicsPath_value);
            }
            else
            {
                using SolidBrush brush = new(
                    Color.FromArgb(ColorValue_Transparency, RGB ? DrawEngine.HSV_To_RGB(h, 1f, 1f) : ColorProgressBar));
                graphics.FillPath(brush, graphicsPath_value);
            }
        }
    }
    #endregion
}
