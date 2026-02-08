using System.Drawing;
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
    public void Constructor_DefaultStyle_BackgroundIsTrue()
    {
        Assert.True(_groupBox.Background);
    }

    [Fact]
    public void Constructor_DefaultStyle_RoundingIsTrue()
    {
        Assert.True(_groupBox.Rounding);
    }

    [Fact]
    public void Constructor_DefaultStyle_RoundingIntIs60()
    {
        Assert.Equal(60, _groupBox.RoundingInt);
    }

    [Fact]
    public void Constructor_DefaultStyle_RGBIsFalse()
    {
        Assert.False(_groupBox.RGB);
    }

    [Fact]
    public void Constructor_DefaultStyle_BackgroundPenIsTrue()
    {
        Assert.True(_groupBox.BackgroundPen);
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
    public void RoundingInt_ValidValues_AreAccepted(int value)
    {
        _groupBox.RoundingInt = value;

        Assert.Equal(value, _groupBox.RoundingInt);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(101)]
    public void RoundingInt_InvalidValues_AreRejected(int value)
    {
        int original = _groupBox.RoundingInt;
        _groupBox.RoundingInt = value;

        Assert.Equal(original, _groupBox.RoundingInt);
    }

    [Fact]
    public void ColorBackground_SetValue_ReturnsSetValue()
    {
        _groupBox.ColorBackground = Color.DarkGreen;

        Assert.Equal(Color.DarkGreen, _groupBox.ColorBackground);
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
