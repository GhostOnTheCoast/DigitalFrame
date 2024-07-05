using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using DigitalFrame.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace DigitalFrame.ViewModels;
public class MainWindowViewModel : ViewModelBase
{

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
    
    /// <summary>
    /// Holder for timer
    /// </summary>
    [Reactive] public IDisposable? FrameTimer { get; set; }
   
    // TODO: This is a temp constant that needs to be pulled from somewhere
    private List<string> _files = [ "c:/!/splash.jpg", @"C:\!\kit.jpg", @"C:\!\panda.jpg", @"C:\!\ulrich.png"];
    
    public MainWindowViewModel()
    {
        Greeting = "Digital Frame";
        CurrentImage = new Bitmap( _files.First());
        PreviousImage = ReactiveCommand.Create(DoPrevious);
        NextImage = ReactiveCommand.Create(DoNext);
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

    public void DoPrevious()
    {
        CurrentIndex = (CurrentIndex == 0) ? _files.Count -1 : CurrentIndex - 1;
        CurrentImage = new Bitmap(_files[CurrentIndex]);    
    }
    public void DoNext()
    {
        CurrentIndex = (CurrentIndex == _files.Count - 1) ? 0 : CurrentIndex + 1;
        CurrentImage = new Bitmap(_files[CurrentIndex]);    
    }

}