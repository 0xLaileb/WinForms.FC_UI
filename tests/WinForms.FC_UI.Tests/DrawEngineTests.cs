using FC_UI;

namespace WinForms.FC_UI.Tests;

[Collection("GlobalRGBTimer")]
public class DrawEngineTests
{
    #region HsvToRgb Tests

    [Fact]
    public void HsvToRgb_PureRed_ReturnsRed()
    {
        var result = DrawEngine.HsvToRgb(0, 1f, 1f);

        Assert.Equal(255, result.R);
        Assert.Equal(0, result.G);
        Assert.Equal(0, result.B);
    }

    [Fact]
    public void HsvToRgb_PureGreen_ReturnsGreen()
    {
        var result = DrawEngine.HsvToRgb(120, 1f, 1f);

        Assert.Equal(0, result.R);
        Assert.Equal(255, result.G);
        Assert.Equal(0, result.B);
    }

    [Fact]
    public void HsvToRgb_PureBlue_ReturnsBlue()
    {
        var result = DrawEngine.HsvToRgb(240, 1f, 1f);

        Assert.Equal(0, result.R);
        Assert.Equal(0, result.G);
        Assert.Equal(255, result.B);
    }

    [Fact]
    public void HsvToRgb_Yellow_ReturnsYellow()
    {
        var result = DrawEngine.HsvToRgb(60, 1f, 1f);

        Assert.Equal(255, result.R);
        Assert.Equal(255, result.G);
        Assert.Equal(0, result.B);
    }

    [Fact]
    public void HsvToRgb_Cyan_ReturnsCyan()
    {
        var result = DrawEngine.HsvToRgb(180, 1f, 1f);

        Assert.Equal(0, result.R);
        Assert.Equal(255, result.G);
        Assert.Equal(255, result.B);
    }

    [Fact]
    public void HsvToRgb_Magenta_ReturnsMagenta()
    {
        var result = DrawEngine.HsvToRgb(300, 1f, 1f);

        Assert.Equal(255, result.R);
        Assert.Equal(0, result.G);
        Assert.Equal(255, result.B);
    }

    [Fact]
    public void HsvToRgb_ZeroSaturation_ReturnsGray()
    {
        var result = DrawEngine.HsvToRgb(0, 0f, 0.5f);

        Assert.Equal(127, result.R);
        Assert.Equal(127, result.G);
        Assert.Equal(127, result.B);
    }

    [Fact]
    public void HsvToRgb_FullBrightnessZeroSaturation_ReturnsWhite()
    {
        var result = DrawEngine.HsvToRgb(0, 0f, 1f);

        Assert.Equal(255, result.R);
        Assert.Equal(255, result.G);
        Assert.Equal(255, result.B);
    }

    [Fact]
    public void HsvToRgb_ZeroBrightnessZeroSaturation_ReturnsBlack()
    {
        var result = DrawEngine.HsvToRgb(0, 0f, 0f);

        Assert.Equal(0, result.R);
        Assert.Equal(0, result.G);
        Assert.Equal(0, result.B);
    }

    [Fact]
    public void HsvToRgb_AlphaIsAlways255()
    {
        var result = DrawEngine.HsvToRgb(180, 0.5f, 0.8f);

        Assert.Equal(255, result.A);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(60)]
    [InlineData(120)]
    [InlineData(180)]
    [InlineData(240)]
    [InlineData(300)]
    public void HsvToRgb_BoundaryHueValues_DoNotThrow(float hue)
    {
        var result = DrawEngine.HsvToRgb(hue, 1f, 1f);

        Assert.True(result.R >= 0 && result.R <= 255);
        Assert.True(result.G >= 0 && result.G <= 255);
        Assert.True(result.B >= 0 && result.B <= 255);
    }

    #endregion

    #region CreateRoundedPath Tests

    [Fact]
    public void CreateRoundedPath_ValidInput_ReturnsNonEmptyPath()
    {
        Rectangle rect = new(10, 10, 100, 50);

        using var path = DrawEngine.CreateRoundedPath(rect, 10f);

        Assert.NotNull(path);
        Assert.True(path.PointCount > 0);
    }

    [Fact]
    public void CreateRoundedPath_ZeroAngle_ReturnsValidPath()
    {
        Rectangle rect = new(0, 0, 100, 50);

        using var path = DrawEngine.CreateRoundedPath(rect, 0.1f);

        Assert.NotNull(path);
        Assert.True(path.PointCount > 0);
    }

    [Fact]
    public void CreateRoundedPath_FigureIsClosed()
    {
        Rectangle rect = new(0, 0, 100, 100);

        using var path = DrawEngine.CreateRoundedPath(rect, 20f);

        var bounds = path.GetBounds();
        Assert.True(bounds.Width > 0);
        Assert.True(bounds.Height > 0);
    }

    [Fact]
    public void CreateRoundedPath_SmallRectangle_ReturnsValidPath()
    {
        Rectangle rect = new(0, 0, 10, 10);

        using var path = DrawEngine.CreateRoundedPath(rect, 5f);

        Assert.NotNull(path);
        Assert.True(path.PointCount > 0);
    }

    [Fact]
    public void CreateRoundedPath_LargeAngle_ReturnsValidPath()
    {
        Rectangle rect = new(0, 0, 200, 200);

        using var path = DrawEngine.CreateRoundedPath(rect, 100f);

        Assert.NotNull(path);
        Assert.True(path.PointCount > 0);
    }

    #endregion

    #region SetGlobalRgbTimer Tests

    [Fact]
    public void SetGlobalRgbTimer_Enable_StartsTimer()
    {
        DrawEngine.SetGlobalRgbTimer(true);

        Assert.True(DrawEngine.GlobalRgbTimer.Enabled);

        // Cleanup
        DrawEngine.SetGlobalRgbTimer(false);
    }

    [Fact]
    public void SetGlobalRgbTimer_Disable_StopsTimer()
    {
        DrawEngine.SetGlobalRgbTimer(true);
        DrawEngine.SetGlobalRgbTimer(false);

        Assert.False(DrawEngine.GlobalRgbTimer.Enabled);
    }

    [Fact]
    public void GlobalRgbTimer_DefaultInterval_Is50()
    {
        Assert.Equal(50, DrawEngine.GlobalRgbTimer.Interval);
    }

    #endregion
}
