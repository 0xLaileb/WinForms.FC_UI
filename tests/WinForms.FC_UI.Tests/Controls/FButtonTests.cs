using System.Drawing.Drawing2D;
using System.Drawing.Text;
using FC_UI.Controls;

namespace WinForms.FC_UI.Tests.Controls;

public class FButtonTests : IDisposable
{
    private readonly FButton _button;

    public FButtonTests()
    {
        _button = new FButton();
    }

    public void Dispose()
    {
        _button.Dispose();
    }

    #region Default Style Tests

    [Fact]
    public void Constructor_DefaultStyle_SetsExpectedSize()
    {
        Assert.Equal(130, _button.Width);
        Assert.Equal(50, _button.Height);
    }

    [Fact]
    public void Constructor_DefaultStyle_SetsDisplayText()
    {
        Assert.Equal("FButton", _button.DisplayText);
    }

    [Fact]
    public void Constructor_DefaultStyle_ShowBackgroundIsTrue()
    {
        Assert.True(_button.ShowBackground);
    }

    [Fact]
    public void Constructor_DefaultStyle_RoundingIsTrue()
    {
        Assert.True(_button.Rounding);
    }

    [Fact]
    public void Constructor_DefaultStyle_CornerRadiusIs70()
    {
        Assert.Equal(70, _button.CornerRadius);
    }

    [Fact]
    public void Constructor_DefaultStyle_RGBIsFalse()
    {
        Assert.False(_button.Rgb);
    }

    [Fact]
    public void Constructor_DefaultStyle_EnableClickEffectIsTrue()
    {
        Assert.True(_button.EnableClickEffect);
    }

    [Fact]
    public void Constructor_DefaultStyle_EnableHoverEffectIsTrue()
    {
        Assert.True(_button.EnableHoverEffect);
    }

    [Fact]
    public void Constructor_DefaultStyle_ShowBorderIsTrue()
    {
        Assert.True(_button.ShowBorder);
    }

    [Fact]
    public void Constructor_DefaultStyle_SmoothingModeIsHighQuality()
    {
        Assert.Equal(SmoothingMode.HighQuality, _button.SmoothingMode);
    }

    [Fact]
    public void Constructor_DefaultStyle_TextRenderingHintIsClearType()
    {
        Assert.Equal(TextRenderingHint.ClearTypeGridFit, _button.TextRenderingHint);
    }

    #endregion

    #region Property Validation Tests

    [Theory]
    [InlineData(0)]
    [InlineData(50)]
    [InlineData(100)]
    public void CornerRadius_ValidValues_AreAccepted(int value)
    {
        _button.CornerRadius = value;

        Assert.Equal(value, _button.CornerRadius);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(101)]
    [InlineData(200)]
    public void CornerRadius_InvalidValues_AreRejected(int value)
    {
        var original = _button.CornerRadius;

        _button.CornerRadius = value;

        Assert.Equal(original, _button.CornerRadius);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(128)]
    [InlineData(255)]
    public void ClickEffectOpacity_ValidValues_AreAccepted(int value)
    {
        _button.ClickEffectOpacity = value;

        Assert.Equal(value, _button.ClickEffectOpacity);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(256)]
    [InlineData(-1)]
    public void ClickEffectOpacity_InvalidValues_AreRejected(int value)
    {
        _button.ClickEffectOpacity = 100;
        _button.ClickEffectOpacity = value;

        Assert.Equal(100, _button.ClickEffectOpacity);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(128)]
    [InlineData(255)]
    public void HoverEffectOpacity_ValidValues_AreAccepted(int value)
    {
        _button.HoverEffectOpacity = value;

        Assert.Equal(value, _button.HoverEffectOpacity);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(256)]
    public void HoverEffectOpacity_InvalidValues_AreRejected(int value)
    {
        _button.HoverEffectOpacity = 100;
        _button.HoverEffectOpacity = value;

        Assert.Equal(100, _button.HoverEffectOpacity);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void ClickEffectInterval_InvalidValues_AreRejected(int value)
    {
        _button.ClickEffectInterval = 10;
        _button.ClickEffectInterval = value;

        Assert.Equal(10, _button.ClickEffectInterval);
    }

    #endregion

    #region Property Setter Tests

    [Fact]
    public void BackgroundColor_SetValue_ReturnsSetValue()
    {
        var expected = Color.Red;

        _button.BackgroundColor = expected;

        Assert.Equal(expected, _button.BackgroundColor);
    }

    [Fact]
    public void BorderColor_SetValue_ReturnsSetValue()
    {
        var expected = Color.Blue;

        _button.BorderColor = expected;

        Assert.Equal(expected, _button.BorderColor);
    }

    [Fact]
    public void BorderWidth_SetValue_ReturnsSetValue()
    {
        _button.BorderWidth = 5.5F;

        Assert.Equal(5.5F, _button.BorderWidth);
    }

    [Fact]
    public void SmoothingMode_InvalidValue_IsRejected()
    {
        _button.SmoothingMode = SmoothingMode.HighQuality;
        _button.SmoothingMode = SmoothingMode.Invalid;

        Assert.Equal(SmoothingMode.HighQuality, _button.SmoothingMode);
    }

    [Fact]
    public void Tag_IsSetToFC_UI()
    {
        Assert.Equal("FC_UI", _button.Tag);
    }

    #endregion

    #region Click Animation Tests

    [Fact]
    public void StepClickAnimation_RippleCoversControl_StopsTimer()
    {
        using ClickableFButton button = new();
        button.InvokeMouseUp(MouseButtons.Left);
        Assert.True(button.IsClickAnimationRunning);

        for (var i = 0; i < 100 && button.IsClickAnimationRunning; i++) button.StepClickAnimation();

        Assert.False(button.IsClickAnimationRunning);
    }

    [Fact]
    public void OnMouseUp_ClickEffectDisabled_DoesNotStartAnimation()
    {
        using ClickableFButton button = new() { EnableClickEffect = false };

        button.InvokeMouseUp(MouseButtons.Left);

        Assert.False(button.IsClickAnimationRunning);
    }

    private sealed class ClickableFButton : FButton
    {
        public void InvokeMouseUp(MouseButtons button) =>
            OnMouseUp(new MouseEventArgs(button, 1, 10, 10, 0));

        public void InvokeKeyUp(Keys key) => OnKeyUp(new KeyEventArgs(key));

        public void InvokeClick() => OnClick(EventArgs.Empty);

        public void InvokeKeyDown(Keys key) => OnKeyDown(new KeyEventArgs(key));

        public bool IsInput(Keys key) => IsInputKey(key);
    }

    #endregion

    #region Button Behavior Tests

    [Fact]
    public void PerformClick_Enabled_RaisesClick()
    {
        var clicks = 0;
        _button.Click += (_, _) => clicks++;

        _button.PerformClick();

        Assert.Equal(1, clicks);
    }

    [Fact]
    public void PerformClick_Disabled_DoesNotRaiseClick()
    {
        var clicks = 0;
        _button.Click += (_, _) => clicks++;
        _button.Enabled = false;

        _button.PerformClick();

        Assert.Equal(0, clicks);
    }

    [Fact]
    public void SpaceKeyUp_Focusable_RaisesClick()
    {
        using ClickableFButton button = new();
        var clicks = 0;
        button.Click += (_, _) => clicks++;

        button.InvokeKeyUp(Keys.Space);

        Assert.Equal(1, clicks);
    }

    [Fact]
    public void EnterKeyDown_Focusable_RaisesClick()
    {
        using ClickableFButton button = new();
        var clicks = 0;
        button.Click += (_, _) => clicks++;

        button.InvokeKeyDown(Keys.Enter);

        Assert.Equal(1, clicks);
        Assert.True(button.IsInput(Keys.Enter));
    }

    [Fact]
    public void Click_DialogResultSet_AssignsFormDialogResult()
    {
        using Form form = new();
        ClickableFButton button = new() { DialogResult = DialogResult.OK };
        form.Controls.Add(button);

        button.InvokeClick();

        Assert.Equal(DialogResult.OK, form.DialogResult);
    }

    [Fact]
    public void AcceptButton_FButton_IsAccepted()
    {
        using Form form = new();
        FButton button = new();
        form.Controls.Add(button);

        form.AcceptButton = button;

        Assert.Same(button, form.AcceptButton);
    }

    [Fact]
    public void AccessibilityObject_Default_ReportsPushButtonWithText()
    {
        var accessible = _button.AccessibilityObject;

        Assert.Equal(AccessibleRole.PushButton, accessible.Role);
        Assert.Equal("FButton", accessible.Name);
    }

    #endregion

    #region Image Tests

    [Fact]
    public void TextImageRelation_Default_IsImageBeforeText()
    {
        Assert.Equal(TextImageRelation.ImageBeforeText, _button.TextImageRelation);
        Assert.Null(_button.Image);
    }

    [Theory]
    [InlineData(TextImageRelation.ImageBeforeText)]
    [InlineData(TextImageRelation.TextBeforeImage)]
    [InlineData(TextImageRelation.ImageAboveText)]
    [InlineData(TextImageRelation.TextAboveImage)]
    public void LayoutImageAndText_Relation_KeepsImageAndTextApartAndInside(TextImageRelation relation)
    {
        Rectangle bounds = new(0, 0, 200, 100);

        var (image, text) = FButton.LayoutImageAndText(bounds, new Size(20, 20), new SizeF(60, 14), relation, 6);

        Assert.True(bounds.Contains(image));
        switch (relation)
        {
            case TextImageRelation.ImageBeforeText: Assert.True(image.Right <= text.Left); break;
            case TextImageRelation.TextBeforeImage: Assert.True(text.Right <= image.Left); break;
            case TextImageRelation.ImageAboveText: Assert.True(image.Bottom <= text.Top); break;
            case TextImageRelation.TextAboveImage: Assert.True(text.Bottom <= image.Top); break;
        }
    }

    [Fact]
    public void LayoutImageAndText_Overlay_CentersImage()
    {
        var (image, _) = FButton.LayoutImageAndText(
            new Rectangle(0, 0, 200, 100), new Size(20, 20), new SizeF(60, 14), TextImageRelation.Overlay, 6);

        Assert.Equal(new Rectangle(90, 40, 20, 20), image);
    }

    [Fact]
    public void DrawToBitmap_WithImage_DrawsImagePixels()
    {
        using Bitmap icon = new(16, 16);
        using (var g = Graphics.FromImage(icon)) g.Clear(Color.Lime);
        _button.Image = icon;
        _button.DisplayText = string.Empty;

        using Bitmap bitmap = new(_button.Width, _button.Height);
        _button.DrawToBitmap(bitmap, new Rectangle(Point.Empty, _button.Size));

        Assert.Equal(Color.Lime.ToArgb(), bitmap.GetPixel(_button.Width / 2, _button.Height / 2).ToArgb());
    }

    #endregion
}
