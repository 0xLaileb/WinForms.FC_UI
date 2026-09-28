using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace FC_UI.Controls;

[ToolboxBitmap(typeof(TextBox))]
[Description("Allows user text input with multi-line editing and password character masking.")]
public partial class FTextBox : FControlBase
{
    #region Fields

    private readonly StringFormat _textFormat = new();
    private Font? _cachedFont;
    private int _lastFontHeight = -1;
    private bool _updatingDisplayText;

    public TextBox InnerTextBox { get; } = new();

    #endregion

    #region Properties

    public delegate void TextChangedHandler();

    [Category("FC_UI")]
    [Description("Occurs when the Text property value changes.")]
    public new event TextChangedHandler TextChanged = delegate { };

    [Category("FTextBox")]
    [Description("Control text")]
    [DefaultValue("FTextBox")]
    public string DisplayText
    {
        get => InnerTextBox.Text;
        set
        {
            if (InnerTextBox.Text == value) return;

            _updatingDisplayText = true;
            try
            {
                InnerTextBox.Text = value;
            }
            finally
            {
                _updatingDisplayText = false;
            }

            TextChanged();
        }
    }

    [Category("FTextBox")]
    [DefaultValue(false)]
    [Description("Enable/Disable password display mode")]
    public bool Password
    {
        get;
        set
        {
            field = value;
            UpdateTextBox(true);
        }
    }

    [Category("FTextBox")]
    [DefaultValue('●')]
    [Description("Password masking character")]
    public char PasswordChar
    {
        get;
        set
        {
            field = value;
            UpdateTextBox(true);
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

    [Category("FTextBox")]
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
                    Size = new Size(200, 40);
                    BackColor = Color.Transparent;
                    ForeColor = Color.FromArgb(245, 245, 245);
                    DisplayText = "FTextBox";
                    Rgb = false;
                    Password = false;
                    PasswordChar = '●';
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
                    Password = HelpEngine.RandomBool();
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

    public FTextBox()
    {
        ControlStyle = ControlStyleMode.Default;
        ControlStyle = ControlStyleMode.Custom;

        _textFormat.Alignment = StringAlignment.Center;
        _textFormat.LineAlignment = StringAlignment.Center;

        InnerTextBox.Text = @"Text";
        InnerTextBox.TextChanged += (_, _) =>
        {
            if (!_updatingDisplayText) TextChanged();
        };
        InnerTextBox.GotFocus += (_, _) => Invalidate();
        InnerTextBox.LostFocus += (_, _) => Invalidate();
        UpdateTextBox(false);
        Controls.Add(InnerTextBox);

        UpdateGeometry();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _textFormat.Dispose();
            _cachedFont?.Dispose();
        }
        base.Dispose(disposing);
    }

    public void UpdateTextBox(bool visible)
    {
        InnerTextBox.Visible = visible;
        InnerTextBox.Size = new Size((int)(ControlSize.Width - CornerRadius / 2 - BorderWidth / 2), ControlSize.Height / 2);
        InnerTextBox.Location = new Point(Width / 2 - InnerTextBox.Size.Width / 2, Height / 2 - InnerTextBox.Size.Height / 2);
        if (BackgroundColor.Name != "Transparent") InnerTextBox.BackColor = BackgroundColor;
        InnerTextBox.ForeColor = ForeColor;
        InnerTextBox.BorderStyle = BorderStyle.None;
        if (Height != _lastFontHeight)
        {
            var fontSize = Math.Max(1f, Height / 4f);
            _cachedFont?.Dispose();
            _cachedFont = new Font(Font.Name, fontSize, Font.Style);
            Font = _cachedFont;
            _lastFontHeight = Height;
        }
        InnerTextBox.Font = Font;
        InnerTextBox.TextAlign = HorizontalAlignment.Center;
        InnerTextBox.MaxLength = 10000;
        InnerTextBox.PasswordChar = Password ? PasswordChar : '\0';
    }

    #endregion

    #region Drawing

    protected override void PaintControl(Graphics graphics)
    {
        var roundingValue = PrepareGeometry(Height);

        var focused = EnableFocusEffect && InnerTextBox.Focused;
        DrawBorder(graphics, roundingValue, focused && ShowBorder ? FocusBorderColor : null);

        // The native text field cannot be transparent, so the frame always uses the solid BackgroundColor.
        var state = ClipToContent(graphics, roundingValue, (int)(2 + BorderWidth));
        using SolidBrush brush = new(BackgroundColor);
        graphics.FillPath(brush, ShapePath);
        graphics.Restore(state);

        UpdateTextBox(true);
    }

    #endregion
}
