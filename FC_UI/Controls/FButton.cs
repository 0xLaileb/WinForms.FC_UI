using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace FC_UI.Controls
{
    [ToolboxBitmap(typeof(Button))]
    [Description("При щелчке возникает событие.")]
    [DefaultEvent("Click")]
    public partial class FButton : UserControl
    {
        #region ПЕРЕМЕННЫЕ
        private float h = 0;
        private Rectangle rectangle_region = new Rectangle();
        private GraphicsPath graphicsPath = new GraphicsPath();
        private Point ClickLocation = new Point();
        private readonly StringFormat stringFormat = new StringFormat();
        private int temp = 0;
        private bool Mouse_Enter = false;
        private Size size_fbutton = new Size();
        public enum Style
        {
            Default,
            Custom,
            Random
        }
        #endregion

        #region НАСТРОЙКИ
        private string tmp_text_button;
        [Category("FButton")]
        [Description("Текст на контроле")]
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
        [Description("Вкл/Выкл RGB режима")]
        public bool RGB
        {
            get => tmp_rgb_status;
            set
            {
                tmp_rgb_status = value;

                if (tmp_rgb_status == true)
                {
                    timer_rgb.Stop();
                    if (!DrawEngine.timer_global_rgb.Enabled)
                    {
                        timer_rgb.Tick += (Sender, EventArgs) =>
                        {
                            h += 4;
                            if (h >= 360) h = 0;
                            Refresh();
                        };
                        timer_rgb.Start();
                    }
                }
                else
                {
                    timer_rgb.Stop();
                    Refresh();
                }
            }
        }
        //
        private bool tmp_background;
        [Category("FButton")]
        [Description("Вкл/Выкл фон")]
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
        [Description("Вкл/Выкл закругление кнопки")]
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
        [Description("Закругление в процентах")]
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
        [Description("Цвет анимации при клике")]
        public Color Effect_1_ColorBackground { get; set; }
        //
        private Color tmp_color_background;
        [Category("FButton")]
        [Description("Цвет фона")]
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
        [Description("Вкл/Выкл эффекта кругов при клике")]
        public bool Effect_1 { get; set; }
        //
        private int tmp_effect1_transparency;
        [Category("Effects")]
        [Description("Прозрачность effect_1")]
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
        [Description("Вкл/Выкл эффекта белого подфона на кнопке")]
        public bool Effect_2 { get; set; }
        //
        private int tmp_effect2_transparency;
        [Category("Effects")]
        [Description("Прозрачность effect_2")]
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
        [Description("Цвет эффекта")]
        public Color Effect_2_ColorBackground { get; set; }
        //
        private readonly Timer timer_effect_1 = new Timer();
        [Category("Timers")]
        [Description("Скорость эффекта <effect_1> (действует перерисовка)")]
        public int Timer_Effect_1
        {
            get => timer_effect_1.Interval;
            set => timer_effect_1.Interval = value;
        }
        //
        private readonly Timer timer_rgb = new Timer();
        [Category("Timers")]
        [Description("Скорость обновления RGB режима (действует перерисовка)")]
        public int Timer_RGB
        {
            get => timer_rgb.Interval;
            set => timer_rgb.Interval = value;
        }
        //
        private bool tmp_background_pen;
        [Category("BorderStyle")]
        [Description("Вкл/Выкл обводки")]
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
        [Description("Размер обводки")]
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
        public static Color tmp_color_background_pen;
        [Category("BorderStyle")]
        [Description("Цвет обводки")]
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
        [Description("Вкл/Выкл подсветку")]
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
        [Description("Цвет подсветки / тени")]
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
        [Description("Вкл/Выкл градиент фона")]
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
        [Description("Цвет №1 для градиента")]
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
        [Description("Цвет №2 для градиента")]
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
        [Description("Вкл/Выкл градиент обводки")]
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
        [Description("Цвет №1 для градиента обводки")]
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
        [Description("Цвет №2 для градиента обводки")]
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
        [Description("Режим <graphics.SmoothingMode>")]
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
        [Description("Режим <graphics.TextRenderingHint>")]
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
        [Description("Стиль контрола")]
        public Style FButtonStyle
        {
            get => tmp_fbutton_style;
            set
            {
                tmp_fbutton_style = value;
                switch (tmp_fbutton_style)
                {
                    case Style.Default:
                        Size = new Size(130, 50);
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
                        HelpEngine.GetRandom random = new HelpEngine.GetRandom();
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

        #region ЗАГРУЗКА
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

        #region СОБЫТИЯ
        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                Settings_Load(e.Graphics);
                Draw_Background(e.Graphics);
                Draw_Text(e.Graphics);

                graphicsPath.ClearMarkers();
                graphicsPath.Dispose();
            }
            catch (Exception er) { HelpEngine.MSB_Error($"[{Name}] Ошибка: \n{er}"); }
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            Mouse_Enter = true;
            Refresh();
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            timer_effect_1.Stop();
            timer_effect_1.Dispose();
            Mouse_Enter = false;
            temp = 0;

            Refresh();
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            timer_effect_1.Stop();
            timer_effect_1.Dispose();
            if (e.Button == MouseButtons.Left && Effect_1 == true)
            {
                ClickLocation = e.Location;
                temp = 2;

                timer_effect_1.Tick += (Sender, EventArgs) =>
                {
                    temp += 20;
                    Refresh();
                };
                timer_effect_1.Start();
            }
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            int tmp = (int)((BackgroundPen ? Background_WidthPen : 0) + (Lighting ? (PenWidth) / 4 : 0));
            size_fbutton = new Size(Width - tmp * 2, Height - tmp * 2);
            rectangle_region = new Rectangle(tmp, tmp, size_fbutton.Width, size_fbutton.Height);
        }
        #endregion

        #region МЕТОДЫ РИСОВАНИЯ
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
                //Закругление
                if (Rounding && RoundingInt > 0)
                {
                    roundingValue = Height / 100F * RoundingInt;
                }
                //RoundedRectangle
                graphicsPath = DrawEngine.RoundedRectangle(rectangle_region, roundingValue);

                //Region
                Region = new Region(DrawEngine.RoundedRectangle(new Rectangle(
                0, 0,
                Width, Height),
                roundingValue));
            }
            Bitmap Layer_1()
            {
                Bitmap bitmap = new Bitmap(Width, Height);
                Graphics graphics = HelpEngine.GetGraphics(ref bitmap, SmoothingMode, TextRenderingHint);

                //Тень
                if (Lighting) DrawEngine.DrawBlurred(graphics, ColorLighting, DrawEngine.RoundedRectangle(rectangle_region, roundingValue), Alpha, PenWidth);

                //Обводка фона
                if (Background_WidthPen != 0 && BackgroundPen == true)
                {
                    Pen pen;
                    if (LinearGradientPen) pen = new Pen(brush: new LinearGradientBrush(rectangle_region, ColorPen_1, ColorPen_2, 360), Background_WidthPen);
                    else pen = new Pen(RGB ? DrawEngine.HSV_To_RGB(h, 1f, 1f) : ColorBackground_Pen, Background_WidthPen);
                    pen.LineJoin = LineJoin.Round;
                    pen.DashCap = DashCap.Round;
                    graphics.DrawPath(pen, graphicsPath);
                }

                return bitmap;
            }
            Bitmap Layer_2()
            {
                Bitmap bitmap = new Bitmap(Width, Height);
                Graphics graphics = HelpEngine.GetGraphics(ref bitmap, SmoothingMode, TextRenderingHint);

                //Region_Clip
                int смещение_2 = 1;
                graphics.Clip = new Region(DrawEngine.RoundedRectangle(new Rectangle(
                    rectangle_region.X - смещение_2,
                    rectangle_region.Y - смещение_2,
                    rectangle_region.Width + смещение_2 * 2,
                    rectangle_region.Height + смещение_2 * 2), Rounding ? roundingValue : 0.1F));

                //Фон
                if (Background == true)
                {
                    Brush brush = new LinearGradientBrush(rectangle_region, ColorBackground_1, ColorBackground_2, 360);
                    graphics.FillPath(LinearGradient_Background ? brush : new SolidBrush(ColorBackground), graphicsPath);
                }

                //Эффекты
                if (Effect_1) Draw_Animation_Circles(graphics);
                if (Effect_2 && Mouse_Enter) Draw_Animation_WhiteBackground(graphics);

                return bitmap;
            }

            BaseLoading();
            graphics_form.DrawImage(Layer_1(), new PointF(0, 0));
            graphics_form.DrawImage(Layer_2(), new PointF(0, 0));
        }
        private void Draw_Text(Graphics graphics)
        {
            graphics.DrawString(
                TextButton,
                Font,
                new SolidBrush(ForeColor),
                new Rectangle(rectangle_region.X, rectangle_region.Y, rectangle_region.Width, rectangle_region.Height),
                stringFormat);
        }
        private void Draw_Animation_Circles(Graphics graphics)
        {
            if (temp < ((size_fbutton.Width >= size_fbutton.Height) ? size_fbutton.Width * 2 : size_fbutton.Height * 2))
            {
                Rectangle rectangle_circles = new Rectangle(ClickLocation.X - (temp / 2), ClickLocation.Y - (temp / 2), temp, temp);
                if (rectangle_circles.Width != 0 && rectangle_circles.Height != 0)
                {
                    graphics.FillEllipse(new SolidBrush(Color.FromArgb(Effect_1_Transparency, Effect_1_ColorBackground)), rectangle_circles);
                }
            }
        }
        private void Draw_Animation_WhiteBackground(Graphics graphics)
        {
            graphics.FillPath(new SolidBrush(Color.FromArgb(Effect_2_Transparency, Effect_2_ColorBackground)), graphicsPath);
        }
        #endregion
    }
}
