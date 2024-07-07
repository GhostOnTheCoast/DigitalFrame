using System;
using System.Reactive.Disposables;
using Avalonia.ReactiveUI;
using DigitalFrame.ViewModels;
using ReactiveUI; 

namespace DigitalFrame.Views;

// TODO: Figure out Reactive.Fody, Reactive.Validation, Pharmacist (?)
public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
        
        // TODO: Is there a more elegant way of getting the screen count?
        DataContextChanged += OnDataContextLoaded;
        
        // TODO: look into the use of the disposable block here
        this.WhenActivated(disposable =>
        {
            this.BindCommand(ViewModel,
                vm => vm.PreviousImage,
                v => v.PreviousBtn
            ).DisposeWith(disposable);
            this.BindCommand(ViewModel,
                vm => vm.NextImage,
                v => v.NextBtn
            ).DisposeWith(disposable);
            
            this.BindCommand(ViewModel,
                vm => vm.ShowDisplay,
                v => v.ShowBtn
            ).DisposeWith(disposable);
            this.BindCommand(ViewModel,
                vm => vm.CloseDisplay,
                v => v.CloseBtn
            ).DisposeWith(disposable);
        });
    }
    private void OnDataContextLoaded(object? o, EventArgs args)
    {
        var vm = (MainWindowViewModel?)DataContext;
        if (vm != null) vm.NumberOfScreens = this.Screens.ScreenCount;
    }
}