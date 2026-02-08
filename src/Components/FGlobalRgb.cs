using System.ComponentModel;

namespace FC_UI.Components;

public partial class FGlobalRgb : Component
{
    public FGlobalRgb(IContainer container)
    {
        container.Add(this);
    }

    [Category("GLOBAL_RGB")]
    [Description("Enable/Disable global RGB mode for all FC_UI controls")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool Status
    {
        get;
        set
        {
            field = value;
            DrawEngine.SetGlobalRgbTimer(field);
        }
    }

    [Category("GLOBAL_RGB")]
    [Description("RGB timer update interval")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int TimerInterval
    {
        get => DrawEngine.GlobalRgbTimer.Interval;
        set => DrawEngine.GlobalRgbTimer.Interval = value;
    }
}
