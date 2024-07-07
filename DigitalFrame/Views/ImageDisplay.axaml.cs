using Avalonia.Controls;
using Avalonia.ReactiveUI;
using DigitalFrame.ViewModels;

namespace DigitalFrame.Views;

public partial class ImageDisplay : ReactiveWindow<ImageDisplayViewModel>
{
    public ImageDisplay(int screenNumber, bool fullScreen)
    {
        this.DataContext = new ImageDisplayViewModel();
        var screen = this.Screens.All[screenNumber];
        this.Position = new Avalonia.PixelPoint(screen.Bounds.X, screen.Bounds.Y);
        if (fullScreen) this.WindowState = WindowState.FullScreen;
        InitializeComponent();
    }
}