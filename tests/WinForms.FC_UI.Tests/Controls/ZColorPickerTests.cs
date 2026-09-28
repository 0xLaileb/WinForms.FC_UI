using FC_UI.Controls;

namespace WinForms.FC_UI.Tests.Controls;

public class ZColorPickerTests : IDisposable
{
    private readonly ZColorPicker _picker = new();

    public void Dispose()
    {
        _picker.Dispose();
    }

    [Fact]
    public void Constructor_DefaultStyle_KeepsOriginalLayout()
    {
        Assert.Equal(new Size(185, 210), _picker.Size);
        Assert.Equal(new Rectangle(3, 3, 150, 150), _picker.WheelBounds);
        Assert.Equal(new Rectangle(165, 3, 17, 107), _picker.BrightnessBarBounds);
        Assert.Equal(new Rectangle(165, 116, 17, 37), _picker.PreviewBounds);
        Assert.Equal(Color.Empty, _picker.SelectedColor);
    }

    [Fact]
    public void SelectedColor_SetRed_UpdatesHsvAndRaisesColorChanged()
    {
        Color? raised = null;
        _picker.ColorChanged += color => raised = color;

        _picker.SelectedColor = Color.FromArgb(255, 0, 0);

        Assert.Equal(Color.FromArgb(255, 0, 0), raised);
        Assert.Equal((0F, 1F, 1F), _picker.Hsv);
    }

    [Fact]
    public void SelectedColor_SetSameValue_DoesNotRaiseColorChanged()
    {
        _picker.SelectedColor = Color.Blue;
        var raised = 0;
        _picker.ColorChanged += _ => raised++;

        _picker.SelectedColor = Color.Blue;

        Assert.Equal(0, raised);
    }

    [Fact]
    public void SetHsv_HalfBrightnessGreen_SelectsDarkGreen()
    {
        _picker.SetHsv(120, 1F, 0.5F);

        Assert.Equal(Color.FromArgb(255, 0, 127, 0).ToArgb(), _picker.SelectedColor.ToArgb());
        Assert.Equal((120F, 1F, 0.5F), _picker.Hsv);
    }

    [Fact]
    public void SetHsv_OutOfRange_ClampsAndWrapsHue()
    {
        _picker.SetHsv(-30, 2F, -1F);

        Assert.Equal((330F, 1F, 0F), _picker.Hsv);
    }

    [Fact]
    public void MouseDown_RightEdgeOfWheel_SelectsRed()
    {
        using TestPicker picker = new();
        var wheel = picker.WheelBounds;

        picker.InvokeMouseDown(new Point(wheel.Right - 1, wheel.Y + wheel.Height / 2));

        Assert.True(picker.SelectedColor.R > 240);
        Assert.True(picker.SelectedColor.G < 20);
        Assert.True(picker.SelectedColor.B < 20);
    }

    [Fact]
    public void MouseDown_BrightnessBarBottom_SelectsBlackKeepingHue()
    {
        using TestPicker picker = new();
        picker.SetHsv(200, 1F, 1F);
        var bar = picker.BrightnessBarBounds;

        picker.InvokeMouseDown(new Point(bar.X + bar.Width / 2, bar.Bottom));

        Assert.Equal(Color.Black.ToArgb(), picker.SelectedColor.ToArgb());
        Assert.Equal(200F, picker.Hsv.Hue);
    }

    [Fact]
    public void CreateWheelBitmap_Pixels_MatchHsvAtPosition()
    {
        using var wheel = ZColorPicker.CreateWheelBitmap(101);

        var center = wheel.GetPixel(50, 50);
        var right = wheel.GetPixel(99, 50);
        var corner = wheel.GetPixel(0, 0);

        Assert.True(center is { R: > 240, G: > 240, B: > 240 });
        Assert.True(right is { R: > 240, G: < 20, B: < 20 });
        Assert.Equal(0, corner.A);
    }

    [Fact]
    public void ShowColorInfo_Disabled_GivesSpaceToWheel()
    {
        _picker.Size = new Size(185, 180);
        var withInfo = _picker.WheelBounds.Width;

        _picker.ShowColorInfo = false;

        Assert.True(_picker.WheelBounds.Width > withInfo);
    }

    [Fact]
    public void Resize_Larger_ScalesWheel()
    {
        _picker.Size = new Size(370, 420);

        Assert.True(_picker.WheelBounds.Width > 300);
        Assert.True(_picker.BrightnessBarBounds.Left > _picker.WheelBounds.Right);
    }

    [Fact]
    public void DrawToBitmap_DefaultStyle_DrawsWheel()
    {
        using Bitmap bitmap = new(_picker.Width, _picker.Height);

        _picker.DrawToBitmap(bitmap, new Rectangle(Point.Empty, _picker.Size));

        var wheel = _picker.WheelBounds;
        Assert.True(bitmap.GetPixel(wheel.Right - 3, wheel.Y + wheel.Height / 2).R > 200);
    }

    private sealed class TestPicker : ZColorPicker
    {
        public void InvokeMouseDown(Point point) =>
            OnMouseDown(new MouseEventArgs(MouseButtons.Left, 1, point.X, point.Y, 0));
    }
}
