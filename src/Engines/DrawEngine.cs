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
        if (penWidth <= 0 || maxAlpha <= 0) return;

        var alpha = Math.Clamp(maxAlpha, 0, 255);
        var alphaStep = (float)alpha / penWidth;
        var currentAlpha = alphaStep;

        for (var width = penWidth; width > 0; width--)
        {
            var blurredColor = Color.FromArgb(Math.Clamp((int)currentAlpha, 0, 255), color);

            using Pen pen = new(blurredColor, width);

            pen.StartCap = LineCap.Round;
            pen.EndCap = LineCap.Round;

            graphics.DrawLine(pen, start, end);
            currentAlpha += alphaStep;
        }
    }

    /// <summary>
    /// Draws a blurred shape (GraphicsPath) used as a shadow effect.
    /// </summary>
    public static void DrawBlurredShadow(Graphics graphics, Color color, GraphicsPath path, int maxAlpha, int penWidth)
    {
        if (penWidth <= 0 || maxAlpha <= 0) return;

        var alpha = Math.Clamp(maxAlpha, 0, 255);
        var alphaStep = (float)alpha / penWidth;
        var currentAlpha = alphaStep;

        for (var width = penWidth; width > 0; width--)
        {
            using Pen pen = new(Color.FromArgb(Math.Clamp((int)currentAlpha, 0, 255), color), width);
            pen.StartCap = LineCap.Round;
            pen.EndCap = LineCap.Round;
            currentAlpha += alphaStep;

            graphics.DrawPath(pen, path);
        }
    }

    #region RGB

    private static float _sGlobalHue;

    /// <summary>
    /// Timer for redrawing controls at a specified interval in global RGB mode.
    /// </summary>
    public static readonly Timer GlobalRgbTimer = new() { Interval = 50 };

    /// <summary>
    /// Raised on every global RGB timer tick after the shared hue has advanced.
    /// </summary>
    public static event EventHandler? GlobalRgbTick;

    static DrawEngine()
    {
        GlobalRgbTimer.Tick += static (_, _) =>
        {
            _sGlobalHue += 4;
            if (_sGlobalHue >= 360) _sGlobalHue = 0;
            GlobalRgbTick?.Invoke(null, EventArgs.Empty);
        };
    }

    /// <summary>
    /// Enables or disables the global RGB timer.
    /// </summary>
    public static void SetGlobalRgbTimer(bool enabled)
    {
        if (enabled) GlobalRgbTimer.Start();
        else GlobalRgbTimer.Stop();
    }

    /// <summary>
    /// Returns the active RGB animation color.
    /// </summary>
    public static Color GetRgbColor(float hue) =>
        HsvToRgb(GlobalRgbTimer.Enabled ? _sGlobalHue : hue, 1f, 1f);

    /// <summary>
    /// Converts an RGB color to HSV (hue 0..360, saturation 0..1, value 0..1). Alpha is ignored.
    /// </summary>
    public static (float Hue, float Saturation, float Value) RgbToHsv(Color color)
    {
        var r = color.R / 255f;
        var g = color.G / 255f;
        var b = color.B / 255f;
        var max = Math.Max(r, Math.Max(g, b));
        var min = Math.Min(r, Math.Min(g, b));
        var delta = max - min;

        var hue = 0f;
        if (delta > 0)
        {
            if (max == r) hue = 60 * ((g - b) / delta % 6);
            else if (max == g) hue = 60 * ((b - r) / delta + 2);
            else hue = 60 * ((r - g) / delta + 4);
            if (hue < 0) hue += 360;
        }

        var saturation = max > 0 ? delta / max : 0;
        return (hue, saturation, max);
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
        if (float.IsNaN(hue) || float.IsInfinity(hue)) hue = 0;
        if (float.IsNaN(saturation) || float.IsInfinity(saturation)) saturation = 0;
        if (float.IsNaN(value) || float.IsInfinity(value)) value = 0;

        hue %= 360;
        if (hue < 0) hue += 360;
        saturation = Math.Clamp(saturation, 0f, 1f);
        value = Math.Clamp(value, 0f, 1f);

        if (saturation < float.Epsilon)
        {
            var gray = (int)(value * 255);
            return Color.FromArgb(gray, gray, gray);
        }

        hue /= 60;
        var sector = (int)Math.Floor(hue);

        var f = hue - sector;
        var p = value * (1 - saturation);
        var q = value * (1 - saturation * f);
        var t = value * (1 - saturation * (1 - f));

        var (r, g, b) = sector switch
        {
            0 => (value, t, p),
            1 => (q, value, p),
            2 => (p, value, t),
            3 => (p, q, value),
            4 => (t, p, value),
            _ => (value, p, q)
        };

        return Color.FromArgb(255, (int)(r * 255), (int)(g * 255), (int)(b * 255));
    }

    #endregion
}
