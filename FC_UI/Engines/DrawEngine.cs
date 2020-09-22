using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FC_UI
{
    internal class DrawEngine
    {
        /// <summary>
        /// Создает новый объект <c>GraphicsPath</c> с закругленными краями.
        /// </summary>
        /// 
        /// <returns>Этот метод возвращает объект <c>GraphicsPath</c>.</returns>
        public static GraphicsPath RoundedRectangle(Rectangle rectangle, float value_angle)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            try
            {
                graphicsPath.AddArc(rectangle.X, rectangle.Y, value_angle, value_angle, 180, 90);
                graphicsPath.AddArc(rectangle.X + rectangle.Width - value_angle, rectangle.Y, value_angle, value_angle, 270, 90);
                graphicsPath.AddArc(rectangle.X + rectangle.Width - value_angle, rectangle.Y + rectangle.Height - value_angle, value_angle, value_angle, 0, 90);
                graphicsPath.AddArc(rectangle.X, rectangle.Y + rectangle.Height - value_angle, value_angle, value_angle, 90, 90);

                graphicsPath.CloseFigure();
            }
            catch (Exception er) { HelpEngine.MSB_Error($"[DrawEngine.RoundedRectangle] Ошибка: \n{er}"); }
            return graphicsPath;
        }

        /// <summary>
        /// Рисует размытую линию, которая используется в роле тени для дизайна.
        /// </summary>
        /// 
        /// <param name="color">
        /// Основной цвет.
        /// </param>
        /// <param name="max_alpha">
        /// Максимальная альфа.
        /// </param>
        /// <param name="pen_width">
        /// Размер Pen.
        /// </param>
        public static void DrawBlurred(Graphics graphics, Color color, Point point_1, Point point_2, int max_alpha, int pen_width)
        {
            float stepAlpha = (float)max_alpha / pen_width;

            float actualAlpha = stepAlpha;
            for (int pWidth = pen_width; pWidth > 0; pWidth--)
            {
                Color BlurredColor = Color.FromArgb((int)actualAlpha, color);
                Pen BlurredPen = new Pen(BlurredColor, pWidth)
                {
                    StartCap = LineCap.Round,
                    EndCap = LineCap.Round
                };

                graphics.DrawLine(BlurredPen, point_1, point_2);

                actualAlpha += stepAlpha;
            }
        }

        /// <summary>
        /// Рисует размытый объект (GraphicsPath), который используется в роле тени для дизайна.
        /// </summary>
        /// 
        /// <param name="color">
        /// Основной цвет.
        /// </param>
        /// <param name="max_alpha">
        /// Максимальная альфа.
        /// </param>
        /// <param name="pen_width">
        /// Размер Pen.
        /// </param>
        public static void DrawBlurred(Graphics graphics, Color color, GraphicsPath graphicsPath, int max_alpha, int pen_width)
        {
            float tmp = max_alpha / pen_width;
            float actualAlpha = tmp;

            for (int tmp_width = pen_width; tmp_width > 0; tmp_width--)
            {
                Pen blurredPen = new Pen(Color.FromArgb((int)actualAlpha, color), tmp_width)
                {
                    StartCap = LineCap.Round,
                    EndCap = LineCap.Round
                };
                actualAlpha += tmp;

                graphics.DrawPath(blurredPen, graphicsPath);
            }
        }

        #region RGB
        private static float h_temp = 0;
        /// <summary>
        /// Объект <c>Timer</c> для перерисовки контролов по заданному интервалу.
        /// </summary>
        public static readonly Timer timer_global_rgb = new Timer() { Interval = 300 };

        /// <summary>
        /// Управление глобальным таймером для режима <c>RGB</c>.
        /// </summary>
        /// 
        /// <param name="status">
        /// Статус таймера (включить или выключить).
        /// </param>
        public static void TimerGlobalRGB(bool status)
        {
            timer_global_rgb.Stop();
            if (!status) return;

            timer_global_rgb.Tick += (Sender, EventArgs) =>
            {
                h_temp++;
                if (h_temp >= 360) h_temp = 0;
            };
            timer_global_rgb.Start();
        }

        /// <summary>
        /// Преобразует HSV в RGB.
        /// </summary>
        /// 
        /// <param name="hue">
        /// Тон.
        /// </param>
        /// <param name="saturation">
        /// Насыщенность.
        /// </param>
        /// <param name="value">
        /// Значение яркости.
        /// </param>
        /// 
        /// <returns>Этот метод возвращает объект <c>Color</c>.</returns>
        public static Color HSV_To_RGB(float hue, float saturation, float value)
        {
            if (saturation < float.Epsilon)
            {
                int c = (int)(value * 255);
                return Color.FromArgb(c, c, c);
            }
            if (timer_global_rgb.Enabled) hue = h_temp;

            float r, g, b, f, p, q, t;
            int i;

            hue /= 60;
            i = (int)Math.Floor(hue);

            f = hue - i;
            p = value * (1 - saturation);
            q = value * (1 - saturation * f);
            t = value * (1 - saturation * (1 - f));

            switch (i)
            {
                case 0: r = value; g = t; b = p; break;
                case 1: r = q; g = value; b = p; break;
                case 2: r = p; g = value; b = t; break;
                case 3: r = p; g = q; b = value; break;
                case 4: r = t; g = p; b = value; break;
                default: r = value; g = p; b = q; break;
            }
            return Color.FromArgb(255, (int)(r * 255), (int)(g * 255), (int)(b * 255));
        }
        #endregion
    }
}
