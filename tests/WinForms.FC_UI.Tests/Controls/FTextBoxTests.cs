using FC_UI.Controls;

namespace WinForms.FC_UI.Tests.Controls;

public class FTextBoxTests : IDisposable
{
    private readonly FTextBox _textBox;

    public FTextBoxTests()
    {
        _textBox = new FTextBox();
    }

    public void Dispose()
    {
        _textBox.Dispose();
    }

    [Fact]
    public void DisplayText_SetValue_RaisesTextChangedOnce()
    {
        var eventCount = 0;
        _textBox.TextChanged += () => eventCount++;

        _textBox.DisplayText = "Updated";

        Assert.Equal(1, eventCount);
    }

    [Fact]
    public void InnerTextBox_TextChanged_RaisesTextChangedOnce()
    {
        var eventCount = 0;
        _textBox.TextChanged += () => eventCount++;

        _textBox.InnerTextBox.Text = "User text";

        Assert.Equal(1, eventCount);
    }
}
