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
    public void Height_SetDoubleValue_ScalesBoxAndRaisesSizeChangedOnce()
    {
        using ClickableFCheckBox checkBox = new();
        var sizeChangedCount = 0;
        checkBox.SizeChanged += (_, _) => sizeChangedCount++;

        checkBox.Size = new Size(200, 90);

        Assert.Equal(new Size(200, 90), checkBox.Size);
        Assert.Equal(1, sizeChangedCount);
        Assert.Equal(new Size(42, 42), checkBox.Box.Size);
    }

    [Fact]
    public void Constructor_DefaultHeight_KeepsOriginalBoxGeometry()
    {
        using ClickableFCheckBox checkBox = new();

        Assert.Equal(new Rectangle(15, 10, 21, 21), checkBox.Box);
    }

    [Fact]
    public void DrawToBitmap_LightingEnabled_DrawsGlowAroundBox()
    {
        using FCheckBox checkBox = new() { LightingColor = Color.Red, LightingAlpha = 200 };
        using Bitmap bitmap = new(checkBox.Width, checkBox.Height);
        // Just outside the box border (box spans x 15..36).
        Point sample = new(12, checkBox.Height / 2 - 1);

        checkBox.DrawToBitmap(bitmap, new Rectangle(Point.Empty, checkBox.Size));
        var withoutLighting = bitmap.GetPixel(sample.X, sample.Y);
        checkBox.Lighting = true;
        checkBox.DrawToBitmap(bitmap, new Rectangle(Point.Empty, checkBox.Size));
        var withLighting = bitmap.GetPixel(sample.X, sample.Y);

        Assert.True(withoutLighting.G - withLighting.G > 50, $"{withoutLighting} -> {withLighting}");
    }

    [Fact]
    public void SpaceKeyUp_Unchecked_ChecksControl()
    {
        using ClickableFCheckBox checkBox = new();

        checkBox.InvokeKeyUp(Keys.Space);

        Assert.True(checkBox.Checked);
    }

    [Fact]
    public void AccessibilityObject_Checked_ReportsRoleNameAndState()
    {
        _checkBox.Checked = true;

        var accessible = _checkBox.AccessibilityObject;

        Assert.Equal(AccessibleRole.CheckButton, accessible.Role);
        Assert.Equal("FCheckBox", accessible.Name);
        Assert.True(accessible.State.HasFlag(AccessibleStates.Checked));
    }

    [Fact]
    public void AccessibilityObject_DoDefaultAction_TogglesChecked()
    {
        _checkBox.AccessibilityObject.DoDefaultAction();

        Assert.True(_checkBox.Checked);
    }

    private sealed class ClickableFCheckBox : FCheckBox
    {
        public void InvokeMouseClick(MouseButtons button) =>
            OnMouseClick(new MouseEventArgs(button, 1, 0, 0, 0));

        public void InvokeKeyUp(Keys key) => OnKeyUp(new KeyEventArgs(key));

        public Rectangle Box => RegionRect;
    }
}
