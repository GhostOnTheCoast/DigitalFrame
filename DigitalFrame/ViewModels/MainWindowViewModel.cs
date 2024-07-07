using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using DigitalFrame.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace DigitalFrame.ViewModels;
public class MainWindowViewModel : ViewModelBase
{
    /// <summary>
    /// List of ImageDisplays, one for each screen
    /// </summary>
    [Reactive] public List<ImageDisplay> Displays { get; set; }
    
    /// <summary>
    /// Number of attached screens
    /// </summary>
    [Reactive] public int NumberOfScreens { get; set; }

    /// <summary>
    /// The currently displayed image in the frame.
    /// </summary>
    [Reactive] public Bitmap CurrentImage { get; set; }

    /// <summary>
    /// Top bar label
    /// </summary>
    [Reactive] public string Greeting { get; set; }
    
    /// <summary>
    /// The current image index
    /// </summary>
    [Reactive] public int CurrentIndex { get; set; }
    
    /// <summary>
    /// Reactive command to move to previous image.
    /// </summary>
    public ReactiveCommand<Unit, Unit> PreviousImage { get; }
   
    /// <summary>
    /// Reactive Command to move to next image
    /// </summary>
    public ReactiveCommand<Unit, Unit> NextImage { get; }
    
    public ReactiveCommand<Unit, Unit> ShowDisplay { get; }
    
    public ReactiveCommand<Unit, Unit> CloseDisplay { get; }
    
    /// <summary>
    /// Holder for timer
    /// </summary>
    [Reactive] public IDisposable? FrameTimer { get; set; }
   
    // TODO: This is a temp constant that needs to be pulled from somewhere
    private List<string> _files = [ "c:/!/splash.jpg", @"C:\!\kit.jpg", @"C:\!\panda.jpg", @"C:\!\ulrich.png"];
    
    public MainWindowViewModel()
    {
        Displays = new List<ImageDisplay>();
        //Displays.Add(new ImageDisplay(0, false));
        Greeting = "Digital Frame";
        CurrentImage = new Bitmap( _files.First());
        PreviousImage = ReactiveCommand.Create(DoPrevious);
        NextImage = ReactiveCommand.Create(DoNext);
        ShowDisplay = ReactiveCommand.Create(DoShow);
        CloseDisplay = ReactiveCommand.Create(DoClose);
        FrameTimer =
            Observable
                .Interval(TimeSpan.FromSeconds(5.0))
                .Subscribe(x =>
                {
                    DoNext();
                });
        
        // This ends the timer
        //FrameTimer.Dispose(); 
    }

    private void DoPrevious()
    {
        CurrentIndex = (CurrentIndex == 0) ? _files.Count -1 : CurrentIndex - 1;
        CurrentImage = new Bitmap(_files[CurrentIndex]);
        /*foreach (var screen in Displays)
        {
            screen.ViewModel.CurrentImage = CurrentImage;
        }*/
    }
    private void DoNext()
    {
        CurrentIndex = (CurrentIndex == _files.Count - 1) ? 0 : CurrentIndex + 1;
        CurrentImage = new Bitmap(_files[CurrentIndex]);    
        /*foreach (var screen in Displays)
        {
            screen.ViewModel.CurrentImage = CurrentImage;
        }*/
    }

    private void DoShow()
    {
        MakeScreens();
        foreach (var screen in Displays)
        {
            screen.Show();
        }

    }

    private void DoClose()
    {
        foreach (var screen in Displays)
        {
            screen.Close();
        }

    }

    private void MakeScreens()
    {
        //for (int i = 0; i < NumberOfScreens; i++)
        //{
            //int n = i;
            var sImageDisplay = new ImageDisplay(0, false);
            Displays =
            [
                sImageDisplay
            ];
        this.WhenAnyValue(x => x.CurrentIndex)
            .Do(x => Dispatcher.UIThread.Post(
                    () => 
                    ChangeImage(0, x)
                    )
            )
            .Subscribe();
    }

    private void ChangeImage(int display, int fileIndex)
    {
        var v = Displays.ElementAt(display);
        var a = v.DataContext;
        v.ViewModel?.ChangeImage.Execute(_files[fileIndex]);
        
    }
}