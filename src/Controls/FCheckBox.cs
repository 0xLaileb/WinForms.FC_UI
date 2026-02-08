using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using Timer = System.Windows.Forms.Timer;

namespace FC_UI.Controls;

[ToolboxBitmap(typeof(CheckBox))]
[Description("Allows the user to select or deselect the corresponding option.")]
public partial class FCheckBox : UserControl
{
    #region VARIABLES
    private float h = 0;
    private Rectangle rectangle_region = new();
    private GraphicsPath graphicsPath = new();
    private int temp = 0;
    private bool Mouse_Enter = false;
    private Size size_fcheckbox = new();
    private System.EventHandler? _rgbTickHandler;
    private System.EventHandler? _effectTickHandler;
    public enum Style
    {
        Default,
        Custom,
        Random
    }
    #endregion

    #region SETTINGS
    public delegate void EventHandler();
    [Category("FC_UI")]
    [Description("Occurs on every Checked property change.")]
    public event EventHandler CheckedChanged = delegate { };

    private bool tmp_checked_status;
    [Category("FCheckBox")]
    [Description("Enable/Disable checked status")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool Checked
    {
        get => tmp_checked_status;
        set
        {
            tmp_checked_status = value;
            CheckedChanged();
            Refresh();
        }
    }
    //
    private string tmp_text_button;
    [Category("FCheckBox")]
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
    [Category("FCheckBox")]
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
    private bool tmp_background;
    [Category("FCheckBox")]
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
    [Category("FCheckBox")]
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
    [Category("FCheckBox")]
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
    private Color tmp_color_click_circle;
    [Category("Effects")]
    [Description("Click animation color")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color Effect_1_ColorBackground
    {
        get => tmp_color_click_circle;
        set
        {
            tmp_color_click_circle = value;
            Refresh();
        }
    }
    //
    private Color tmp_color_background;
    [Category("FCheckBox")]
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
    private Color color_checked;
    [Category("FCheckBox")]
    [Description("Checkmark color")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color ColorChecked
    {
        get => color_checked;
        set
        {
            color_checked = value;
            Refresh();
        }
    }
    //
    [Category("Effects")]
    [DefaultValue(true)]
    [Description("Enable/Disable circle effect on hover/activation")]
    public bool Effect_1 { get; set; }
    //
    private int effect1_transparency;
    [Category("Effects")]
    [Description("Effect_1 transparency")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int Effect_1_Transparency
    {
        get => effect1_transparency;
        set
        {
            if (value > 0 && value <= 255) effect1_transparency = value;
        }
    }
    //
    [Category("Effects")]
    [Description("Enable/Disable white overlay effect")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool Effect_2 { get; set; }
    //
    private int effect2_transparency;
    [Category("Effects")]
    [Description("Effect_2 transparency")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int Effect_2_Transparency
    {
        get => effect2_transparency;
        set
        {
            if (value > 0 && value <= 255) effect2_transparency = value;
        }
    }
    //
    [Category("Effects")]
    [Description("Effect color")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color Effect_2_ColorBackground { get; set; }
    //
    private readonly Timer timer_effect_1 = new() { Interval = 1 };
    [Category("Timers")]
    [Description("Effect_1 speed (triggers repaint)")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int Timer_Effect_1
    {
        get => timer_effect_1.Interval;
        set => timer_effect_1.Interval = value;
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
    [Category("FCheckBox")]
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
    [Category("FCheckBox")]
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
    private Style tmp_fcheckbox_style = Style.Default;
    [Category("FCheckBox")]
    [Description("Control style")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Style FCheckBoxStyle
    {
        get => tmp_fcheckbox_style;
        set
        {
            tmp_fcheckbox_style = value;
            switch (tmp_fcheckbox_style)
            {
                case Style.Default:
                    Size = new(140, 45);
                    BackColor = Color.Transparent;
                    ForeColor = Color.FromArgb(245, 245, 245);

                    Checked = false;
                    TextButton = "FCheckBox";
                    RGB = false;
                    Background = true;
                    Rounding = true;
                    RoundingInt = 100;
                    Effect_1_ColorBackground = Color.FromArgb(29, 200, 238);
                    ColorBackground = Color.FromArgb(37, 52, 68);
                    BackgroundPen = true;
                    Background_WidthPen = 2F;
                    ColorBackground_Pen = Color.FromArgb(29, 200, 238);
                    ColorChecked = Color.FromArgb(29, 200, 238);
                    Effect_1 = true;
                    Effect_1_Transparency = 25;
                    Effect_2 = true;
                    Effect_2_Transparency = 15;
                    Effect_2_ColorBackground = Color.White;
                    Timer_Effect_1 = 1;
                    Timer_RGB = 300;
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
                        ColorChecked = random.ColorArgb(random.Int(0, 255));
                    }
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
    public FCheckBox()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.ResizeRedraw |
        ControlStyles.Selectable | ControlStyles.SupportsTransparentBackColor | ControlStyles.StandardDoubleClick, true);
        DoubleBuffered = true;

        Tag = "FC_UI";
        FCheckBoxStyle = Style.Default;
        FCheckBoxStyle = Style.Custom;

        OnSizeChanged(null);
    }
    protected override CreateParams CreateParams //WS_CLIPCHILDREN
    {
        get
        {
            CreateParams createParams = base.CreateParams;
            createParams.ExStyle |= 0x02000000;
            return createParams;
        }
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
        }
        catch (Exception er) { HelpEngine.MSB_Error($"[{Name}] Error: \n{er}"); }
    }
    protected override void OnMouseClick(MouseEventArgs e)
    {
        Checked = !Checked;

        timer_effect_1.Stop();
        // Remove old handler to prevent event handler leak
        if (_effectTickHandler is not null)
        {
            timer_effect_1.Tick -= _effectTickHandler;
            _effectTickHandler = null;
        }

        if (e.Button == MouseButtons.Left)
        {
            temp = size_fcheckbox.Width;

            if (Checked)
            {
                _effectTickHandler = (sender, args) =>
                {
                    temp += 1;
                    Refresh();
                };
                timer_effect_1.Tick += _effectTickHandler;
                timer_effect_1.Start();
            }
            else Refresh();
        }
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
    protected override void OnSizeChanged(EventArgs e)
    {
        Size = new(Size.Width, 45);
        size_fcheckbox = new(21, 21);
        rectangle_region = new(15, Size.Height / 2 - 12, size_fcheckbox.Width, size_fcheckbox.Height);
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
                roundingValue = size_fcheckbox.Height / 100F * RoundingInt;
            }
            // RoundedRectangle
            graphicsPath = DrawEngine.RoundedRectangle(rectangle_region, roundingValue);

            // Region
            using GraphicsPath regionPath = DrawEngine.RoundedRectangle(new Rectangle(
                0, 0,
                Width, Height),
                roundingValue);
            Region?.Dispose();
            Region = new Region(regionPath);
        }
        Bitmap Layer_1()
        {
            Bitmap bitmap = new(Width, Height);
            using Graphics graphics = HelpEngine.GetGraphics(ref bitmap, SmoothingMode, TextRenderingHint);

            // Background border
            if (Background_WidthPen != 0 && BackgroundPen == true)
            {
                if (LinearGradientPen)
                {
                    using LinearGradientBrush lgBrush = new(rectangle_region, ColorPen_1, ColorPen_2, 360);
                    using Pen pen = new(lgBrush, Background_WidthPen);
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

            // Effects
            if (Effect_1) Draw_Animation_Circles(graphics);
            if (Effect_2 && Mouse_Enter) Draw_Animation_WhiteBackground_CirclesStyle(graphics);

            // Background
            if (Background == true)
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

            // Additional
            if (Checked) Draw_Checked(graphics);

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
            new Rectangle((int)(25 + graphicsPath.GetBounds().Width), (Size.Height / 2) - (Font.Height / 2), 0, 0));
    }
    private void Draw_Checked(Graphics graphics)
    {
        using Font checkFont = new("Segoe MDL2 Assets", 10F, FontStyle.Regular);
        using SolidBrush brush = new(RGB ? DrawEngine.HSV_To_RGB(h, 1f, 1f) : ColorChecked);
        graphics.DrawString(
            "\uE73E",
            checkFont,
            brush,
            new Rectangle(15 + 3, (Size.Height / 2) - (25 / 2) + 5, 0, 0));
    }
    private void Draw_Animation_Circles(Graphics graphics)
    {
        int size_circles = 40;
        if (temp < size_circles)
        {
            Rectangle rectangle_circles = new(
                (15 + (25 / 2)) - (temp / 2),
                ((Size.Height / 2) - (25 / 2) + (25 / 2)) - (temp / 2),
                temp, temp);
            rectangle_circles.X -= 2;
            rectangle_circles.Y -= 2;
            if (rectangle_circles.Width != 0 && rectangle_circles.Height != 0)
            {
                using SolidBrush brush = new(Color.FromArgb(Effect_1_Transparency, Effect_1_ColorBackground));
                graphics.FillEllipse(brush, rectangle_circles);
            }
        }
    }
    private void Draw_Animation_WhiteBackground_CirclesStyle(Graphics graphics)
    {
        int size_circles = 40;

        Rectangle rectangle_circles = new(
            (15 + (25 / 2)) - size_circles / 2,
            ((Size.Height / 2) - (25 / 2) + (25 / 2)) - size_circles / 2,
            size_circles, size_circles);
        rectangle_circles.X -= 2;
        rectangle_circles.Y -= 2;
        if (rectangle_circles.Width != 0 && rectangle_circles.Height != 0)
        {
            using SolidBrush brush = new(Color.FromArgb(Effect_2_Transparency, Effect_2_ColorBackground));
            graphics.FillEllipse(brush, rectangle_circles);
        }
    }
    #endregion
}
