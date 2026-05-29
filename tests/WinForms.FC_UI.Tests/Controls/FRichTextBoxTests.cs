using FC_UI.Controls;

namespace WinForms.FC_UI.Tests.Controls;

public class FRichTextBoxTests : IDisposable
{
    private readonly FRichTextBox _richTextBox;

    public FRichTextBoxTests()
    {
        _richTextBox = new FRichTextBox();
    }

    public void Dispose()
    {
        _richTextBox.Dispose();
    }

    [Fact]
    public void DisplayText_SetValue_RaisesTextChangedOnce()
    {
        var eventCount = 0;
        _richTextBox.TextChanged += () => eventCount++;

        _richTextBox.DisplayText = "Updated";

        Assert.Equal(1, eventCount);
    }

    [Fact]
    public void InnerRichTextBox_TextChanged_RaisesTextChangedOnce()
    {
        var eventCount = 0;
        _richTextBox.TextChanged += () => eventCount++;
        var inner = Assert.IsType<RichTextBox>(_richTextBox.Controls[0]);
        _richTextBox.UpdateRichTextBox(true);
        inner.CreateControl();
        eventCount = 0;

        inner.Text = "User text";

        Assert.Equal(1, eventCount);
    }
}
