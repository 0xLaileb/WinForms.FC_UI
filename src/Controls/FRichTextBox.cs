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
    private bool _updatingDisplayText;

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
            if (_innerRichTextBox.Text == value) return;

            _updatingDisplayText = true;
            try
            {
                _innerRichTextBox.Text = value;
            }
            finally
            {
                _updatingDisplayText = false;
            }

            TextChanged();
        }
    }

    // --- Effects ---

    [Category("Effects")]
    [Description("Highlight the border while the text field has keyboard focus")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool EnableFocusEffect
    {
        get;
        set { field = value; Invalidate(); }
    }

    [Category("Effects")]
    [Description("Border color used while focused (EnableFocusEffect)")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color FocusBorderColor
    {
        get;
        set { field = value; Invalidate(); }
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
                    EnableFocusEffect = true;
                    FocusBorderColor = Color.FromArgb(140, 230, 250);
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
            Invalidate(true);
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
        _innerRichTextBox.TextChanged += (_, _) =>
        {
            if (!_updatingDisplayText) TextChanged();
        };
        _innerRichTextBox.GotFocus += (_, _) => Invalidate();
        _innerRichTextBox.LostFocus += (_, _) => Invalidate();
        UpdateRichTextBox(false);
        Controls.Add(_innerRichTextBox);

        UpdateGeometry();
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
        _innerRichTextBox.ForeColor = ForeColor;
        _innerRichTextBox.BorderStyle = BorderStyle.None;
        _innerRichTextBox.Font = Font;
        _innerRichTextBox.ScrollBars = RichTextBoxScrollBars.None;
        _innerRichTextBox.MaxLength = 10000;
    }

    #endregion

    #region Drawing

    protected override void PaintControl(Graphics graphics)
    {
        var roundingValue = PrepareGeometry(Height);

        var focused = EnableFocusEffect && _innerRichTextBox.Focused;
        DrawBorder(graphics, roundingValue, focused && ShowBorder ? FocusBorderColor : null);

        // The native text field cannot be transparent, so the frame always uses the solid BackgroundColor.
        var state = ClipToContent(graphics, roundingValue, (int)(2 + BorderWidth));
        using SolidBrush brush = new(BackgroundColor);
        graphics.FillPath(brush, ShapePath);
        graphics.Restore(state);

        UpdateRichTextBox(true);
    }

    #endregion
}
