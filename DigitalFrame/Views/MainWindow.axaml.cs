using System.Reactive.Disposables;
using Avalonia.ReactiveUI;
using DigitalFrame.ViewModels;
using ReactiveUI; 

namespace DigitalFrame.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        this.WhenActivated(disposableImage =>
        {
            this.BindCommand(ViewModel,
                vm => vm.PreviousImage,
                v => v.PreviousBtn
            ).DisposeWith(disposableImage);
        });
        this.WhenActivated(disposableImage =>
        {
            this.BindCommand(ViewModel,
                vm => vm.NextImage,
                v => v.NextBtn
            ).DisposeWith(disposableImage);
        });
        InitializeComponent();
    }
}