using System;
using System.Reactive;
using System.Reactive.Linq;
using Avalonia.Media.Imaging;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace DigitalFrame.ViewModels;

public class ImageDisplayViewModel : ViewModelBase
{
    /// <summary>
    /// The currently displayed image in the frame.
    /// </summary>
    [Reactive] public Bitmap CurrentImage { get; set; }
    
    public ReactiveCommand<string,Unit> ChangeImage { get; }
    public ImageDisplayViewModel()
    {
        CurrentImage = new Bitmap("c:/!/splash.jpg");
        ChangeImage = ReactiveCommand.Create<string>(DoChange);
    }

    private void DoChange(string path)
    {
        CurrentImage = new Bitmap(path);
    }
    
}