using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace FC_UI.Controls;

[ToolboxBitmap(typeof(ProgressBar))]
[Description("Displays an operation progress indicator.")]
public partial class FProgressBar : FControlBase
{
    #region Fields

    private Rectangle _valueRect = new();
    private readonly StringFormat _textFormat = new();
    private int _drawnValueWidth;

    #endregion

    #region Properties

    [Category("Value")]
    [Description("Current value")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int Value
    {
        get;
        set
        {
            if (value <= Maximum && value >= Minimum)
            {
                field = value;
                Refresh();
            }
        }
    }

    [Category("Value")]
    [Description("Minimum value")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int Minimum
    {
        get;
        set
        {
            if (value < Maximum)
            {
                field = value;
                Refresh();
            }
        }
    }

    [Category("Value")]
    [Description("Maximum value")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int Maximum
    {
        get;
        set
        {
            if (value > Minimum)
            {
                field = value;
                Refresh();
            }
        }
    }

    [Category("Value")]
    [Description("Value at which drawing starts (use when Rounding is true to fix small-value artifacts)")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int StartDrawingValue
    {
        get;
        set { field = value; Refresh(); }
    }

    [Category("FProgressBar")]
    [Description("Enable/Disable progress text")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool ProgressText
    {
        get;
        set { field = value; Refresh(); }
    }

    [Category("FProgressBar")]
    [Description("Progress fill color")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color FillColor
    {
        get;
        set { field = value; Refresh(); }
    }

    [Category("Value")]
    [Description("Fill transparency (5-255)")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int FillOpacity
    {
        get;
        set
        {
            if (value is >= 5 and <= 255)
            {
                field = value;
                Refresh();
            }
        }
    }

    // --- Gradient Fill ---

    [Category("LinearGradient")]
    [Description("Enable/Disable fill gradient")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool UseGradientFill
    {
        get;
        set { field = value; Refresh(); }
    }

    [Category("LinearGradient")]
    [Description("Fill gradient color #1")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color GradientFillColor1
    {
        get;
        set { field = value; Refresh(); }
    }

    [Category("LinearGradient")]
    [Description("Fill gradient color #2")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color GradientFillColor2
    {
        get;
        set { field = value; Refresh(); }
    }

    // --- Style ---

    [Category("FProgressBar")]
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
                    Size = new(300, 34);
                    BackColor = Color.Transparent;
                    ForeColor = Color.FromArgb(245, 245, 245);
                    Value = 0;
                    Minimum = 0;
                    Maximum = 100;
                    StartDrawingValue = 0;
                    ProgressText = true;
                    RGB = false;
                    ShowBackground = true;
                    Rounding = true;
                    CornerRadius = 70;
                    BackgroundColor = Color.FromArgb(37, 52, 68);
                    RgbUpdateInterval = 300;
                    ShowBorder = true;
                    BorderWidth = 3F;
                    BorderColor = Color.FromArgb(29, 200, 238);
                    Lighting = false;
                    LightingColor = Color.FromArgb(29, 200, 238);
                    LightingAlpha = 50;
                    LightingWidth = 10;
                    UseGradientBackground = false;
                    GradientColor1 = Color.FromArgb(37, 52, 68);
                    GradientColor2 = Color.FromArgb(41, 63, 86);
                    UseGradientBorder = false;
                    GradientBorderColor1 = Color.FromArgb(37, 52, 68);
                    GradientBorderColor2 = Color.FromArgb(41, 63, 86);
                    UseGradientFill = false;
                    GradientFillColor1 = Color.FromArgb(28, 200, 238);
                    GradientFillColor2 = Color.FromArgb(100, 208, 232);
                    FillOpacity = 200;
                    FillColor = Color.FromArgb(29, 200, 238);
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
                    ProgressText = HelpEngine.RandomBool();
                    FillColor = HelpEngine.RandomColor();
                    FillOpacity = HelpEngine.RandomInt(5, 255);
                    break;
            }
            Refresh();
        }
    }

    #endregion

    #region Initialization

    public FProgressBar()
    {
        ControlStyle = ControlStyleMode.Default;
        ControlStyle = ControlStyleMode.Custom;

        _textFormat.Alignment = StringAlignment.Center;
        _textFormat.LineAlignment = StringAlignment.Center;

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
            if (ProgressText) DrawText(e.Graphics);
        }
        catch (Exception ex) { HelpEngine.ShowError($"[{Name}] Error: \n{ex}"); }
    }

    protected override void OnSizeChanged(EventArgs e)
    {
        RecalculateRegion();
    }

    #endregion

    #region Drawing

    private void DrawBackground(Graphics formGraphics)
    {
        float roundingValue = PrepareGeometry(Height);

        // Border layer
        using Bitmap borderLayer = RenderBorderLayer(roundingValue);
        formGraphics.DrawImage(borderLayer, PointF.Empty);

        // Content layer
        Bitmap contentBitmap = new(Width, Height);
        using (Graphics g = HelpEngine.GetGraphics(contentBitmap, SmoothingMode, TextRenderingHint))
        {
            using GraphicsPath clipPath = DrawEngine.CreateRoundedPath(new(
                _regionRect.X - (int)(2 + BorderWidth),
                _regionRect.Y - (int)(2 + BorderWidth),
                _regionRect.Width + (int)(2 + BorderWidth) * 2,
                _regionRect.Height + (int)(2 + BorderWidth) * 2), Rounding ? roundingValue : 0.1F);
            using Region clipRegion = new(clipPath);
            g.Clip = clipRegion;

            if (ShowBackground)
            {
                if (UseGradientBackground)
                {
                    using LinearGradientBrush brush = new(_regionRect, GradientColor1, GradientColor2, 360);
                    g.FillPath(brush, _shapePath);
                }
                else
                {
                    using SolidBrush brush = new(BackgroundColor);
                    g.FillPath(brush, _shapePath);
                }
            }

            if (Value >= StartDrawingValue) DrawProgressFill(g, roundingValue);
        }
        using (contentBitmap) formGraphics.DrawImage(contentBitmap, PointF.Empty);
    }

    private void DrawText(Graphics graphics)
    {
        using SolidBrush brush = new(ForeColor);
        int percent = Maximum != 0 ? Value / (Maximum / 100) : 0;
        graphics.DrawString(
            $"{percent}%", Font, brush, _regionRect, _textFormat);
    }

    private void DrawProgressFill(Graphics graphics, float roundingValue)
    {
        if (Value == 0) return;

        _drawnValueWidth = Convert.ToInt32(_shapePath.GetBounds().Width / 100 * (Value / (Maximum / 100)));
        _valueRect = new(_regionRect.X, _regionRect.Y, _drawnValueWidth, _regionRect.Height);

        const int offset = 1;
        _valueRect.X -= offset;
        _valueRect.Y -= offset;
        _valueRect.Width += offset * 2;
        _valueRect.Height += offset * 2;
        roundingValue += offset * 2;

        float valueRounding = Math.Min(roundingValue, Math.Min(_valueRect.Width, _valueRect.Height) / 2f);
        if (valueRounding < 0.5f) valueRounding = 0.1f;

        using GraphicsPath valuePath = DrawEngine.CreateRoundedPath(_valueRect, valueRounding);

        if (UseGradientFill)
        {
            using LinearGradientBrush brush = new(_valueRect,
                Color.FromArgb(FillOpacity, GetRgbOrColor(GradientFillColor1)),
                Color.FromArgb(FillOpacity, RGB ? DrawEngine.HsvToRgb(_hue + 20, 1f, 1f) : GradientFillColor2),
                360);
            graphics.FillPath(brush, valuePath);
        }
        else
        {
            using SolidBrush brush = new(Color.FromArgb(FillOpacity, GetRgbOrColor(FillColor)));
            graphics.FillPath(brush, valuePath);
        }
    }

    #endregion
}
