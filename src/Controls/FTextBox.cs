using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using Timer = System.Windows.Forms.Timer;

namespace FC_UI.Controls;

[ToolboxBitmap(typeof(TextBox))]
[Description("Allows user text input with multi-line editing and password character masking.")]
public partial class FTextBox : UserControl
{
    #region VARIABLES
    private float h = 0;
    private Rectangle rectangle_region = new();
    private readonly StringFormat stringFormat = new();
    private Size size_ftextbox = new();
    public TextBox textBox = new();
    private System.EventHandler? _rgbTickHandler;
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
    [Description("Occurs when the Text property value changes.")]
    public event EventHandler TextChanged = delegate { };

    [Category("FTextBox")]
    [Description("Control text")]
    [DefaultValue("FTextBox")]
    public string TextButton
    {
        get => textBox.Text;
        set
        {
            textBox.Text = value;
            TextChanged();
        }
    }
    //
    private bool tmp_rgb_status;
    [Category("FTextBox")]
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
    private bool tmp_password;
    [Category("FTextBox")]
    [DefaultValue(false)]
    [Description("Enable/Disable password display mode")]
    public bool Password
    {
        get => tmp_password;
        set
        {
            tmp_password = value;
            Update_TextBox(true);
        }
    }
    //
    private char tmp_passwordchar;
    [Category("FTextBox")]
    [DefaultValue('●')]
    [Description("Specifies the character used for password masking in single-line edit control")]
    public char PasswordChar
    {
        get => tmp_passwordchar;
        set
        {
            tmp_passwordchar = value;
            Update_TextBox(true);
        }
    }
    //
    private bool tmp_rounding_status;
    [Category("FTextBox")]
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
    [Category("FTextBox")]
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
    private Color tmp_color_background;
    [Category("FTextBox")]
    [Description("Background color")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color ColorBackground
    {
        get => tmp_color_background;
        set
        {
            if (value != Color.Transparent && value.A >= 255)
            {
                tmp_color_background = value;
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
    private SmoothingMode tmp_smoothing_mode;
    [Category("FTextBox")]
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
    [Category("FTextBox")]
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
    private Style tmp_ftextbox_style = Style.Default;
    [Category("FTextBox")]
    [Description("Control style")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Style FTextBoxStyle
    {
        get => tmp_ftextbox_style;
        set
        {
            tmp_ftextbox_style = value;
            switch (tmp_ftextbox_style)
            {
                case Style.Default:
                    Size = new(200, 40);
                    BackColor = Color.Transparent;
                    ForeColor = Color.FromArgb(245, 245, 245);

                    TextButton = "FTextBox";
                    RGB = false;
                    Password = false;
                    PasswordChar = '●';
                    Rounding = true;
                    RoundingInt = 60;
                    BackgroundPen = true;
                    Background_WidthPen = 3F;
                    ColorBackground_Pen = Color.FromArgb(29, 200, 238);
                    ColorBackground = Color.FromArgb(37, 52, 68);
                    Timer_RGB = 300;
                    Lighting = false;
                    ColorLighting = Color.FromArgb(29, 200, 238);
                    Alpha = 20;
                    PenWidth = 15;
                    LinearGradientPen = false;
                    ColorPen_1 = Color.FromArgb(29, 200, 238);
                    ColorPen_2 = Color.FromArgb(37, 52, 68);
                    SmoothingMode = SmoothingMode.HighQuality;
                    TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                    Font = HelpEngine.GetDefaultFont();
                    break;
                case Style.Custom:
                    break;
                case Style.Random:
                    HelpEngine.GetRandom random = new();
                    ColorBackground = random.ColorArgb();
                    Password = random.Bool();
                    Rounding = random.Bool();
                    if (Rounding) RoundingInt = random.Int(5, 90);
                    BackgroundPen = random.Bool();
                    if (BackgroundPen)
                    {
                        Background_WidthPen = random.Float(1, 3);
                        ColorBackground_Pen = random.ColorArgb(random.Int(0, 255));
                    }
                    Lighting = random.Bool();
                    if (Lighting) ColorLighting = random.ColorArgb();
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
    public FTextBox()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.ResizeRedraw |
        ControlStyles.Selectable | ControlStyles.SupportsTransparentBackColor | ControlStyles.StandardDoubleClick, true);
        DoubleBuffered = true;

        Tag = "FC_UI";
        FTextBoxStyle = Style.Default;
        FTextBoxStyle = Style.Custom;

        stringFormat.Alignment = StringAlignment.Center;
        stringFormat.LineAlignment = StringAlignment.Center;

        textBox.Text = "Text";
        textBox.TextChanged += TextBox_TextChanged;
        Update_TextBox(false);
        Controls.Add(textBox);

        OnSizeChanged(null);
    }

    private void TextBox_TextChanged(object sender, EventArgs e)
    {
        TextButton = textBox.Text;
    }

    public void Update_TextBox(bool Visible)
    {
        textBox.Visible = Visible;
        textBox.Size = new((int)(size_ftextbox.Width - RoundingInt / 2 - Background_WidthPen / 2), size_ftextbox.Height / 2);
        textBox.Location = new((Width / 2) - (textBox.Size.Width / 2), (Height / 2) - (textBox.Size.Height / 2));
        if (ColorBackground.Name != "Transparent") textBox.BackColor = ColorBackground;
        textBox.ForeColor = Color.WhiteSmoke;
        textBox.BorderStyle = BorderStyle.None;
        Font = new Font(Font.Name, Height / 4, Font.Style);
        textBox.Font = Font;
        textBox.TextAlign = HorizontalAlignment.Center;
        textBox.MaxLength = 10000;
        textBox.PasswordChar = Password ? PasswordChar : '\0';
    }
    #endregion

    #region EVENTS
    protected override void OnPaint(PaintEventArgs e)
    {
        try
        {
            Settings_Load(e.Graphics);
            Draw_Background(e.Graphics);

            Update_TextBox(true);
        }
        catch (Exception er) { HelpEngine.MSB_Error($"[{Name}] Error: \n{er}"); }
    }
    protected override void OnSizeChanged(EventArgs e)
    {
        int tmp = (int)((BackgroundPen ? Background_WidthPen : 0) + (Lighting ? (PenWidth) / 4 : 0));
        size_ftextbox = new(Width - tmp * 2, Height - tmp * 2);
        rectangle_region = new(tmp, tmp, size_ftextbox.Width, size_ftextbox.Height);
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
        //Rounding
        float roundingValue = 0.1F;
        if (Rounding && RoundingInt > 0)
        {
            roundingValue = Height / 100F * RoundingInt;
        }

        //RoundedRectangle
        using var graphicsPath = DrawEngine.RoundedRectangle(rectangle_region, roundingValue);

        //Region
        using var regionPath = DrawEngine.RoundedRectangle(new Rectangle(
            0, 0,
            Width, Height),
            roundingValue);
        Region = new Region(regionPath);

        Bitmap Layer_1()
        {
            Bitmap bitmap = new(Width, Height);
            using Graphics graphics = HelpEngine.GetGraphics(ref bitmap, SmoothingMode, TextRenderingHint);

            //Shadow
            if (Lighting)
            {
                using var shadowPath = DrawEngine.RoundedRectangle(rectangle_region, roundingValue);
                DrawEngine.DrawBlurred(graphics, ColorLighting, shadowPath, Alpha, PenWidth);
            }

            //Background border
            if (Background_WidthPen != 0 && BackgroundPen == true)
            {
                if (LinearGradientPen)
                {
                    using var brush = new LinearGradientBrush(rectangle_region, ColorPen_1, ColorPen_2, 360);
                    using var pen = new Pen(brush, Background_WidthPen);
                    pen.LineJoin = LineJoin.Round;
                    pen.DashCap = DashCap.Round;
                    graphics.DrawPath(pen, graphicsPath);
                }
                else
                {
                    using var pen = new Pen(RGB ? DrawEngine.HSV_To_RGB(h, 1f, 1f) : ColorBackground_Pen, Background_WidthPen);
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
            using var clipPath = DrawEngine.RoundedRectangle(new Rectangle(
                rectangle_region.X - (int)(2 + Background_WidthPen),
                rectangle_region.Y - (int)(2 + Background_WidthPen),
                rectangle_region.Width + (int)(2 + Background_WidthPen) * 2,
                rectangle_region.Height + (int)(2 + Background_WidthPen) * 2), Rounding ? roundingValue : 0.1F);
            using var clipRegion = new Region(clipPath);
            graphics.Clip = clipRegion;

            //Background
            using var brush = new SolidBrush(ColorBackground);
            graphics.FillPath(brush, graphicsPath);

            return bitmap;
        }

        using (Bitmap layer1 = Layer_1())
            graphics_form.DrawImage(layer1, new PointF(0, 0));
        using (Bitmap layer2 = Layer_2())
            graphics_form.DrawImage(layer2, new PointF(0, 0));
    }
    #endregion
}
