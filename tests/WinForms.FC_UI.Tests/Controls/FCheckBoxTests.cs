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
    public void Checked_SetSameValue_DoesNotRaiseCheckedChanged()
    {
        var eventFired = false;
        _checkBox.CheckedChanged += () => eventFired = true;

        _checkBox.Checked = false;

        Assert.False(eventFired);
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

    [Fact]
    public void RightClick_DoesNotToggleChecked()
    {
        using var checkBox = new ClickableFCheckBox();

        checkBox.InvokeMouseClick(MouseButtons.Right);

        Assert.False(checkBox.Checked);
    }

    [Fact]
    public void StepClickAnimation_RippleReachesMaxSize_StopsTimer()
    {
        using var checkBox = new ClickableFCheckBox();
        checkBox.InvokeMouseClick(MouseButtons.Left);
        Assert.True(checkBox.IsClickAnimationRunning);

        for (var i = 0; i < 100 && checkBox.IsClickAnimationRunning; i++) checkBox.StepClickAnimation();

        Assert.False(checkBox.IsClickAnimationRunning);
    }

    [Fact]
    public void LeftClick_CheckedControl_UnchecksWithoutAnimation()
    {
        using var checkBox = new ClickableFCheckBox { Checked = true };

        checkBox.InvokeMouseClick(MouseButtons.Left);

        Assert.False(checkBox.Checked);
        Assert.False(checkBox.IsClickAnimationRunning);
    }

    [Fact]
    public void Height_SetDifferentValue_StaysFixedAndRaisesSizeChangedOnce()
    {
        var sizeChangedCount = 0;
        _checkBox.SizeChanged += (_, _) => sizeChangedCount++;

        _checkBox.Size = new Size(200, 100);

        Assert.Equal(new Size(200, 45), _checkBox.Size);
        Assert.Equal(1, sizeChangedCount);
    }

    private sealed class ClickableFCheckBox : FCheckBox
    {
        public void InvokeMouseClick(MouseButtons button) =>
            OnMouseClick(new MouseEventArgs(button, 1, 0, 0, 0));
    }
}
