using System.Drawing;
using FC_UI.Controls;

namespace WinForms.FC_UI.Tests.Controls;

public class FSwitchBoxTests : IDisposable
{
    private readonly FSwitchBox _switchBox;

    public FSwitchBoxTests()
    {
        _switchBox = new FSwitchBox();
    }

    public void Dispose()
    {
        _switchBox.Dispose();
    }

    [Fact]
    public void Constructor_DefaultStyle_CheckedIsFalse()
    {
        Assert.False(_switchBox.Checked);
    }

    [Fact]
    public void Constructor_DefaultStyle_BackgroundIsTrue()
    {
        Assert.True(_switchBox.Background);
    }

    [Fact]
    public void Constructor_DefaultStyle_RoundingIsTrue()
    {
        Assert.True(_switchBox.Rounding);
    }

    [Fact]
    public void Constructor_DefaultStyle_RoundingIntIs90()
    {
        Assert.Equal(90, _switchBox.RoundingInt);
    }

    [Fact]
    public void Constructor_DefaultStyle_SetsExpectedSize()
    {
        Assert.Equal(35, _switchBox.Width);
        Assert.Equal(20, _switchBox.Height);
    }

    [Fact]
    public void Checked_Toggle_RaisesCheckedChanged()
    {
        bool eventFired = false;
        _switchBox.CheckedChanged += () => eventFired = true;

        _switchBox.Checked = true;

        Assert.True(eventFired);
    }

    [Fact]
    public void Checked_SetTrue_ReturnsTrue()
    {
        _switchBox.Checked = true;

        Assert.True(_switchBox.Checked);
    }

    [Fact]
    public void Checked_SetFalse_ReturnsFalse()
    {
        _switchBox.Checked = true;
        _switchBox.Checked = false;

        Assert.False(_switchBox.Checked);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(45)]
    [InlineData(100)]
    public void RoundingInt_ValidValues_AreAccepted(int value)
    {
        _switchBox.RoundingInt = value;

        Assert.Equal(value, _switchBox.RoundingInt);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(101)]
    public void RoundingInt_InvalidValues_AreRejected(int value)
    {
        int original = _switchBox.RoundingInt;
        _switchBox.RoundingInt = value;

        Assert.Equal(original, _switchBox.RoundingInt);
    }

    [Fact]
    public void ColorValue_SetValue_ReturnsSetValue()
    {
        _switchBox.ColorValue = Color.Purple;

        Assert.Equal(Color.Purple, _switchBox.ColorValue);
    }

    [Fact]
    public void Tag_IsSetToFC_UI()
    {
        Assert.Equal("FC_UI", _switchBox.Tag);
    }
}
