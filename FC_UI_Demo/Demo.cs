using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace FC_UI_Demo
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
            Message msg = Message.Create(Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            WndProc(ref msg);
        }
        #endregion

        private void zColorPicker_ColorChanged(Color color)
        {
            fButton1.ColorBackground_Pen = color;
            fCheckBox1.ColorBackground_Pen = color;
            fCheckBox1.ColorChecked = color;
            fRadioButton1.ColorBackground_Pen = color;
            fRadioButton1.ColorChecked = color;
            fProgressBar1.ColorProgressBar = color;
            fProgressBar1.ColorBackground_Pen = color;
            fScrollBar1.ColorBackground_Pen = color;
            fScrollBar1.ColorScrollBar = color;
            fGroupBox1.ColorBackground_Pen = color;
            fRichTextBox1.ColorBackground_Pen = color;
            fTextBox1.ColorBackground_Pen = color;
            fTextBox2.ColorBackground_Pen = color;
            fSwitchBox_global_rgb.ColorBackground_Pen = color;
            fSwitchBox_random_style.ColorBackground_Pen = color;
            fSwitchBox_rgb_mode.ColorBackground_Pen = color;
        }
        private void fSwitchBox_random_style_CheckedChanged()
        {
            if (fSwitchBox_random_style.Checked)
            {
                new Thread(delegate ()
                {
                    int tmp_sleep = 150;
                    fButton1.Invoke((MethodInvoker)delegate { fButton1.FButtonStyle = FC_UI.Controls.FButton.Style.Random; });
                    Thread.Sleep(tmp_sleep);
                    fCheckBox1.Invoke((MethodInvoker)delegate { fCheckBox1.FCheckBoxStyle = FC_UI.Controls.FCheckBox.Style.Random; });
                    Thread.Sleep(tmp_sleep);
                    fRadioButton1.Invoke((MethodInvoker)delegate { fRadioButton1.FRadioButtonStyle = FC_UI.Controls.FRadioButton.Style.Random; });
                    Thread.Sleep(tmp_sleep);
                    fProgressBar1.Invoke((MethodInvoker)delegate { fProgressBar1.FProgressBarStyle = FC_UI.Controls.FProgressBar.Style.Random; });
                    Thread.Sleep(tmp_sleep);
                    fScrollBar1.Invoke((MethodInvoker)delegate { fScrollBar1.FScrollBarStyle = FC_UI.Controls.FScrollBar.Style.Random; });
                    Thread.Sleep(tmp_sleep);
                    fGroupBox1.Invoke((MethodInvoker)delegate { fGroupBox1.FGroupBoxStyle = FC_UI.Controls.FGroupBox.Style.Random; });
                    Thread.Sleep(tmp_sleep);
                    fRichTextBox1.Invoke((MethodInvoker)delegate { fRichTextBox1.FRichTextBoxStyle = FC_UI.Controls.FRichTextBox.Style.Random; });
                    Thread.Sleep(tmp_sleep);
                    fTextBox1.Invoke((MethodInvoker)delegate { fTextBox1.FTextBoxStyle = FC_UI.Controls.FTextBox.Style.Random; });
                    Thread.Sleep(tmp_sleep);
                    fTextBox2.Invoke((MethodInvoker)delegate { fTextBox2.FTextBoxStyle = FC_UI.Controls.FTextBox.Style.Random; });
                    Thread.Sleep(tmp_sleep);
                    fSwitchBox_global_rgb.Invoke((MethodInvoker)delegate { fSwitchBox_global_rgb.FSwitchBoxStyle = FC_UI.Controls.FSwitchBox.Style.Random; });
                    Thread.Sleep(tmp_sleep);
                    fSwitchBox_rgb_mode.Invoke((MethodInvoker)delegate { fSwitchBox_rgb_mode.FSwitchBoxStyle = FC_UI.Controls.FSwitchBox.Style.Random; });
                }).Start();
            }
            else
            {
                new Thread(delegate ()
                {
                    int tmp_sleep = 100;
                    fButton1.Invoke((MethodInvoker)delegate { fButton1.FButtonStyle = FC_UI.Controls.FButton.Style.Default; });
                    Thread.Sleep(tmp_sleep);
                    fCheckBox1.Invoke((MethodInvoker)delegate { fCheckBox1.FCheckBoxStyle = FC_UI.Controls.FCheckBox.Style.Default; });
                    Thread.Sleep(tmp_sleep);
                    fRadioButton1.Invoke((MethodInvoker)delegate { fRadioButton1.FRadioButtonStyle = FC_UI.Controls.FRadioButton.Style.Default; });
                    Thread.Sleep(tmp_sleep);
                    fProgressBar1.Invoke((MethodInvoker)delegate { fProgressBar1.FProgressBarStyle = FC_UI.Controls.FProgressBar.Style.Default; });
                    Thread.Sleep(tmp_sleep);
                    fScrollBar1.Invoke((MethodInvoker)delegate
                    {
                        fScrollBar1.FScrollBarStyle = FC_UI.Controls.FScrollBar.Style.Default; fScrollBar1.OrientationValue = Orientation.Horizontal;
                    });
                    Thread.Sleep(tmp_sleep);
                    fGroupBox1.Invoke((MethodInvoker)delegate { fGroupBox1.FGroupBoxStyle = FC_UI.Controls.FGroupBox.Style.Default; });
                    Thread.Sleep(tmp_sleep);
                    fRichTextBox1.Invoke((MethodInvoker)delegate { fRichTextBox1.FRichTextBoxStyle = FC_UI.Controls.FRichTextBox.Style.Default; });
                    Thread.Sleep(tmp_sleep);
                    fTextBox1.Invoke((MethodInvoker)delegate { fTextBox1.FTextBoxStyle = FC_UI.Controls.FTextBox.Style.Default; });
                    Thread.Sleep(tmp_sleep);
                    fTextBox2.Invoke((MethodInvoker)delegate { fTextBox2.FTextBoxStyle = FC_UI.Controls.FTextBox.Style.Default; });
                    Thread.Sleep(tmp_sleep);
                    fSwitchBox_global_rgb.Invoke((MethodInvoker)delegate { fSwitchBox_global_rgb.FSwitchBoxStyle = FC_UI.Controls.FSwitchBox.Style.Default; });
                    Thread.Sleep(tmp_sleep);
                    fSwitchBox_rgb_mode.Invoke((MethodInvoker)delegate { fSwitchBox_rgb_mode.FSwitchBoxStyle = FC_UI.Controls.FSwitchBox.Style.Default; });
                }).Start();
            }
        }
        private void fSwitchBox_rgb_mode_CheckedChanged()
        {
            bool tmp = fSwitchBox_rgb_mode.Checked;
            new Thread(delegate ()
            {
                fButton1.Invoke((MethodInvoker)delegate { fButton1.RGB = tmp; });
                Thread.Sleep(1000);
                fCheckBox1.Invoke((MethodInvoker)delegate { fCheckBox1.RGB = tmp; });
                Thread.Sleep(1000);
                fRadioButton1.Invoke((MethodInvoker)delegate { fRadioButton1.RGB = tmp; });
                Thread.Sleep(1000);
                fProgressBar1.Invoke((MethodInvoker)delegate { fProgressBar1.RGB = tmp; });
                Thread.Sleep(1000);
                fScrollBar1.Invoke((MethodInvoker)delegate { fScrollBar1.RGB = tmp; });
                Thread.Sleep(1000);
                fGroupBox1.Invoke((MethodInvoker)delegate { fGroupBox1.RGB = tmp; });
                Thread.Sleep(1000);
                fRichTextBox1.Invoke((MethodInvoker)delegate { fRichTextBox1.RGB = tmp; });
                Thread.Sleep(1000);
                fTextBox1.Invoke((MethodInvoker)delegate { fTextBox1.RGB = tmp; });
                Thread.Sleep(1000);
                fTextBox2.Invoke((MethodInvoker)delegate { fTextBox2.RGB = tmp; });
                Thread.Sleep(1000);
                fSwitchBox_global_rgb.Invoke((MethodInvoker)delegate { fSwitchBox_global_rgb.RGB = tmp; });
                Thread.Sleep(1000);
                fSwitchBox_random_style.Invoke((MethodInvoker)delegate { fSwitchBox_random_style.RGB = tmp; });
                Thread.Sleep(1000);
                fSwitchBox_rgb_mode.Invoke((MethodInvoker)delegate { fSwitchBox_rgb_mode.RGB = tmp; });
            }).Start();
        }
        private void fSwitchBox_global_rgb_CheckedChanged()
        {
            fGlobal_RGB1.Status = fSwitchBox_global_rgb.Checked;
        }
        private void fScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            fProgressBar1.Value = fScrollBar1.Value;
        }
        private void exit_Click(object sender, EventArgs e) => Environment.Exit(0);
    }
}
