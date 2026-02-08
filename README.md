<p align="center">
  <img src="resources/logo.png?raw=true" alt="FC_UI Logo" width="140" />
</p>

<h1 align="center">WinForms.FC_UI</h1>

<p align="center">
  <b>A custom WinForms UI control library with rich styling, RGB effects, gradients, lighting, and rounding.</b>
</p>

<p align="center">
  <a href="https://github.com/0xLaileb/WinForms.FC_UI/releases"><img src="https://img.shields.io/github/v/release/0xLaileb/WinForms.FC_UI?color=%231DC8EE&label=Release&style=flat-square" alt="Release" /></a>
  <a href="https://github.com/0xLaileb/WinForms.FC_UI/releases"><img src="https://img.shields.io/github/downloads/0xLaileb/WinForms.FC_UI/total?color=%231DC8EE&label=Downloads&logo=github&style=flat-square" alt="Downloads" /></a>
  <a href="https://github.com/0xLaileb/WinForms.FC_UI/commits"><img src="https://img.shields.io/github/last-commit/0xLaileb/WinForms.FC_UI?color=%231DC8EE&label=Last%20Commit&style=flat-square" alt="Last Commit" /></a>
  <img src="https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square&logo=dotnet" alt=".NET 10" />
  <img src="https://img.shields.io/badge/License-MIT-green?style=flat-square" alt="License" />
</p>

---

## 📋 Table of Contents

- [📖 About](#-about)
- [✨ Features](#-features)
- [🚀 Getting Started](#-getting-started)
  - [📌 Prerequisites](#-prerequisites)
  - [📦 Installation](#-installation)
- [💡 Usage](#-usage)
- [📚 API Reference](#-api-reference)
- [🧪 Running Tests](#-running-tests)
- [🏗️ Project Structure](#️-project-structure)
- [🔎 Demos](#-demos)
- [🤝 Contributing](#-contributing)
- [🔮 Roadmap](#-roadmap)
- [📄 License](#-license)

---

## 📖 About

**WinForms.FC_UI** is a custom UI control library for Windows Forms applications. It provides a set of fully customizable controls — buttons, checkboxes, radio buttons, switches, progress bars, scroll bars, text boxes, group boxes, and a color picker — all built with GDI+ custom rendering.

Each control supports fine-grained visual customization including background color, border, gradient fills, lighting/shadow effects, corner rounding, click animations, and an animated RGB color-cycling mode.

<p align="center">
  <img src="resources/default_style.gif?raw=true" alt="FC_UI Demo" />
</p>

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
| FCheckBox    | ✅      | ✅       | ✅           | ✅          | ✅              | ❌       | ✅       | ❌     |
| FRadioButton | ✅      | ✅       | ✅           | ✅          | ✅              | ❌       | ✅       | ❌     |
| FSwitchBox   | ❌      | ✅       | ✅           | ✅          | ✅              | ✅       | ✅       | ✅     |
| FProgressBar | ❌      | ✅       | ✅           | ✅          | ✅              | ✅       | ✅       | ✅     |
| FScrollBar   | ❌      | ✅       | ✅           | ✅          | ✅              | ✅       | ✅       | ✅     |
| FRichTextBox | ❌      | ✅       | ✅           | ❌          | ✅              | ✅       | ✅       | ✅     |
| FTextBox     | ❌      | ✅       | ✅           | ❌          | ✅              | ✅       | ✅       | ✅     |
| FGroupBox    | ❌      | ✅       | ✅           | ✅          | ✅              | ✅       | ✅       | ✅     |
| ZColorPicker | ❌      | ❌       | ❌           | ✅          | ❌              | ❌       | ✅       | ❌     |

**Key capabilities:**
- **Fine-grained styling** — background, border, effects, gradient, lighting, rounding, smoothing mode, font, and more
- **RGB mode** — animated HSV color cycling across controls
- **Random style** — randomly generates control appearance parameters
- **Click effects** — circle ripple and white overlay animations (FButton, FCheckBox, FRadioButton)
- **Gradient fills** — linear gradients for background and border
- **Lighting/shadow** — blurred shadow effect around controls
- **Corner rounding** — percentage-based corner radius for any control
- **Global RGB component** — synchronizes RGB animation across all FC_UI controls

---

## 🚀 Getting Started

### 📌 Prerequisites

- **SDK:** [.NET 10 SDK](https://dotnet.microsoft.com/download) or later
- **Language:** C# 14
- **Platform:** Windows (WinForms)

### 📦 Installation

Clone the repository and add a project reference:

```xml
<ProjectReference Include="path\to\src\WinForms.FC_UI.csproj" />
```

---

## 💡 Usage

```csharp
using FC_UI.Controls;

// Create and configure an FButton
var button = new FButton
{
    FButtonStyle = FButton.Style.Default,
    TextButton = "Click me",
    ColorBackground = Color.FromArgb(37, 52, 68),
    ColorBackground_Pen = Color.FromArgb(29, 200, 238),
    Rounding = true,
    RoundingInt = 70,
    Effect_1 = true,
    BackgroundPen = true,
    Background_WidthPen = 4F
};
this.Controls.Add(button);

// Enable RGB mode
button.RGB = true;

// Or use the Random style for a surprise
button.FButtonStyle = FButton.Style.Random;
```

👉 See the full working demo in [`examples/WinForms.FC_UI.Example/`](examples/WinForms.FC_UI.Example/).

---

## 📚 API Reference

### Controls

| Control | Description |
|---|---|
| `FButton` | Customizable button with click effects, gradients, lighting, and RGB mode |
| `FCheckBox` | Checkbox with circle animation effects and gradient support |
| `FRadioButton` | Radio button with customizable checked indicator and effects |
| `FSwitchBox` | Toggle switch with smooth visual feedback |
| `FProgressBar` | Progress indicator with gradient fill and text overlay |
| `FScrollBar` | Horizontal/vertical scrollbar with thumb customization |
| `FRichTextBox` | Rich text editor with styled border and lighting |
| `FTextBox` | Text input with password masking and styled border |
| `FGroupBox` | Container with styled frame and gradient background |
| `ZColorPicker` | HSV color wheel picker with brightness slider |

### Common Properties

| Property | Type | Description |
|---|---|---|
| `Background` | `bool` | Enable/disable background fill |
| `ColorBackground` | `Color` | Background color |
| `Rounding` | `bool` | Enable/disable corner rounding |
| `RoundingInt` | `int` | Rounding percentage (0–100) |
| `RGB` | `bool` | Enable/disable RGB color cycling mode |
| `BackgroundPen` | `bool` | Enable/disable border |
| `Background_WidthPen` | `float` | Border width |
| `ColorBackground_Pen` | `Color` | Border color |
| `Lighting` | `bool` | Enable/disable lighting/shadow effect |
| `ColorLighting` | `Color` | Lighting/shadow color |
| `LinearGradient_Background` | `bool` | Enable/disable background gradient |
| `LinearGradientPen` | `bool` | Enable/disable border gradient |
| `SmoothingMode` | `SmoothingMode` | Graphics smoothing mode |
| `TextRenderingHint` | `TextRenderingHint` | Text rendering quality |

### Components

| Component | Description |
|---|---|
| `FGlobal_RGB` | Enables synchronized global RGB mode for all FC_UI controls |

---

## 🧪 Running Tests

```bash
dotnet test
```

Tests are located in [`tests/WinForms.FC_UI.Tests/`](tests/WinForms.FC_UI.Tests/) and use **xUnit**. They cover engine utilities (HSV-to-RGB conversion, rounded rectangle generation, random helpers), and control property validation (defaults, bounds checking, events).

---

## 🏗️ Project Structure

```
WinForms.FC_UI/
├── 📁 src/
│   ├── 📁 Components/
│   │   └── 📄 FGlobal_RGB.cs             # Global RGB component
│   ├── 📁 Controls/
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
│       ├── 📄 Program.cs
│       └── 📄 WinForms.FC_UI.Example.csproj
├── 📁 resources/                           # Logo, demo GIFs
├── 📄 Directory.Build.props                # Shared build settings
├── 📄 Directory.Packages.props             # Central package management
├── 📄 WinForms.FC_UI.slnx                 # Solution file
├── 📄 LICENSE
└── 📄 README.md
```

---

## 🔎 Demos

### Default Style

![Default Style](resources/default_style.gif?raw=true)

### RGB Mode (Global_RGB component)

![RGB Mode](resources/rgb.gif?raw=true)

### Random Style

![Random Style](resources/random_style.gif?raw=true)

---

## 🤝 Contributing

Contributions are welcome! To get started:

1. 🍴 Fork the repository
2. 🌿 Create a feature branch (`git checkout -b feature/my-feature`)
3. ✏️ Make your changes and add tests
4. ✅ Run `dotnet test` to verify everything passes
5. 📬 Open a Pull Request

---

## 🔮 Roadmap

Things to add or fix in future releases:

1. **FButton** — Add image/icon support so a picture can be placed inside the button.
2. **ZColorPicker** — Finish refactoring to remove the internal `PictureBox` dependency.

---

## 📄 License

This project is licensed under the [MIT License](LICENSE).
