using FC_UI.Controls;

namespace WinForms.FC_UI.Tests.Controls;

public class FScrollBarTests : IDisposable
{
    private readonly FScrollBar _scrollBar;

    public FScrollBarTests()
    {
        _scrollBar = new FScrollBar();
    }

    public void Dispose()
    {
        _scrollBar.Dispose();
    }

    [Fact]
    public void Constructor_DefaultStyle_ValueIs0()
    {
        Assert.Equal(0, _scrollBar.Value);
    }

    [Fact]
    public void Constructor_DefaultStyle_MinimumIs0()
    {
        Assert.Equal(0, _scrollBar.Minimum);
    }

    [Fact]
    public void Constructor_DefaultStyle_MaximumIs100()
    {
        Assert.Equal(100, _scrollBar.Maximum);
    }

    [Fact]
    public void Constructor_DefaultStyle_SmallStepIs1()
    {
        Assert.Equal(1, _scrollBar.SmallStep);
    }

    [Fact]
    public void Constructor_DefaultStyle_ThumbSizeIs60()
    {
        Assert.Equal(60, _scrollBar.ThumbSize);
    }

    [Fact]
    public void Value_SetValid_ReturnsSetValue()
    {
        _scrollBar.Value = 50;

        Assert.Equal(50, _scrollBar.Value);
    }

    [Fact]
    public void Value_AboveMaximum_IsClamped()
    {
        _scrollBar.Value = 101;

        Assert.Equal(100, _scrollBar.Value);
    }

    [Fact]
    public void Value_BelowMinimum_IsClamped()
    {
        _scrollBar.Minimum = 10;
        _scrollBar.Value = 0;

        Assert.Equal(10, _scrollBar.Value);
    }

    [Fact]
    public void Value_Changed_RaisesValueChangedEvent()
    {
        var eventFired = false;
        _scrollBar.ValueChanged += (_, _) => eventFired = true;

        _scrollBar.Value = 10;

        Assert.True(eventFired);
    }

    [Fact]
    public void Value_SameValue_DoesNotRaiseEvent()
    {
        _scrollBar.Value = 0;
        var eventFired = false;
        _scrollBar.ValueChanged += (_, _) => eventFired = true;

        _scrollBar.Value = 0;

        Assert.False(eventFired);
    }

    [Fact]
    public void Maximum_GreaterThanMinimum_IsAccepted()
    {
        _scrollBar.Maximum = 200;

        Assert.Equal(200, _scrollBar.Maximum);
    }

    [Fact]
    public void Maximum_LessThanMinimum_IsRejected()
    {
        _scrollBar.Maximum = 100;
        _scrollBar.Maximum = -1;

        Assert.Equal(100, _scrollBar.Maximum);
    }

    [Fact]
    public void Minimum_LessThanMaximum_IsAccepted()
    {
        _scrollBar.Minimum = 10;

        Assert.Equal(10, _scrollBar.Minimum);
    }

    [Fact]
    public void Minimum_AboveCurrentValue_ClampsValueToMinimum()
    {
        _scrollBar.Minimum = 10;

        Assert.Equal(10, _scrollBar.Value);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(50)]
    [InlineData(100)]
    public void CornerRadius_ValidValues_AreAccepted(int value)
    {
        _scrollBar.CornerRadius = value;

        Assert.Equal(value, _scrollBar.CornerRadius);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(101)]
    public void CornerRadius_InvalidValues_AreRejected(int value)
    {
        var original = _scrollBar.CornerRadius;
        _scrollBar.CornerRadius = value;

        Assert.Equal(original, _scrollBar.CornerRadius);
    }

    [Fact]
    public void ThumbColor_SetValue_ReturnsSetValue()
    {
        _scrollBar.ThumbColor = Color.Green;

        Assert.Equal(Color.Green, _scrollBar.ThumbColor);
    }

    [Fact]
    public void ThumbOpacity_ValidRange_IsAccepted()
    {
        _scrollBar.ThumbOpacity = 128;

        Assert.Equal(128, _scrollBar.ThumbOpacity);
    }

    [Fact]
    public void ThumbOpacity_BelowMin_IsRejected()
    {
        _scrollBar.ThumbOpacity = 100;
        _scrollBar.ThumbOpacity = 5;

        Assert.Equal(100, _scrollBar.ThumbOpacity);
    }

    [Fact]
    public void Orientation_SetSameVerticalValue_KeepsCornerRadius()
    {
        var original = _scrollBar.CornerRadius;

        _scrollBar.Orientation = Orientation.Vertical;

        Assert.Equal(original, _scrollBar.CornerRadius);
    }

    [Fact]
    public void Orientation_SetHorizontalTwice_SwapsSizeOnce()
    {
        var original = _scrollBar.Size;

        _scrollBar.Orientation = Orientation.Horizontal;
        _scrollBar.Orientation = Orientation.Horizontal;

        Assert.Equal(new Size(original.Height, original.Width), _scrollBar.Size);
    }

    [Fact]
    public void Orientation_HorizontalThenVertical_RestoresSizeAndCornerRadius()
    {
        var originalSize = _scrollBar.Size;
        var originalRadius = _scrollBar.CornerRadius;

        _scrollBar.Orientation = Orientation.Horizontal;
        _scrollBar.Orientation = Orientation.Vertical;

        Assert.Equal(originalSize, _scrollBar.Size);
        Assert.Equal(originalRadius, _scrollBar.CornerRadius);
    }

    [Fact]
    public void Orientation_DesignerLoadOrderForHorizontalBar_KeepsSerializedValues()
    {
        // WinForms designer code assigns CornerRadius, then Orientation, then Size.
        _scrollBar.CornerRadius = 70;
        _scrollBar.Orientation = Orientation.Horizontal;
        _scrollBar.Size = new Size(350, 30);

        Assert.Equal(70, _scrollBar.CornerRadius);
        Assert.Equal(new Size(350, 30), _scrollBar.Size);
    }

    [Fact]
    public void ControlStyleDefault_HorizontalBar_RestoresVerticalDefaultSize()
    {
        _scrollBar.Orientation = Orientation.Horizontal;

        _scrollBar.ControlStyle = FControlBase.ControlStyleMode.Default;

        Assert.Equal(Orientation.Vertical, _scrollBar.Orientation);
        Assert.Equal(new Size(26, 300), _scrollBar.Size);
        Assert.Equal(7, _scrollBar.CornerRadius);
    }

    [Fact]
    public void DesignerLoad_HorizontalBarWithSmallRadius_KeepsSerializedValues()
    {
        // Designer code wraps the property assignments in BeginInit/EndInit.
        _scrollBar.BeginInit();
        _scrollBar.CornerRadius = 5;
        _scrollBar.Orientation = Orientation.Horizontal;
        _scrollBar.Size = new Size(350, 30);
        _scrollBar.EndInit();

        Assert.Equal(5, _scrollBar.CornerRadius);
        Assert.Equal(new Size(350, 30), _scrollBar.Size);
        Assert.Equal(Orientation.Horizontal, _scrollBar.Orientation);
    }

    [Theory]
    [InlineData(Keys.Down, 51)]
    [InlineData(Keys.Right, 51)]
    [InlineData(Keys.Up, 49)]
    [InlineData(Keys.PageDown, 60)]
    [InlineData(Keys.PageUp, 40)]
    [InlineData(Keys.Home, 0)]
    [InlineData(Keys.End, 100)]
    public void KeyDown_NavigationKey_ChangesValue(Keys key, int expected)
    {
        using TestScrollBar scrollBar = new() { Value = 50 };

        scrollBar.InvokeKeyDown(key);

        Assert.Equal(expected, scrollBar.Value);
    }

    [Fact]
    public void MouseWheel_ScrollDown_IncreasesValueBySystemLines()
    {
        using TestScrollBar scrollBar = new() { Value = 50 };

        scrollBar.InvokeMouseWheel(-SystemInformation.MouseWheelScrollDelta);

        var lines = SystemInformation.MouseWheelScrollLines;
        Assert.Equal(Math.Min(100, 50 + (lines > 0 ? lines : 10)), scrollBar.Value);
    }

    [Fact]
    public void MouseWheel_FractionalDeltas_AccumulateToOneNotch()
    {
        using TestScrollBar scrollBar = new() { Value = 50 };
        var quarter = SystemInformation.MouseWheelScrollDelta / 4;

        for (var i = 0; i < 3; i++) scrollBar.InvokeMouseWheel(-quarter);
        Assert.Equal(50, scrollBar.Value);
        scrollBar.InvokeMouseWheel(-quarter);

        Assert.True(scrollBar.Value > 50);
    }

    [Fact]
    public void MouseWheel_HandledArgs_MarksEventHandled()
    {
        using TestScrollBar scrollBar = new() { Value = 50 };
        HandledMouseEventArgs args = new(MouseButtons.None, 0, 5, 5, -SystemInformation.MouseWheelScrollDelta);

        scrollBar.InvokeMouseWheel(args);

        Assert.True(args.Handled);
    }

    [Fact]
    public void MouseDrag_GrabbedThumb_KeepsGrabOffset()
    {
        using TestScrollBar scrollBar = new();
        var thumb = scrollBar.GetThumbBounds();
        Point grab = new(thumb.X + thumb.Width / 2, thumb.Y + thumb.Height / 2);

        scrollBar.InvokeMouseDown(grab);
        Assert.Equal(0, scrollBar.Value);

        var track = scrollBar.Height - 2 * (int)scrollBar.BorderWidth - scrollBar.ThumbSize;
        scrollBar.InvokeMouseMove(grab with { Y = grab.Y + track / 2 });

        Assert.Equal(50, scrollBar.Value);
    }

    [Fact]
    public void MouseDrag_BeyondTrack_ClampsToMaximum()
    {
        using TestScrollBar scrollBar = new();
        var thumb = scrollBar.GetThumbBounds();

        scrollBar.InvokeMouseDown(new Point(thumb.X + 2, thumb.Y + 2));
        scrollBar.InvokeMouseMove(new Point(thumb.X + 2, 5000));

        Assert.Equal(100, scrollBar.Value);
    }

    [Fact]
    public void GetThumbBounds_ValueAtMaximum_TouchesTrackEnd()
    {
        _scrollBar.Value = _scrollBar.Maximum;

        var thumb = _scrollBar.GetThumbBounds();

        Assert.Equal(_scrollBar.Height - (int)_scrollBar.BorderWidth, thumb.Bottom);
    }

    [Fact]
    public void AccessibilityObject_Value_ReportsScrollBarAndValue()
    {
        _scrollBar.Value = 42;

        var accessible = _scrollBar.AccessibilityObject;

        Assert.Equal(AccessibleRole.ScrollBar, accessible.Role);
        Assert.Equal("42", accessible.Value);
    }

    private sealed class TestScrollBar : FScrollBar
    {
        public void InvokeKeyDown(Keys key) => OnKeyDown(new KeyEventArgs(key));

        public void InvokeMouseWheel(int delta) =>
            OnMouseWheel(new MouseEventArgs(MouseButtons.None, 0, 5, 5, delta));

        public void InvokeMouseWheel(MouseEventArgs args) => OnMouseWheel(args);

        public void InvokeMouseDown(Point point) =>
            OnMouseDown(new MouseEventArgs(MouseButtons.Left, 1, point.X, point.Y, 0));

        public void InvokeMouseMove(Point point) =>
            OnMouseMove(new MouseEventArgs(MouseButtons.Left, 0, point.X, point.Y, 0));
    }

    [Fact]
    public void Tag_IsSetToFC_UI()
    {
        Assert.Equal("FC_UI", _scrollBar.Tag);
    }
}
