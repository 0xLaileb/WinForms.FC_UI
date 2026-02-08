using System.ComponentModel;

namespace FC_UI.Controls;

partial class ZColorPicker
{
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.pictureBox1 = new();
        this.pictureBox2 = new();
        this.label1 = new();
        this.pictureBox3 = new();
        this.label2 = new();
        ((ISupportInitialize)(this.pictureBox1)).BeginInit();
        ((ISupportInitialize)(this.pictureBox2)).BeginInit();
        ((ISupportInitialize)(this.pictureBox3)).BeginInit();
        this.SuspendLayout();
        // 
        // pictureBox1
        // 
        this.pictureBox1.Location = new(3, 3);
        this.pictureBox1.Name = "pictureBox1";
        this.pictureBox1.Size = new(150, 150);
        this.pictureBox1.TabIndex = 8;
        this.pictureBox1.TabStop = false;
        this.pictureBox1.Paint += this.pictureBox1_Paint;
        this.pictureBox1.MouseDown += this.pictureBox1_MouseDown;
        this.pictureBox1.MouseMove += this.pictureBox1_MouseMove;
        this.pictureBox1.MouseUp += this.pictureBox1_MouseUp;
        // 
        // pictureBox2
        // 
        this.pictureBox2.Location = new(165, 116);
        this.pictureBox2.Name = "pictureBox2";
        this.pictureBox2.Size = new(17, 37);
        this.pictureBox2.TabIndex = 9;
        this.pictureBox2.TabStop = false;
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.BackColor = Color.Transparent;
        this.label1.Font = new("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
        this.label1.ForeColor = Color.WhiteSmoke;
        this.label1.Location = new(47, 162);
        this.label1.Name = "label1";
        this.label1.Size = new(73, 16);
        this.label1.TabIndex = 12;
        this.label1.Text = "RGB: none";
        // 
        // pictureBox3
        // 
        this.pictureBox3.Location = new(165, 3);
        this.pictureBox3.Name = "pictureBox3";
        this.pictureBox3.Size = new(17, 107);
        this.pictureBox3.TabIndex = 13;
        this.pictureBox3.TabStop = false;
        this.pictureBox3.Paint += this.pictureBox3_Paint;
        this.pictureBox3.MouseDown += this.pictureBox3_MouseDown;
        this.pictureBox3.MouseMove += this.pictureBox3_MouseMove;
        this.pictureBox3.MouseUp += this.pictureBox3_MouseUp;
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Font = new("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, (byte)204);
        this.label2.ForeColor = Color.WhiteSmoke;
        this.label2.Location = new(47, 182);
        this.label2.Name = "label2";
        this.label2.Size = new(71, 16);
        this.label2.TabIndex = 16;
        this.label2.Text = "HEX: none";
        // 
        // ZColorPicker
        // 
        this.AutoScaleDimensions = new SizeF(6F, 13F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.BackColor = Color.Transparent;
        this.Controls.Add(this.label2);
        this.Controls.Add(this.pictureBox3);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.pictureBox2);
        this.Controls.Add(this.pictureBox1);
        this.ForeColor = Color.Black;
        this.Name = "ZColorPicker";
        this.Size = new(185, 210);
        ((ISupportInitialize)(this.pictureBox1)).EndInit();
        ((ISupportInitialize)(this.pictureBox2)).EndInit();
        ((ISupportInitialize)(this.pictureBox3)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion

    private PictureBox pictureBox1;
    private PictureBox pictureBox2;
    private Label label1;
    private PictureBox pictureBox3;
    private Label label2;
}
