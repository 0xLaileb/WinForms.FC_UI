using System.ComponentModel;

namespace FC_UI.Comonents
{
    public partial class FGlobal_RGB : Component
    {
        private bool tmp_status = false;
        [Category("GLOBAL_RGB")]
        [Description("Вкл/Выкл глобального RGB режима для всех контролов FC_UI")]
        public bool Status
        {
            get => tmp_status;
            set
            {
                tmp_status = value;
                DrawEngine.TimerGlobalRGB(tmp_status);
            }
        }
        //
        [Category("GLOBAL_RGB")]
        [Description("Интервал обновления таймера RGB")]
        public int TimerInterval
        {
            get => DrawEngine.timer_global_rgb.Interval;
            set { DrawEngine.timer_global_rgb.Interval = value; }
        }

        public FGlobal_RGB(IContainer container) => container.Add(this);
    }
}
