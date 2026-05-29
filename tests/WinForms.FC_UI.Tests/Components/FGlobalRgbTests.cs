using System.ComponentModel;
using FC_UI;
using FC_UI.Components;

namespace WinForms.FC_UI.Tests.Components;

[Collection("GlobalRGBTimer")]
public class FGlobalRgbTests : IDisposable
{
    private readonly Container _container;
    private readonly FGlobalRgb _globalRgb;

    public FGlobalRgbTests()
    {
        _container = new Container();
        _globalRgb = new FGlobalRgb(_container);
    }

    public void Dispose()
    {
        _globalRgb.Status = false;
        _globalRgb.TimerInterval = 50;
        _container.Dispose();
    }

    [Fact]
    public void Constructor_StatusIsFalse()
    {
        Assert.False(_globalRgb.Status);
    }

    [Fact]
    public void Status_SetTrue_EnablesGlobalTimer()
    {
        _globalRgb.Status = true;

        Assert.True(DrawEngine.GlobalRgbTimer.Enabled);
    }

    [Fact]
    public void Status_SetFalse_DisablesGlobalTimer()
    {
        _globalRgb.Status = true;
        _globalRgb.Status = false;

        Assert.False(DrawEngine.GlobalRgbTimer.Enabled);
    }

    [Fact]
    public void TimerInterval_GetDefault_Returns50()
    {
        Assert.Equal(50, _globalRgb.TimerInterval);
    }

    [Fact]
    public void TimerInterval_SetValue_UpdatesGlobalTimer()
    {
        _globalRgb.TimerInterval = 500;

        Assert.Equal(500, DrawEngine.GlobalRgbTimer.Interval);

        // Reset to default
        _globalRgb.TimerInterval = 50;
    }

    [Fact]
    public void TimerInterval_SetValue_ReturnsSetValue()
    {
        _globalRgb.TimerInterval = 100;

        Assert.Equal(100, _globalRgb.TimerInterval);

        // Reset to default
        _globalRgb.TimerInterval = 50;
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void TimerInterval_InvalidValues_AreRejected(int value)
    {
        _globalRgb.TimerInterval = 100;
        _globalRgb.TimerInterval = value;

        Assert.Equal(100, _globalRgb.TimerInterval);
    }
}
