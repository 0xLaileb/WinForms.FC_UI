using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace FC_UI;

internal static class HelpEngine
{
    /// <summary>
    /// Shows an error MessageBox with preset parameters.
    /// </summary>
    public static void ShowError(string text) =>
        System.Windows.Forms.MessageBox.Show(text, "FC-UI", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

    /// <summary>
    /// Creates a new <c>Font</c> with default FC_UI parameters.
    /// </summary>
    public static Font GetDefaultFont(
        string familyName = "Arial",
        float emSize = 11.0F,
        FontStyle fontStyle = FontStyle.Regular) => new(familyName, emSize, fontStyle);

    /// <summary>
    /// Creates a <c>Graphics</c> object from a Bitmap with the specified rendering settings.
    /// </summary>
    public static Graphics GetGraphics(Bitmap bitmap, SmoothingMode smoothingMode, TextRenderingHint textRenderingHint)
    {
        Graphics graphics = Graphics.FromImage(bitmap);
        graphics.SmoothingMode = smoothingMode;
        graphics.TextRenderingHint = textRenderingHint;
        return graphics;
    }

    /// <summary>
    /// Generates a random <c>Color</c> with the specified alpha.
    /// </summary>
    public static Color RandomColor(int alpha = 255) =>
        Color.FromArgb(alpha, Random.Shared.Next(0, 256), Random.Shared.Next(0, 256), Random.Shared.Next(0, 256));

    /// <summary>
    /// Returns a random integer in the specified range [min, max).
    /// </summary>
    public static int RandomInt(int min, int max) => Random.Shared.Next(min, max);

    /// <summary>
    /// Returns a random float in the specified range.
    /// </summary>
    public static float RandomFloat(int min, int max) => Random.Shared.Next(min * 100, max * 100) / 100f;

    /// <summary>
    /// Returns a random boolean.
    /// </summary>
    public static bool RandomBool() => Random.Shared.Next(0, 2) == 1;
}
