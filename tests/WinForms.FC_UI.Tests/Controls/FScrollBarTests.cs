using FC_UI.Controls;

namespace WinForms.FC_UI.Tests.Controls;

public class FScrollBarTests : IDisposable
{
    private readonly FScrollBar _scrollBar;

    public FScrollBarTests()
    {
        _scrollBar = new FScrollBar();
    }

    public void Dispose()
    {
        _scrollBar.Dispose();
    }

    [Fact]
    public void Constructor_DefaultStyle_ValueIs0()
    {
        Assert.Equal(0, _scrollBar.Value);
    }

    [Fact]
    public void Constructor_DefaultStyle_MinimumIs0()
    {
        Assert.Equal(0, _scrollBar.Minimum);
    }

    [Fact]
    public void Constructor_DefaultStyle_MaximumIs100()
    {
        Assert.Equal(100, _scrollBar.Maximum);
    }

    [Fact]
    public void Constructor_DefaultStyle_SmallStepIs1()
    {
        Assert.Equal(1, _scrollBar.SmallStep);
    }

    [Fact]
    public void Constructor_DefaultStyle_ThumbSizeIs60()
    {
        Assert.Equal(60, _scrollBar.ThumbSize);
    }

    [Fact]
    public void Value_SetValid_ReturnsSetValue()
    {
        _scrollBar.Value = 50;

        Assert.Equal(50, _scrollBar.Value);
    }

    [Fact]
    public void Value_Changed_RaisesValueChangedEvent()
    {
        var eventFired = false;
        _scrollBar.ValueChanged += (_, _) => eventFired = true;

        _scrollBar.Value = 10;

        Assert.True(eventFired);
    }

    [Fact]
    public void Value_SameValue_DoesNotRaiseEvent()
    {
        _scrollBar.Value = 0;
        var eventFired = false;
        _scrollBar.ValueChanged += (_, _) => eventFired = true;

        _scrollBar.Value = 0;

        Assert.False(eventFired);
    }

    [Fact]
    public void Maximum_GreaterThanMinimum_IsAccepted()
    {
        _scrollBar.Maximum = 200;

        Assert.Equal(200, _scrollBar.Maximum);
    }

    [Fact]
    public void Maximum_LessThanMinimum_IsRejected()
    {
        _scrollBar.Maximum = 100;
        _scrollBar.Maximum = -1;

        Assert.Equal(100, _scrollBar.Maximum);
    }

    [Fact]
    public void Minimum_LessThanMaximum_IsAccepted()
    {
        _scrollBar.Minimum = 10;

        Assert.Equal(10, _scrollBar.Minimum);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(50)]
    [InlineData(100)]
    public void CornerRadius_ValidValues_AreAccepted(int value)
    {
        _scrollBar.CornerRadius = value;

        Assert.Equal(value, _scrollBar.CornerRadius);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(101)]
    public void CornerRadius_InvalidValues_AreRejected(int value)
    {
        var original = _scrollBar.CornerRadius;
        _scrollBar.CornerRadius = value;

        Assert.Equal(original, _scrollBar.CornerRadius);
    }

    [Fact]
    public void ThumbColor_SetValue_ReturnsSetValue()
    {
        _scrollBar.ThumbColor = Color.Green;

        Assert.Equal(Color.Green, _scrollBar.ThumbColor);
    }

    [Fact]
    public void ThumbOpacity_ValidRange_IsAccepted()
    {
        _scrollBar.ThumbOpacity = 128;

        Assert.Equal(128, _scrollBar.ThumbOpacity);
    }

    [Fact]
    public void ThumbOpacity_BelowMin_IsRejected()
    {
        _scrollBar.ThumbOpacity = 100;
        _scrollBar.ThumbOpacity = 5;

        Assert.Equal(100, _scrollBar.ThumbOpacity);
    }

    [Fact]
    public void Tag_IsSetToFC_UI()
    {
        Assert.Equal("FC_UI", _scrollBar.Tag);
    }
}
