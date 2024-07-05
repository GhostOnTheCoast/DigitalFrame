using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;

namespace DigitalFrame.Views;

public partial class ImageDisplay : Window
{
    public ImageDisplay()
    {
        //this.WindowState = WindowState.FullScreen;
        int numScreens = this.Screens.ScreenCount;
        var screen = this.Screens.All[numScreens - 1];
        this.Position = new Avalonia.PixelPoint(screen.Bounds.X, screen.Bounds.Y);
        InitializeComponent();

        this.WindowState = WindowState.FullScreen;
    }
}