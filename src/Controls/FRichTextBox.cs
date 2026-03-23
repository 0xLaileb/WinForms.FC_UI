using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace FC_UI.Controls;

[ToolboxBitmap(typeof(RichTextBox))]
[Description("Provides additional text input and editing capabilities (e.g., character and paragraph formatting).")]
public partial class FRichTextBox : FControlBase
{
    #region Fields

    private readonly StringFormat _textFormat = new();
    private readonly RichTextBox _innerRichTextBox = new();

    #endregion

    #region Properties

    public delegate void TextChangedHandler();

    [Category("FC_UI")]
    [Description("Occurs when the Text property value changes.")]
    public new event TextChangedHandler TextChanged = delegate { };

    [Category("FRichTextBox")]
    [Description("Control text")]
    [DefaultValue("Text")]
    public string DisplayText
    {
        get => _innerRichTextBox.Text;
        set
        {
            _innerRichTextBox.Text = value;
            TextChanged();
        }
    }

    // --- Style ---

    [Category("FRichTextBox")]
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
                    Size = new Size(150, 130);
                    BackColor = Color.Transparent;
                    ForeColor = Color.FromArgb(245, 245, 245);
                    DisplayText = "FRichTextBox";
                    Rgb = false;
                    Rounding = true;
                    CornerRadius = 60;
                    ShowBorder = true;
                    BorderWidth = 3F;
                    BorderColor = Color.FromArgb(29, 200, 238);
                    BackgroundColor = Color.FromArgb(37, 52, 68);
                    RgbUpdateInterval = 300;
                    Lighting = false;
                    LightingColor = Color.FromArgb(29, 200, 238);
                    LightingAlpha = 20;
                    LightingWidth = 15;
                    UseGradientBorder = false;
                    GradientBorderColor1 = Color.FromArgb(29, 200, 238);
                    GradientBorderColor2 = Color.FromArgb(37, 52, 68);
                    SmoothingMode = SmoothingMode.HighQuality;
                    TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                    Font = HelpEngine.GetDefaultFont();
                    break;
                case ControlStyleMode.Custom:
                    break;
                case ControlStyleMode.Random:
                    BackgroundColor = HelpEngine.RandomColor();
                    Rounding = HelpEngine.RandomBool();
                    if (Rounding) CornerRadius = HelpEngine.RandomInt(5, 90);
                    ShowBorder = HelpEngine.RandomBool();
                    if (ShowBorder)
                    {
                        BorderWidth = HelpEngine.RandomFloat(1, 3);
                        BorderColor = HelpEngine.RandomColor(HelpEngine.RandomInt(0, 255));
                    }
                    Lighting = HelpEngine.RandomBool();
                    if (Lighting) LightingColor = HelpEngine.RandomColor();
                    UseGradientBorder = HelpEngine.RandomBool();
                    if (UseGradientBorder)
                    {
                        GradientBorderColor1 = HelpEngine.RandomColor();
                        GradientBorderColor2 = HelpEngine.RandomColor();
                    }
                    break;
            }
            Refresh();
        }
    }

    #endregion

    #region Initialization

    public FRichTextBox()
    {
        ControlStyle = ControlStyleMode.Default;
        ControlStyle = ControlStyleMode.Custom;

        _textFormat.Alignment = StringAlignment.Center;
        _textFormat.LineAlignment = StringAlignment.Center;

        _innerRichTextBox.Text = @"Text";
        UpdateRichTextBox(false);
        Controls.Add(_innerRichTextBox);

        OnSizeChanged(EventArgs.Empty);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing) _textFormat.Dispose();
        base.Dispose(disposing);
    }

    public void UpdateRichTextBox(bool visible)
    {
        _innerRichTextBox.Visible = visible;
        _innerRichTextBox.Size = new Size(
            (int)(ControlSize.Width - CornerRadius / 2 - BorderWidth / 2),
            (int)(ControlSize.Height - CornerRadius / 2 - BorderWidth / 2));
        _innerRichTextBox.Location = new Point(Width / 2 - _innerRichTextBox.Size.Width / 2, Height / 2 - _innerRichTextBox.Size.Height / 2);
        if (BackgroundColor.Name != "Transparent")
            _innerRichTextBox.BackColor = BackgroundColor;
        _innerRichTextBox.ForeColor = Color.WhiteSmoke;
        _innerRichTextBox.BorderStyle = BorderStyle.None;
        _innerRichTextBox.Font = Font;
        _innerRichTextBox.ScrollBars = RichTextBoxScrollBars.None;
        _innerRichTextBox.MaxLength = 10000;
    }

    #endregion

    #region Events

    protected override void OnPaint(PaintEventArgs e)
    {
        try
        {
            ApplyGraphicsSettings(e.Graphics);
            DrawBackground(e.Graphics);
            UpdateRichTextBox(true);
        }
        catch (Exception ex) { System.Diagnostics.Debug.WriteLine($"[{Name}] OnPaint error: {ex}"); }
    }

    protected override void OnSizeChanged(EventArgs e)
    {
        RecalculateRegion();
    }

    #endregion

    #region Drawing

    private void DrawBackground(Graphics formGraphics)
    {
        var roundingValue = CalculateRoundingValue(Height);
        using var shapePath = DrawEngine.CreateRoundedPath(RegionRect, roundingValue);
        using var regionPath = DrawEngine.CreateRoundedPath(new Rectangle(0, 0, Width, Height), roundingValue);
        Region?.Dispose();
        Region = new Region(regionPath);

        // Border layer
        Bitmap borderBitmap = new(Width, Height);
        using (var g = HelpEngine.GetGraphics(borderBitmap, SmoothingMode, TextRenderingHint))
        {
            if (Lighting)
            {
                using var shadowPath = DrawEngine.CreateRoundedPath(RegionRect, roundingValue);
                DrawEngine.DrawBlurredShadow(g, LightingColor, shadowPath, LightingAlpha, LightingWidth);
            }

            if (BorderWidth != 0 && ShowBorder)
            {
                if (UseGradientBorder)
                {
                    using var brush = new LinearGradientBrush(RegionRect, GradientBorderColor1, GradientBorderColor2, 360);
                    
                    using var pen = new Pen(brush, BorderWidth);
                    pen.LineJoin = LineJoin.Round;
                    pen.DashCap = DashCap.Round;
                    
                    g.DrawPath(pen, shapePath);
                }
                else
                {
                    using var pen = new Pen(GetRgbOrColor(BorderColor), BorderWidth);
                    pen.LineJoin = LineJoin.Round;
                    pen.DashCap = DashCap.Round;
                    
                    g.DrawPath(pen, shapePath);
                }
            }
        }
        using (borderBitmap) formGraphics.DrawImage(borderBitmap, PointF.Empty);

        // Content layer
        Bitmap contentBitmap = new(Width, Height);
        using (var g = HelpEngine.GetGraphics(contentBitmap, SmoothingMode, TextRenderingHint))
        {
            using var clipPath = DrawEngine.CreateRoundedPath(new Rectangle(
                RegionRect.X - (int)(2 + BorderWidth),
                RegionRect.Y - (int)(2 + BorderWidth),
                RegionRect.Width + (int)(2 + BorderWidth) * 2,
                RegionRect.Height + (int)(2 + BorderWidth) * 2), Rounding ? roundingValue : 0.1F);
            using var clipRegion = new Region(clipPath);
            g.Clip = clipRegion;

            using var brush = new SolidBrush(BackgroundColor);
            g.FillPath(brush, shapePath);
        }
        using (contentBitmap) formGraphics.DrawImage(contentBitmap, PointF.Empty);
    }

    #endregion
}
