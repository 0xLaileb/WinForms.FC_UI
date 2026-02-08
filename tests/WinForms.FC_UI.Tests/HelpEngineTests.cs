using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using FC_UI;

namespace WinForms.FC_UI.Tests;

public class HelpEngineTests
{
    #region GetDefaultFont Tests

    [Fact]
    public void GetDefaultFont_DefaultParams_ReturnsArial11()
    {
        using Font font = HelpEngine.GetDefaultFont();

        Assert.Equal("Arial", font.FontFamily.Name);
        Assert.Equal(11.0F, font.Size);
        Assert.Equal(FontStyle.Regular, font.Style);
    }

    [Fact]
    public void GetDefaultFont_CustomParams_ReturnsCustomFont()
    {
        using Font font = HelpEngine.GetDefaultFont("Segoe UI", 14F, FontStyle.Bold);

        Assert.Equal("Segoe UI", font.FontFamily.Name);
        Assert.Equal(14.0F, font.Size);
        Assert.Equal(FontStyle.Bold, font.Style);
    }

    [Fact]
    public void GetDefaultFont_CustomFamilyOnly_ReturnsDefaultSizeAndStyle()
    {
        using Font font = HelpEngine.GetDefaultFont("Consolas");

        Assert.Equal("Consolas", font.FontFamily.Name);
        Assert.Equal(11.0F, font.Size);
        Assert.Equal(FontStyle.Regular, font.Style);
    }

    #endregion

    #region GetGraphics Tests

    [Fact]
    public void GetGraphics_SetsCorrectSmoothingMode()
    {
        Bitmap bitmap = new(100, 100);

        using Graphics graphics = HelpEngine.GetGraphics(bitmap, SmoothingMode.HighQuality, TextRenderingHint.ClearTypeGridFit);

        // SmoothingMode.HighQuality is an alias for AntiAlias in GDI+;
        // the getter always returns AntiAlias when either value is set.
        Assert.Equal(SmoothingMode.AntiAlias, graphics.SmoothingMode);
        bitmap.Dispose();
    }

    [Fact]
    public void GetGraphics_SetsCorrectTextRenderingHint()
    {
        Bitmap bitmap = new(100, 100);

        using Graphics graphics = HelpEngine.GetGraphics(bitmap, SmoothingMode.AntiAlias, TextRenderingHint.AntiAlias);

        Assert.Equal(TextRenderingHint.AntiAlias, graphics.TextRenderingHint);
        bitmap.Dispose();
    }

    [Fact]
    public void GetGraphics_ReturnsNonNullGraphics()
    {
        Bitmap bitmap = new(50, 50);

        using Graphics graphics = HelpEngine.GetGraphics(bitmap, SmoothingMode.Default, TextRenderingHint.SystemDefault);

        Assert.NotNull(graphics);
        bitmap.Dispose();
    }

    #endregion

    #region Random Tests

    [Fact]
    public void RandomColor_DefaultAlpha_Returns255Alpha()
    {
        Color color = HelpEngine.RandomColor();

        Assert.Equal(255, color.A);
    }

    [Fact]
    public void RandomColor_CustomAlpha_ReturnsCorrectAlpha()
    {
        Color color = HelpEngine.RandomColor(128);

        Assert.Equal(128, color.A);
    }

    [Fact]
    public void RandomColor_ReturnsValidRGBRange()
    {
        for (int i = 0; i < 100; i++)
        {
            Color color = HelpEngine.RandomColor();
            Assert.InRange(color.R, 0, 255);
            Assert.InRange(color.G, 0, 255);
            Assert.InRange(color.B, 0, 255);
        }
    }

    [Fact]
    public void RandomInt_ReturnsWithinRange()
    {
        for (int i = 0; i < 100; i++)
        {
            int value = HelpEngine.RandomInt(10, 50);
            Assert.InRange(value, 10, 49);
        }
    }

    [Fact]
    public void RandomInt_MinEqualsMax_ReturnsSameValue()
    {
        int result = HelpEngine.RandomInt(5, 5);
        Assert.Equal(5, result);
    }

    [Fact]
    public void RandomFloat_ReturnsWithinRange()
    {
        for (int i = 0; i < 100; i++)
        {
            float value = HelpEngine.RandomFloat(1, 10);
            Assert.True(value >= 1f && value < 10f, $"Value {value} was out of expected range [1, 10)");
        }
    }

    [Fact]
    public void RandomBool_ReturnsBothValues()
    {
        bool sawTrue = false;
        bool sawFalse = false;

        for (int i = 0; i < 1000; i++)
        {
            if (HelpEngine.RandomBool()) sawTrue = true;
            else sawFalse = true;

            if (sawTrue && sawFalse) break;
        }

        Assert.True(sawTrue, "Expected to see true at least once in 1000 iterations");
        Assert.True(sawFalse, "Expected to see false at least once in 1000 iterations");
    }

    #endregion
}
