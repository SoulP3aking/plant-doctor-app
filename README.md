# Plant Doctor App

This repository contains a minimal WPF application written in C# (.NET 6) that demonstrates a basic "Plant Doctor" prototype. The app allows users to drag and drop up to two images of plants. Each image is classified locally (placeholder implementation) and the result is sent to the OpenAI API to generate care advice. All analyses are stored in a local SQLite database and shown in the history list.

## Building
1. Install **Visual Studio 2022** or later with the **.NET Desktop Development** workload.
2. Clone this repository and open `PlantDoctorApp.sln`.
3. Ensure your system has the **.NET 6 SDK** installed.
4. Set the configuration to `Release` and build the solution. The compiled application will appear in `PlantDoctorApp/bin/Release/net6.0-windows/`.

## Running
Before starting the app you must set the environment variable `OPENAI_API_KEY` with your API key. The app uses this to request care advice from the OpenAI API.

```
set OPENAI_API_KEY=your-key-here
```

After building, run `PlantDoctorApp.exe`. Drag images onto the drop area or use the button to select files. A simple history is shown at the bottom of the window.

## Notes
The plant classification service currently returns placeholder data. To integrate a real model you can extend `PlantClassifierService` using TensorFlow.NET and a trained model.
