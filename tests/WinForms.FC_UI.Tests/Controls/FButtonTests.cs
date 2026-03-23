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
    public void Constructor_DefaultStyle_SetsDisplayText()
    {
        Assert.Equal("FButton", _button.DisplayText);
    }

    [Fact]
    public void Constructor_DefaultStyle_ShowBackgroundIsTrue()
    {
        Assert.True(_button.ShowBackground);
    }

    [Fact]
    public void Constructor_DefaultStyle_RoundingIsTrue()
    {
        Assert.True(_button.Rounding);
    }

    [Fact]
    public void Constructor_DefaultStyle_CornerRadiusIs70()
    {
        Assert.Equal(70, _button.CornerRadius);
    }

    [Fact]
    public void Constructor_DefaultStyle_RGBIsFalse()
    {
        Assert.False(_button.Rgb);
    }

    [Fact]
    public void Constructor_DefaultStyle_EnableClickEffectIsTrue()
    {
        Assert.True(_button.EnableClickEffect);
    }

    [Fact]
    public void Constructor_DefaultStyle_EnableHoverEffectIsTrue()
    {
        Assert.True(_button.EnableHoverEffect);
    }

    [Fact]
    public void Constructor_DefaultStyle_ShowBorderIsTrue()
    {
        Assert.True(_button.ShowBorder);
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
    public void CornerRadius_ValidValues_AreAccepted(int value)
    {
        _button.CornerRadius = value;

        Assert.Equal(value, _button.CornerRadius);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(101)]
    [InlineData(200)]
    public void CornerRadius_InvalidValues_AreRejected(int value)
    {
        var original = _button.CornerRadius;

        _button.CornerRadius = value;

        Assert.Equal(original, _button.CornerRadius);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(128)]
    [InlineData(255)]
    public void ClickEffectOpacity_ValidValues_AreAccepted(int value)
    {
        _button.ClickEffectOpacity = value;

        Assert.Equal(value, _button.ClickEffectOpacity);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(256)]
    [InlineData(-1)]
    public void ClickEffectOpacity_InvalidValues_AreRejected(int value)
    {
        _button.ClickEffectOpacity = 100;
        _button.ClickEffectOpacity = value;

        Assert.Equal(100, _button.ClickEffectOpacity);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(128)]
    [InlineData(255)]
    public void HoverEffectOpacity_ValidValues_AreAccepted(int value)
    {
        _button.HoverEffectOpacity = value;

        Assert.Equal(value, _button.HoverEffectOpacity);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(256)]
    public void HoverEffectOpacity_InvalidValues_AreRejected(int value)
    {
        _button.HoverEffectOpacity = 100;
        _button.HoverEffectOpacity = value;

        Assert.Equal(100, _button.HoverEffectOpacity);
    }

    #endregion

    #region Property Setter Tests

    [Fact]
    public void BackgroundColor_SetValue_ReturnsSetValue()
    {
        var expected = Color.Red;

        _button.BackgroundColor = expected;

        Assert.Equal(expected, _button.BackgroundColor);
    }

    [Fact]
    public void BorderColor_SetValue_ReturnsSetValue()
    {
        var expected = Color.Blue;

        _button.BorderColor = expected;

        Assert.Equal(expected, _button.BorderColor);
    }

    [Fact]
    public void BorderWidth_SetValue_ReturnsSetValue()
    {
        _button.BorderWidth = 5.5F;

        Assert.Equal(5.5F, _button.BorderWidth);
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
