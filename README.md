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
  <b>Данная библиотека</b> представляет возможность использовать пользовательские элементы управления (user control) и тонко настраивать их в своих приложениях WinForms.<br>
  <b>Кроме того</b> она используется в дизайне ПО от организации «Fun-Code»: https://vk.com/official_funcode <br>
  <b>Поддержка</b>: .Net Framework 4.5+ / .Net Core
</p>

## 🚀 Как использовать

- ### .Net Framework
1. Скачайте последний **[releases][releases]**.
2. Откройте свой проект и перейдите в **Панель элементов**.
3. ПКМ -> **Добавить вкладку** (имя **FC_UI**).
4. ПКМ по вкладке **FC_UI** -> **Выбрать элементы** -> **Обзор** -> **FC_UI.dll** -> **ОК**.

- ### .Net Core
1. Скачайте последний **[исходный код][releases]**.
2. Перекиньте папки **Components**, **Controls** и **Engines** в папку исходного кода вашего проекта.
3. Откройте свой проект и в **Панель элементов** вы увидите данные контролы.

## ❔ Что нужно добавить / исправить
- FSwitchBox -> исправить отображение при активации (расчеты неверные, при больших размерах неверно рисуется).
- FButton -> добавить поддержку постановки картинки.
- FProgressBar -> исправить рисование Value (если Value < 6 (зависит от RoundingInt, то появляется дефект) [пока можете использовать StartDrawingValue].
- Исправить анимацию контролов (после нескольких кликов анимация становится быстрее).
- Добавить эффект клика (навел - исчезло - появилось обратно).
- Доделать ZColorPicker (убрать использование picturebox).

## 🔧 Особенности данной библиотеки 
- **Тонкая настройка** контрола
(фон (*вкл\выкл, цвет*), обводка (*вкл\выкл, цвет*), эффекты (*вкл\выкл, цвет, скорость, прозрачность*), градиент фона и обводки (*вкл\выкл, цвета*), подсветка (*вкл\выкл, цвет, толщина*), закругление (*вкл\выкл, значение*), стиль контрола (*дефолт, кастом, RGB, случайный*), режим сглаживания, размер, шрифт и т.д).
- **Режим RGB** включает любимое многим переливание цвета (HSV).
- **Стиль Random** случайно задаёт параметры контролу, из-за этого получается «случайный стиль».
- **Эффекты** присутствуют в некоторых контролам (см. ниже).
- **Градиент** присутствует в виде фона и обводки, что даёт возможность создать «объемный» дизайн.
- **Подсветка** даёт возможность создать «тень» или же просто подсветку.
- **Закругление** даёт возможность закруглить края контрола или же его весь. 
- **Компонент Global_RGB** даёт возможность глобального RGB-режима, т.е все контролы будут всегда на "одной волне".

## ⚡ Список контролов и их характеристика
| User Control | Effects | RGBMode | RandomStyle | GradientBackground | GradientPen | Lighting | Rounding | ReSize |
| :----------- | :-----: | :-----: | :---------: | :----------------: | :---------: | :------: | :------: | :----: |
| FButton      | ➕     | ➕      | ➕         | ➕                 | ➕          | ➕      | ➕       | ➕    |
| FCheckBox    | ➕     | ➕      | ➕         | ➕                 | ➕          | ➖      | ➕       | ➖    |
| FRadioButton | ➕     | ➕      | ➕         | ➕                 | ➕          | ➖      | ➕       | ➖    |
| FSwitchBox   | ➖     | ➕      | ➕         | ➕                 | ➕          | ➕      | ➕       | ➕    |
| FProgressBar | ➖     | ➕      | ➕         | ➕                 | ➕          | ➕      | ➕       | ➕    |
| FScrollBar   | ➖     | ➕      | ➕         | ➕                 | ➕          | ➕      | ➕       | ➕    |
| FRichTextBox | ➖     | ➕      | ➕         | ➖                 | ➕          | ➕      | ➕       | ➕    |
| FTextBox     | ➖     | ➕      | ➕         | ➖                 | ➕          | ➕      | ➕       | ➕    |
| FGroupBox    | ➖     | ➕      | ➕         | ➕                 | ➕          | ➕      | ➕       | ➕    |
| ZColorPicker | ➖     | ➖      | ➖         | ➕                 | ➖          | ➖      | ➕       | ➖    |

## 🔎 Демонстрация (из-за обработки качество хуже)
- ### Стандартный стиль
![](https://github.com/0xLaileb/WinForms.FC_UI/blob/master/GITHUB_RESOURCES/default_style.gif?raw=true)

- ### Режим RGB, компонент Global_RGB
![](https://github.com/0xLaileb/WinForms.FC_UI/blob/master/GITHUB_RESOURCES/rgb.gif?raw=true)

- ### Случайный стиль
![](https://github.com/0xLaileb/WinForms.FC_UI/blob/master/GITHUB_RESOURCES/random_style.gif?raw=true)
