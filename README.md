# Tiny Bytes Academy
 
A beginner-friendly app that teaches **binary** and **hexadecimal** through short, interactive lessons. Built as my software engineering senior project.
 
> **Version 1.0:** the senior project as submitted. See [Known issues](#known-issues) and [Planned improvements](#planned-improvements) for where it goes next.
 
## Why I built it
 
Binary and hexadecimal are the foundation of embedded systems, and they are often where beginners get stuck. Tiny Bytes Academy introduces these number systems through hands-on practice instead of long explanations, as a first step toward understanding how embedded devices work.
 
## Screenshots
 
| Lesson path | Lesson 1 | Settings |
|---|---|---|
| ![Lesson path](docs/screenshots/lesson-path.png) | ![Lesson 1](docs/screenshots/lesson-1.png) | ![Settings](docs/screenshots/settings.png) |
 
## Lessons
 
| # | Lesson | What you learn and practice |
|---|---|---|
| 1 | Counting in Binary, Part 1 | How binary counting works using only `0` and `1`, and how digits shift when a value exceeds the available range. Practice by stepping through decimal and binary counters, then entering binary equivalents and getting feedback. |
| 2 | The Basic Building Block | The bit as the smallest unit of data (`0` = OFF, `1` = ON). Practice by toggling individual bits like light switches and working with a nibble (4 bits) and a byte (8 bits). |
| 3 | About Hexadecimal | Hexadecimal is a base-16 system (`0-9`, `A-F`) that gives a shorter way to write binary data. Practice by matching binary nibbles such as `0001`, `1100`, and `1111` to hex values, then see how hex codes describe RGB colors. |
| 4 | Binary and Hexadecimal | Converting longer binary strings to hex by splitting them into groups of four bits (for example, `11110000` becomes `F0`). Practice converting groups to hex characters and editing six-digit RGB hex codes to watch the color change. |
 
The app also includes a **Number Converter** page where you choose an input format (decimal, binary, or hexadecimal) and an output format, and convert values between them.
 
## Tech stack
 
- **.NET 9** and **.NET MAUI**
- **C#** and **XAML**
- **MVVM** pattern with a navigation service abstraction and custom value converters
- [CommunityToolkit.Maui](https://github.com/CommunityToolkit/Maui) and [CommunityToolkit.Mvvm](https://github.com/CommunityToolkit/dotnet)
## Getting started
 
The project is configured to build for Android, iOS, Mac Catalyst, and Windows. **It was developed and tested on Windows only.**
 
### Prerequisites
 
- Windows 10 (version 1809 or later) or Windows 11
- [Visual Studio 2022](https://visualstudio.microsoft.com/) with the **.NET Multi-platform App UI development** workload
- .NET 9 SDK
### Run it
 
```
git clone https://github.com/austin-eaquinto/Tiny_Bytes.git
```
 
1. Open `Tiny Bytes Academy.sln` in Visual Studio.
2. Set the debug target to **Windows Machine**.
3. Press **F5** to build and run.
## Project structure
 
```
Tiny Bytes Academy/
├── Converters/   XAML value converters (for example, bool to status icon)
├── Interfaces/   Service contracts, such as navigation
├── Mocks/        Design-time dummy data for the XAML designer
├── Models/       Data models, including lesson content
├── Platforms/    Platform-specific MAUI code
├── Resources/    Fonts, images, styles, and app icon
├── Services/     Service implementations, such as navigation
├── ViewModels/   Menu and per-lesson ViewModels
└── Views/        XAML pages: start, menu, lessons, converter, settings
```
 
## Known issues
 
- **Theme toggle:** The first time the Settings page is opened, the light/dark mode changes immediately instead of waiting for the toggle to be switched. Toggling afterward works normally.
- **Progress and settings are not saved:** Lesson unlocks and the light/dark mode setting reset every time the app is closed. A local JSON data service exists in the code but is not yet connected to this behavior.
- **Platforms:** Only the Windows build has been tested.
- There is no final quiz or assessment at the end of the lesson path yet.
## Planned improvements
 
This project works, but I reviewed the code after graduating and identified areas to clean up:
 
- Connect the existing data service so lesson progress and the theme preference persist between sessions
- Move lesson interaction logic out of code-behind and into ViewModels
- Consolidate duplicated lesson-step and ViewModel code into shared models and a common base
- Extract binary, hexadecimal, and color-conversion logic into services with unit tests
- Replace magic numbers with named identifiers
## License
 
All rights reserved. This repository is public to showcase my work. Please do not reuse the code without permission.
 
