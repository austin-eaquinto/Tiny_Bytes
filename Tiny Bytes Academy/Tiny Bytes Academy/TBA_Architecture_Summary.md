# Tiny Bytes Academy (TBA) Project Architecture Summary

This document consolidates the core architecture, technology stack, and key components of the Tiny Bytes Academy application to provide context for AI assistance.

## 🎯 1. Project Goal & Status
* **Goal:** To teach beginners the basics of binary and hexadecimal through four dedicated lessons (two for binary, two for hexadecimal). This is a time-bound senior project.
* **Current Status:** Core MVVM structure is defined with specific ViewModels for each lesson. The core lesson flow and content presentation are implemented. The final lesson conclusion/quiz mechanism is **TBD**.

---

## 🛠️ 2. Technology Stack & Design Pattern
* **Primary Framework:** **.NET MAUI**
* **Target SDK:** **net9.0**
* **Language(s):** C#, XAML
* **Design Pattern:** **Model-View-ViewModel (MVVM) with Code-Behind Reliance**
* **Key Architectural Details:** 
    * All ViewModels inherit from BaseViewModel.
    * Dependency Injection is NOT currently used for ViewModels.

---

## 📦 3. Core Models (Data Structures)

* **`UserModel`:**
    Stores personalized user data.
* **`LessonInfo`:**
    The central Model defining the structure and content of a single lesson step or screen.
* **`UserProgress`:**
    (Model structure TBD, but required for tracking completion).

---

## 💻 4. Key ViewModels (Business Logic)
These classes manage the data and logic for the corresponding Views.

* **`BaseViewModel`:**
    Provides the foundational implementation for property change notification.
* **`MenuPageViewModel`:**
   Manages the main menu/home screen and navigation to the four lessons.
* **Specific Lesson ViewModels:**
    * `BinaryLesson01ViewModel`, `BinaryLesson02ViewModel`, `HexLesson01ViewModel`, `HexLesson02ViewModel`. Each holds specific lesson content and manages flow commands (NextCommand, etc.).

---

## ⚙️ 5. Services & Interfaces (Dependencies)
This section confirms the existence of the navigation service.

* **`INavigationService` (from INavigationService.cs)::**
    * **Purpose:** Defines the contract for navigation methods, decoupling ViewModels from the .NET MAUI navigation stack.
* **`IDataService`:** (Interface TBD, but required for user/progress persistence).
* **`IAnimationService`:**(Service TBD, but required to abstract animation logic).

---

## 🎨 6. XAML Components & Value Converters
This section confirms the use of Value Converters to transform data for the UI.

* **Custom Converters Confirmed:** 
    * `InverseBooleanConverter` **(from InverseBooleanConverter.cs)**: Flips a boolean value, typically used for toggling visibility.
    * `BoolToStatusIconConverter` **(from BoolToStatusIconConverter.cs)**: Converts a boolean value into a resource (like an image source) to display status.
* **Flow Quirk (MUST NOTE):** The `NextButton` uses a code-behind event handler which manually executes the ViewModel's command. UI state changes (like the lightbulb toggle) are also often handled in the code-behind.
---

## 📌 7. Key Conversation History Notes
* **Last Topic:** Confirmed architecture uses dedicated INavigationService and two specific Value Converters.
* **Important Constraint:** Final lesson conclusion/assessment format is an open question. Focus should be on timely completion of core functionality.

---