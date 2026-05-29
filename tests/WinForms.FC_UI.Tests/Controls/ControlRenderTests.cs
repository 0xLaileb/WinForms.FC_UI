using FC_UI.Controls;

namespace WinForms.FC_UI.Tests.Controls;

public class ControlRenderTests
{
    [Fact]
    public void MainControls_DrawToBitmap_ProducesNonEmptyOutput()
    {
        Control[] controls =
        [
            new FButton(),
            new FCheckBox(),
            new FRadioButton(),
            new FSwitchBox(),
            new FProgressBar { Value = 50 },
            new FScrollBar { Value = 50 },
            new FGroupBox(),
            new FTextBox(),
            new FRichTextBox()
        ];

        try
        {
            foreach (var control in controls)
            {
                using Bitmap bitmap = new(control.Width, control.Height);

                control.DrawToBitmap(bitmap, new Rectangle(Point.Empty, control.Size));

                var nonTransparentPixels = EnumeratePixels(bitmap).Count(color => color.A > 0);
                Assert.True(
                    nonTransparentPixels > 1,
                    $"{control.GetType().Name} rendered too few visible sampled pixels.");
            }
        }
        finally
        {
            foreach (var control in controls) control.Dispose();
        }
    }

    private static IEnumerable<Color> EnumeratePixels(Bitmap bitmap)
    {
        for (var y = 0; y < bitmap.Height; y += Math.Max(1, bitmap.Height / 10))
        for (var x = 0; x < bitmap.Width; x += Math.Max(1, bitmap.Width / 10))
            yield return bitmap.GetPixel(x, y);
    }
}
