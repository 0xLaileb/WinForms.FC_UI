<p align="center">
<img align="center" src="https://github.com/0xLaileb/WinForms.FC_UI/blob/master/GITHUB_RESOURCES/logo.png?raw=true" width="150"/>
</p>

<h1><div align="center">FC_UI</h1>
<p align="center">
<img src="https://img.shields.io/badge/PRICE-free-%231DC8EE"/>
<img src="https://img.shields.io/badge/SUPPORT-yes-%231DC8EE"/>
</p>

<p align="center">
<img src="https://img.shields.io/github/downloads/0xLaileb/WinForms.FC_UI/total?color=%231DC8EE&label=DOWNLOADS&logo=GitHub&logoColor=%231DC8EE&style=flat"/>
<img src="https://img.shields.io/github/last-commit/0xLaileb/WinForms.FC_UI?color=%231DC8EE&label=LAST%20COMMIT&style=flat"/>
<img src="https://img.shields.io/github/release-date/0xLaileb/WinForms.FC_UI?color=%231DC8EE&label=RELEASE%20DATE&style=flat"/>
</p>

[releases]: https://github.com/0xLaileb/WinForms.FC_UI/releases/
[fun_code]: https://github.com/Fun-Coders/

<p align="center">
<b>This library</b> provides the ability to use user controls and fine-tune them in your WinForms applications.<br>
<b>Also</b> it is used in the design OF software from the organization "Fun-Code": https://vk.com/official_funcode <br>
<b>Support</b>: .Net Framework 4.5+ / .Net Core
</p>

## 🚀 How to use

- ### .Net Framework
1. Download the latest **[releases][releases]**.
2. Open your project and go to **Toolbox**.
3. PCM -> **Add tab** (name **FC_UI**).
4. PCM by tab **FC_UI** -> **Choose items** -> **Overview** -> **FC_UI.dll** -> **OK**.

- ### .Net Core
1. Download the latest **[source code][releases]**.
2. Move the **Components**, **Controls**, and **Engines** folders to the source code folder of your project.
3. Open your project and in **Elements panel** you will see these controls.

## ❔ What to add / fix
- FSwitchBox -> fix the display when activated (calculations are incorrect, it is drawn incorrectly at large sizes).
- FButton -> add support for setting images.
- FProgressBar -> fix drawing Value (if Value < 6 (depends on RoundingInt, then a defect appears) [you can still use StartDrawingValue].
- Fix the animation of controls (after a few clicks, the animation becomes faster).
- Add a click effect (hovered - disappeared - appeared back).
- To finish ZColorPicker (remove the use of the picturebox).

## 🔧 Features of this library
- **Fine-tuning** of the control
(background (*on / off, color*), stroke (*on / off, color*), effects (*on / off, color, speed, transparency*), background and stroke gradient (*on / off, colors*), highlighting (*on / off, color, thickness*), rounding (*on / off, value*), control style (*default, custom, RGB, random*), anti-aliasing mode, size, font, etc.).
- **RGB mode** enables many people's favorite color transfusion (HSV).
- **Random Style** randomly sets parameters to the control, which results in a "random style".
- **Effects** are present in some controls (see below).
- **Gradient** is present as a background and outline, which makes it possible to create a "three-dimensional" design.
- **Highlighting** allows you to create a "shadow" or just a highlight.
- **Rounding** allows you to round the edges of the control or the entire control.
- **The Global_RGB** component enables global RGB mode, i.e. all controls will always be on the "same wave".

## ⚡ List of controls and their characteristics
| User Control | Effects | RGBMode | RandomStyle | GradientBackground | GradientPen | Lighting | Rounding | ReSize |
| :----------- | :-----: | :-----: | :---------: | :----------------: | :---------: | :------: | :------: | :----: |
| FButton      | ➕ | ➕ | ➕ | ➕ | ➕ | ➕ | ➕ | ➕ |
| FCheckBox    | ➕ | ➕ | ➕ | ➕ | ➕ | ➖ | ➕ | ➖ |
| FRadioButton | ➕ | ➕ | ➕ | ➕ | ➕ | ➖ | ➕ | ➖ |
| FSwitchBox   | ➖ | ➕ | ➕ | ➕ | ➕ | ➕ | ➕ | ➕ |
| FProgressBar | ➖ | ➕ | ➕ | ➕ | ➕ | ➕ | ➕ | ➕ |
| FScrollBar   | ➖ | ➕ | ➕ | ➕ | ➕ | ➕ | ➕ | ➕ |
| FRichTextBox | ➖ | ➕ | ➕ | ➖ | ➕ | ➕ | ➕ | ➕ |
| FTextBox     | ➖ | ➕ | ➕ | ➖ | ➕ | ➕ | ➕ | ➕ |
| FGroupBox    | ➖ | ➕ | ➕ | ➕ | ➕ | ➕ | ➕ | ➕ |
| ZColorPicker | ➖ | ➖ | ➖ | ➕ | ➖ | ➖ | ➕ | ➖ |

## 🔎 Demonstration (due to processing, the quality is worse)
- ### Standard style
![](https://github.com/0xLaileb/WinForms.FC_UI/blob/master/GITHUB_RESOURCES/default_style.gif?raw=true)

- ### RGB mode, Global_RGB component
![](https://github.com/0xLaileb/WinForms.FC_UI/blob/master/GITHUB_RESOURCES/rgb.gif?raw=true)

- ### Random style
![](https://github.com/0xLaileb/WinForms.FC_UI/blob/master/GITHUB_RESOURCES/random_style.gif?raw=true)
