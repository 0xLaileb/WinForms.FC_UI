# 👀 WinForms.FC_UI

**A custom WinForms UI control library with rich styling, RGB effects, gradients, lighting, and rounding.**

[![Release](https://img.shields.io/github/v/release/0xLaileb/WinForms.FC_UI?color=%231DC8EE&label=Release&style=flat-square)](https://github.com/0xLaileb/WinForms.FC_UI/releases)
[![NuGet](https://img.shields.io/nuget/v/WinForms.FC_UI?color=%231DC8EE&label=NuGet&style=flat-square&logo=nuget)](https://www.nuget.org/packages/WinForms.FC_UI)
[![NuGet Downloads](https://img.shields.io/nuget/dt/WinForms.FC_UI?color=%231DC8EE&label=Downloads&style=flat-square&logo=nuget)](https://www.nuget.org/packages/WinForms.FC_UI)
[![Last Commit](https://img.shields.io/github/last-commit/0xLaileb/WinForms.FC_UI?color=%231DC8EE&label=Last%20Commit&style=flat-square)](https://github.com/0xLaileb/WinForms.FC_UI/commits)
![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square&logo=dotnet)
![Windows](https://img.shields.io/badge/Platform-Windows-0078D4?style=flat-square&logo=windows)
![License](https://img.shields.io/badge/License-MIT-green?style=flat-square)

---

## 📋 Table of Contents

- [📖 About](#-about)
- [✨ Features](#-features)
- [🚀 Getting Started](#-getting-started)
  - [📌 Prerequisites](#-prerequisites)
  - [📦 Installation](#-installation)
- [💡 Usage](#-usage)
- [⌨️ Keyboard and Accessibility](#️-keyboard-and-accessibility)
- [🔄 Upgrading from 3.1.x to 4.0.0](#-upgrading-from-31x-to-400)
- [📚 API Reference](#-api-reference)
- [🧪 Running Tests](#-running-tests)
- [🏗️ Project Structure](#️-project-structure)
- [🔎 Demos](#-demos)
- [🤝 Contributing](#-contributing)
- [📄 License](#-license)

---

## 📖 About

**WinForms.FC_UI** is a custom UI control library for Windows Forms applications. It provides a set of fully customizable controls — buttons, checkboxes, radio buttons, switches, progress bars, scroll bars, text boxes, group boxes, and a color picker — all built with GDI+ custom rendering.

Each control supports fine-grained visual customization including background color, border, gradient fills, lighting/shadow effects, corner rounding, hover and click animations, and an animated RGB color-cycling mode. Controls also work from the keyboard, expose their role and state to screen readers, and render a grayscale look when disabled.

![FC_UI Demo](https://raw.githubusercontent.com/0xLaileb/WinForms.FC_UI/v4.0.0/resources/default_style.gif)

---

## ✨ Features

| Characteristic | Value |
|---|---|
| Created | 2020 |
| Framework | .NET 10 (Windows Forms) |
| Language | C# |
| Controls | 10 custom controls + 1 component |

| Control | Effects | RGB Mode | Random Style | Gradient BG | Gradient Border | Lighting | Rounding | Resize |
| :----------- | :-----: | :------: | :----------: | :---------: | :-------------: | :------: | :------: | :----: |
| FButton      | ✅      | ✅       | ✅           | ✅          | ✅              | ✅       | ✅       | ✅     |
| FCheckBox    | ✅      | ✅       | ✅           | ✅          | ✅              | ✅       | ✅       | ✅     |
| FRadioButton | ✅      | ✅       | ✅           | ✅          | ✅              | ✅       | ✅       | ✅     |
| FSwitchBox   | ✅      | ✅       | ✅           | ✅          | ✅              | ✅       | ✅       | ✅     |
| FProgressBar | ✅      | ✅       | ✅           | ✅          | ✅              | ✅       | ✅       | ✅     |
| FScrollBar   | ✅      | ✅       | ✅           | ✅          | ✅              | ✅       | ✅       | ✅     |
| FRichTextBox | ✅      | ✅       | ✅           | ❌¹         | ✅              | ✅       | ✅       | ✅     |
| FTextBox     | ✅      | ✅       | ✅           | ❌¹         | ✅              | ✅       | ✅       | ✅     |
| FGroupBox    | N/A     | ✅       | ✅           | ✅          | ✅              | ✅       | ✅       | ✅     |
| ZColorPicker | N/A     | ✅²      | ✅           | ✅          | ✅              | ✅       | ✅       | ✅     |

Effects per control: click ripple and hover overlay (FButton, FCheckBox, FRadioButton), hover overlay and sliding knob (FSwitchBox), smooth value animation (FProgressBar, opt-in via `EnableValueAnimation`), thumb hover highlight (FScrollBar), focus highlight of the border (FTextBox, FRichTextBox). N/A means not applicable.

¹ The text is edited by a native `TextBox`/`RichTextBox`, which cannot draw a transparent or gradient background, so these controls always use the solid `BackgroundColor`.
² RGB mode animates the border (`ShowBorder` is off in the default picker style).

**Key capabilities:**
- **Fine-grained styling**: background, border, effects, gradient, lighting, rounding, smoothing mode, font, and more
- **RGB mode**: animated HSV color cycling across controls
- **Random style**: randomly generates control appearance parameters
- **Click and hover effects**: circle ripple, overlays, sliding switch knob, animated progress
- **Gradient fills**: linear gradients for background and border
- **Lighting/shadow**: blurred shadow effect around controls
- **Corner rounding**: percentage-based corner radius for any control
- **Global RGB component**: synchronizes RGB animation across all FC_UI controls
- **Standard behavior**: keyboard operation, focus cues, screen reader roles, disabled look, `AcceptButton`/`DialogResult` support in `FButton`, grouped `FRadioButton` selection

---

## 🚀 Getting Started

### 📌 Prerequisites

- **SDK:** [.NET 10 SDK](https://dotnet.microsoft.com/download) 10.0.300 or later
- **Language:** C# 14
- **Platform:** Windows (WinForms)

The repository includes `global.json` (SDK 10.0.300 or a later feature band) and `packages.lock.json` files, so `dotnet restore --locked-mode` resolves the same dependency graph locally and in CI. After changing a package version in `Directory.Packages.props`, run a regular `dotnet restore` once and commit the updated lock files.

### 📦 Installation

#### NuGet Package Manager

```
dotnet add package WinForms.FC_UI
```

Or via the Package Manager Console in Visual Studio:

```
Install-Package WinForms.FC_UI
```

Or add directly to your `.csproj`:

```xml
<PackageReference Include="WinForms.FC_UI" Version="4.0.0" />
```

---

## 💡 Usage

```csharp
using FC_UI.Controls;

// Create and configure an FButton
var button = new FButton
{
    ControlStyle = FControlBase.ControlStyleMode.Default,
    DisplayText = "Click me",
    BackgroundColor = Color.FromArgb(37, 52, 68),
    BorderColor = Color.FromArgb(29, 200, 238),
    Rounding = true,
    CornerRadius = 70,
    EnableClickEffect = true,
    ShowBorder = true,
    BorderWidth = 4F
};
this.Controls.Add(button);

// Enable RGB mode
button.Rgb = true;

// Or use the Random style for a surprise
button.ControlStyle = FControlBase.ControlStyleMode.Random;

// Image next to the text; the button works as the form's Enter button
button.Image = Image.FromFile("save.png"); // any Image; the button does not dispose it
button.TextImageRelation = TextImageRelation.ImageBeforeText;
button.DialogResult = DialogResult.OK;
this.AcceptButton = button;
```

`FRadioButton` behaves like the standard `RadioButton`: checking one button unchecks the other `AutoCheck` buttons in the same container, so put each group in its own `FGroupBox` or `Panel`.

To animate all RGB-enabled controls in sync, add an `FGlobalRgb` component (from the Toolbox or in code):

```csharp
using FC_UI.Components;

components ??= new System.ComponentModel.Container(); // the form's designer container
var globalRgb = new FGlobalRgb(components) { TimerInterval = 50 };
globalRgb.Status = true;  // controls with Rgb = true now share one hue
globalRgb.Status = false; // they fall back to their own RgbUpdateInterval timers
```

👉 See the full working demo in [`examples/WinForms.FC_UI.Example/`](https://github.com/0xLaileb/WinForms.FC_UI/tree/v4.0.0/examples/WinForms.FC_UI.Example).

---

## 📚 API Reference

### Controls

| Control | Description | Notable members |
|---|---|---|
| `FButton` | Button with click/hover effects, image, `IButtonControl` support | `DisplayText`, `Image`, `ImageSize`, `TextImageRelation`, `DialogResult`, `PerformClick()` |
| `FCheckBox` | Checkbox with animated check effect; scales with `Height` | `Checked`, `CheckedChanged`, `DisplayText`, `ColorChecked` |
| `FRadioButton` | Radio button with group behavior; scales with `Height` | `Checked`, `AutoCheck`, `CheckedChanged`, `SizeChecked`, `UseGradientFill` |
| `FSwitchBox` | Toggle switch with sliding knob | `Checked`, `CheckedChanged`, `ColorValue`, `EnableToggleAnimation` |
| `FProgressBar` | Progress indicator with gradient fill and percent text | `Value`, `Minimum`, `Maximum`, `ProgressText`, `EnableValueAnimation` |
| `FScrollBar` | Horizontal/vertical scroll bar with draggable thumb | `Value`, `ValueChanged`, `Orientation`, `SmallStep`, `LargeStep`, `ThumbSize` |
| `FRichTextBox` | Rich text editor with styled frame | `DisplayText`, `TextChanged`, `EnableFocusEffect`, `FocusBorderColor` |
| `FTextBox` | Text input with password masking and styled frame | `DisplayText`, `Password`, `PasswordChar`, `InnerTextBox`, `EnableFocusEffect` |
| `FGroupBox` | Container with styled frame and optional caption | `DisplayText` |
| `ZColorPicker` | HSV color wheel with brightness slider, preview and RGB/HEX text | `SelectedColor`, `ColorChanged`, `ShowColorInfo`, `InfoTextColor`, `MarkerColor` |

Every control also exposes `ControlStyle` (`Default`, `Custom`, `Random`).

### Common Properties

Shared by all controls (they derive from `FControlBase`):

| Property | Type | Description |
|---|---|---|
| `ShowBackground` | `bool` | Enable/disable background fill |
| `BackgroundColor` | `Color` | Background color |
| `Rounding` | `bool` | Enable/disable corner rounding |
| `CornerRadius` | `int` | Rounding percentage (0–100) |
| `Rgb` | `bool` | Enable/disable RGB color cycling mode |
| `RgbUpdateInterval` | `int` | RGB animation timer interval in milliseconds (used while `FGlobalRgb` is off) |
| `ShowBorder` | `bool` | Enable/disable border |
| `BorderWidth` | `float` | Border width |
| `BorderColor` | `Color` | Border color |
| `Lighting` | `bool` | Enable/disable lighting/shadow effect |
| `LightingColor` | `Color` | Lighting/shadow color |
| `LightingAlpha` | `int` | Maximum lighting alpha (0–255) |
| `LightingWidth` | `int` | Lighting/shadow width |
| `UseGradientBackground` | `bool` | Enable/disable background gradient (`GradientColor1`, `GradientColor2`) |
| `UseGradientBorder` | `bool` | Enable/disable border gradient (`GradientBorderColor1`, `GradientBorderColor2`) |
| `SmoothingMode` | `SmoothingMode` | Graphics smoothing mode |
| `TextRenderingHint` | `TextRenderingHint` | Text rendering quality |

### Components

| Component | Description |
|---|---|
| `FGlobalRgb` | Enables synchronized global RGB mode for all FC_UI controls (`Status`, `TimerInterval`) |

### Custom controls

`FControlBase` can be subclassed: override `PaintControl(Graphics)` to draw (disabled controls are rendered through a grayscale filter automatically), `UpdateGeometry()` to recalculate layout after size, border, or lighting changes, and `DefaultAccessibleRole`/`AccessibleText`/`AccessibleStateFlags` to describe the control to screen readers. `PrepareGeometry`, `DrawBorder`, `ClipToContent`, `FillBackground`, and `DrawFocusCue` provide the shared rendering steps.

---

## ⌨️ Keyboard and Accessibility

| Control | Keyboard |
|---|---|
| `FButton` | Space or Enter clicks the focused button; Enter elsewhere clicks the form's `AcceptButton` |
| `FCheckBox`, `FSwitchBox` | Space toggles |
| `FRadioButton` | Space selects; arrow keys move the selection within the group |
| `FScrollBar` | Arrow keys (`SmallStep`), Page Up/Down (`LargeStep`), Home/End; mouse wheel |
| `ZColorPicker` | Left/Right change the hue, Up/Down change the brightness |
| `FProgressBar`, `FGroupBox` | Not focusable (skipped by Tab) |

Focused controls draw a dotted focus cue when Windows shows keyboard cues. Each control reports an accessibility role (push button, check button, radio button, progress bar, scroll bar, grouping), its text as the accessible name, and its checked state or value; `AccessibleName` and `AccessibleRole` override the defaults.

---

## 🔄 Upgrading from 3.1.x to 4.0.0

Behavior that changed in 4.0.0 and may affect existing code:

- Standard events are now raised: `SizeChanged`, `Resize`, `Paint`, `MouseEnter`/`MouseLeave`/`MouseDown`/`MouseMove`/`MouseUp`/`MouseClick`. Docked and anchored children of `FGroupBox` follow its size.
- Property setters repaint asynchronously (`Invalidate`), so several changes produce one repaint. `FProgressBar.Value` (unless `EnableValueAnimation` is on) and `FScrollBar.Value` still repaint immediately.
- New effect properties are not in existing designer files, so their `Default` style values apply after upgrading: hover overlay and sliding knob in `FSwitchBox`, thumb hover in `FScrollBar`, focus highlight in `FTextBox`/`FRichTextBox`. Turn them off with `EnableHoverEffect`, `EnableToggleAnimation`, or `EnableFocusEffect`.
- The inner text field of `FTextBox`/`FRichTextBox` uses `ForeColor` instead of a fixed `WhiteSmoke` (the default `ForeColor` is the same color).
- `FRadioButton`: clicking a checked button no longer unchecks it, and checking a button unchecks the other `AutoCheck` buttons in the same container. Set `AutoCheck = false` to manage `Checked` yourself.
- `FCheckBox`/`FRadioButton` no longer force a 45 px height; the box, check mark, and effects scale with `Height`. The 45 px layout is unchanged.
- `ZColorPicker` derives from `FControlBase` and no longer contains `PictureBox`/`Label` children. `SelectedColor` can be set from code (it moves the markers), and `ColorChanged` is raised only when the color actually changes.
- `FScrollBar` implements `ISupportInitialize`; the WinForms designer adds `BeginInit`/`EndInit` the next time the form is saved, which keeps the serialized `Size` and `CornerRadius` of horizontal bars.
- Disabled controls (`Enabled = false`) are drawn in grayscale.
- `FProgressBar` and `FGroupBox` are no longer Tab stops, like the standard `ProgressBar` and `GroupBox`.
- Two quick clicks on `FButton` raise two `Click` events (as with `Button`) instead of `Click` and `DoubleClick`.

---

## 🧪 Running Tests

```bash
dotnet restore WinForms.FC_UI.slnx --locked-mode
dotnet build WinForms.FC_UI.slnx --no-restore --configuration Release
dotnet test WinForms.FC_UI.slnx --no-build --configuration Release --verbosity normal
```

Tests are located in [`tests/WinForms.FC_UI.Tests/`](https://github.com/0xLaileb/WinForms.FC_UI/tree/v4.0.0/tests/WinForms.FC_UI.Tests) and use **xUnit**. They cover engine utilities (HSV/RGB conversion, rounded rectangle generation, random helpers), control property validation (defaults, bounds checking, events), keyboard, mouse, and accessibility behavior, animations, layout, and render checks.

To run the demo application:

```bash
dotnet run --project examples/WinForms.FC_UI.Example
```

---

## 🏗️ Project Structure

```
WinForms.FC_UI/
├── 📁 src/
│   ├── 📁 Components/
│   │   └── 📄 FGlobalRgb.cs              # Global RGB component
│   ├── 📁 Controls/
│   │   ├── 📄 FControlBase.cs            # Shared base class for FC_UI controls
│   │   ├── 📄 FButton.cs                 # Button control
│   │   ├── 📄 FCheckBox.cs               # CheckBox control
│   │   ├── 📄 FRadioButton.cs            # RadioButton control
│   │   ├── 📄 FSwitchBox.cs              # SwitchBox control
│   │   ├── 📄 FProgressBar.cs            # ProgressBar control
│   │   ├── 📄 FScrollBar.cs              # ScrollBar control
│   │   ├── 📄 FRichTextBox.cs            # RichTextBox control
│   │   ├── 📄 FTextBox.cs                # TextBox control
│   │   ├── 📄 FGroupBox.cs               # GroupBox control
│   │   └── 📄 ZColorPicker.cs            # Color picker control
│   ├── 📁 Engines/
│   │   ├── 📄 DrawEngine.cs              # Drawing utilities (rounded rects, HSV, shadow)
│   │   └── 📄 HelpEngine.cs              # Helper utilities (fonts, graphics, random)
│   └── 📄 WinForms.FC_UI.csproj
├── 📁 tests/
│   └── 📁 WinForms.FC_UI.Tests/          # xUnit tests
│       ├── 📄 DrawEngineTests.cs
│       ├── 📄 HelpEngineTests.cs
│       ├── 📁 Controls/                   # Control tests
│       ├── 📁 Components/                 # Component tests
│       └── 📄 WinForms.FC_UI.Tests.csproj
├── 📁 examples/
│   └── 📁 WinForms.FC_UI.Example/        # Demo application
│       ├── 📄 Demo.cs                    # Demo form
│       ├── 📄 Program.cs
│       └── 📄 WinForms.FC_UI.Example.csproj
├── 📁 resources/                           # Logo, demo GIFs
├── 📄 Directory.Build.props                # Shared build settings
├── 📄 Directory.Packages.props             # Central package management
├── 📄 global.json                          # .NET SDK version
├── 📄 WinForms.FC_UI.slnx                 # Solution file
├── 📄 LICENSE
└── 📄 README.md
```

---

## 🔎 Demos

### Default Style

Hover and click effects, checkbox animation, scroll bar driving the progress bar, color picking, and keyboard focus cues.

![Default Style](https://raw.githubusercontent.com/0xLaileb/WinForms.FC_UI/v4.0.0/resources/default_style.gif)

### RGB Mode (FGlobalRgb component)

All controls with `Rgb = true` cycling through one shared hue.

![RGB Mode](https://raw.githubusercontent.com/0xLaileb/WinForms.FC_UI/v4.0.0/resources/rgb.gif)

### Random Style

`ControlStyle = Random` applied to every control several times.

![Random Style](https://raw.githubusercontent.com/0xLaileb/WinForms.FC_UI/v4.0.0/resources/random_style.gif)

---

## 🤝 Contributing

Contributions are welcome! To get started:

1. 🍴 Fork the repository
2. 🌿 Create a feature branch (`git checkout -b feature/my-feature`)
3. ✏️ Make your changes and add tests
4. ✅ Run `dotnet test` to verify everything passes
5. 📬 Open a Pull Request

---

## 📄 License

This project is licensed under the [MIT License](https://github.com/0xLaileb/WinForms.FC_UI/blob/v4.0.0/LICENSE).
