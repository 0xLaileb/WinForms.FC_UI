using System.Drawing.Drawing2D;
using FC_UI.Controls;

namespace WinForms.FC_UI.Tests.Controls;

public class FCheckBoxTests : IDisposable
{
    private readonly FCheckBox _checkBox;

    public FCheckBoxTests()
    {
        _checkBox = new FCheckBox();
    }

    public void Dispose()
    {
        _checkBox.Dispose();
    }

    [Fact]
    public void Constructor_DefaultStyle_CheckedIsFalse()
    {
        Assert.False(_checkBox.Checked);
    }

    [Fact]
    public void Constructor_DefaultStyle_SetsDisplayText()
    {
        Assert.Equal("FCheckBox", _checkBox.DisplayText);
    }

    [Fact]
    public void Constructor_DefaultStyle_ShowBackgroundIsTrue()
    {
        Assert.True(_checkBox.ShowBackground);
    }

    [Fact]
    public void Constructor_DefaultStyle_RoundingIsTrue()
    {
        Assert.True(_checkBox.Rounding);
    }

    [Fact]
    public void Constructor_DefaultStyle_CornerRadiusIs100()
    {
        Assert.Equal(100, _checkBox.CornerRadius);
    }

    [Fact]
    public void Checked_Toggle_RaisesCheckedChanged()
    {
        var eventFired = false;
        _checkBox.CheckedChanged += () => eventFired = true;

        _checkBox.Checked = true;

        Assert.True(eventFired);
    }

    [Fact]
    public void Checked_SetTrue_ReturnsTrue()
    {
        _checkBox.Checked = true;

        Assert.True(_checkBox.Checked);
    }

    [Fact]
    public void Checked_SetFalse_ReturnsFalse()
    {
        _checkBox.Checked = true;
        _checkBox.Checked = false;

        Assert.False(_checkBox.Checked);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(50)]
    [InlineData(100)]
    public void CornerRadius_ValidValues_AreAccepted(int value)
    {
        _checkBox.CornerRadius = value;

        Assert.Equal(value, _checkBox.CornerRadius);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(101)]
    public void CornerRadius_InvalidValues_AreRejected(int value)
    {
        var original = _checkBox.CornerRadius;
        _checkBox.CornerRadius = value;

        Assert.Equal(original, _checkBox.CornerRadius);
    }

    [Fact]
    public void ColorChecked_SetValue_ReturnsSetValue()
    {
        _checkBox.ColorChecked = Color.Green;

        Assert.Equal(Color.Green, _checkBox.ColorChecked);
    }

    [Fact]
    public void SmoothingMode_InvalidValue_IsRejected()
    {
        _checkBox.SmoothingMode = SmoothingMode.HighQuality;
        _checkBox.SmoothingMode = SmoothingMode.Invalid;

        Assert.Equal(SmoothingMode.HighQuality, _checkBox.SmoothingMode);
    }

    [Fact]
    public void Tag_IsSetToFC_UI()
    {
        Assert.Equal("FC_UI", _checkBox.Tag);
    }
}
