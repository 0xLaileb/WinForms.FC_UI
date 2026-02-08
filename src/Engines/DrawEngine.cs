using System.Drawing;
using System.Drawing.Drawing2D;
using Timer = System.Windows.Forms.Timer;

namespace FC_UI;

internal class DrawEngine
{
    /// <summary>
    /// Creates a new <c>GraphicsPath</c> object with rounded corners.
    /// </summary>
    /// <returns>A <c>GraphicsPath</c> representing the rounded rectangle.</returns>
    public static GraphicsPath RoundedRectangle(Rectangle rectangle, float value_angle)
    {
        GraphicsPath graphicsPath = new();
        try
        {
            graphicsPath.AddArc(rectangle.X, rectangle.Y, value_angle, value_angle, 180, 90);
            graphicsPath.AddArc(rectangle.X + rectangle.Width - value_angle, rectangle.Y, value_angle, value_angle, 270, 90);
            graphicsPath.AddArc(rectangle.X + rectangle.Width - value_angle, rectangle.Y + rectangle.Height - value_angle, value_angle, value_angle, 0, 90);
            graphicsPath.AddArc(rectangle.X, rectangle.Y + rectangle.Height - value_angle, value_angle, value_angle, 90, 90);

            graphicsPath.CloseFigure();
        }
        catch (Exception er) { HelpEngine.MSB_Error($"[DrawEngine.RoundedRectangle] Error: \n{er}"); }
        return graphicsPath;
    }

    /// <summary>
    /// Draws a blurred line used as a shadow effect for design.
    /// </summary>
    /// <param name="graphics">The graphics surface to draw on.</param>
    /// <param name="color">The base color.</param>
    /// <param name="point_1">Start point of the line.</param>
    /// <param name="point_2">End point of the line.</param>
    /// <param name="max_alpha">Maximum alpha value.</param>
    /// <param name="pen_width">Pen width.</param>
    public static void DrawBlurred(Graphics graphics, Color color, Point point_1, Point point_2, int max_alpha, int pen_width)
    {
        float stepAlpha = (float)max_alpha / pen_width;
        float actualAlpha = stepAlpha;

        for (int pWidth = pen_width; pWidth > 0; pWidth--)
        {
            Color blurredColor = Color.FromArgb((int)actualAlpha, color);
            using Pen blurredPen = new(blurredColor, pWidth)
            {
                StartCap = LineCap.Round,
                EndCap = LineCap.Round
            };

            graphics.DrawLine(blurredPen, point_1, point_2);
            actualAlpha += stepAlpha;
        }
    }

    /// <summary>
    /// Draws a blurred shape (GraphicsPath) used as a shadow effect for design.
    /// </summary>
    /// <param name="graphics">The graphics surface to draw on.</param>
    /// <param name="color">The base color.</param>
    /// <param name="graphicsPath">The path to draw.</param>
    /// <param name="max_alpha">Maximum alpha value.</param>
    /// <param name="pen_width">Pen width.</param>
    public static void DrawBlurred(Graphics graphics, Color color, GraphicsPath graphicsPath, int max_alpha, int pen_width)
    {
        float tmp = (float)max_alpha / pen_width;
        float actualAlpha = tmp;

        for (int tmp_width = pen_width; tmp_width > 0; tmp_width--)
        {
            using Pen blurredPen = new(Color.FromArgb((int)actualAlpha, color), tmp_width)
            {
                StartCap = LineCap.Round,
                EndCap = LineCap.Round
            };
            actualAlpha += tmp;

            graphics.DrawPath(blurredPen, graphicsPath);
        }
    }

    #region RGB
    private static float h_temp;

    /// <summary>
    /// <c>Timer</c> object for redrawing controls at a specified interval.
    /// </summary>
    public static readonly Timer timer_global_rgb = new() { Interval = 50 };

    private static EventHandler? _globalRgbTickHandler;

    /// <summary>
    /// Controls the global timer for RGB mode.
    /// </summary>
    /// <param name="status">Timer status (enable or disable).</param>
    public static void TimerGlobalRGB(bool status)
    {
        timer_global_rgb.Stop();

        // Remove the previous handler to prevent event handler leak
        if (_globalRgbTickHandler is not null)
        {
            timer_global_rgb.Tick -= _globalRgbTickHandler;
            _globalRgbTickHandler = null;
        }

        if (!status) return;

        _globalRgbTickHandler = (sender, args) =>
        {
            h_temp += 4;
            if (h_temp >= 360) h_temp = 0;
        };
        timer_global_rgb.Tick += _globalRgbTickHandler;
        timer_global_rgb.Start();
    }

    /// <summary>
    /// Converts HSV to RGB.
    /// </summary>
    /// <param name="hue">Hue (0..360).</param>
    /// <param name="saturation">Saturation (0..1).</param>
    /// <param name="value">Brightness value (0..1).</param>
    /// <returns>A <c>Color</c> object.</returns>
    public static Color HSV_To_RGB(float hue, float saturation, float value)
    {
        if (saturation < float.Epsilon)
        {
            int c = (int)(value * 255);
            return Color.FromArgb(c, c, c);
        }
        if (timer_global_rgb.Enabled) hue = h_temp;

        hue /= 60;
        int i = (int)Math.Floor(hue);

        float f = hue - i;
        float p = value * (1 - saturation);
        float q = value * (1 - saturation * f);
        float t = value * (1 - saturation * (1 - f));

        float r, g, b;
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
