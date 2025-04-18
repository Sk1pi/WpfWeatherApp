# WpfWeatherApp 

## Description
**WpfWeatherApp** is a desktop application for retrieving real-time weather forecasts based on user-defined city or location. The application integrates with **OpenWeatherMap API** and a custom **GeoCoder** to convert city names into geographic coordinates. Built using WPF and the **MVVM** architecture, it ensures a responsive and clean user experience.

## Features
- 🌍 Search for any location using a custom **GeoCoder API**
- ☁️ Get current and multi-day weather forecasts via **OpenWeatherMap API**
- 💾 Save or cache data using **MongoDB** integration
- ⚡ Asynchronous processing with `async/await` to avoid UI blocking
- 🧱 Well-structured MVVM architecture with clear separation of concerns
- 🔌 Flexible and scalable: uses interfaces like `IWeatherProvider` and `IDataRepository`
- 🧰 Clean design and responsive UI built with WPF and XAML

- **WPF**
- **MVVM**
- **MongoDB**
- **OpenWeatherMap API**
- **GeoCoding API**
- **Async/Await**
- **LINQ**
- **RelayCommand** for View-ViewModel communication

### Prerequisites
- [.NET SDK 8.0+](https://dotnet.microsoft.com/en-us/download)
- [MongoDB installed locally or in the cloud](https://www.mongodb.com/)
- OpenWeatherMap API key
- Optional: GeoCoder API key (if required)
