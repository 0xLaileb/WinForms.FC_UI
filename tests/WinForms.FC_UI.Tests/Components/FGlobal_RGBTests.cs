using System.ComponentModel;
using FC_UI;
using FC_UI.Components;

namespace WinForms.FC_UI.Tests.Components;

[Collection("GlobalRGBTimer")]
public class FGlobal_RGBTests : IDisposable
{
    private readonly Container _container;
    private readonly FGlobal_RGB _globalRgb;

    public FGlobal_RGBTests()
    {
        _container = new Container();
        _globalRgb = new FGlobal_RGB(_container);
    }

    public void Dispose()
    {
        _globalRgb.Status = false;
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

        Assert.True(DrawEngine.timer_global_rgb.Enabled);
    }

    [Fact]
    public void Status_SetFalse_DisablesGlobalTimer()
    {
        _globalRgb.Status = true;
        _globalRgb.Status = false;

        Assert.False(DrawEngine.timer_global_rgb.Enabled);
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

        Assert.Equal(500, DrawEngine.timer_global_rgb.Interval);

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
}
