using FC_UI;
using FC_UI.Controls;

namespace WinForms.FC_UI.Tests.Controls;

[Collection("GlobalRGBTimer")]
public class FControlBaseRgbTests : IDisposable
{
    public void Dispose()
    {
        DrawEngine.SetGlobalRgbTimer(false);
    }

    [Fact]
    public void Rgb_SetTrue_StartsLocalTimer()
    {
        using FButton button = new();

        button.Rgb = true;

        Assert.True(button.IsRgbTimerRunning);
    }

    [Fact]
    public void Rgb_SetFalse_StopsLocalTimer()
    {
        using FButton button = new() { Rgb = true };

        button.Rgb = false;

        Assert.False(button.IsRgbTimerRunning);
    }

    [Fact]
    public void Rgb_EnabledDuringGlobalModeThenGlobalModeDisabled_KeepsLocalTimerRunning()
    {
        using FButton button = new();
        DrawEngine.SetGlobalRgbTimer(true);
        button.Rgb = true;

        DrawEngine.SetGlobalRgbTimer(false);

        Assert.True(button.IsRgbTimerRunning);
    }
}
