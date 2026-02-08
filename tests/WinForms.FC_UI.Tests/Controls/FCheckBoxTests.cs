using System.Drawing;
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
    public void Constructor_DefaultStyle_SetsTextButton()
    {
        Assert.Equal("FCheckBox", _checkBox.TextButton);
    }

    [Fact]
    public void Constructor_DefaultStyle_BackgroundIsTrue()
    {
        Assert.True(_checkBox.Background);
    }

    [Fact]
    public void Constructor_DefaultStyle_RoundingIsTrue()
    {
        Assert.True(_checkBox.Rounding);
    }

    [Fact]
    public void Constructor_DefaultStyle_RoundingIntIs100()
    {
        Assert.Equal(100, _checkBox.RoundingInt);
    }

    [Fact]
    public void Checked_Toggle_RaisesCheckedChanged()
    {
        bool eventFired = false;
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
    public void RoundingInt_ValidValues_AreAccepted(int value)
    {
        _checkBox.RoundingInt = value;

        Assert.Equal(value, _checkBox.RoundingInt);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(101)]
    public void RoundingInt_InvalidValues_AreRejected(int value)
    {
        int original = _checkBox.RoundingInt;
        _checkBox.RoundingInt = value;

        Assert.Equal(original, _checkBox.RoundingInt);
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
