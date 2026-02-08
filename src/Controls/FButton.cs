using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using Timer = System.Windows.Forms.Timer;

namespace FC_UI.Controls;

[ToolboxBitmap(typeof(Button))]
[Description("Raises an event when clicked.")]
[DefaultEvent("Click")]
public partial class FButton : UserControl
{
    #region VARIABLES
    private float h = 0;
    private Rectangle rectangle_region = new();
    private GraphicsPath graphicsPath = new();
    private Point ClickLocation = new();
    private readonly StringFormat stringFormat = new();
    private int temp = 0;
    private bool Mouse_Enter = false;
    private Size size_fbutton = new();
    private EventHandler? _rgbTickHandler;
    private EventHandler? _effectTickHandler;
    public enum Style
    {
        Default,
        Custom,
        Random
    }
    #endregion

    #region SETTINGS
    private string tmp_text_button;
    [Category("FButton")]
    [Description("Control text")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public string TextButton
    {
        get => tmp_text_button;
        set
        {
            tmp_text_button = value;
            Refresh();
        }
    }
    //
    private bool tmp_rgb_status;
    [Category("FButton")]
    [DefaultValue(false)]
    [Description("Enable/Disable RGB mode")]
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
    private bool tmp_background;
    [Category("FButton")]
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
    private bool tmp_rounding_status;
    [Category("Rouding")]
    [Description("Enable/Disable button rounding")]
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
    [Category("Rouding")]
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
    [Category("Effects")]
    [Description("Click animation color")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color Effect_1_ColorBackground { get; set; }
    //
    private Color tmp_color_background;
    [Category("FButton")]
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
    [Category("Effects")]
    [Description("Enable/Disable circle effect on click")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool Effect_1 { get; set; }
    //
    private int tmp_effect1_transparency;
    [Category("Effects")]
    [Description("Effect_1 transparency")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int Effect_1_Transparency
    {
        get => tmp_effect1_transparency;
        set
        {
            if (value > 0 && value <= 255) tmp_effect1_transparency = value;
        }
    }
    //
    [Category("Effects")]
    [Description("Enable/Disable white overlay effect on button")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool Effect_2 { get; set; }
    //
    private int tmp_effect2_transparency;
    [Category("Effects")]
    [Description("Effect_2 transparency")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int Effect_2_Transparency
    {
        get => tmp_effect2_transparency;
        set
        {
            if (value > 0 && value <= 255) tmp_effect2_transparency = value;
        }
    }
    //
    [Category("Effects")]
    [Description("Effect color")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color Effect_2_ColorBackground { get; set; }
    //
    private readonly Timer timer_effect_1 = new();
    [Category("Timers")]
    [Description("Effect_1 speed (triggers repaint)")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int Timer_Effect_1
    {
        get => timer_effect_1.Interval;
        set => timer_effect_1.Interval = value;
    }
    //
    private readonly Timer timer_rgb = new();
    [Category("Timers")]
    [Description("RGB mode update speed (triggers repaint)")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int Timer_RGB
    {
        get => timer_rgb.Interval;
        set => timer_rgb.Interval = value;
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
    private float background_width_pen;
    [Category("BorderStyle")]
    [Description("Border width")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public float Background_WidthPen
    {
        get => background_width_pen;
        set
        {
            background_width_pen = value;
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
    [Description("Gradient color #1")]
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
    [Description("Gradient color #2")]
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
    [Category("FButton")]
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
    [Category("FButton")]
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
    private Style tmp_fbutton_style = Style.Default;
    [Category("FButton")]
    [Description("Control style")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Style FButtonStyle
    {
        get => tmp_fbutton_style;
        set
        {
            tmp_fbutton_style = value;
            switch (tmp_fbutton_style)
            {
                case Style.Default:
                    Size = new(130, 50);
                    BackColor = Color.Transparent;
                    ForeColor = Color.FromArgb(245, 245, 245);

                    TextButton = "FButton";
                    RGB = false;
                    Background = true;
                    Rounding = true;
                    RoundingInt = 70;
                    Effect_1_ColorBackground = Color.FromArgb(29, 200, 238);
                    ColorBackground = Color.FromArgb(37, 52, 68);
                    Effect_1 = true;
                    Effect_1_Transparency = 25;
                    Effect_2 = true;
                    Effect_2_Transparency = 20;
                    Effect_2_ColorBackground = Color.White;
                    Timer_Effect_1 = 5;
                    Timer_RGB = 300;
                    BackgroundPen = true;
                    Background_WidthPen = 4F;
                    ColorBackground_Pen = Color.FromArgb(29, 200, 238);
                    Lighting = false;
                    ColorLighting = Color.FromArgb(29, 200, 238);
                    Alpha = 20;
                    PenWidth = 15;
                    LinearGradient_Background = false;
                    ColorBackground_1 = Color.FromArgb(37, 52, 68);
                    ColorBackground_2 = Color.FromArgb(41, 63, 86);
                    LinearGradientPen = false;
                    ColorPen_1 = Color.FromArgb(37, 52, 68);
                    ColorPen_2 = Color.FromArgb(41, 63, 86);
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
    public FButton()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.ResizeRedraw |
        ControlStyles.Selectable | ControlStyles.SupportsTransparentBackColor | ControlStyles.StandardDoubleClick, true);
        DoubleBuffered = true;

        Tag = "FC_UI";
        FButtonStyle = Style.Default;
        FButtonStyle = Style.Custom;

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
            Draw_Text(e.Graphics);

            graphicsPath.ClearMarkers();
        }
        catch (Exception er) { HelpEngine.MSB_Error($"[{Name}] Error: \n{er}"); }
    }
    protected override void OnMouseEnter(EventArgs e)
    {
        Mouse_Enter = true;
        Refresh();
    }
    protected override void OnMouseLeave(EventArgs e)
    {
        timer_effect_1.Stop();
        Mouse_Enter = false;
        temp = 0;

        Refresh();
    }
    protected override void OnMouseUp(MouseEventArgs e)
    {
        timer_effect_1.Stop();
        // Remove old handler to prevent event handler leak
        if (_effectTickHandler is not null)
        {
            timer_effect_1.Tick -= _effectTickHandler;
            _effectTickHandler = null;
        }

        if (e.Button == MouseButtons.Left && Effect_1)
        {
            ClickLocation = e.Location;
            temp = 2;

            _effectTickHandler = (sender, args) =>
            {
                temp += 20;
                Refresh();
            };
            timer_effect_1.Tick += _effectTickHandler;
            timer_effect_1.Start();
        }
    }
    protected override void OnSizeChanged(EventArgs e)
    {
        int tmp = (int)((BackgroundPen ? Background_WidthPen : 0) + (Lighting ? PenWidth / 4 : 0));
        size_fbutton = new(Width - tmp * 2, Height - tmp * 2);
        rectangle_region = new(tmp, tmp, size_fbutton.Width, size_fbutton.Height);
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
            // Rounding
            if (Rounding && RoundingInt > 0)
            {
                roundingValue = Height / 100F * RoundingInt;
            }
            // RoundedRectangle
            graphicsPath?.Dispose();
            graphicsPath = DrawEngine.RoundedRectangle(rectangle_region, roundingValue);

            // Region
            using GraphicsPath regionPath = DrawEngine.RoundedRectangle(new(0, 0, Width, Height), roundingValue);
            Region?.Dispose();
            Region = new Region(regionPath);
        }
        Bitmap Layer_1()
        {
            Bitmap bitmap = new(Width, Height);
            using Graphics graphics = HelpEngine.GetGraphics(ref bitmap, SmoothingMode, TextRenderingHint);

            // Shadow
            if (Lighting)
            {
                using GraphicsPath shadowPath = DrawEngine.RoundedRectangle(rectangle_region, roundingValue);
                DrawEngine.DrawBlurred(graphics, ColorLighting, shadowPath, Alpha, PenWidth);
            }

            // Background border
            if (Background_WidthPen != 0 && BackgroundPen)
            {
                if (LinearGradientPen)
                {
                    using LinearGradientBrush gradientBrush = new(rectangle_region, ColorPen_1, ColorPen_2, 360);
                    using Pen pen = new(gradientBrush, Background_WidthPen)
                    {
                        LineJoin = LineJoin.Round,
                        DashCap = DashCap.Round
                    };
                    graphics.DrawPath(pen, graphicsPath);
                }
                else
                {
                    using Pen pen = new(RGB ? DrawEngine.HSV_To_RGB(h, 1f, 1f) : ColorBackground_Pen, Background_WidthPen)
                    {
                        LineJoin = LineJoin.Round,
                        DashCap = DashCap.Round
                    };
                    graphics.DrawPath(pen, graphicsPath);
                }
            }

            return bitmap;
        }
        Bitmap Layer_2()
        {
            Bitmap bitmap = new(Width, Height);
            using Graphics graphics = HelpEngine.GetGraphics(ref bitmap, SmoothingMode, TextRenderingHint);

            // Region_Clip
            int offset = 1;
            using GraphicsPath clipPath = DrawEngine.RoundedRectangle(new(
                rectangle_region.X - offset,
                rectangle_region.Y - offset,
                rectangle_region.Width + offset * 2,
                rectangle_region.Height + offset * 2), Rounding ? roundingValue : 0.1F);
            using Region clipRegion = new(clipPath);
            graphics.Clip = clipRegion;

            // Background
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

            // Effects
            if (Effect_1) Draw_Animation_Circles(graphics);
            if (Effect_2 && Mouse_Enter) Draw_Animation_WhiteBackground(graphics);

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
        using SolidBrush brush = new(ForeColor);
        graphics.DrawString(
            TextButton,
            Font,
            brush,
            rectangle_region,
            stringFormat);
    }
    private void Draw_Animation_Circles(Graphics graphics)
    {
        if (temp < ((size_fbutton.Width >= size_fbutton.Height) ? size_fbutton.Width * 2 : size_fbutton.Height * 2))
        {
            Rectangle rectangle_circles = new(ClickLocation.X - (temp / 2), ClickLocation.Y - (temp / 2), temp, temp);
            if (rectangle_circles.Width != 0 && rectangle_circles.Height != 0)
            {
                using SolidBrush brush = new(Color.FromArgb(Effect_1_Transparency, Effect_1_ColorBackground));
                graphics.FillEllipse(brush, rectangle_circles);
            }
        }
    }
    private void Draw_Animation_WhiteBackground(Graphics graphics)
    {
        using SolidBrush brush = new(Color.FromArgb(Effect_2_Transparency, Effect_2_ColorBackground));
        graphics.FillPath(brush, graphicsPath);
    }
    #endregion
}
