using FC_UI.Controls;

namespace WinForms.FC_UI.Tests.Controls;

public class FProgressBarTests : IDisposable
{
    private readonly FProgressBar _progressBar;

    public FProgressBarTests()
    {
        _progressBar = new FProgressBar();
    }

    public void Dispose()
    {
        _progressBar.Dispose();
    }

    [Fact]
    public void Constructor_DefaultStyle_ValueIs0()
    {
        Assert.Equal(0, _progressBar.Value);
    }

    [Fact]
    public void Constructor_DefaultStyle_MinimumIs0()
    {
        Assert.Equal(0, _progressBar.Minimum);
    }

    [Fact]
    public void Constructor_DefaultStyle_MaximumIs100()
    {
        Assert.Equal(100, _progressBar.Maximum);
    }

    [Fact]
    public void Constructor_DefaultStyle_ProgressTextIsTrue()
    {
        Assert.True(_progressBar.ProgressText);
    }

    [Fact]
    public void Constructor_DefaultStyle_ShowBackgroundIsTrue()
    {
        Assert.True(_progressBar.ShowBackground);
    }

    [Fact]
    public void Value_WithinRange_IsAccepted()
    {
        _progressBar.Value = 50;

        Assert.Equal(50, _progressBar.Value);
    }

    [Fact]
    public void Value_AboveMaximum_IsRejected()
    {
        _progressBar.Value = 50;
        _progressBar.Value = 101;

        Assert.Equal(50, _progressBar.Value);
    }

    [Fact]
    public void Value_BelowMinimum_IsRejected()
    {
        _progressBar.Value = 50;
        _progressBar.Value = -1;

        Assert.Equal(50, _progressBar.Value);
    }

    [Fact]
    public void Value_AtMinimum_IsAccepted()
    {
        _progressBar.Value = 0;

        Assert.Equal(0, _progressBar.Value);
    }

    [Fact]
    public void Value_AtMaximum_IsAccepted()
    {
        _progressBar.Value = 100;

        Assert.Equal(100, _progressBar.Value);
    }

    [Fact]
    public void Maximum_GreaterThanMinimum_IsAccepted()
    {
        _progressBar.Maximum = 200;

        Assert.Equal(200, _progressBar.Maximum);
    }

    [Fact]
    public void Maximum_LessThanMinimum_IsRejected()
    {
        _progressBar.Maximum = 100;
        _progressBar.Maximum = -1;

        Assert.Equal(100, _progressBar.Maximum);
    }

    [Fact]
    public void Minimum_LessThanMaximum_IsAccepted()
    {
        _progressBar.Minimum = 10;

        Assert.Equal(10, _progressBar.Minimum);
    }

    [Fact]
    public void Minimum_AboveCurrentValue_ClampsValueToMinimum()
    {
        _progressBar.Minimum = 10;

        Assert.Equal(10, _progressBar.Value);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(50)]
    [InlineData(100)]
    public void CornerRadius_ValidValues_AreAccepted(int value)
    {
        _progressBar.CornerRadius = value;

        Assert.Equal(value, _progressBar.CornerRadius);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(101)]
    public void CornerRadius_InvalidValues_AreRejected(int value)
    {
        var original = _progressBar.CornerRadius;
        _progressBar.CornerRadius = value;

        Assert.Equal(original, _progressBar.CornerRadius);
    }

    [Fact]
    public void FillColor_SetValue_ReturnsSetValue()
    {
        _progressBar.FillColor = Color.Red;

        Assert.Equal(Color.Red, _progressBar.FillColor);
    }

    [Fact]
    public void Tag_IsSetToFC_UI()
    {
        Assert.Equal("FC_UI", _progressBar.Tag);
    }

    [Fact]
    public void TabStop_Default_IsFalse()
    {
        Assert.False(_progressBar.TabStop);
    }

    [Fact]
    public void AccessibilityObject_Value_ReportsProgressPercent()
    {
        _progressBar.Value = 50;

        var accessible = _progressBar.AccessibilityObject;

        Assert.Equal(AccessibleRole.ProgressBar, accessible.Role);
        Assert.Equal("50%", accessible.Value);
    }

    [Fact]
    public void Value_AnimationDisabled_DrawsNewValueImmediately()
    {
        _ = _progressBar.Handle;

        _progressBar.Value = 80;

        Assert.False(_progressBar.IsValueAnimationRunning);
        Assert.Equal(80, _progressBar.DisplayedValue);
    }

    [Fact]
    public void Value_AnimationEnabled_ReachesTargetAndStops()
    {
        _ = _progressBar.Handle;
        _progressBar.EnableValueAnimation = true;

        _progressBar.Value = 80;
        Assert.True(_progressBar.IsValueAnimationRunning);
        Assert.Equal(80, _progressBar.Value);

        for (var i = 0; i < 200 && _progressBar.IsValueAnimationRunning; i++) _progressBar.StepValueAnimation();

        Assert.False(_progressBar.IsValueAnimationRunning);
        Assert.Equal(80, _progressBar.DisplayedValue);
    }
}
