using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using FC_UI.Controls;

namespace WinForms.FC_UI.Tests.Controls;

public class FButtonTests : IDisposable
{
    private readonly FButton _button;

    public FButtonTests()
    {
        _button = new FButton();
    }

    public void Dispose()
    {
        _button.Dispose();
    }

    #region Default Style Tests

    [Fact]
    public void Constructor_DefaultStyle_SetsExpectedSize()
    {
        Assert.Equal(130, _button.Width);
        Assert.Equal(50, _button.Height);
    }

    [Fact]
    public void Constructor_DefaultStyle_SetsTextButton()
    {
        Assert.Equal("FButton", _button.TextButton);
    }

    [Fact]
    public void Constructor_DefaultStyle_BackgroundIsTrue()
    {
        Assert.True(_button.Background);
    }

    [Fact]
    public void Constructor_DefaultStyle_RoundingIsTrue()
    {
        Assert.True(_button.Rounding);
    }

    [Fact]
    public void Constructor_DefaultStyle_RoundingIntIs70()
    {
        Assert.Equal(70, _button.RoundingInt);
    }

    [Fact]
    public void Constructor_DefaultStyle_RGBIsFalse()
    {
        Assert.False(_button.RGB);
    }

    [Fact]
    public void Constructor_DefaultStyle_Effect1IsTrue()
    {
        Assert.True(_button.Effect_1);
    }

    [Fact]
    public void Constructor_DefaultStyle_Effect2IsTrue()
    {
        Assert.True(_button.Effect_2);
    }

    [Fact]
    public void Constructor_DefaultStyle_BackgroundPenIsTrue()
    {
        Assert.True(_button.BackgroundPen);
    }

    [Fact]
    public void Constructor_DefaultStyle_SmoothingModeIsHighQuality()
    {
        Assert.Equal(SmoothingMode.HighQuality, _button.SmoothingMode);
    }

    [Fact]
    public void Constructor_DefaultStyle_TextRenderingHintIsClearType()
    {
        Assert.Equal(TextRenderingHint.ClearTypeGridFit, _button.TextRenderingHint);
    }

    #endregion

    #region Property Validation Tests

    [Theory]
    [InlineData(0)]
    [InlineData(50)]
    [InlineData(100)]
    public void RoundingInt_ValidValues_AreAccepted(int value)
    {
        _button.RoundingInt = value;

        Assert.Equal(value, _button.RoundingInt);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(101)]
    [InlineData(200)]
    public void RoundingInt_InvalidValues_AreRejected(int value)
    {
        int original = _button.RoundingInt;

        _button.RoundingInt = value;

        Assert.Equal(original, _button.RoundingInt);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(128)]
    [InlineData(255)]
    public void Effect1Transparency_ValidValues_AreAccepted(int value)
    {
        _button.Effect_1_Transparency = value;

        Assert.Equal(value, _button.Effect_1_Transparency);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(256)]
    [InlineData(-1)]
    public void Effect1Transparency_InvalidValues_AreRejected(int value)
    {
        _button.Effect_1_Transparency = 100;
        _button.Effect_1_Transparency = value;

        Assert.Equal(100, _button.Effect_1_Transparency);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(128)]
    [InlineData(255)]
    public void Effect2Transparency_ValidValues_AreAccepted(int value)
    {
        _button.Effect_2_Transparency = value;

        Assert.Equal(value, _button.Effect_2_Transparency);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(256)]
    public void Effect2Transparency_InvalidValues_AreRejected(int value)
    {
        _button.Effect_2_Transparency = 100;
        _button.Effect_2_Transparency = value;

        Assert.Equal(100, _button.Effect_2_Transparency);
    }

    #endregion

    #region Property Setter Tests

    [Fact]
    public void ColorBackground_SetValue_ReturnsSetValue()
    {
        Color expected = Color.Red;

        _button.ColorBackground = expected;

        Assert.Equal(expected, _button.ColorBackground);
    }

    [Fact]
    public void ColorBackgroundPen_SetValue_ReturnsSetValue()
    {
        Color expected = Color.Blue;

        _button.ColorBackground_Pen = expected;

        Assert.Equal(expected, _button.ColorBackground_Pen);
    }

    [Fact]
    public void BackgroundWidthPen_SetValue_ReturnsSetValue()
    {
        _button.Background_WidthPen = 5.5F;

        Assert.Equal(5.5F, _button.Background_WidthPen);
    }

    [Fact]
    public void SmoothingMode_InvalidValue_IsRejected()
    {
        _button.SmoothingMode = SmoothingMode.HighQuality;
        _button.SmoothingMode = SmoothingMode.Invalid;

        Assert.Equal(SmoothingMode.HighQuality, _button.SmoothingMode);
    }

    [Fact]
    public void Tag_IsSetToFC_UI()
    {
        Assert.Equal("FC_UI", _button.Tag);
    }

    #endregion
}
