# Pixel Art Editor

A modern pixel art editor built with C#, WPF, and SkiaSharp. It features a clean MVVM architecture with Dependency Injection, making it easily extensible.

## Features

-   **Tools**: Pencil, Eraser, Eyedropper, Fill, and Rectangle Select tools are implemented.
-   **Layers**: Support for multiple layers with opacity and blend modes.
-   **Symmetry**: Horizontal, vertical, and both-axis symmetry for drawing.
-   **Color Palette**: A rich color palette with a color picker.
-   **Undo/Redo**: Full undo/redo support for all drawing operations.

## Architecture

The application is divided into two main projects:

### `PixelArtEditor.Core`

This project contains the core business logic of the application.

-   **Models**: Contains the data models for the application, such as `PixelCanvas`, `Layer`, and `SymmetrySettings`.
-   **Interfaces**: Defines the interfaces for the services and tools, such as `IRenderer`, `ITool`, and `ILayerService`.
-   **Services**: Contains the implementations of the services, such as `CanvasRenderer`, `ToolService`, and `LayerService`.
-   **Tools**: Contains the implementations of the drawing tools, such as `PencilTool`, `EraserTool`, and `FillTool`.

### `PixelArtEditor.UI`

This project contains the user interface of the application, built with WPF.

-   **MVVM**: Uses the Model-View-ViewModel pattern with the CommunityToolkit.Mvvm library.
-   **ViewModels**: Contains the view models for the application, such as `MainViewModel`, `CanvasViewModel`, and `ToolbarViewModel`.
-   **Views**: Contains the WPF views for the application, such as `MainWindow`, `CanvasView`, and `ToolbarView`.
-   **Converters**: Contains value converters for data binding.
-   **Messages**: Contains messages for communication between view models.

## Getting Started

### Prerequisites

-   .NET 8 SDK

### Building and Running

1.  Clone the repository:
    ```bash
    git clone https://github.com/your-username/pixel-art-editor.git
    ```
2.  Navigate to the project directory:
    ```bash
    cd pixel-art-editor
    ```
3.  Build the solution:
    ```bash
    dotnet build
    ```
4.  Run the application:
    ```bash
    dotnet run --project PixelArtEditor.UI
    ```

## Extensibility

The application is designed to be extensible. For example, to add a new tool:

1.  Create a new class that implements the `ITool` interface in the `PixelArtEditor.Core.Tools` directory.
2.  Implement the `OnMouseDown`, `OnMouseMove`, and `OnMouseUp` methods.
3.  Add the new tool to the `AvailableTools` list in `ToolService.cs`.

## Contributing

Contributions are welcome! Please feel free to open an issue or submit a pull request.
