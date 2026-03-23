using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace FC_UI.Controls;

[ToolboxBitmap(typeof(CheckBox))]
[Description("Allows the user to enable or disable the corresponding option.")]
public partial class FSwitchBox : FControlBase
{
    #region Properties

    public delegate void CheckedChangedHandler();

    [Category("FC_UI")]
    [Description("Occurs on every Checked property change.")]
    public event CheckedChangedHandler CheckedChanged = delegate { };

    [Category("FSwitchBox")]
    [Description("Enable/Disable")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool Checked
    {
        get;
        set
        {
            field = value;
            CheckedChanged();
            Refresh();
        }
    }

    [Category("FSwitchBox")]
    [Description("Inner circle color")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color ColorValue
    {
        get;
        set { field = value; Refresh(); }
    }

    // --- Gradient Fill ---

    [Category("LinearGradient")]
    [Description("Enable/Disable value gradient")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool UseGradientFill
    {
        get;
        set { field = value; Refresh(); }
    }

    [Category("LinearGradient")]
    [Description("Value gradient color #1")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color GradientFillColor1
    {
        get;
        set { field = value; Refresh(); }
    }

    [Category("LinearGradient")]
    [Description("Value gradient color #2")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color GradientFillColor2
    {
        get;
        set { field = value; Refresh(); }
    }

    // --- Style ---

    [Category("FSwitchBox")]
    [Description("Control style")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public ControlStyleMode ControlStyle
    {
        get;
        set
        {
            field = value;
            switch (field)
            {
                case ControlStyleMode.Default:
                    Size = new Size(35, 20);
                    BackColor = Color.Transparent;
                    ForeColor = Color.FromArgb(245, 245, 245);
                    Checked = false;
                    Rgb = false;
                    ShowBackground = true;
                    Rounding = true;
                    CornerRadius = 90;
                    ColorValue = Color.FromArgb(29, 200, 238);
                    BackgroundColor = Color.FromArgb(37, 52, 68);
                    RgbUpdateInterval = 300;
                    ShowBorder = true;
                    BorderWidth = 2F;
                    BorderColor = Color.FromArgb(29, 200, 238);
                    UseGradientBackground = false;
                    GradientColor1 = Color.FromArgb(37, 52, 68);
                    GradientColor2 = Color.FromArgb(41, 63, 86);
                    UseGradientFill = false;
                    GradientFillColor1 = Color.FromArgb(28, 200, 238);
                    GradientFillColor2 = Color.FromArgb(100, 208, 232);
                    Lighting = false;
                    LightingColor = Color.FromArgb(29, 200, 238);
                    LightingAlpha = 50;
                    LightingWidth = 10;
                    UseGradientBorder = false;
                    GradientBorderColor1 = Color.FromArgb(37, 52, 68);
                    GradientBorderColor2 = Color.FromArgb(41, 63, 86);
                    SmoothingMode = SmoothingMode.HighQuality;
                    TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                    Font = HelpEngine.GetDefaultFont();
                    break;
                case ControlStyleMode.Custom:
                    break;
                case ControlStyleMode.Random:
                    ShowBackground = HelpEngine.RandomBool();
                    Rounding = HelpEngine.RandomBool();
                    if (Rounding) CornerRadius = HelpEngine.RandomInt(5, 90);
                    if (ShowBackground) BackgroundColor = HelpEngine.RandomColor(HelpEngine.RandomInt(0, 255));
                    ShowBorder = HelpEngine.RandomBool();
                    if (ShowBorder)
                    {
                        BorderWidth = HelpEngine.RandomFloat(1, 3);
                        BorderColor = HelpEngine.RandomColor(HelpEngine.RandomInt(0, 255));
                    }
                    Lighting = HelpEngine.RandomBool();
                    if (Lighting) LightingColor = HelpEngine.RandomColor();
                    UseGradientBackground = HelpEngine.RandomBool();
                    if (UseGradientBackground)
                    {
                        GradientColor1 = HelpEngine.RandomColor();
                        GradientColor2 = HelpEngine.RandomColor();
                    }
                    UseGradientFill = HelpEngine.RandomBool();
                    if (UseGradientFill)
                    {
                        GradientFillColor1 = HelpEngine.RandomColor();
                        GradientFillColor2 = HelpEngine.RandomColor();
                    }
                    UseGradientBorder = HelpEngine.RandomBool();
                    if (UseGradientBorder)
                    {
                        GradientBorderColor1 = HelpEngine.RandomColor();
                        GradientBorderColor2 = HelpEngine.RandomColor();
                    }
                    ColorValue = HelpEngine.RandomColor(HelpEngine.RandomInt(0, 255));
                    break;
            }
            Refresh();
        }
    }

    #endregion

    #region Initialization

    public FSwitchBox()
    {
        ControlStyle = ControlStyleMode.Default;
        ControlStyle = ControlStyleMode.Custom;
        OnSizeChanged(EventArgs.Empty);
    }

    #endregion

    #region Events

    protected override void OnPaint(PaintEventArgs e)
    {
        try
        {
            ApplyGraphicsSettings(e.Graphics);
            DrawBackground(e.Graphics);
        }
        catch (Exception ex) { System.Diagnostics.Debug.WriteLine($"[{Name}] OnPaint error: {ex}"); }
    }

    protected override void OnMouseClick(MouseEventArgs e)
    {
        Checked = !Checked;
        Refresh();
    }

    protected override void OnSizeChanged(EventArgs e)
    {
        RecalculateRegion();
    }

    #endregion

    #region Drawing

    private void DrawBackground(Graphics formGraphics)
    {
        var roundingValue = CalculateRoundingValue(ControlSize.Height);

        ShapePath.Dispose();
        ShapePath = DrawEngine.CreateRoundedPath(RegionRect, roundingValue);

        using var regionPath = DrawEngine.CreateRoundedPath(new Rectangle(0, 0, Width, Height), roundingValue);
        Region?.Dispose();
        Region = new Region(regionPath);

        // Border layer
        using var borderLayer = RenderBorderLayer(roundingValue);
        formGraphics.DrawImage(borderLayer, PointF.Empty);

        // Content layer
        Bitmap contentBitmap = new(Width, Height);
        using (var g = HelpEngine.GetGraphics(contentBitmap, SmoothingMode, TextRenderingHint))
        {
            using var clipPath = DrawEngine.CreateRoundedPath(new Rectangle(
                RegionRect.X - (int)(2 + BorderWidth),
                RegionRect.Y - (int)(2 + BorderWidth),
                RegionRect.Width + (int)(2 + BorderWidth) * 2,
                RegionRect.Height + (int)(2 + BorderWidth) * 2), Rounding ? roundingValue : 0.1F);
            using Region clipRegion = new(clipPath);
            g.Clip = clipRegion;

            if (ShowBackground)
            {
                if (UseGradientBackground)
                {
                    using LinearGradientBrush brush = new(RegionRect, GradientColor1, GradientColor2, 360);
                    g.FillPath(brush, ShapePath);
                }
                else
                {
                    using SolidBrush brush = new(BackgroundColor);
                    g.FillPath(brush, ShapePath);
                }
            }

            DrawToggle(g);
        }
        using (contentBitmap) formGraphics.DrawImage(contentBitmap, PointF.Empty);
    }

    private void DrawToggle(Graphics graphics)
    {
        Rectangle toggleRect = new();

        if (Checked)
        {
            var offsetY = RegionRect.Height / 6;
            toggleRect.Height = RegionRect.Height - offsetY * 2;
            toggleRect.Width = toggleRect.Height;
            toggleRect.X = RegionRect.X + RegionRect.Width - (RegionRect.Width / 10) - toggleRect.Width;
            toggleRect.Y = RegionRect.Y + offsetY;

            if (UseGradientFill)
            {
                using LinearGradientBrush brush = new(RegionRect,
                    GetRgbOrColor(GradientFillColor1),
                    Rgb ? DrawEngine.HsvToRgb(Hue + 20, 1f, 1f) : GradientFillColor2,
                    360);
                graphics.FillEllipse(brush, toggleRect);
            }
            else
            {
                using SolidBrush brush = new(GetRgbOrColor(ColorValue));
                graphics.FillEllipse(brush, toggleRect);
            }
        }
        else
        {
            var offsetX = RegionRect.Width / 10;
            var offsetY = RegionRect.Height / 6;
            toggleRect.X = RegionRect.X + offsetX;
            toggleRect.Y = RegionRect.Y + offsetY;
            toggleRect.Height = RegionRect.Height - offsetY * 2;
            toggleRect.Width = toggleRect.Height;
            const float dimFactor = 0.5F;

            Color DimColor(Color c) => Color.FromArgb((int)(c.R * dimFactor), (int)(c.G * dimFactor), (int)(c.B * dimFactor));

            if (UseGradientFill)
            {
                var c1 = DimColor(GetRgbOrColor(GradientFillColor1));
                var c2 = DimColor(Rgb ? DrawEngine.HsvToRgb(Hue + 20, 1f, 1f) : GradientFillColor2);
                using LinearGradientBrush brush = new(RegionRect, c1, c2, 360);
                graphics.FillEllipse(brush, toggleRect);
            }
            else
            {
                var dimmed = DimColor(GetRgbOrColor(ColorValue));
                using SolidBrush brush = new(Color.FromArgb(100, dimmed.R, dimmed.G, dimmed.B));
                graphics.FillEllipse(brush, toggleRect);
            }
        }
    }

    #endregion
}
