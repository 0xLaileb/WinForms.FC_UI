using System.ComponentModel;

namespace FC_UI.Components;

public partial class FGlobal_RGB : Component
{
    private bool tmp_status;

    [Category("GLOBAL_RGB")]
    [Description("Enable/Disable global RGB mode for all FC_UI controls")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool Status
    {
        get => tmp_status;
        set
        {
            tmp_status = value;
            DrawEngine.TimerGlobalRGB(tmp_status);
        }
    }

    [Category("GLOBAL_RGB")]
    [Description("RGB timer update interval")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int TimerInterval
    {
        get => DrawEngine.timer_global_rgb.Interval;
        set => DrawEngine.timer_global_rgb.Interval = value;
    }

    public FGlobal_RGB(IContainer container) => container.Add(this);
}
