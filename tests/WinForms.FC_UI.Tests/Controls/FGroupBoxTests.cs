using System.Drawing.Drawing2D;
using FC_UI.Controls;

namespace WinForms.FC_UI.Tests.Controls;

public class FGroupBoxTests : IDisposable
{
    private readonly FGroupBox _groupBox;

    public FGroupBoxTests()
    {
        _groupBox = new FGroupBox();
    }

    public void Dispose()
    {
        _groupBox.Dispose();
    }

    [Fact]
    public void Constructor_DefaultStyle_SetsExpectedSize()
    {
        Assert.Equal(150, _groupBox.Width);
        Assert.Equal(130, _groupBox.Height);
    }

    [Fact]
    public void Constructor_DefaultStyle_ShowBackgroundIsTrue()
    {
        Assert.True(_groupBox.ShowBackground);
    }

    [Fact]
    public void Constructor_DefaultStyle_RoundingIsTrue()
    {
        Assert.True(_groupBox.Rounding);
    }

    [Fact]
    public void Constructor_DefaultStyle_CornerRadiusIs60()
    {
        Assert.Equal(60, _groupBox.CornerRadius);
    }

    [Fact]
    public void Constructor_DefaultStyle_RGBIsFalse()
    {
        Assert.False(_groupBox.Rgb);
    }

    [Fact]
    public void Constructor_DefaultStyle_ShowBorderIsTrue()
    {
        Assert.True(_groupBox.ShowBorder);
    }

    [Fact]
    public void Constructor_DefaultStyle_LightingIsFalse()
    {
        Assert.False(_groupBox.Lighting);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(50)]
    [InlineData(100)]
    public void CornerRadius_ValidValues_AreAccepted(int value)
    {
        _groupBox.CornerRadius = value;

        Assert.Equal(value, _groupBox.CornerRadius);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(101)]
    public void CornerRadius_InvalidValues_AreRejected(int value)
    {
        var original = _groupBox.CornerRadius;
        _groupBox.CornerRadius = value;

        Assert.Equal(original, _groupBox.CornerRadius);
    }

    [Fact]
    public void BackgroundColor_SetValue_ReturnsSetValue()
    {
        _groupBox.BackgroundColor = Color.DarkGreen;

        Assert.Equal(Color.DarkGreen, _groupBox.BackgroundColor);
    }

    [Fact]
    public void SmoothingMode_InvalidValue_IsRejected()
    {
        _groupBox.SmoothingMode = SmoothingMode.HighQuality;
        _groupBox.SmoothingMode = SmoothingMode.Invalid;

        Assert.Equal(SmoothingMode.HighQuality, _groupBox.SmoothingMode);
    }

    [Fact]
    public void Tag_IsSetToFC_UI()
    {
        Assert.Equal("FC_UI", _groupBox.Tag);
    }
}
