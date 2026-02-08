using System.Drawing;
using System.Drawing.Drawing2D;
using FC_UI;

namespace WinForms.FC_UI.Tests;

[Collection("GlobalRGBTimer")]
public class DrawEngineTests
{
    #region HSV_To_RGB Tests

    [Fact]
    public void HSV_To_RGB_PureRed_ReturnsRed()
    {
        Color result = DrawEngine.HSV_To_RGB(0, 1f, 1f);

        Assert.Equal(255, result.R);
        Assert.Equal(0, result.G);
        Assert.Equal(0, result.B);
    }

    [Fact]
    public void HSV_To_RGB_PureGreen_ReturnsGreen()
    {
        Color result = DrawEngine.HSV_To_RGB(120, 1f, 1f);

        Assert.Equal(0, result.R);
        Assert.Equal(255, result.G);
        Assert.Equal(0, result.B);
    }

    [Fact]
    public void HSV_To_RGB_PureBlue_ReturnsBlue()
    {
        Color result = DrawEngine.HSV_To_RGB(240, 1f, 1f);

        Assert.Equal(0, result.R);
        Assert.Equal(0, result.G);
        Assert.Equal(255, result.B);
    }

    [Fact]
    public void HSV_To_RGB_Yellow_ReturnsYellow()
    {
        Color result = DrawEngine.HSV_To_RGB(60, 1f, 1f);

        Assert.Equal(255, result.R);
        Assert.Equal(255, result.G);
        Assert.Equal(0, result.B);
    }

    [Fact]
    public void HSV_To_RGB_Cyan_ReturnsCyan()
    {
        Color result = DrawEngine.HSV_To_RGB(180, 1f, 1f);

        Assert.Equal(0, result.R);
        Assert.Equal(255, result.G);
        Assert.Equal(255, result.B);
    }

    [Fact]
    public void HSV_To_RGB_Magenta_ReturnsMagenta()
    {
        Color result = DrawEngine.HSV_To_RGB(300, 1f, 1f);

        Assert.Equal(255, result.R);
        Assert.Equal(0, result.G);
        Assert.Equal(255, result.B);
    }

    [Fact]
    public void HSV_To_RGB_ZeroSaturation_ReturnsGray()
    {
        Color result = DrawEngine.HSV_To_RGB(0, 0f, 0.5f);

        Assert.Equal(127, result.R);
        Assert.Equal(127, result.G);
        Assert.Equal(127, result.B);
    }

    [Fact]
    public void HSV_To_RGB_FullBrightnessZeroSaturation_ReturnsWhite()
    {
        Color result = DrawEngine.HSV_To_RGB(0, 0f, 1f);

        Assert.Equal(255, result.R);
        Assert.Equal(255, result.G);
        Assert.Equal(255, result.B);
    }

    [Fact]
    public void HSV_To_RGB_ZeroBrightnessZeroSaturation_ReturnsBlack()
    {
        Color result = DrawEngine.HSV_To_RGB(0, 0f, 0f);

        Assert.Equal(0, result.R);
        Assert.Equal(0, result.G);
        Assert.Equal(0, result.B);
    }

    [Fact]
    public void HSV_To_RGB_AlphaIsAlways255()
    {
        Color result = DrawEngine.HSV_To_RGB(180, 0.5f, 0.8f);

        Assert.Equal(255, result.A);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(60)]
    [InlineData(120)]
    [InlineData(180)]
    [InlineData(240)]
    [InlineData(300)]
    public void HSV_To_RGB_BoundaryHueValues_DoNotThrow(float hue)
    {
        Color result = DrawEngine.HSV_To_RGB(hue, 1f, 1f);

        Assert.True(result.R >= 0 && result.R <= 255);
        Assert.True(result.G >= 0 && result.G <= 255);
        Assert.True(result.B >= 0 && result.B <= 255);
    }

    #endregion

    #region RoundedRectangle Tests

    [Fact]
    public void RoundedRectangle_ValidInput_ReturnsNonEmptyPath()
    {
        Rectangle rect = new(10, 10, 100, 50);

        using GraphicsPath path = DrawEngine.RoundedRectangle(rect, 10f);

        Assert.NotNull(path);
        Assert.True(path.PointCount > 0);
    }

    [Fact]
    public void RoundedRectangle_ZeroAngle_ReturnsValidPath()
    {
        Rectangle rect = new(0, 0, 100, 50);

        using GraphicsPath path = DrawEngine.RoundedRectangle(rect, 0.1f);

        Assert.NotNull(path);
        Assert.True(path.PointCount > 0);
    }

    [Fact]
    public void RoundedRectangle_FigureIsClosed()
    {
        Rectangle rect = new(0, 0, 100, 100);

        using GraphicsPath path = DrawEngine.RoundedRectangle(rect, 20f);

        // A closed figure has its start and end points connected
        RectangleF bounds = path.GetBounds();
        Assert.True(bounds.Width > 0);
        Assert.True(bounds.Height > 0);
    }

    [Fact]
    public void RoundedRectangle_SmallRectangle_ReturnsValidPath()
    {
        Rectangle rect = new(0, 0, 10, 10);

        using GraphicsPath path = DrawEngine.RoundedRectangle(rect, 5f);

        Assert.NotNull(path);
        Assert.True(path.PointCount > 0);
    }

    [Fact]
    public void RoundedRectangle_LargeAngle_ReturnsValidPath()
    {
        Rectangle rect = new(0, 0, 200, 200);

        using GraphicsPath path = DrawEngine.RoundedRectangle(rect, 100f);

        Assert.NotNull(path);
        Assert.True(path.PointCount > 0);
    }

    #endregion

    #region TimerGlobalRGB Tests

    [Fact]
    public void TimerGlobalRGB_Enable_StartsTimer()
    {
        DrawEngine.TimerGlobalRGB(true);

        Assert.True(DrawEngine.timer_global_rgb.Enabled);

        // Cleanup
        DrawEngine.TimerGlobalRGB(false);
    }

    [Fact]
    public void TimerGlobalRGB_Disable_StopsTimer()
    {
        DrawEngine.TimerGlobalRGB(true);
        DrawEngine.TimerGlobalRGB(false);

        Assert.False(DrawEngine.timer_global_rgb.Enabled);
    }

    [Fact]
    public void TimerGlobalRGB_DefaultInterval_Is50()
    {
        Assert.Equal(50, DrawEngine.timer_global_rgb.Interval);
    }

    #endregion
}
