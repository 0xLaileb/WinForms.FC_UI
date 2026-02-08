using FC_UI.Components;
using FC_UI.Controls;

namespace WinForms.FC_UI.Example
{
    partial class Demo
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            fSwitchBox_global_rgb = new FSwitchBox();
            fSwitchBox_rgb_mode = new FSwitchBox();
            fTextBox2 = new FTextBox();
            fSwitchBox_random_style = new FSwitchBox();
            zColorPicker = new ZColorPicker();
            fTextBox1 = new FTextBox();
            fScrollBar1 = new FScrollBar();
            fRichTextBox1 = new FRichTextBox();
            fRadioButton1 = new FRadioButton();
            fProgressBar1 = new FProgressBar();
            fGroupBox1 = new FGroupBox();
            fCheckBox1 = new FCheckBox();
            fButton1 = new FButton();
            fGlobal_RGB1 = new FGlobal_RGB(this.components);
            exit = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)exit).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial", 11F);
            label1.ForeColor = Color.WhiteSmoke;
            label1.Location = new Point(568, 383);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(96, 17);
            label1.TabIndex = 11;
            label1.Text = "RandomStyle";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Arial", 11F);
            label2.ForeColor = Color.WhiteSmoke;
            label2.Location = new Point(568, 419);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(75, 17);
            label2.TabIndex = 13;
            label2.Text = "RGBMode";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Arial", 11F);
            label3.ForeColor = Color.WhiteSmoke;
            label3.Location = new Point(568, 453);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(89, 17);
            label3.TabIndex = 15;
            label3.Text = "Global_RGB";
            // 
            // fSwitchBox_global_rgb
            // 
            fSwitchBox_global_rgb.Alpha = 50;
            fSwitchBox_global_rgb.BackColor = Color.Transparent;
            fSwitchBox_global_rgb.Background = true;
            fSwitchBox_global_rgb.Background_WidthPen = 2F;
            fSwitchBox_global_rgb.BackgroundPen = true;
            fSwitchBox_global_rgb.Checked = false;
            fSwitchBox_global_rgb.ColorBackground = Color.FromArgb(37, 52, 68);
            fSwitchBox_global_rgb.ColorBackground_1 = Color.FromArgb(37, 52, 68);
            fSwitchBox_global_rgb.ColorBackground_2 = Color.FromArgb(41, 63, 86);
            fSwitchBox_global_rgb.ColorBackground_Pen = Color.FromArgb(29, 200, 238);
            fSwitchBox_global_rgb.ColorBackground_Value_1 = Color.FromArgb(28, 200, 238);
            fSwitchBox_global_rgb.ColorBackground_Value_2 = Color.FromArgb(100, 208, 232);
            fSwitchBox_global_rgb.ColorLighting = Color.FromArgb(29, 200, 238);
            fSwitchBox_global_rgb.ColorPen_1 = Color.FromArgb(37, 52, 68);
            fSwitchBox_global_rgb.ColorPen_2 = Color.FromArgb(41, 63, 86);
            fSwitchBox_global_rgb.ColorValue = Color.FromArgb(29, 200, 238);
            fSwitchBox_global_rgb.Font = new Font("Arial", 11F);
            fSwitchBox_global_rgb.ForeColor = Color.FromArgb(245, 245, 245);
            fSwitchBox_global_rgb.FSwitchBoxStyle = FSwitchBox.Style.Custom;
            fSwitchBox_global_rgb.Lighting = false;
            fSwitchBox_global_rgb.LinearGradient_Background = false;
            fSwitchBox_global_rgb.LinearGradient_Value = false;
            fSwitchBox_global_rgb.LinearGradientPen = false;
            fSwitchBox_global_rgb.Location = new Point(520, 451);
            fSwitchBox_global_rgb.Margin = new Padding(4, 3, 4, 3);
            fSwitchBox_global_rgb.Name = "fSwitchBox_global_rgb";
            fSwitchBox_global_rgb.PenWidth = 10;
            fSwitchBox_global_rgb.RGB = false;
            fSwitchBox_global_rgb.Rounding = true;
            fSwitchBox_global_rgb.RoundingInt = 90;
            fSwitchBox_global_rgb.Size = new Size(41, 23);
            fSwitchBox_global_rgb.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            fSwitchBox_global_rgb.TabIndex = 14;
            fSwitchBox_global_rgb.Tag = "FC_UI";
            fSwitchBox_global_rgb.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            fSwitchBox_global_rgb.Timer_RGB = 300;
            fSwitchBox_global_rgb.CheckedChanged += fSwitchBox_global_rgb_CheckedChanged;
            // 
            // fSwitchBox_rgb_mode
            // 
            fSwitchBox_rgb_mode.Alpha = 50;
            fSwitchBox_rgb_mode.BackColor = Color.Transparent;
            fSwitchBox_rgb_mode.Background = true;
            fSwitchBox_rgb_mode.Background_WidthPen = 2F;
            fSwitchBox_rgb_mode.BackgroundPen = true;
            fSwitchBox_rgb_mode.Checked = false;
            fSwitchBox_rgb_mode.ColorBackground = Color.FromArgb(37, 52, 68);
            fSwitchBox_rgb_mode.ColorBackground_1 = Color.FromArgb(37, 52, 68);
            fSwitchBox_rgb_mode.ColorBackground_2 = Color.FromArgb(41, 63, 86);
            fSwitchBox_rgb_mode.ColorBackground_Pen = Color.FromArgb(29, 200, 238);
            fSwitchBox_rgb_mode.ColorBackground_Value_1 = Color.FromArgb(28, 200, 238);
            fSwitchBox_rgb_mode.ColorBackground_Value_2 = Color.FromArgb(100, 208, 232);
            fSwitchBox_rgb_mode.ColorLighting = Color.FromArgb(29, 200, 238);
            fSwitchBox_rgb_mode.ColorPen_1 = Color.FromArgb(37, 52, 68);
            fSwitchBox_rgb_mode.ColorPen_2 = Color.FromArgb(41, 63, 86);
            fSwitchBox_rgb_mode.ColorValue = Color.FromArgb(29, 200, 238);
            fSwitchBox_rgb_mode.Font = new Font("Arial", 11F);
            fSwitchBox_rgb_mode.ForeColor = Color.FromArgb(245, 245, 245);
            fSwitchBox_rgb_mode.FSwitchBoxStyle = FSwitchBox.Style.Custom;
            fSwitchBox_rgb_mode.Lighting = false;
            fSwitchBox_rgb_mode.LinearGradient_Background = false;
            fSwitchBox_rgb_mode.LinearGradient_Value = false;
            fSwitchBox_rgb_mode.LinearGradientPen = false;
            fSwitchBox_rgb_mode.Location = new Point(520, 417);
            fSwitchBox_rgb_mode.Margin = new Padding(4, 3, 4, 3);
            fSwitchBox_rgb_mode.Name = "fSwitchBox_rgb_mode";
            fSwitchBox_rgb_mode.PenWidth = 10;
            fSwitchBox_rgb_mode.RGB = false;
            fSwitchBox_rgb_mode.Rounding = true;
            fSwitchBox_rgb_mode.RoundingInt = 90;
            fSwitchBox_rgb_mode.Size = new Size(41, 23);
            fSwitchBox_rgb_mode.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            fSwitchBox_rgb_mode.TabIndex = 12;
            fSwitchBox_rgb_mode.Tag = "FC_UI";
            fSwitchBox_rgb_mode.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            fSwitchBox_rgb_mode.Timer_RGB = 300;
            fSwitchBox_rgb_mode.CheckedChanged += fSwitchBox_rgb_mode_CheckedChanged;
            // 
            // fTextBox2
            // 
            fTextBox2.Alpha = 20;
            fTextBox2.BackColor = Color.Transparent;
            fTextBox2.Background_WidthPen = 3F;
            fTextBox2.BackgroundPen = true;
            fTextBox2.ColorBackground = Color.FromArgb(37, 52, 68);
            fTextBox2.ColorBackground_Pen = Color.FromArgb(29, 200, 238);
            fTextBox2.ColorLighting = Color.FromArgb(29, 200, 238);
            fTextBox2.ColorPen_1 = Color.FromArgb(29, 200, 238);
            fTextBox2.ColorPen_2 = Color.FromArgb(37, 52, 68);
            fTextBox2.Font = new Font("Arial", 13F);
            fTextBox2.ForeColor = Color.FromArgb(245, 245, 245);
            fTextBox2.FTextBoxStyle = FTextBox.Style.Custom;
            fTextBox2.Lighting = false;
            fTextBox2.LinearGradientPen = false;
            fTextBox2.Location = new Point(153, 673);
            fTextBox2.Margin = new Padding(6, 3, 6, 3);
            fTextBox2.Name = "fTextBox2";
            fTextBox2.Password = true;
            fTextBox2.PenWidth = 15;
            fTextBox2.RGB = false;
            fTextBox2.Rounding = true;
            fTextBox2.RoundingInt = 60;
            fTextBox2.Size = new Size(317, 61);
            fTextBox2.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            fTextBox2.TabIndex = 10;
            fTextBox2.Tag = "FC_UI";
            fTextBox2.TextButton = "Text";
            fTextBox2.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            fTextBox2.Timer_RGB = 300;
            // 
            // fSwitchBox_random_style
            // 
            fSwitchBox_random_style.Alpha = 50;
            fSwitchBox_random_style.BackColor = Color.Transparent;
            fSwitchBox_random_style.Background = true;
            fSwitchBox_random_style.Background_WidthPen = 2F;
            fSwitchBox_random_style.BackgroundPen = true;
            fSwitchBox_random_style.Checked = false;
            fSwitchBox_random_style.ColorBackground = Color.FromArgb(37, 52, 68);
            fSwitchBox_random_style.ColorBackground_1 = Color.FromArgb(37, 52, 68);
            fSwitchBox_random_style.ColorBackground_2 = Color.FromArgb(41, 63, 86);
            fSwitchBox_random_style.ColorBackground_Pen = Color.FromArgb(29, 200, 238);
            fSwitchBox_random_style.ColorBackground_Value_1 = Color.FromArgb(28, 200, 238);
            fSwitchBox_random_style.ColorBackground_Value_2 = Color.FromArgb(100, 208, 232);
            fSwitchBox_random_style.ColorLighting = Color.FromArgb(29, 200, 238);
            fSwitchBox_random_style.ColorPen_1 = Color.FromArgb(37, 52, 68);
            fSwitchBox_random_style.ColorPen_2 = Color.FromArgb(41, 63, 86);
            fSwitchBox_random_style.ColorValue = Color.FromArgb(29, 200, 238);
            fSwitchBox_random_style.Font = new Font("Arial", 11F);
            fSwitchBox_random_style.ForeColor = Color.FromArgb(245, 245, 245);
            fSwitchBox_random_style.FSwitchBoxStyle = FSwitchBox.Style.Custom;
            fSwitchBox_random_style.Lighting = false;
            fSwitchBox_random_style.LinearGradient_Background = false;
            fSwitchBox_random_style.LinearGradient_Value = false;
            fSwitchBox_random_style.LinearGradientPen = false;
            fSwitchBox_random_style.Location = new Point(520, 381);
            fSwitchBox_random_style.Margin = new Padding(4, 3, 4, 3);
            fSwitchBox_random_style.Name = "fSwitchBox_random_style";
            fSwitchBox_random_style.PenWidth = 10;
            fSwitchBox_random_style.RGB = false;
            fSwitchBox_random_style.Rounding = true;
            fSwitchBox_random_style.RoundingInt = 90;
            fSwitchBox_random_style.Size = new Size(41, 23);
            fSwitchBox_random_style.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            fSwitchBox_random_style.TabIndex = 9;
            fSwitchBox_random_style.Tag = "FC_UI";
            fSwitchBox_random_style.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            fSwitchBox_random_style.Timer_RGB = 300;
            fSwitchBox_random_style.CheckedChanged += fSwitchBox_random_style_CheckedChanged;
            // 
            // zColorPicker
            // 
            zColorPicker.BackColor = Color.Transparent;
            zColorPicker.ForeColor = Color.Black;
            zColorPicker.Location = new Point(491, 110);
            zColorPicker.Margin = new Padding(5, 3, 5, 3);
            zColorPicker.Name = "zColorPicker";
            zColorPicker.SelectedColor = Color.Empty;
            zColorPicker.Size = new Size(216, 242);
            zColorPicker.TabIndex = 8;
            zColorPicker.Tag = "FC_UI";
            zColorPicker.ColorChanged += zColorPicker_ColorChanged;
            // 
            // fTextBox1
            // 
            fTextBox1.Alpha = 20;
            fTextBox1.BackColor = Color.Transparent;
            fTextBox1.Background_WidthPen = 3F;
            fTextBox1.BackgroundPen = true;
            fTextBox1.ColorBackground = Color.FromArgb(37, 52, 68);
            fTextBox1.ColorBackground_Pen = Color.FromArgb(29, 200, 238);
            fTextBox1.ColorLighting = Color.FromArgb(29, 200, 238);
            fTextBox1.ColorPen_1 = Color.FromArgb(29, 200, 238);
            fTextBox1.ColorPen_2 = Color.FromArgb(37, 52, 68);
            fTextBox1.Font = new Font("Arial", 15F);
            fTextBox1.ForeColor = Color.FromArgb(245, 245, 245);
            fTextBox1.FTextBoxStyle = FTextBox.Style.Custom;
            fTextBox1.Lighting = false;
            fTextBox1.LinearGradientPen = false;
            fTextBox1.Location = new Point(178, 676);
            fTextBox1.Margin = new Padding(7, 3, 7, 3);
            fTextBox1.Name = "fTextBox1";
            fTextBox1.PenWidth = 15;
            fTextBox1.RGB = false;
            fTextBox1.Rounding = true;
            fTextBox1.RoundingInt = 60;
            fTextBox1.Size = new Size(370, 70);
            fTextBox1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            fTextBox1.TabIndex = 7;
            fTextBox1.Tag = "FC_UI";
            fTextBox1.TextButton = "Text";
            fTextBox1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            fTextBox1.Timer_RGB = 300;
            // 
            // fScrollBar1
            // 
            fScrollBar1.Alpha = 50;
            fScrollBar1.BackColor = Color.Transparent;
            fScrollBar1.Background = true;
            fScrollBar1.Background_WidthPen = 3F;
            fScrollBar1.BackgroundPen = true;
            fScrollBar1.ColorBackground = Color.FromArgb(37, 52, 68);
            fScrollBar1.ColorBackground_1 = Color.FromArgb(37, 52, 68);
            fScrollBar1.ColorBackground_2 = Color.FromArgb(41, 63, 86);
            fScrollBar1.ColorBackground_Pen = Color.FromArgb(29, 200, 238);
            fScrollBar1.ColorBackground_Value_1 = Color.FromArgb(28, 200, 238);
            fScrollBar1.ColorBackground_Value_2 = Color.FromArgb(100, 208, 232);
            fScrollBar1.ColorLighting = Color.FromArgb(29, 200, 238);
            fScrollBar1.ColorPen_1 = Color.FromArgb(37, 52, 68);
            fScrollBar1.ColorPen_2 = Color.FromArgb(41, 63, 86);
            fScrollBar1.ColorScrollBar = Color.FromArgb(29, 200, 238);
            fScrollBar1.ColorScrollBar_Transparency = 255;
            fScrollBar1.ForeColor = Color.FromArgb(245, 245, 245);
            fScrollBar1.FScrollBarStyle = FScrollBar.Style.Custom;
            fScrollBar1.Lighting = false;
            fScrollBar1.LinearGradient_Background = false;
            fScrollBar1.LinearGradient_Value = false;
            fScrollBar1.LinearGradientPen = false;
            fScrollBar1.Location = new Point(59, 224);
            fScrollBar1.Margin = new Padding(4, 3, 4, 3);
            fScrollBar1.Maximum = 100;
            fScrollBar1.Minimum = 0;
            fScrollBar1.Name = "fScrollBar1";
            fScrollBar1.OrientationValue = Orientation.Horizontal;
            fScrollBar1.PenWidth = 10;
            fScrollBar1.RGB = false;
            fScrollBar1.Rounding = true;
            fScrollBar1.RoundingInt = 70;
            fScrollBar1.Size = new Size(350, 30);
            fScrollBar1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            fScrollBar1.TabIndex = 6;
            fScrollBar1.Tag = "FC_UI";
            fScrollBar1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            fScrollBar1.ThumbSize = 60;
            fScrollBar1.Timer_RGB = 300;
            fScrollBar1.Value = 0;
            fScrollBar1.ValueChanged += fScrollBar1_ValueChanged;
            // 
            // fRichTextBox1
            // 
            fRichTextBox1.Alpha = 20;
            fRichTextBox1.BackColor = Color.Transparent;
            fRichTextBox1.Background_WidthPen = 3F;
            fRichTextBox1.BackgroundPen = true;
            fRichTextBox1.ColorBackground = Color.FromArgb(37, 52, 68);
            fRichTextBox1.ColorBackground_Pen = Color.FromArgb(29, 200, 238);
            fRichTextBox1.ColorLighting = Color.FromArgb(29, 200, 238);
            fRichTextBox1.ColorPen_1 = Color.FromArgb(29, 200, 238);
            fRichTextBox1.ColorPen_2 = Color.FromArgb(37, 52, 68);
            fRichTextBox1.Font = new Font("Arial", 11F);
            fRichTextBox1.ForeColor = Color.FromArgb(245, 245, 245);
            fRichTextBox1.FRichTextBoxStyle = FRichTextBox.Style.Custom;
            fRichTextBox1.Lighting = false;
            fRichTextBox1.LinearGradientPen = false;
            fRichTextBox1.Location = new Point(238, 272);
            fRichTextBox1.Margin = new Padding(4, 3, 4, 3);
            fRichTextBox1.Name = "fRichTextBox1";
            fRichTextBox1.PenWidth = 15;
            fRichTextBox1.RGB = false;
            fRichTextBox1.Rounding = true;
            fRichTextBox1.RoundingInt = 60;
            fRichTextBox1.Size = new Size(175, 150);
            fRichTextBox1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            fRichTextBox1.TabIndex = 5;
            fRichTextBox1.Tag = "FC_UI";
            fRichTextBox1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            fRichTextBox1.Timer_RGB = 300;
            // 
            // fRadioButton1
            // 
            fRadioButton1.BackColor = Color.Transparent;
            fRadioButton1.Background = true;
            fRadioButton1.Background_WidthPen = 2F;
            fRadioButton1.BackgroundPen = true;
            fRadioButton1.Checked = true;
            fRadioButton1.Color_1_Background_value = Color.Empty;
            fRadioButton1.Color_2_Background_value = Color.Empty;
            fRadioButton1.ColorBackground = Color.FromArgb(37, 52, 68);
            fRadioButton1.ColorBackground_1 = Color.FromArgb(37, 52, 68);
            fRadioButton1.ColorBackground_2 = Color.FromArgb(41, 63, 86);
            fRadioButton1.ColorBackground_Pen = Color.FromArgb(29, 200, 238);
            fRadioButton1.ColorChecked = Color.FromArgb(29, 200, 238);
            fRadioButton1.ColorPen_1 = Color.FromArgb(37, 52, 68);
            fRadioButton1.ColorPen_2 = Color.FromArgb(41, 63, 86);
            fRadioButton1.Effect_1_ColorBackground = Color.FromArgb(29, 200, 238);
            fRadioButton1.Effect_1_Transparency = 25;
            fRadioButton1.Effect_2 = true;
            fRadioButton1.Effect_2_ColorBackground = Color.White;
            fRadioButton1.Effect_2_Transparency = 15;
            fRadioButton1.Font = new Font("Arial", 11F);
            fRadioButton1.ForeColor = Color.FromArgb(245, 245, 245);
            fRadioButton1.FRadioButtonStyle = FRadioButton.Style.Custom;
            fRadioButton1.LinearGradient_Background = false;
            fRadioButton1.LinearGradient_Value = false;
            fRadioButton1.LinearGradientPen = false;
            fRadioButton1.Location = new Point(234, 106);
            fRadioButton1.Margin = new Padding(4, 3, 4, 3);
            fRadioButton1.Name = "fRadioButton1";
            fRadioButton1.RGB = false;
            fRadioButton1.Rounding = true;
            fRadioButton1.RoundingInt = 100;
            fRadioButton1.Size = new Size(163, 45);
            fRadioButton1.SizeChecked = 8;
            fRadioButton1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            fRadioButton1.TabIndex = 4;
            fRadioButton1.Tag = "FC_UI";
            fRadioButton1.TextButton = "FRadioButton";
            fRadioButton1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            fRadioButton1.Timer_Effect_1 = 1;
            fRadioButton1.Timer_RGB = 300;
            // 
            // fProgressBar1
            // 
            fProgressBar1.Alpha = 50;
            fProgressBar1.BackColor = Color.Transparent;
            fProgressBar1.Background = true;
            fProgressBar1.Background_WidthPen = 3F;
            fProgressBar1.BackgroundPen = true;
            fProgressBar1.ColorBackground = Color.FromArgb(37, 52, 68);
            fProgressBar1.ColorBackground_1 = Color.FromArgb(37, 52, 68);
            fProgressBar1.ColorBackground_2 = Color.FromArgb(41, 63, 86);
            fProgressBar1.ColorBackground_Pen = Color.FromArgb(29, 200, 238);
            fProgressBar1.ColorBackground_Value_1 = Color.FromArgb(28, 200, 238);
            fProgressBar1.ColorBackground_Value_2 = Color.FromArgb(100, 208, 232);
            fProgressBar1.ColorLighting = Color.FromArgb(29, 200, 238);
            fProgressBar1.ColorPen_1 = Color.FromArgb(37, 52, 68);
            fProgressBar1.ColorPen_2 = Color.FromArgb(41, 63, 86);
            fProgressBar1.ColorProgressBar = Color.FromArgb(29, 200, 238);
            fProgressBar1.ColorValue_Transparency = 200;
            fProgressBar1.Font = new Font("Arial", 11F);
            fProgressBar1.ForeColor = Color.FromArgb(245, 245, 245);
            fProgressBar1.FProgressBarStyle = FProgressBar.Style.Custom;
            fProgressBar1.Lighting = false;
            fProgressBar1.LinearGradient_Background = false;
            fProgressBar1.LinearGradient_Value = false;
            fProgressBar1.LinearGradientPen = false;
            fProgressBar1.Location = new Point(59, 170);
            fProgressBar1.Margin = new Padding(4, 3, 4, 3);
            fProgressBar1.Maximum = 100;
            fProgressBar1.Minimum = 0;
            fProgressBar1.Name = "fProgressBar1";
            fProgressBar1.PenWidth = 10;
            fProgressBar1.ProgressText = true;
            fProgressBar1.RGB = false;
            fProgressBar1.Rounding = true;
            fProgressBar1.RoundingInt = 70;
            fProgressBar1.Size = new Size(350, 39);
            fProgressBar1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            fProgressBar1.StartDrawingValue = 0;
            fProgressBar1.TabIndex = 3;
            fProgressBar1.Tag = "FC_UI";
            fProgressBar1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            fProgressBar1.Timer_RGB = 300;
            fProgressBar1.Value = 48;
            // 
            // fGroupBox1
            // 
            fGroupBox1.Alpha = 20;
            fGroupBox1.BackColor = Color.Transparent;
            fGroupBox1.Background = true;
            fGroupBox1.Background_WidthPen = 3F;
            fGroupBox1.BackgroundPen = true;
            fGroupBox1.ColorBackground = Color.FromArgb(37, 52, 68);
            fGroupBox1.ColorBackground_1 = Color.FromArgb(37, 52, 68);
            fGroupBox1.ColorBackground_2 = Color.FromArgb(41, 63, 86);
            fGroupBox1.ColorBackground_Pen = Color.FromArgb(29, 200, 238);
            fGroupBox1.ColorLighting = Color.FromArgb(29, 200, 238);
            fGroupBox1.ColorPen_1 = Color.FromArgb(37, 52, 68);
            fGroupBox1.ColorPen_2 = Color.FromArgb(41, 63, 86);
            fGroupBox1.FGroupBoxStyle = FGroupBox.Style.Custom;
            fGroupBox1.ForeColor = Color.FromArgb(245, 245, 245);
            fGroupBox1.Lighting = false;
            fGroupBox1.LinearGradient_Background = false;
            fGroupBox1.LinearGradientPen = false;
            fGroupBox1.Location = new Point(56, 272);
            fGroupBox1.Margin = new Padding(4, 3, 4, 3);
            fGroupBox1.Name = "fGroupBox1";
            fGroupBox1.PenWidth = 15;
            fGroupBox1.RGB = false;
            fGroupBox1.Rounding = true;
            fGroupBox1.RoundingInt = 60;
            fGroupBox1.Size = new Size(175, 150);
            fGroupBox1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            fGroupBox1.TabIndex = 2;
            fGroupBox1.Tag = "FC_UI";
            fGroupBox1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            fGroupBox1.Timer_RGB = 300;
            // 
            // fCheckBox1
            // 
            fCheckBox1.BackColor = Color.Transparent;
            fCheckBox1.Background = true;
            fCheckBox1.Background_WidthPen = 2F;
            fCheckBox1.BackgroundPen = true;
            fCheckBox1.Checked = true;
            fCheckBox1.ColorBackground = Color.FromArgb(37, 52, 68);
            fCheckBox1.ColorBackground_1 = Color.FromArgb(37, 52, 68);
            fCheckBox1.ColorBackground_2 = Color.FromArgb(41, 63, 86);
            fCheckBox1.ColorBackground_Pen = Color.FromArgb(29, 200, 238);
            fCheckBox1.ColorChecked = Color.FromArgb(29, 200, 238);
            fCheckBox1.ColorPen_1 = Color.FromArgb(37, 52, 68);
            fCheckBox1.ColorPen_2 = Color.FromArgb(41, 63, 86);
            fCheckBox1.Effect_1_ColorBackground = Color.FromArgb(29, 200, 238);
            fCheckBox1.Effect_1_Transparency = 25;
            fCheckBox1.Effect_2 = true;
            fCheckBox1.Effect_2_ColorBackground = Color.White;
            fCheckBox1.Effect_2_Transparency = 15;
            fCheckBox1.FCheckBoxStyle = FCheckBox.Style.Custom;
            fCheckBox1.Font = new Font("Arial", 11F);
            fCheckBox1.ForeColor = Color.FromArgb(245, 245, 245);
            fCheckBox1.LinearGradient_Background = false;
            fCheckBox1.LinearGradientPen = false;
            fCheckBox1.Location = new Point(59, 106);
            fCheckBox1.Margin = new Padding(4, 3, 4, 3);
            fCheckBox1.Name = "fCheckBox1";
            fCheckBox1.RGB = false;
            fCheckBox1.Rounding = true;
            fCheckBox1.RoundingInt = 100;
            fCheckBox1.Size = new Size(163, 45);
            fCheckBox1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            fCheckBox1.TabIndex = 1;
            fCheckBox1.Tag = "FC_UI";
            fCheckBox1.TextButton = "FCheckBox";
            fCheckBox1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            fCheckBox1.Timer_Effect_1 = 1;
            fCheckBox1.Timer_RGB = 300;
            // 
            // fButton1
            // 
            fButton1.Alpha = 20;
            fButton1.BackColor = Color.Transparent;
            fButton1.Background = true;
            fButton1.Background_WidthPen = 4F;
            fButton1.BackgroundPen = true;
            fButton1.ColorBackground = Color.FromArgb(37, 52, 68);
            fButton1.ColorBackground_1 = Color.FromArgb(37, 52, 68);
            fButton1.ColorBackground_2 = Color.FromArgb(41, 63, 86);
            fButton1.ColorBackground_Pen = Color.FromArgb(29, 200, 238);
            fButton1.ColorLighting = Color.FromArgb(29, 200, 238);
            fButton1.ColorPen_1 = Color.FromArgb(37, 52, 68);
            fButton1.ColorPen_2 = Color.FromArgb(41, 63, 86);
            fButton1.Effect_1 = true;
            fButton1.Effect_1_ColorBackground = Color.FromArgb(29, 200, 238);
            fButton1.Effect_1_Transparency = 25;
            fButton1.Effect_2 = true;
            fButton1.Effect_2_ColorBackground = Color.White;
            fButton1.Effect_2_Transparency = 20;
            fButton1.FButtonStyle = FButton.Style.Custom;
            fButton1.Font = new Font("Arial", 11F);
            fButton1.ForeColor = Color.FromArgb(245, 245, 245);
            fButton1.Lighting = false;
            fButton1.LinearGradient_Background = false;
            fButton1.LinearGradientPen = false;
            fButton1.Location = new Point(147, 29);
            fButton1.Margin = new Padding(4, 3, 4, 3);
            fButton1.Name = "fButton1";
            fButton1.PenWidth = 15;
            fButton1.Rounding = true;
            fButton1.RoundingInt = 70;
            fButton1.Size = new Size(152, 58);
            fButton1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            fButton1.TabIndex = 0;
            fButton1.Tag = "FC_UI";
            fButton1.TextButton = "FButton";
            fButton1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            fButton1.Timer_Effect_1 = 5;
            fButton1.Timer_RGB = 300;
            // 
            // fGlobal_RGB1
            // 
            fGlobal_RGB1.Status = false;
            fGlobal_RGB1.TimerInterval = 300;
            // 
            // exit
            // 
            exit.BackgroundImage = Properties.Resources.x;
            exit.BackgroundImageLayout = ImageLayout.Zoom;
            exit.Location = new Point(714, 14);
            exit.Margin = new Padding(4, 3, 4, 3);
            exit.Name = "exit";
            exit.Size = new Size(33, 29);
            exit.TabIndex = 16;
            exit.TabStop = false;
            exit.Click += exit_Click;
            // 
            // Demo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(29, 40, 52);
            ClientSize = new Size(761, 594);
            Controls.Add(exit);
            Controls.Add(label3);
            Controls.Add(fSwitchBox_global_rgb);
            Controls.Add(label2);
            Controls.Add(fSwitchBox_rgb_mode);
            Controls.Add(label1);
            Controls.Add(fTextBox2);
            Controls.Add(fSwitchBox_random_style);
            Controls.Add(zColorPicker);
            Controls.Add(fTextBox1);
            Controls.Add(fScrollBar1);
            Controls.Add(fRichTextBox1);
            Controls.Add(fRadioButton1);
            Controls.Add(fProgressBar1);
            Controls.Add(fGroupBox1);
            Controls.Add(fCheckBox1);
            Controls.Add(fButton1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 3, 4, 3);
            Name = "Demo";
            MouseDown += Demo_MouseDown;
            ((System.ComponentModel.ISupportInitialize)exit).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FButton fButton1;
        private FCheckBox fCheckBox1;
        private FGlobal_RGB fGlobal_RGB1;
        private FGroupBox fGroupBox1;
        private FProgressBar fProgressBar1;
        private FRadioButton fRadioButton1;
        private FRichTextBox fRichTextBox1;
        private FScrollBar fScrollBar1;
        private FTextBox fTextBox1;
        private ZColorPicker zColorPicker;
        private FSwitchBox fSwitchBox_random_style;
        private FTextBox fTextBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private FSwitchBox fSwitchBox_rgb_mode;
        private System.Windows.Forms.Label label3;
        private FSwitchBox fSwitchBox_global_rgb;
        private System.Windows.Forms.PictureBox exit;
    }
}
