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
    public void Constructor_DefaultStyle_ShowBackgroundIsTrue()
    {
        Assert.True(_switchBox.ShowBackground);
    }

    [Fact]
    public void Constructor_DefaultStyle_RoundingIsTrue()
    {
        Assert.True(_switchBox.Rounding);
    }

    [Fact]
    public void Constructor_DefaultStyle_CornerRadiusIs90()
    {
        Assert.Equal(90, _switchBox.CornerRadius);
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
        var eventFired = false;
        _switchBox.CheckedChanged += () => eventFired = true;

        _switchBox.Checked = true;

        Assert.True(eventFired);
    }

    [Fact]
    public void Checked_SetSameValue_DoesNotRaiseCheckedChanged()
    {
        var eventFired = false;
        _switchBox.CheckedChanged += () => eventFired = true;

        _switchBox.Checked = false;

        Assert.False(eventFired);
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
    public void CornerRadius_ValidValues_AreAccepted(int value)
    {
        _switchBox.CornerRadius = value;

        Assert.Equal(value, _switchBox.CornerRadius);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(101)]
    public void CornerRadius_InvalidValues_AreRejected(int value)
    {
        var original = _switchBox.CornerRadius;
        _switchBox.CornerRadius = value;

        Assert.Equal(original, _switchBox.CornerRadius);
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

    [Fact]
    public void RightClick_DoesNotToggleChecked()
    {
        using var switchBox = new ClickableFSwitchBox();

        switchBox.InvokeMouseClick(MouseButtons.Right);

        Assert.False(switchBox.Checked);
    }

    [Fact]
    public void Checked_NoHandle_MovesKnobImmediately()
    {
        _switchBox.Checked = true;

        Assert.False(_switchBox.IsToggleAnimationRunning);
        Assert.Equal(1F, _switchBox.KnobPosition);
    }

    [Fact]
    public void Checked_HandleCreated_AnimatesKnobToTarget()
    {
        _ = _switchBox.Handle;

        _switchBox.Checked = true;
        Assert.True(_switchBox.IsToggleAnimationRunning);
        Assert.Equal(0F, _switchBox.KnobPosition);

        for (var i = 0; i < 20 && _switchBox.IsToggleAnimationRunning; i++) _switchBox.StepToggleAnimation();

        Assert.False(_switchBox.IsToggleAnimationRunning);
        Assert.Equal(1F, _switchBox.KnobPosition);
    }

    [Fact]
    public void SpaceKeyUp_Unchecked_ChecksControl()
    {
        using ClickableFSwitchBox switchBox = new();

        switchBox.InvokeKeyUp(Keys.Space);

        Assert.True(switchBox.Checked);
    }

    [Fact]
    public void AccessibilityObject_Checked_ReportsCheckButtonState()
    {
        _switchBox.Checked = true;

        var accessible = _switchBox.AccessibilityObject;

        Assert.Equal(AccessibleRole.CheckButton, accessible.Role);
        Assert.True(accessible.State.HasFlag(AccessibleStates.Checked));
    }

    private sealed class ClickableFSwitchBox : FSwitchBox
    {
        public void InvokeMouseClick(MouseButtons button) =>
            OnMouseClick(new MouseEventArgs(button, 1, 0, 0, 0));

        public void InvokeKeyUp(Keys key) => OnKeyUp(new KeyEventArgs(key));
    }
}
