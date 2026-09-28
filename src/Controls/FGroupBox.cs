using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace FC_UI.Controls;

[ToolboxBitmap(typeof(GroupBox))]
[Description("Displays a frame around a group of controls with an optional caption.")]
[Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
public partial class FGroupBox : FControlBase
{
    #region Fields

    private readonly StringFormat _textFormat = new();

    #endregion

    #region Properties

    [Category("FGroupBox")]
    [Description("Caption drawn at the top of the frame (empty = no caption)")]
    [DefaultValue("")]
    public string DisplayText
    {
        get;
        set { field = value ?? string.Empty; Invalidate(); }
    } = string.Empty;

    [Category("FGroupBox")]
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
                    ShowBackground = true;
                    Rgb = false;
                    Rounding = true;
                    CornerRadius = 60;
                    BackgroundColor = Color.FromArgb(37, 52, 68);
                    RgbUpdateInterval = 300;
                    Lighting = false;
                    LightingColor = Color.FromArgb(29, 200, 238);
                    LightingAlpha = 20;
                    LightingWidth = 15;
                    UseGradientBorder = false;
                    GradientBorderColor1 = Color.FromArgb(37, 52, 68);
                    GradientBorderColor2 = Color.FromArgb(41, 63, 86);
                    ShowBorder = true;
                    BorderWidth = 3F;
                    BorderColor = Color.FromArgb(29, 200, 238);
                    UseGradientBackground = false;
                    GradientColor1 = Color.FromArgb(37, 52, 68);
                    GradientColor2 = Color.FromArgb(41, 63, 86);
                    SmoothingMode = SmoothingMode.HighQuality;
                    TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
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
                    UseGradientBorder = HelpEngine.RandomBool();
                    if (UseGradientBorder)
                    {
                        GradientBorderColor1 = HelpEngine.RandomColor();
                        GradientBorderColor2 = HelpEngine.RandomColor();
                    }
                    break;
            }
            Invalidate(true);
        }
    }

    #endregion

    #region Initialization

    public FGroupBox()
    {
        // Like GroupBox: the frame itself is skipped by Tab navigation, its children are not.
        SetStyle(ControlStyles.Selectable, false);
        TabStop = false;

        ControlStyle = ControlStyleMode.Default;
        ControlStyle = ControlStyleMode.Custom;

        _textFormat.Alignment = StringAlignment.Near;
        _textFormat.LineAlignment = StringAlignment.Near;
        _textFormat.Trimming = StringTrimming.EllipsisCharacter;
        _textFormat.FormatFlags = StringFormatFlags.NoWrap;

        UpdateGeometry();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing) _textFormat.Dispose();
        base.Dispose(disposing);
    }

    #endregion

    #region Accessibility

    protected override AccessibleRole DefaultAccessibleRole => AccessibleRole.Grouping;

    protected override string? AccessibleText => string.IsNullOrEmpty(DisplayText) ? null : DisplayText;

    #endregion

    #region Drawing

    protected override void PaintControl(Graphics graphics)
    {
        var roundingValue = PrepareGeometry(Height);

        DrawBorder(graphics, roundingValue);

        var state = ClipToContent(graphics, roundingValue, (int)(2 + BorderWidth));
        FillBackground(graphics);
        graphics.Restore(state);

        if (!string.IsNullOrEmpty(DisplayText)) DrawCaption(graphics, roundingValue);
    }

    private void DrawCaption(Graphics graphics, float roundingValue)
    {
        // Start the caption after the rounded corner so it stays inside the frame.
        var inset = (int)Math.Max(BorderWidth + 6, roundingValue / 3);
        RectangleF captionRect = new(
            RegionRect.X + inset,
            RegionRect.Y + BorderWidth + 4,
            Math.Max(0, RegionRect.Width - inset * 2),
            Font.Height + 2);

        using SolidBrush brush = new(ForeColor);
        graphics.DrawString(DisplayText, Font, brush, captionRect, _textFormat);
    }

    #endregion
}
