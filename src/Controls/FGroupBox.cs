using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using Timer = System.Windows.Forms.Timer;

namespace FC_UI.Controls;

[ToolboxBitmap(typeof(GroupBox))]
[Description("Displays a frame around a group of controls with an optional caption.")]
[Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(System.ComponentModel.Design.IDesigner))]
public partial class FGroupBox : UserControl
{
    #region VARIABLES
    private float h = 0;
    private Rectangle rectangle_region = new();
    private GraphicsPath graphicsPath = new();
    private readonly StringFormat stringFormat = new();
    private Size size_fgroupbox = new();
    private EventHandler? _rgbTickHandler;
    public enum Style
    {
        Default,
        Custom,
        Random
    }
    #endregion

    #region SETTINGS
    private bool tmp_background;
    [Category("FGroupBox")]
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
    private bool tmp_rgb_status;
    [Category("FGroupBox")]
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
    private bool tmp_rounding_status;
    [Category("FGroupBox")]
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
    [Category("FGroupBox")]
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
    [Category("FGroupBox")]
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
    private bool tmp_lineargradient_background;
    [Category("LinearGradient")]
    [Description("Enable/Disable background gradient")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool LinearGradient_Background
    {
        get => tmp_lineargradient_background;
        set
        {
            tmp_lineargradient_background = value;
            Refresh();
        }
    }
    //
    private Color tmp_color_1_for_gradient;
    [Category("LinearGradient")]
    [Description("Background gradient color #1")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color ColorBackground_1
    {
        get => tmp_color_1_for_gradient;
        set
        {
            tmp_color_1_for_gradient = value;
            Refresh();
        }
    }
    //
    private Color tmp_color_2_for_gradient;
    [Category("LinearGradient")]
    [Description("Background gradient color #2")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color ColorBackground_2
    {
        get => tmp_color_2_for_gradient;
        set
        {
            tmp_color_2_for_gradient = value;
            Refresh();
        }
    }
    //
    private SmoothingMode tmp_smoothing_mode;
    [Category("FGroupBox")]
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
    [Category("FGroupBox")]
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
    private Style tmp_fgroupbox_style = Style.Default;
    [Category("FGroupBox")]
    [Description("Control style")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Style FGroupBoxStyle
    {
        get => tmp_fgroupbox_style;
        set
        {
            tmp_fgroupbox_style = value;
            switch (tmp_fgroupbox_style)
            {
                case Style.Default:
                    Size = new(150, 130);
                    BackColor = Color.Transparent;
                    ForeColor = Color.FromArgb(245, 245, 245);

                    Background = true;
                    RGB = false;
                    Rounding = true;
                    RoundingInt = 60;
                    ColorBackground = Color.FromArgb(37, 52, 68);
                    Timer_RGB = 300;
                    Lighting = false;
                    ColorLighting = Color.FromArgb(29, 200, 238);
                    Alpha = 20;
                    PenWidth = 15;
                    LinearGradientPen = false;
                    ColorPen_1 = Color.FromArgb(37, 52, 68);
                    ColorPen_2 = Color.FromArgb(41, 63, 86);
                    BackgroundPen = true;
                    Background_WidthPen = 3F;
                    ColorBackground_Pen = Color.FromArgb(29, 200, 238);
                    LinearGradient_Background = false;
                    ColorBackground_1 = Color.FromArgb(37, 52, 68);
                    ColorBackground_2 = Color.FromArgb(41, 63, 86);
                    SmoothingMode = SmoothingMode.HighQuality;
                    TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
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
                    LinearGradientPen = random.Bool();
                    if (LinearGradientPen)
                    {
                        ColorPen_1 = random.ColorArgb();
                        ColorPen_2 = random.ColorArgb();
                    }
                    break;
            }
            Refresh();
        }
    }
    #endregion

    #region INITIALIZATION
    public FGroupBox()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.ResizeRedraw |
        ControlStyles.Selectable | ControlStyles.SupportsTransparentBackColor | ControlStyles.StandardDoubleClick, true);
        DoubleBuffered = true;

        Tag = "FC_UI";
        FGroupBoxStyle = Style.Default;
        FGroupBoxStyle = Style.Custom;

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
        }
        catch (Exception er) { HelpEngine.MSB_Error($"[{Name}] Error: \n{er}"); }
    }
    protected override void OnSizeChanged(EventArgs e)
    {
        int tmp = (int)((BackgroundPen ? Background_WidthPen : 0) + (Lighting ? (PenWidth) / 4 : 0));
        size_fgroupbox = new(Width - tmp * 2, Height - tmp * 2);
        rectangle_region = new(tmp, tmp, size_fgroupbox.Width, size_fgroupbox.Height);
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
        float roundingValue = 0.1F;
        void BaseLoading()
        {
            //Rounding
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

            return bitmap;
        }

        BaseLoading();
        using Bitmap layer1 = Layer_1();
        graphics_form.DrawImage(layer1, new PointF(0, 0));
        using Bitmap layer2 = Layer_2();
        graphics_form.DrawImage(layer2, new PointF(0, 0));
    }
    #endregion
}
