using FC_UI.Controls;

namespace WinForms.FC_UI.Tests.Controls;

public class FRadioButtonTests : IDisposable
{
    private readonly FRadioButton _radioButton;

    public FRadioButtonTests()
    {
        _radioButton = new FRadioButton();
    }

    public void Dispose()
    {
        _radioButton.Dispose();
    }

    [Fact]
    public void Constructor_DefaultStyle_CheckedIsFalse()
    {
        Assert.False(_radioButton.Checked);
    }

    [Fact]
    public void Checked_SetSameValue_DoesNotRaiseCheckedChanged()
    {
        var eventFired = false;
        _radioButton.CheckedChanged += () => eventFired = true;

        _radioButton.Checked = false;

        Assert.False(eventFired);
    }

    [Fact]
    public void RightClick_DoesNotToggleChecked()
    {
        using var radioButton = new ClickableFRadioButton();

        radioButton.InvokeMouseClick(MouseButtons.Right);

        Assert.False(radioButton.Checked);
    }

    [Fact]
    public void StepClickAnimation_RippleReachesMaxSize_StopsTimer()
    {
        using var radioButton = new ClickableFRadioButton();
        radioButton.InvokeMouseClick(MouseButtons.Left);
        Assert.True(radioButton.IsClickAnimationRunning);

        for (var i = 0; i < 100 && radioButton.IsClickAnimationRunning; i++) radioButton.StepClickAnimation();

        Assert.False(radioButton.IsClickAnimationRunning);
    }

    [Fact]
    public void LeftClick_CheckedControl_StaysChecked()
    {
        using var radioButton = new ClickableFRadioButton { Checked = true };

        radioButton.InvokeMouseClick(MouseButtons.Left);

        Assert.True(radioButton.Checked);
        Assert.False(radioButton.IsClickAnimationRunning);
    }

    [Fact]
    public void LeftClick_AutoCheckDisabled_DoesNotCheck()
    {
        using var radioButton = new ClickableFRadioButton { AutoCheck = false };

        radioButton.InvokeMouseClick(MouseButtons.Left);

        Assert.False(radioButton.Checked);
    }

    [Fact]
    public void Checked_SetTrue_UnchecksSiblingsInSameContainer()
    {
        using Panel panel = new();
        FRadioButton first = new() { Checked = true };
        FRadioButton second = new();
        FRadioButton manual = new() { AutoCheck = false, Checked = true };
        panel.Controls.AddRange([first, second, manual]);

        second.Checked = true;

        Assert.False(first.Checked);
        Assert.True(second.Checked);
        Assert.True(manual.Checked);
    }

    [Fact]
    public void Checked_SetTrue_DoesNotAffectOtherContainers()
    {
        using Panel left = new();
        using Panel right = new();
        FRadioButton leftButton = new() { Checked = true };
        FRadioButton rightButton = new();
        left.Controls.Add(leftButton);
        right.Controls.Add(rightButton);

        rightButton.Checked = true;

        Assert.True(leftButton.Checked);
    }

    [Fact]
    public void DownArrow_InGroup_SelectsNextButton()
    {
        using Panel panel = new();
        ClickableFRadioButton first = new() { TabIndex = 0, Checked = true };
        ClickableFRadioButton second = new() { TabIndex = 1 };
        panel.Controls.AddRange([first, second]);

        first.InvokeKeyDown(Keys.Down);

        Assert.False(first.Checked);
        Assert.True(second.Checked);
    }

    [Fact]
    public void IsInputKey_ArrowWithoutGroup_LeftToFocusNavigation()
    {
        using ClickableFRadioButton radioButton = new();

        Assert.False(radioButton.IsInput(Keys.Down));
    }

    [Fact]
    public void SpaceKeyUp_Unchecked_ChecksControl()
    {
        using ClickableFRadioButton radioButton = new();

        radioButton.InvokeKeyUp(Keys.Space);

        Assert.True(radioButton.Checked);
    }

    [Fact]
    public void Height_SetDoubleValue_ScalesBoxAndRaisesSizeChangedOnce()
    {
        using ClickableFRadioButton radioButton = new();
        var sizeChangedCount = 0;
        radioButton.SizeChanged += (_, _) => sizeChangedCount++;

        radioButton.Size = new Size(200, 90);

        Assert.Equal(new Size(200, 90), radioButton.Size);
        Assert.Equal(1, sizeChangedCount);
        Assert.Equal(new Size(42, 42), radioButton.Box.Size);
    }

    [Fact]
    public void AccessibilityObject_Checked_ReportsRoleAndState()
    {
        _radioButton.Checked = true;

        var accessible = _radioButton.AccessibilityObject;

        Assert.Equal(AccessibleRole.RadioButton, accessible.Role);
        Assert.Equal("FRadioButton", accessible.Name);
        Assert.True(accessible.State.HasFlag(AccessibleStates.Checked));
    }

    private sealed class ClickableFRadioButton : FRadioButton
    {
        public void InvokeMouseClick(MouseButtons button) =>
            OnMouseClick(new MouseEventArgs(button, 1, 0, 0, 0));

        public void InvokeKeyUp(Keys key) => OnKeyUp(new KeyEventArgs(key));

        public void InvokeKeyDown(Keys key) => OnKeyDown(new KeyEventArgs(key));

        public bool IsInput(Keys key) => IsInputKey(key);

        public Rectangle Box => RegionRect;
    }
}
