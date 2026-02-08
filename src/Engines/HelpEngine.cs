using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace FC_UI;

internal class HelpEngine
{
    /// <summary>
    /// Calls <c>MessageBox.Show(...)</c> with preset error parameters.
    /// </summary>
    /// <param name="text">The message text for the MessageBox.</param>
    public static void MSB_Error(string text) =>
        System.Windows.Forms.MessageBox.Show(text, "FC-UI", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

    /// <summary>
    /// Creates a new <c>Font</c> object.
    /// </summary>
    /// <param name="familyName">Font family name.</param>
    /// <param name="emSize">Font size.</param>
    /// <param name="fontStyle">Font style.</param>
    /// <returns>A new <c>Font</c> object with the specified or default FC_UI parameters.</returns>
    public static Font GetDefaultFont(
        string familyName = "Arial",
        float emSize = 11.0F,
        FontStyle fontStyle = FontStyle.Regular) => new(familyName, emSize, fontStyle);

    /// <summary>
    /// Creates a new <c>Graphics</c> object based on a referenced Bitmap.
    /// </summary>
    /// <param name="bitmap">The bitmap to create graphics from.</param>
    /// <param name="SmoothingMode">Smoothing mode for lines and edges.</param>
    /// <param name="TextRenderingHint">Text rendering quality.</param>
    /// <returns>A new <c>Graphics</c> object with the specified parameters.</returns>
    public static Graphics GetGraphics(ref Bitmap bitmap, SmoothingMode SmoothingMode, TextRenderingHint TextRenderingHint)
    {
        Graphics graphics = Graphics.FromImage(bitmap);
        graphics.SmoothingMode = SmoothingMode;
        graphics.TextRenderingHint = TextRenderingHint;

        return graphics;
    }

    /// <summary>
    /// Helper class with a ready-to-use <c>Random</c> instance and convenience methods.
    /// </summary>
    public class GetRandom
    {
        /// <summary>
        /// Creates a <c>Color</c> object with random RGB values.
        /// </summary>
        /// <param name="alpha">Alpha channel (0..255).</param>
        /// <returns>A new <c>Color</c> with random parameters.</returns>
        public Color ColorArgb(int alpha = 255) => Color.FromArgb(alpha, Int(0, 255), Int(0, 255), Int(0, 255));

        /// <returns>A random integer in the specified range.</returns>
        public int Int(int min, int max) => Random.Shared.Next(min, max);

        /// <returns>A random float in the specified range.</returns>
        public float Float(int min, int max) => Random.Shared.Next(min * 100, max * 100) / 100f;

        /// <returns><c>true</c> or <c>false</c>.</returns>
        public bool Bool() => Int(0, 2) == 1;
    }
}
