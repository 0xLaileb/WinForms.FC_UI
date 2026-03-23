using FC_UI.Controls;

namespace WinForms.FC_UI.Example
{
    public partial class Demo : Form
    {
        #region Demo
        public Demo()
        {
            InitializeComponent();
        }
        private void Demo_MouseDown(object sender, MouseEventArgs e)
        {
            Capture = false;
            var msg = Message.Create(Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            WndProc(ref msg);
        }
        #endregion

        private void zColorPicker_ColorChanged(Color color)
        {
            fButton1.BorderColor = color;
            fCheckBox1.BorderColor = color;
            fCheckBox1.ColorChecked = color;
            fRadioButton1.BorderColor = color;
            fRadioButton1.ColorChecked = color;
            fProgressBar1.FillColor = color;
            fProgressBar1.BorderColor = color;
            fScrollBar1.BorderColor = color;
            fScrollBar1.ThumbColor = color;
            fGroupBox1.BorderColor = color;
            fRichTextBox1.BorderColor = color;
            fTextBox1.BorderColor = color;
            fTextBox2.BorderColor = color;
            fSwitchBox_global_rgb.BorderColor = color;
            fSwitchBox_random_style.BorderColor = color;
            fSwitchBox_rgb_mode.BorderColor = color;
        }
        private async void fSwitchBox_random_style_CheckedChanged()
        {
            if (fSwitchBox_random_style.Checked)
            {
                await Task.Run(async () =>
                {
                    const int delay = 150;
                    fButton1.Invoke(() => fButton1.ControlStyle = FControlBase.ControlStyleMode.Random);
                    await Task.Delay(delay);
                    fCheckBox1.Invoke(() => fCheckBox1.ControlStyle = FControlBase.ControlStyleMode.Random);
                    await Task.Delay(delay);
                    fRadioButton1.Invoke(() => fRadioButton1.ControlStyle = FControlBase.ControlStyleMode.Random);
                    await Task.Delay(delay);
                    fProgressBar1.Invoke(() => fProgressBar1.ControlStyle = FControlBase.ControlStyleMode.Random);
                    await Task.Delay(delay);
                    fScrollBar1.Invoke(() => fScrollBar1.ControlStyle = FControlBase.ControlStyleMode.Random);
                    await Task.Delay(delay);
                    fGroupBox1.Invoke(() => fGroupBox1.ControlStyle = FControlBase.ControlStyleMode.Random);
                    await Task.Delay(delay);
                    fRichTextBox1.Invoke(() => fRichTextBox1.ControlStyle = FControlBase.ControlStyleMode.Random);
                    await Task.Delay(delay);
                    fTextBox1.Invoke(() => fTextBox1.ControlStyle = FControlBase.ControlStyleMode.Random);
                    await Task.Delay(delay);
                    fTextBox2.Invoke(() => fTextBox2.ControlStyle = FControlBase.ControlStyleMode.Random);
                    await Task.Delay(delay);
                    fSwitchBox_global_rgb.Invoke(() => fSwitchBox_global_rgb.ControlStyle = FControlBase.ControlStyleMode.Random);
                    await Task.Delay(delay);
                    fSwitchBox_rgb_mode.Invoke(() => fSwitchBox_rgb_mode.ControlStyle = FControlBase.ControlStyleMode.Random);
                });
            }
            else
            {
                await Task.Run(async () =>
                {
                    const int delay = 100;
                    fButton1.Invoke(() => fButton1.ControlStyle = FControlBase.ControlStyleMode.Default);
                    await Task.Delay(delay);
                    fCheckBox1.Invoke(() => fCheckBox1.ControlStyle = FControlBase.ControlStyleMode.Default);
                    await Task.Delay(delay);
                    fRadioButton1.Invoke(() => fRadioButton1.ControlStyle = FControlBase.ControlStyleMode.Default);
                    await Task.Delay(delay);
                    fProgressBar1.Invoke(() => fProgressBar1.ControlStyle = FControlBase.ControlStyleMode.Default);
                    await Task.Delay(delay);
                    fScrollBar1.Invoke(() =>
                    {
                        fScrollBar1.ControlStyle = FControlBase.ControlStyleMode.Default;
                        fScrollBar1.Orientation = Orientation.Horizontal;
                    });
                    await Task.Delay(delay);
                    fGroupBox1.Invoke(() => fGroupBox1.ControlStyle = FControlBase.ControlStyleMode.Default);
                    await Task.Delay(delay);
                    fRichTextBox1.Invoke(() => fRichTextBox1.ControlStyle = FControlBase.ControlStyleMode.Default);
                    await Task.Delay(delay);
                    fTextBox1.Invoke(() => fTextBox1.ControlStyle = FControlBase.ControlStyleMode.Default);
                    await Task.Delay(delay);
                    fTextBox2.Invoke(() => fTextBox2.ControlStyle = FControlBase.ControlStyleMode.Default);
                    await Task.Delay(delay);
                    fSwitchBox_global_rgb.Invoke(() => fSwitchBox_global_rgb.ControlStyle = FControlBase.ControlStyleMode.Default);
                    await Task.Delay(delay);
                    fSwitchBox_rgb_mode.Invoke(() => fSwitchBox_rgb_mode.ControlStyle = FControlBase.ControlStyleMode.Default);
                });
            }
        }
        private async void fSwitchBox_rgb_mode_CheckedChanged()
        {
            var isEnabled = fSwitchBox_rgb_mode.Checked;
            await Task.Run(async () =>
            {
                fButton1.Invoke(() => fButton1.Rgb = isEnabled);
                await Task.Delay(1000);
                fCheckBox1.Invoke(() => fCheckBox1.Rgb = isEnabled);
                await Task.Delay(1000);
                fRadioButton1.Invoke(() => fRadioButton1.Rgb = isEnabled);
                await Task.Delay(1000);
                fProgressBar1.Invoke(() => fProgressBar1.Rgb = isEnabled);
                await Task.Delay(1000);
                fScrollBar1.Invoke(() => fScrollBar1.Rgb = isEnabled);
                await Task.Delay(1000);
                fGroupBox1.Invoke(() => fGroupBox1.Rgb = isEnabled);
                await Task.Delay(1000);
                fRichTextBox1.Invoke(() => fRichTextBox1.Rgb = isEnabled);
                await Task.Delay(1000);
                fTextBox1.Invoke(() => fTextBox1.Rgb = isEnabled);
                await Task.Delay(1000);
                fTextBox2.Invoke(() => fTextBox2.Rgb = isEnabled);
                await Task.Delay(1000);
                fSwitchBox_global_rgb.Invoke(() => fSwitchBox_global_rgb.Rgb = isEnabled);
                await Task.Delay(1000);
                fSwitchBox_random_style.Invoke(() => fSwitchBox_random_style.Rgb = isEnabled);
                await Task.Delay(1000);
                fSwitchBox_rgb_mode.Invoke(() => fSwitchBox_rgb_mode.Rgb = isEnabled);
            });
        }
        private void fSwitchBox_global_rgb_CheckedChanged()
        {
            fGlobalRgb1.Status = fSwitchBox_global_rgb.Checked;
        }
        private void fScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            fProgressBar1.Value = fScrollBar1.Value;
        }
        private void exit_Click(object sender, EventArgs e) => Environment.Exit(0);
    }
}
