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
            components = new System.ComponentModel.Container();
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
            fGlobalRgb1 = new FGlobalRgb(components);
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
            fSwitchBox_global_rgb.BackColor = Color.Transparent;
            fSwitchBox_global_rgb.BackgroundColor = Color.FromArgb(37, 52, 68);
            fSwitchBox_global_rgb.BorderColor = Color.FromArgb(29, 200, 238);
            fSwitchBox_global_rgb.BorderWidth = 2F;
            fSwitchBox_global_rgb.Checked = false;
            fSwitchBox_global_rgb.ColorValue = Color.FromArgb(29, 200, 238);
            fSwitchBox_global_rgb.ControlStyle = FControlBase.ControlStyleMode.Custom;
            fSwitchBox_global_rgb.CornerRadius = 90;
            fSwitchBox_global_rgb.Font = new Font("Arial", 11F);
            fSwitchBox_global_rgb.ForeColor = Color.FromArgb(245, 245, 245);
            fSwitchBox_global_rgb.GradientBorderColor1 = Color.FromArgb(37, 52, 68);
            fSwitchBox_global_rgb.GradientBorderColor2 = Color.FromArgb(41, 63, 86);
            fSwitchBox_global_rgb.GradientColor1 = Color.FromArgb(37, 52, 68);
            fSwitchBox_global_rgb.GradientColor2 = Color.FromArgb(41, 63, 86);
            fSwitchBox_global_rgb.GradientFillColor1 = Color.FromArgb(28, 200, 238);
            fSwitchBox_global_rgb.GradientFillColor2 = Color.FromArgb(100, 208, 232);
            fSwitchBox_global_rgb.Lighting = false;
            fSwitchBox_global_rgb.LightingAlpha = 50;
            fSwitchBox_global_rgb.LightingColor = Color.FromArgb(29, 200, 238);
            fSwitchBox_global_rgb.LightingWidth = 10;
            fSwitchBox_global_rgb.Location = new Point(520, 451);
            fSwitchBox_global_rgb.Margin = new Padding(4, 3, 4, 3);
            fSwitchBox_global_rgb.Name = "fSwitchBox_global_rgb";
            fSwitchBox_global_rgb.RgbUpdateInterval = 300;
            fSwitchBox_global_rgb.Rounding = true;
            fSwitchBox_global_rgb.ShowBackground = true;
            fSwitchBox_global_rgb.ShowBorder = true;
            fSwitchBox_global_rgb.Size = new Size(41, 23);
            fSwitchBox_global_rgb.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            fSwitchBox_global_rgb.TabIndex = 14;
            fSwitchBox_global_rgb.Tag = "FC_UI";
            fSwitchBox_global_rgb.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            fSwitchBox_global_rgb.UseGradientBackground = false;
            fSwitchBox_global_rgb.UseGradientBorder = false;
            fSwitchBox_global_rgb.UseGradientFill = false;
            fSwitchBox_global_rgb.CheckedChanged += fSwitchBox_global_rgb_CheckedChanged;
            // 
            // fSwitchBox_rgb_mode
            // 
            fSwitchBox_rgb_mode.BackColor = Color.Transparent;
            fSwitchBox_rgb_mode.BackgroundColor = Color.FromArgb(37, 52, 68);
            fSwitchBox_rgb_mode.BorderColor = Color.FromArgb(29, 200, 238);
            fSwitchBox_rgb_mode.BorderWidth = 2F;
            fSwitchBox_rgb_mode.Checked = false;
            fSwitchBox_rgb_mode.ColorValue = Color.FromArgb(29, 200, 238);
            fSwitchBox_rgb_mode.ControlStyle = FControlBase.ControlStyleMode.Custom;
            fSwitchBox_rgb_mode.CornerRadius = 90;
            fSwitchBox_rgb_mode.Font = new Font("Arial", 11F);
            fSwitchBox_rgb_mode.ForeColor = Color.FromArgb(245, 245, 245);
            fSwitchBox_rgb_mode.GradientBorderColor1 = Color.FromArgb(37, 52, 68);
            fSwitchBox_rgb_mode.GradientBorderColor2 = Color.FromArgb(41, 63, 86);
            fSwitchBox_rgb_mode.GradientColor1 = Color.FromArgb(37, 52, 68);
            fSwitchBox_rgb_mode.GradientColor2 = Color.FromArgb(41, 63, 86);
            fSwitchBox_rgb_mode.GradientFillColor1 = Color.FromArgb(28, 200, 238);
            fSwitchBox_rgb_mode.GradientFillColor2 = Color.FromArgb(100, 208, 232);
            fSwitchBox_rgb_mode.Lighting = false;
            fSwitchBox_rgb_mode.LightingAlpha = 50;
            fSwitchBox_rgb_mode.LightingColor = Color.FromArgb(29, 200, 238);
            fSwitchBox_rgb_mode.LightingWidth = 10;
            fSwitchBox_rgb_mode.Location = new Point(520, 417);
            fSwitchBox_rgb_mode.Margin = new Padding(4, 3, 4, 3);
            fSwitchBox_rgb_mode.Name = "fSwitchBox_rgb_mode";
            fSwitchBox_rgb_mode.RgbUpdateInterval = 300;
            fSwitchBox_rgb_mode.Rounding = true;
            fSwitchBox_rgb_mode.ShowBackground = true;
            fSwitchBox_rgb_mode.ShowBorder = true;
            fSwitchBox_rgb_mode.Size = new Size(41, 23);
            fSwitchBox_rgb_mode.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            fSwitchBox_rgb_mode.TabIndex = 12;
            fSwitchBox_rgb_mode.Tag = "FC_UI";
            fSwitchBox_rgb_mode.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            fSwitchBox_rgb_mode.UseGradientBackground = false;
            fSwitchBox_rgb_mode.UseGradientBorder = false;
            fSwitchBox_rgb_mode.UseGradientFill = false;
            fSwitchBox_rgb_mode.CheckedChanged += fSwitchBox_rgb_mode_CheckedChanged;
            // 
            // fTextBox2
            // 
            fTextBox2.BackColor = Color.Transparent;
            fTextBox2.BackgroundColor = Color.FromArgb(37, 52, 68);
            fTextBox2.BorderColor = Color.FromArgb(29, 200, 238);
            fTextBox2.BorderWidth = 3F;
            fTextBox2.ControlStyle = FControlBase.ControlStyleMode.Custom;
            fTextBox2.CornerRadius = 60;
            fTextBox2.DisplayText = "Text";
            fTextBox2.Font = new Font("Arial", 10F);
            fTextBox2.ForeColor = Color.FromArgb(245, 245, 245);
            fTextBox2.GradientBorderColor1 = Color.FromArgb(29, 200, 238);
            fTextBox2.GradientBorderColor2 = Color.FromArgb(37, 52, 68);
            fTextBox2.GradientColor1 = Color.Empty;
            fTextBox2.GradientColor2 = Color.Empty;
            fTextBox2.Lighting = false;
            fTextBox2.LightingAlpha = 20;
            fTextBox2.LightingColor = Color.FromArgb(29, 200, 238);
            fTextBox2.LightingWidth = 15;
            fTextBox2.Location = new Point(153, 673);
            fTextBox2.Margin = new Padding(6, 3, 6, 3);
            fTextBox2.Name = "fTextBox2";
            fTextBox2.Password = true;
            fTextBox2.RgbUpdateInterval = 300;
            fTextBox2.Rounding = true;
            fTextBox2.ShowBackground = false;
            fTextBox2.ShowBorder = true;
            fTextBox2.Size = new Size(317, 61);
            fTextBox2.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            fTextBox2.TabIndex = 10;
            fTextBox2.Tag = "FC_UI";
            fTextBox2.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            fTextBox2.UseGradientBackground = false;
            fTextBox2.UseGradientBorder = false;
            // 
            // fSwitchBox_random_style
            // 
            fSwitchBox_random_style.BackColor = Color.Transparent;
            fSwitchBox_random_style.BackgroundColor = Color.FromArgb(37, 52, 68);
            fSwitchBox_random_style.BorderColor = Color.FromArgb(29, 200, 238);
            fSwitchBox_random_style.BorderWidth = 2F;
            fSwitchBox_random_style.Checked = false;
            fSwitchBox_random_style.ColorValue = Color.FromArgb(29, 200, 238);
            fSwitchBox_random_style.ControlStyle = FControlBase.ControlStyleMode.Custom;
            fSwitchBox_random_style.CornerRadius = 90;
            fSwitchBox_random_style.Font = new Font("Arial", 11F);
            fSwitchBox_random_style.ForeColor = Color.FromArgb(245, 245, 245);
            fSwitchBox_random_style.GradientBorderColor1 = Color.FromArgb(37, 52, 68);
            fSwitchBox_random_style.GradientBorderColor2 = Color.FromArgb(41, 63, 86);
            fSwitchBox_random_style.GradientColor1 = Color.FromArgb(37, 52, 68);
            fSwitchBox_random_style.GradientColor2 = Color.FromArgb(41, 63, 86);
            fSwitchBox_random_style.GradientFillColor1 = Color.FromArgb(28, 200, 238);
            fSwitchBox_random_style.GradientFillColor2 = Color.FromArgb(100, 208, 232);
            fSwitchBox_random_style.Lighting = false;
            fSwitchBox_random_style.LightingAlpha = 50;
            fSwitchBox_random_style.LightingColor = Color.FromArgb(29, 200, 238);
            fSwitchBox_random_style.LightingWidth = 10;
            fSwitchBox_random_style.Location = new Point(520, 381);
            fSwitchBox_random_style.Margin = new Padding(4, 3, 4, 3);
            fSwitchBox_random_style.Name = "fSwitchBox_random_style";
            fSwitchBox_random_style.RgbUpdateInterval = 300;
            fSwitchBox_random_style.Rounding = true;
            fSwitchBox_random_style.ShowBackground = true;
            fSwitchBox_random_style.ShowBorder = true;
            fSwitchBox_random_style.Size = new Size(41, 23);
            fSwitchBox_random_style.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            fSwitchBox_random_style.TabIndex = 9;
            fSwitchBox_random_style.Tag = "FC_UI";
            fSwitchBox_random_style.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            fSwitchBox_random_style.UseGradientBackground = false;
            fSwitchBox_random_style.UseGradientBorder = false;
            fSwitchBox_random_style.UseGradientFill = false;
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
            fTextBox1.BackColor = Color.Transparent;
            fTextBox1.BackgroundColor = Color.FromArgb(37, 52, 68);
            fTextBox1.BorderColor = Color.FromArgb(29, 200, 238);
            fTextBox1.BorderWidth = 3F;
            fTextBox1.ControlStyle = FControlBase.ControlStyleMode.Custom;
            fTextBox1.CornerRadius = 60;
            fTextBox1.DisplayText = "Text";
            fTextBox1.Font = new Font("Arial", 15F);
            fTextBox1.ForeColor = Color.FromArgb(245, 245, 245);
            fTextBox1.GradientBorderColor1 = Color.FromArgb(29, 200, 238);
            fTextBox1.GradientBorderColor2 = Color.FromArgb(37, 52, 68);
            fTextBox1.GradientColor1 = Color.Empty;
            fTextBox1.GradientColor2 = Color.Empty;
            fTextBox1.Lighting = false;
            fTextBox1.LightingAlpha = 20;
            fTextBox1.LightingColor = Color.FromArgb(29, 200, 238);
            fTextBox1.LightingWidth = 15;
            fTextBox1.Location = new Point(178, 676);
            fTextBox1.Margin = new Padding(7, 3, 7, 3);
            fTextBox1.Name = "fTextBox1";
            fTextBox1.RgbUpdateInterval = 300;
            fTextBox1.Rounding = true;
            fTextBox1.ShowBackground = false;
            fTextBox1.ShowBorder = true;
            fTextBox1.Size = new Size(370, 70);
            fTextBox1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            fTextBox1.TabIndex = 7;
            fTextBox1.Tag = "FC_UI";
            fTextBox1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            fTextBox1.UseGradientBackground = false;
            fTextBox1.UseGradientBorder = false;
            // 
            // fScrollBar1
            // 
            fScrollBar1.BackColor = Color.Transparent;
            fScrollBar1.BackgroundColor = Color.FromArgb(37, 52, 68);
            fScrollBar1.BorderColor = Color.FromArgb(29, 200, 238);
            fScrollBar1.BorderWidth = 3F;
            fScrollBar1.ControlStyle = FControlBase.ControlStyleMode.Custom;
            fScrollBar1.CornerRadius = 70;
            fScrollBar1.ForeColor = Color.FromArgb(245, 245, 245);
            fScrollBar1.GradientBorderColor1 = Color.FromArgb(37, 52, 68);
            fScrollBar1.GradientBorderColor2 = Color.FromArgb(41, 63, 86);
            fScrollBar1.GradientColor1 = Color.FromArgb(37, 52, 68);
            fScrollBar1.GradientColor2 = Color.FromArgb(41, 63, 86);
            fScrollBar1.GradientFillColor1 = Color.FromArgb(28, 200, 238);
            fScrollBar1.GradientFillColor2 = Color.FromArgb(100, 208, 232);
            fScrollBar1.Lighting = false;
            fScrollBar1.LightingAlpha = 50;
            fScrollBar1.LightingColor = Color.FromArgb(29, 200, 238);
            fScrollBar1.LightingWidth = 10;
            fScrollBar1.Location = new Point(59, 224);
            fScrollBar1.Margin = new Padding(4, 3, 4, 3);
            fScrollBar1.Maximum = 100;
            fScrollBar1.Minimum = 0;
            fScrollBar1.Name = "fScrollBar1";
            fScrollBar1.Orientation = Orientation.Horizontal;
            fScrollBar1.RgbUpdateInterval = 300;
            fScrollBar1.Rounding = true;
            fScrollBar1.ShowBackground = true;
            fScrollBar1.ShowBorder = true;
            fScrollBar1.Size = new Size(350, 30);
            fScrollBar1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            fScrollBar1.TabIndex = 6;
            fScrollBar1.Tag = "FC_UI";
            fScrollBar1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            fScrollBar1.ThumbColor = Color.FromArgb(29, 200, 238);
            fScrollBar1.ThumbOpacity = 255;
            fScrollBar1.ThumbSize = 60;
            fScrollBar1.UseGradientBackground = false;
            fScrollBar1.UseGradientBorder = false;
            fScrollBar1.UseGradientFill = false;
            fScrollBar1.Value = 0;
            fScrollBar1.ValueChanged += fScrollBar1_ValueChanged;
            // 
            // fRichTextBox1
            // 
            fRichTextBox1.BackColor = Color.Transparent;
            fRichTextBox1.BackgroundColor = Color.FromArgb(37, 52, 68);
            fRichTextBox1.BorderColor = Color.FromArgb(29, 200, 238);
            fRichTextBox1.BorderWidth = 3F;
            fRichTextBox1.ControlStyle = FControlBase.ControlStyleMode.Custom;
            fRichTextBox1.CornerRadius = 60;
            fRichTextBox1.Font = new Font("Arial", 11F);
            fRichTextBox1.ForeColor = Color.FromArgb(245, 245, 245);
            fRichTextBox1.GradientBorderColor1 = Color.FromArgb(29, 200, 238);
            fRichTextBox1.GradientBorderColor2 = Color.FromArgb(37, 52, 68);
            fRichTextBox1.GradientColor1 = Color.Empty;
            fRichTextBox1.GradientColor2 = Color.Empty;
            fRichTextBox1.Lighting = false;
            fRichTextBox1.LightingAlpha = 20;
            fRichTextBox1.LightingColor = Color.FromArgb(29, 200, 238);
            fRichTextBox1.LightingWidth = 15;
            fRichTextBox1.Location = new Point(238, 272);
            fRichTextBox1.Margin = new Padding(4, 3, 4, 3);
            fRichTextBox1.Name = "fRichTextBox1";
            fRichTextBox1.RgbUpdateInterval = 300;
            fRichTextBox1.Rounding = true;
            fRichTextBox1.ShowBackground = false;
            fRichTextBox1.ShowBorder = true;
            fRichTextBox1.Size = new Size(175, 150);
            fRichTextBox1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            fRichTextBox1.TabIndex = 5;
            fRichTextBox1.Tag = "FC_UI";
            fRichTextBox1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            fRichTextBox1.UseGradientBackground = false;
            fRichTextBox1.UseGradientBorder = false;
            // 
            // fRadioButton1
            // 
            fRadioButton1.BackColor = Color.Transparent;
            fRadioButton1.BackgroundColor = Color.FromArgb(37, 52, 68);
            fRadioButton1.BorderColor = Color.FromArgb(29, 200, 238);
            fRadioButton1.BorderWidth = 2F;
            fRadioButton1.Checked = true;
            fRadioButton1.ClickEffectColor = Color.FromArgb(29, 200, 238);
            fRadioButton1.ClickEffectInterval = 1;
            fRadioButton1.ClickEffectOpacity = 25;
            fRadioButton1.ColorChecked = Color.FromArgb(29, 200, 238);
            fRadioButton1.ControlStyle = FControlBase.ControlStyleMode.Custom;
            fRadioButton1.CornerRadius = 100;
            fRadioButton1.DisplayText = "FRadioButton";
            fRadioButton1.EnableHoverEffect = true;
            fRadioButton1.Font = new Font("Arial", 11F);
            fRadioButton1.ForeColor = Color.FromArgb(245, 245, 245);
            fRadioButton1.GradientBorderColor1 = Color.FromArgb(37, 52, 68);
            fRadioButton1.GradientBorderColor2 = Color.FromArgb(41, 63, 86);
            fRadioButton1.GradientColor1 = Color.FromArgb(37, 52, 68);
            fRadioButton1.GradientColor2 = Color.FromArgb(41, 63, 86);
            fRadioButton1.GradientFillColor1 = Color.Empty;
            fRadioButton1.GradientFillColor2 = Color.Empty;
            fRadioButton1.HoverEffectColor = Color.White;
            fRadioButton1.HoverEffectOpacity = 15;
            fRadioButton1.Lighting = false;
            fRadioButton1.LightingAlpha = 0;
            fRadioButton1.LightingColor = Color.Empty;
            fRadioButton1.LightingWidth = 0;
            fRadioButton1.Location = new Point(234, 106);
            fRadioButton1.Margin = new Padding(4, 3, 4, 3);
            fRadioButton1.Name = "fRadioButton1";
            fRadioButton1.RgbUpdateInterval = 300;
            fRadioButton1.Rounding = true;
            fRadioButton1.ShowBackground = true;
            fRadioButton1.ShowBorder = true;
            fRadioButton1.Size = new Size(163, 45);
            fRadioButton1.SizeChecked = 8;
            fRadioButton1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            fRadioButton1.TabIndex = 4;
            fRadioButton1.Tag = "FC_UI";
            fRadioButton1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            fRadioButton1.UseGradientBackground = false;
            fRadioButton1.UseGradientBorder = false;
            fRadioButton1.UseGradientFill = false;
            // 
            // fProgressBar1
            // 
            fProgressBar1.BackColor = Color.Transparent;
            fProgressBar1.BackgroundColor = Color.FromArgb(37, 52, 68);
            fProgressBar1.BorderColor = Color.FromArgb(29, 200, 238);
            fProgressBar1.BorderWidth = 3F;
            fProgressBar1.ControlStyle = FControlBase.ControlStyleMode.Custom;
            fProgressBar1.CornerRadius = 70;
            fProgressBar1.FillColor = Color.FromArgb(29, 200, 238);
            fProgressBar1.FillOpacity = 200;
            fProgressBar1.Font = new Font("Arial", 11F);
            fProgressBar1.ForeColor = Color.FromArgb(245, 245, 245);
            fProgressBar1.GradientBorderColor1 = Color.FromArgb(37, 52, 68);
            fProgressBar1.GradientBorderColor2 = Color.FromArgb(41, 63, 86);
            fProgressBar1.GradientColor1 = Color.FromArgb(37, 52, 68);
            fProgressBar1.GradientColor2 = Color.FromArgb(41, 63, 86);
            fProgressBar1.GradientFillColor1 = Color.FromArgb(28, 200, 238);
            fProgressBar1.GradientFillColor2 = Color.FromArgb(100, 208, 232);
            fProgressBar1.Lighting = false;
            fProgressBar1.LightingAlpha = 50;
            fProgressBar1.LightingColor = Color.FromArgb(29, 200, 238);
            fProgressBar1.LightingWidth = 10;
            fProgressBar1.Location = new Point(59, 170);
            fProgressBar1.Margin = new Padding(4, 3, 4, 3);
            fProgressBar1.Maximum = 100;
            fProgressBar1.Minimum = 0;
            fProgressBar1.Name = "fProgressBar1";
            fProgressBar1.ProgressText = true;
            fProgressBar1.RgbUpdateInterval = 300;
            fProgressBar1.Rounding = true;
            fProgressBar1.ShowBackground = true;
            fProgressBar1.ShowBorder = true;
            fProgressBar1.Size = new Size(350, 39);
            fProgressBar1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            fProgressBar1.StartDrawingValue = 0;
            fProgressBar1.TabIndex = 3;
            fProgressBar1.Tag = "FC_UI";
            fProgressBar1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            fProgressBar1.UseGradientBackground = false;
            fProgressBar1.UseGradientBorder = false;
            fProgressBar1.UseGradientFill = false;
            fProgressBar1.Value = 5;
            // 
            // fGroupBox1
            // 
            fGroupBox1.BackColor = Color.Transparent;
            fGroupBox1.BackgroundColor = Color.FromArgb(37, 52, 68);
            fGroupBox1.BorderColor = Color.FromArgb(29, 200, 238);
            fGroupBox1.BorderWidth = 3F;
            fGroupBox1.ControlStyle = FControlBase.ControlStyleMode.Custom;
            fGroupBox1.CornerRadius = 60;
            fGroupBox1.ForeColor = Color.FromArgb(245, 245, 245);
            fGroupBox1.GradientBorderColor1 = Color.FromArgb(37, 52, 68);
            fGroupBox1.GradientBorderColor2 = Color.FromArgb(41, 63, 86);
            fGroupBox1.GradientColor1 = Color.FromArgb(37, 52, 68);
            fGroupBox1.GradientColor2 = Color.FromArgb(41, 63, 86);
            fGroupBox1.Lighting = false;
            fGroupBox1.LightingAlpha = 20;
            fGroupBox1.LightingColor = Color.FromArgb(29, 200, 238);
            fGroupBox1.LightingWidth = 15;
            fGroupBox1.Location = new Point(56, 272);
            fGroupBox1.Margin = new Padding(4, 3, 4, 3);
            fGroupBox1.Name = "fGroupBox1";
            fGroupBox1.RgbUpdateInterval = 300;
            fGroupBox1.Rounding = true;
            fGroupBox1.ShowBackground = true;
            fGroupBox1.ShowBorder = true;
            fGroupBox1.Size = new Size(175, 150);
            fGroupBox1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            fGroupBox1.TabIndex = 2;
            fGroupBox1.Tag = "FC_UI";
            fGroupBox1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            fGroupBox1.UseGradientBackground = false;
            fGroupBox1.UseGradientBorder = false;
            // 
            // fCheckBox1
            // 
            fCheckBox1.BackColor = Color.Transparent;
            fCheckBox1.BackgroundColor = Color.FromArgb(37, 52, 68);
            fCheckBox1.BorderColor = Color.FromArgb(29, 200, 238);
            fCheckBox1.BorderWidth = 2F;
            fCheckBox1.Checked = true;
            fCheckBox1.ClickEffectColor = Color.FromArgb(29, 200, 238);
            fCheckBox1.ClickEffectInterval = 1;
            fCheckBox1.ClickEffectOpacity = 25;
            fCheckBox1.ColorChecked = Color.FromArgb(29, 200, 238);
            fCheckBox1.ControlStyle = FControlBase.ControlStyleMode.Custom;
            fCheckBox1.CornerRadius = 100;
            fCheckBox1.DisplayText = "FCheckBox";
            fCheckBox1.EnableHoverEffect = true;
            fCheckBox1.Font = new Font("Arial", 11F);
            fCheckBox1.ForeColor = Color.FromArgb(245, 245, 245);
            fCheckBox1.GradientBorderColor1 = Color.FromArgb(37, 52, 68);
            fCheckBox1.GradientBorderColor2 = Color.FromArgb(41, 63, 86);
            fCheckBox1.GradientColor1 = Color.FromArgb(37, 52, 68);
            fCheckBox1.GradientColor2 = Color.FromArgb(41, 63, 86);
            fCheckBox1.HoverEffectColor = Color.White;
            fCheckBox1.HoverEffectOpacity = 15;
            fCheckBox1.Lighting = false;
            fCheckBox1.LightingAlpha = 0;
            fCheckBox1.LightingColor = Color.Empty;
            fCheckBox1.LightingWidth = 0;
            fCheckBox1.Location = new Point(59, 106);
            fCheckBox1.Margin = new Padding(4, 3, 4, 3);
            fCheckBox1.Name = "fCheckBox1";
            fCheckBox1.RgbUpdateInterval = 300;
            fCheckBox1.Rounding = true;
            fCheckBox1.ShowBackground = true;
            fCheckBox1.ShowBorder = true;
            fCheckBox1.Size = new Size(163, 45);
            fCheckBox1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            fCheckBox1.TabIndex = 1;
            fCheckBox1.Tag = "FC_UI";
            fCheckBox1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            fCheckBox1.UseGradientBackground = false;
            fCheckBox1.UseGradientBorder = false;
            // 
            // fButton1
            // 
            fButton1.BackColor = Color.Transparent;
            fButton1.BackgroundColor = Color.FromArgb(37, 52, 68);
            fButton1.BorderColor = Color.FromArgb(29, 200, 238);
            fButton1.BorderWidth = 4F;
            fButton1.ClickEffectColor = Color.FromArgb(29, 200, 238);
            fButton1.ClickEffectInterval = 5;
            fButton1.ClickEffectOpacity = 25;
            fButton1.ControlStyle = FControlBase.ControlStyleMode.Custom;
            fButton1.CornerRadius = 70;
            fButton1.DisplayText = "FButton";
            fButton1.EnableClickEffect = true;
            fButton1.EnableHoverEffect = true;
            fButton1.Font = new Font("Arial", 11F);
            fButton1.ForeColor = Color.FromArgb(245, 245, 245);
            fButton1.GradientBorderColor1 = Color.FromArgb(37, 52, 68);
            fButton1.GradientBorderColor2 = Color.FromArgb(41, 63, 86);
            fButton1.GradientColor1 = Color.FromArgb(37, 52, 68);
            fButton1.GradientColor2 = Color.FromArgb(41, 63, 86);
            fButton1.HoverEffectColor = Color.White;
            fButton1.HoverEffectOpacity = 20;
            fButton1.Lighting = false;
            fButton1.LightingAlpha = 20;
            fButton1.LightingColor = Color.FromArgb(29, 200, 238);
            fButton1.LightingWidth = 15;
            fButton1.Location = new Point(147, 29);
            fButton1.Margin = new Padding(4, 3, 4, 3);
            fButton1.Name = "fButton1";
            fButton1.RgbUpdateInterval = 300;
            fButton1.Rounding = true;
            fButton1.ShowBackground = true;
            fButton1.ShowBorder = true;
            fButton1.Size = new Size(152, 58);
            fButton1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            fButton1.TabIndex = 0;
            fButton1.Tag = "FC_UI";
            fButton1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            fButton1.UseGradientBackground = false;
            fButton1.UseGradientBorder = false;
            // 
            // fGlobalRgb1
            // 
            fGlobalRgb1.Status = false;
            fGlobalRgb1.TimerInterval = 300;
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
        private FGlobalRgb fGlobalRgb1;
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
