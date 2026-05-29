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

    private sealed class ClickableFRadioButton : FRadioButton
    {
        public void InvokeMouseClick(MouseButtons button) =>
            OnMouseClick(new MouseEventArgs(button, 1, 0, 0, 0));
    }
}
