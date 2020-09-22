using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace FC_UI
{
    internal class HelpEngine
    {
        /// <summary>
        /// Вызывает <c>MessageBox.Show(...)</c> с готовыми параметрами.
        /// </summary>
        /// 
        /// <param name="text">
        /// Текст сообщения MessageBox
        /// </param>
        public static void MSB_Error(string text) => System.Windows.Forms.MessageBox.Show(text, "FC-UI", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

        /// <summary>
        /// Создает новый объект <c>Font</c> 
        /// </summary>
        /// 
        /// <param name="familyName">
        /// Имя шрифта.
        /// </param>
        /// <param name="emSize">
        /// Размер шрифта.
        /// </param>
        /// <param name="fontStyle">
        /// Стиль шрифта.
        /// </param>
        /// 
        /// <returns>Этот метод возвращает новый объект <c>Font</c>, который является стандартным шрифтом FC_UI или же шрифтом по заданным параметрам.</returns>
        public static Font GetDefaultFont(
            string familyName = "Arial",
            float emSize = 11.0F,
            FontStyle fontStyle = FontStyle.Regular) => new Font(familyName, emSize, fontStyle);

        /// <summary>
        /// Создает новый объект <c>Graphics</c> на основе ссылочного Bitmap.
        /// </summary>
        /// 
        /// <param name="SmoothingMode">
        /// Режим сглаживания для линий и границ.
        /// </param>
        /// <param name="TextRenderingHint">
        /// Качество отрисовки текста.
        /// </param>
        /// 
        /// <returns>Этот метод возвращает новый объект <c>Graphics</c> по заданным параметрам.</returns>
        public static Graphics GetGraphics(ref Bitmap bitmap, SmoothingMode SmoothingMode, TextRenderingHint TextRenderingHint)
        {
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = SmoothingMode;
            graphics.TextRenderingHint = TextRenderingHint;

            return graphics;
        }

        /// <summary>
        /// Класс <c>GetRandom</c>, который имеет готовый объект <c>Random</c> и готовые методы для работы с ним.
        /// </summary>
        public class GetRandom
        {
            private readonly System.Random random = new System.Random(System.Environment.TickCount);

            /// <summary>
            /// Создает объект <c>Color</c>.
            /// </summary>
            /// 
            /// <param name="alpha">
            /// Альфа (0..255)
            /// </param>
            /// 
            /// <returns>Этот метод возвращает новый объект <c>Color</c> по случайным параметрам.</returns>
            public Color ColorArgb(int alpha = 255) => Color.FromArgb(alpha, Int(0, 255), Int(0, 255), Int(0, 255));

            /// <returns>Этот метод возвращает случайное целое число в указанном диапазоне.</returns>
            public int Int(int min, int max) => random.Next(min, max);

            /// <returns>Этот метод возвращает случайное число с плавающей запятой в указанном диапазоне.</returns>
            public float Float(int min, int max) => random.Next(min * 100, max * 100) / 100;

            /// <returns>Этот метод возвращает <c>true</c> или <c>false</c>.</returns>
            public bool Bool() => Int(0, 2) == 1;
        }
    }
}
