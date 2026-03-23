using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace FC_UI.Controls;

public partial class ZColorPicker : UserControl
{
    #region Fields

    private sealed class BrightnessBox
    {
        public float Value = 1F;
        public Color Color = Color.White;
    }

    private bool _brightnessMouseDown;
    private PointF _cursorPosition;
    private bool _wheelMouseDown;

    #endregion

    #region Properties

    public delegate void ColorChangedHandler(Color color);

    [Category("FC_UI")]
    [Description("Occurs when the selected color changes.")]
    public event ColorChangedHandler ColorChanged = delegate { };

    [Category("ZColorPicker")]
    [Description("Selected color")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color SelectedColor
    {
        get;
        set
        {
            field = value;
            ColorChanged(value);
        }
    }

    #endregion

    #region Initialization

    public ZColorPicker()
    {
        InitializeComponent();

        Tag = "FC_UI";
        pictureBox1.Tag = new PointF(pictureBox1.Width / 2f, pictureBox1.Height / 2f);
        pictureBox3.Tag = new BrightnessBox();

        Bitmap wheel = new(pictureBox1.Width, pictureBox1.Height);
        using (var g = Graphics.FromImage(wheel))
        {
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            DrawWheel(wheel.Width / 2f, g);
        }
        pictureBox1.Image = wheel;
        _cursorPosition = new PointF(pictureBox1.Height / 2f, pictureBox1.Height / 2f);
    }

    #endregion

    #region Events

    private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
    {
        _wheelMouseDown = true;
        PickColor(e.X, e.Y);
    }

    private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
    {
        if (_wheelMouseDown) PickColor(e.X, e.Y);
    }

    private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
    {
        _wheelMouseDown = false;
        PickColor(e.X, e.Y);
    }

    private void pictureBox1_Paint(object sender, PaintEventArgs e)
    {
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        using var pen = new Pen(Color.DarkGray, 1F);
        e.Graphics.DrawEllipse(pen, _cursorPosition.X - 4, _cursorPosition.Y - 4, 8, 8);
    }

    private void pictureBox3_Paint(object sender, PaintEventArgs e)
    {
        if (pictureBox3.Tag is not BrightnessBox box) return;
        using var gradientBrush = new LinearGradientBrush(new Point(0, 0), new Point(0, pictureBox3.Height), box.Color, Color.Black);
        e.Graphics.FillRectangle(gradientBrush, pictureBox3.ClientRectangle);
        using var markerBrush = new SolidBrush(Color.DarkGray);
        e.Graphics.FillRectangle(markerBrush, 0, pictureBox3.Height * (1 - box.Value) - 2, pictureBox3.Width, 4);
    }

    private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
    {
        _brightnessMouseDown = true;
        if (e.Y > pictureBox3.Height || e.Y < 0) return;
        if (pictureBox3.Tag is BrightnessBox box && pictureBox1.Tag is PointF pt)
        {
            box.Value = (float)(pictureBox3.Height - e.Y) / pictureBox3.Height;
            PickColor(pt.X, pt.Y);
        }
    }

    private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
    {
        if (!_brightnessMouseDown) return;
        if (e.Y > pictureBox3.Height || e.Y < 0) return;
        if (pictureBox3.Tag is BrightnessBox box && pictureBox1.Tag is PointF pt)
        {
            box.Value = (float)(pictureBox3.Height - e.Y) / pictureBox3.Height;
            PickColor(pt.X, pt.Y);
        }
    }

    private void pictureBox3_MouseUp(object sender, MouseEventArgs e) => _brightnessMouseDown = false;

    #endregion

    #region Drawing

    private void DrawWheel(float radius, Graphics graphics)
    {
        using GraphicsPath fillPath = new();
        fillPath.AddEllipse(0, 0, radius * 2, radius * 2);
        fillPath.Flatten();

        using GraphicsPath brushPath = new();
        brushPath.AddEllipse(-1, -1, radius * 2 + 2, radius * 2 + 2);
        brushPath.Flatten();

        using var brush = CreateWheelBrush(brushPath);
        graphics.FillPath(brush, fillPath);
    }

    private static Brush CreateWheelBrush(GraphicsPath path)
    {
        PathGradientBrush brush = new(path) { CenterColor = Color.White };

        var colors = new Color[path.PointCount];
        for (var i = 0; i < colors.Length; i++)
        {
            var hue = (float)i / colors.Length;
            colors[i] = DrawEngine.HsvToRgb(hue * 360, 1f, 1f);
        }
        brush.SurroundColors = colors;

        return brush;
    }

    private Color GetPixelColorFromWheel(float x, float y, float brightness, float radius)
    {
        var distance = MathF.Sqrt(x * x + y * y);
        var saturation = distance / radius;
        if (saturation > 1) return Color.Transparent;

        var angle = x switch
        {
            > 0 when y > 0 => Math.Asin(y / distance),
            <= 0 when y > 0 => Math.Acos(y / distance) + Math.PI / 2,
            <= 0 when y <= 0 => Math.Asin(-y / distance) + Math.PI,
            _ => Math.Acos(-y / distance) + 3 * Math.PI / 2
        };

        var hue = (float)(angle / Math.PI / 2) * 360;
        return DrawEngine.HsvToRgb(hue, saturation, brightness);
    }

    private Color PickColor(float x, float y)
    {
        var centerX = pictureBox1.Width / 2f;
        var centerY = pictureBox1.Height / 2f;
        var relX = x - centerX;
        var relY = y - centerY;
        var distance = MathF.Sqrt(relX * relX + relY * relY);

        if (distance > centerX)
        {
            var scale = centerX / distance;
            relX *= scale;
            relY *= scale;
        }

        var brightness = pictureBox3.Tag is BrightnessBox bb ? bb.Value : 1f;
        var color = GetPixelColorFromWheel(relX, relY, brightness, centerX);
        if (color == Color.Transparent) return Color.Empty;

        label1.Text = $@"RGB: {color.R}, {color.G}, {color.B}";
        label2.Text = $@"HEX: #{color.ToArgb():X}";
        _cursorPosition = new PointF(relX + centerX, relY + centerY);
        pictureBox2.BackColor = color;
        if (pictureBox3.Tag is BrightnessBox brightnessBox) brightnessBox.Color = color;
        pictureBox1.Tag = new PointF(x, y);
        pictureBox1.Invalidate();
        pictureBox2.Invalidate();
        pictureBox3.Invalidate();

        SelectedColor = color;
        return color;
    }

    #endregion
}
