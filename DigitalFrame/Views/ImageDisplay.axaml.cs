using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;

namespace DigitalFrame.Views;

public partial class ImageDisplay : Window
{
    public ImageDisplay(int screenNumber, bool fullScreen)
    {
        //int numScreens = this.Screens.ScreenCount;
        var screen = this.Screens.All[screenNumber];
        this.Position = new Avalonia.PixelPoint(screen.Bounds.X, screen.Bounds.Y);
        if (fullScreen) this.WindowState = WindowState.FullScreen;
        InitializeComponent();
    }
}