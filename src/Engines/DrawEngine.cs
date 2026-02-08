using System.Drawing;
using System.Drawing.Drawing2D;
using Timer = System.Windows.Forms.Timer;

namespace FC_UI;

internal static class DrawEngine
{
    /// <summary>
    /// Creates a new <c>GraphicsPath</c> with rounded corners for the given rectangle.
    /// </summary>
    /// <returns>A <c>GraphicsPath</c> representing the rounded rectangle.</returns>
    public static GraphicsPath CreateRoundedPath(Rectangle rectangle, float cornerRadius)
    {
        GraphicsPath path = new();

        path.AddArc(rectangle.X, rectangle.Y, cornerRadius, cornerRadius, 180, 90);
        path.AddArc(rectangle.X + rectangle.Width - cornerRadius, rectangle.Y, cornerRadius, cornerRadius, 270, 90);
        path.AddArc(rectangle.X + rectangle.Width - cornerRadius, rectangle.Y + rectangle.Height - cornerRadius, cornerRadius, cornerRadius, 0, 90);
        path.AddArc(rectangle.X, rectangle.Y + rectangle.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90);
        path.CloseFigure();

        return path;
    }

    /// <summary>
    /// Draws a blurred line used as a shadow effect.
    /// </summary>
    public static void DrawBlurredShadow(Graphics graphics, Color color, Point start, Point end, int maxAlpha, int penWidth)
    {
        float alphaStep = (float)maxAlpha / penWidth;
        float currentAlpha = alphaStep;

        for (int width = penWidth; width > 0; width--)
        {
            Color blurredColor = Color.FromArgb((int)currentAlpha, color);
            using Pen pen = new(blurredColor, width)
            {
                StartCap = LineCap.Round,
                EndCap = LineCap.Round
            };

            graphics.DrawLine(pen, start, end);
            currentAlpha += alphaStep;
        }
    }

    /// <summary>
    /// Draws a blurred shape (GraphicsPath) used as a shadow effect.
    /// </summary>
    public static void DrawBlurredShadow(Graphics graphics, Color color, GraphicsPath path, int maxAlpha, int penWidth)
    {
        float alphaStep = (float)maxAlpha / penWidth;
        float currentAlpha = alphaStep;

        for (int width = penWidth; width > 0; width--)
        {
            using Pen pen = new(Color.FromArgb((int)currentAlpha, color), width)
            {
                StartCap = LineCap.Round,
                EndCap = LineCap.Round
            };
            currentAlpha += alphaStep;

            graphics.DrawPath(pen, path);
        }
    }

    #region RGB

    private static float s_globalHue;

    /// <summary>
    /// Timer for redrawing controls at a specified interval in global RGB mode.
    /// </summary>
    public static readonly Timer GlobalRgbTimer = new() { Interval = 50 };

    private static EventHandler? s_globalRgbHandler;

    /// <summary>
    /// Enables or disables the global RGB timer.
    /// </summary>
    public static void SetGlobalRgbTimer(bool enabled)
    {
        GlobalRgbTimer.Stop();

        if (s_globalRgbHandler is not null)
        {
            GlobalRgbTimer.Tick -= s_globalRgbHandler;
            s_globalRgbHandler = null;
        }

        if (!enabled) return;

        s_globalRgbHandler = static (sender, args) =>
        {
            s_globalHue += 4;
            if (s_globalHue >= 360) s_globalHue = 0;
        };
        GlobalRgbTimer.Tick += s_globalRgbHandler;
        GlobalRgbTimer.Start();
    }

    /// <summary>
    /// Converts HSV color to RGB.
    /// </summary>
    /// <param name="hue">Hue (0..360).</param>
    /// <param name="saturation">Saturation (0..1).</param>
    /// <param name="value">Brightness (0..1).</param>
    /// <returns>A <c>Color</c> object.</returns>
    public static Color HsvToRgb(float hue, float saturation, float value)
    {
        if (saturation < float.Epsilon)
        {
            int gray = (int)(value * 255);
            return Color.FromArgb(gray, gray, gray);
        }

        if (GlobalRgbTimer.Enabled) hue = s_globalHue;

        hue /= 60;
        int sector = (int)Math.Floor(hue);

        float f = hue - sector;
        float p = value * (1 - saturation);
        float q = value * (1 - saturation * f);
        float t = value * (1 - saturation * (1 - f));

        var (r, g, b) = sector switch
        {
            0 => (value, t, p),
            1 => (q, value, p),
            2 => (p, value, t),
            3 => (p, q, value),
            4 => (t, p, value),
            _ => (value, p, q),
        };

        return Color.FromArgb(255, (int)(r * 255), (int)(g * 255), (int)(b * 255));
    }

    #endregion
}
