# Pixel Art Editor

## Overview
This is a modern pixel art editor built with **C#**, **WPF**, and **SkiaSharp**. It uses a clean MVVM architecture with Dependency Injection.

## Architecture

### Core (`PixelArtEditor.Core`)
-   **Models**:
    -   `PixelCanvas`: Represents the pixel data (backed by `SKBitmap`).
    -   `PixelCanvas`: Supports `SetPixel`, `GetPixel`.
-   **Interfaces**:
    -   `IRenderer`: Abstraction for rendering the canvas.
    -   `ITool`: Interface for tools (Pencil, Eraser, etc.).
    -   `IToolService`: Manages the active tool.
-   **Services**:
    -   `CanvasRenderer`: Renders the `PixelCanvas` to an `SKCanvas` with Zoom/Pan support.
    -   `ToolService`: Holds the list of available tools and the active tool.
-   **Tools**:
    -   `PencilTool`: Draws pixels with the active color.
    -   `EraserTool`: Clears pixels (sets to Transparent).

### UI (`PixelArtEditor.UI`)
-   **MVVM**: Uses `CommunityToolkit.Mvvm`.
-   **ViewModels**:
    -   `MainViewModel`: Root VM, orchestrates other VMs.
    -   `CanvasViewModel`: Handles Canvas interaction (Zoom, Pan, Mouse events) and rendering loop.
    -   `ToolbarViewModel`: Handles tool selection.
    -   `ColorPaletteViewModel`: Handles color selection.
-   **Views**:
    -   `CanvasView`: Hosts the `SKElement` for high-performance rendering.
    -   `ToolbarView`: List of tools.
    -   `ColorPaletteView`: List of colors.
-   **Messaging**:
    -   `ColorChangedMessage`: Broadcasts color selection changes.

## Extensibility

### Adding a New Tool
1.  Create a class implementing `ITool` in `PixelArtEditor.Core.Tools`.
2.  Implement `OnMouseDown`, `OnMouseMove`, `OnMouseUp`.
3.  Add the new tool to `ToolService.AvailableTools` list.

### Adding a New Feature
-   **Layers**: Create a `Layer` model, update `PixelCanvas` to hold a list of layers, update `CanvasRenderer` to iterate and blend layers.
-   **Animation**: Create `Frame` model (list of Layers), update `PixelCanvas` to hold Frames, add Timeline UI.

## Build & Run
1.  Ensure .NET 8 SDK is installed.
2.  Run `dotnet build`.
3.  Run `dotnet run --project PixelArtEditor.UI`.
