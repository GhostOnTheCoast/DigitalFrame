using System.Reactive;
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
    
    // TODO: Find out why I can't trigger this from code
    public ReactiveCommand<string,Unit> ChangeImage { get; }
    public ImageDisplayViewModel()
    {
        ChangeImage = ReactiveCommand.Create<string>(DoChange);
        // TODO: convert this back to the ChangeImage reactive command
        DoChange(@"c:\!\splash.jpg");
    }

    public void DoChange(string path)
    {
        CurrentImage = new Bitmap(path);
    }
    
}