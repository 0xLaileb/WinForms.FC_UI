using System.Drawing;
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
    public void Constructor_DefaultStyle_BackgroundIsTrue()
    {
        Assert.True(_progressBar.Background);
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

    [Theory]
    [InlineData(0)]
    [InlineData(50)]
    [InlineData(100)]
    public void RoundingInt_ValidValues_AreAccepted(int value)
    {
        _progressBar.RoundingInt = value;

        Assert.Equal(value, _progressBar.RoundingInt);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(101)]
    public void RoundingInt_InvalidValues_AreRejected(int value)
    {
        int original = _progressBar.RoundingInt;
        _progressBar.RoundingInt = value;

        Assert.Equal(original, _progressBar.RoundingInt);
    }

    [Fact]
    public void ColorProgressBar_SetValue_ReturnsSetValue()
    {
        _progressBar.ColorProgressBar = Color.Red;

        Assert.Equal(Color.Red, _progressBar.ColorProgressBar);
    }

    [Fact]
    public void Tag_IsSetToFC_UI()
    {
        Assert.Equal("FC_UI", _progressBar.Tag);
    }
}
