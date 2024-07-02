using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Avalonia.Media.Imaging;
using ReactiveUI;

namespace DigitalFrame.ViewModels;
public class MainWindowViewModel : ViewModelBase
{
    private Bitmap? _currentImage;
    /// <summary>
    /// The currently displayed image in the frame.
    /// </summary>
    public Bitmap? CurrentImage
    {
        get => _currentImage;
        set => this.RaiseAndSetIfChanged(ref _currentImage, value);
    }
    public string Greeting => "Digital Frame";

    private int _currentIndex = 0;
    public ReactiveCommand<Unit, Unit> PreviousImage { get; }
    public ReactiveCommand<Unit, Unit> NextImage { get; }

    private IDisposable _timer;
    
    // TODO: This is a temp constant that needs to be pulled from somewhere
    private List<string> _files = [ "c:/!/splash.jpg", @"C:\!\kit.jpg", @"C:\!\panda.jpg", @"C:\!\ulrich.png"];
    public MainWindowViewModel()
    {
        CurrentImage = new Bitmap( _files.First());
        PreviousImage = ReactiveCommand.Create(DoPrevious);
        NextImage = ReactiveCommand.Create(DoNext);
        _timer =
            Observable
                .Interval(TimeSpan.FromSeconds(5.0))
                .Subscribe(x =>
                {
                    DoNext();
                });
        
        // This ends the timer
        //_timer.Dispose(); 
    }

    public void DoPrevious()
    {
        _currentIndex = (_currentIndex == 0) ? _files.Count -1 : 0;
        CurrentImage = new Bitmap(_files[_currentIndex]);    
    }
    public void DoNext()
    {
        _currentIndex = (_currentIndex == _files.Count - 1) ? 0 : _currentIndex + 1;
        CurrentImage = new Bitmap(_files[_currentIndex]);    
    }

}