using System.Reflection;
using FC_UI.Controls;

namespace WinForms.FC_UI.Tests.Controls;

public class FControlBaseTests
{
    private static readonly Type[] AllControlTypes =
    [
        typeof(FButton),
        typeof(FCheckBox),
        typeof(FRadioButton),
        typeof(FSwitchBox),
        typeof(FProgressBar),
        typeof(FScrollBar),
        typeof(FGroupBox),
        typeof(FTextBox),
        typeof(FRichTextBox)
    ];

    public static TheoryData<Type> ControlTypes()
    {
        TheoryData<Type> data = [];
        foreach (var type in AllControlTypes) data.Add(type);
        return data;
    }

    [Theory]
    [MemberData(nameof(ControlTypes))]
    public void Width_Changed_RaisesSizeChangedAndResize(Type controlType)
    {
        using var control = (FControlBase)Activator.CreateInstance(controlType)!;
        var sizeChangedCount = 0;
        var resizeCount = 0;
        control.SizeChanged += (_, _) => sizeChangedCount++;
        control.Resize += (_, _) => resizeCount++;

        control.Width += 20;

        Assert.True(sizeChangedCount > 0, $"{controlType.Name} did not raise SizeChanged.");
        Assert.True(resizeCount > 0, $"{controlType.Name} did not raise Resize.");
    }

    [Theory]
    [MemberData(nameof(ControlTypes))]
    public void DrawToBitmap_PaintHandlerAttached_RaisesPaintEvent(Type controlType)
    {
        using var control = (FControlBase)Activator.CreateInstance(controlType)!;
        var painted = false;
        control.Paint += (_, _) => painted = true;

        using Bitmap bitmap = new(control.Width, control.Height);
        control.DrawToBitmap(bitmap, new Rectangle(Point.Empty, control.Size));

        Assert.True(painted, $"{controlType.Name} did not raise Paint.");
    }

    public static TheoryData<Type, string, string> MouseEventCases()
    {
        TheoryData<Type, string, string> data = [];
        foreach (var type in AllControlTypes)
        {
            data.Add(type, "OnMouseEnter", "MouseEnter");
            data.Add(type, "OnMouseLeave", "MouseLeave");
            data.Add(type, "OnMouseDown", "MouseDown");
            data.Add(type, "OnMouseMove", "MouseMove");
            data.Add(type, "OnMouseUp", "MouseUp");
            data.Add(type, "OnMouseClick", "MouseClick");
        }
        return data;
    }

    [Theory]
    [MemberData(nameof(MouseEventCases))]
    public void MouseHandler_Invoked_RaisesPublicEvent(Type controlType, string handlerName, string eventName)
    {
        using var control = (FControlBase)Activator.CreateInstance(controlType)!;
        var raised = false;
        EventHandler eventHandler = (_, _) => raised = true;
        MouseEventHandler mouseHandler = (_, _) => raised = true;
        var eventInfo = typeof(Control).GetEvent(eventName)!;
        eventInfo.AddEventHandler(control, eventInfo.EventHandlerType == typeof(MouseEventHandler) ? mouseHandler : eventHandler);
        var handler = typeof(Control).GetMethod(handlerName, BindingFlags.Instance | BindingFlags.NonPublic)!;
        object args = handler.GetParameters()[0].ParameterType == typeof(MouseEventArgs)
            ? new MouseEventArgs(MouseButtons.Left, 1, 10, 10, 0)
            : EventArgs.Empty;

        handler.Invoke(control, [args]);

        Assert.True(raised, $"{controlType.Name}.{handlerName} did not raise {eventName}.");
    }

    [Fact]
    public void DrawToBitmap_Disabled_RendersGrayscale()
    {
        using FButton button = new() { DisplayText = string.Empty };
        using Bitmap bitmap = new(button.Width, button.Height);
        Point sample = new(20, button.Height / 2);

        button.DrawToBitmap(bitmap, new Rectangle(Point.Empty, button.Size));
        var enabledPixel = bitmap.GetPixel(sample.X, sample.Y);
        button.Enabled = false;
        button.DrawToBitmap(bitmap, new Rectangle(Point.Empty, button.Size));
        var disabledPixel = bitmap.GetPixel(sample.X, sample.Y);

        Assert.True(enabledPixel.B - enabledPixel.R > 10, $"Expected a colored enabled fill, got {enabledPixel}.");
        Assert.InRange(disabledPixel.R - disabledPixel.G, -2, 2);
        Assert.InRange(disabledPixel.G - disabledPixel.B, -2, 2);
    }

    [Fact]
    public void DrawToBitmap_GeometryUnchanged_KeepsRegionInstance()
    {
        using FButton button = new();
        using Bitmap bitmap = new(button.Width, button.Height);

        button.DrawToBitmap(bitmap, new Rectangle(Point.Empty, button.Size));
        var firstRegion = button.Region;
        button.DrawToBitmap(bitmap, new Rectangle(Point.Empty, button.Size));

        Assert.NotNull(firstRegion);
        Assert.Same(firstRegion, button.Region);
    }

    [Fact]
    public void DrawToBitmap_AfterResize_ReplacesRegion()
    {
        using FButton button = new();
        using Bitmap bitmap = new(300, 100);
        button.DrawToBitmap(bitmap, new Rectangle(Point.Empty, button.Size));
        var firstRegion = button.Region;

        button.Size = new Size(200, 80);
        button.DrawToBitmap(bitmap, new Rectangle(Point.Empty, button.Size));

        Assert.NotSame(firstRegion, button.Region);
    }

    [Fact]
    public void ShowBorder_Changed_DoesNotRaiseSizeChanged()
    {
        using FButton button = new();
        var sizeChangedCount = 0;
        button.SizeChanged += (_, _) => sizeChangedCount++;

        button.ShowBorder = !button.ShowBorder;
        button.BorderWidth += 1;
        button.Lighting = !button.Lighting;

        Assert.Equal(0, sizeChangedCount);
    }

    [Fact]
    public void GroupBoxSize_Changed_RelayoutsDockedChild()
    {
        using FGroupBox groupBox = new();
        Panel child = new() { Dock = DockStyle.Fill };
        groupBox.Controls.Add(child);

        groupBox.Size = new Size(300, 200);

        Assert.Equal(groupBox.ClientSize, child.Size);
    }
}
