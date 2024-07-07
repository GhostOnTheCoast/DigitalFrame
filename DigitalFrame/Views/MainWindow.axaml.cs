using System;
using System.Reactive.Disposables;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.ReactiveUI;
using DigitalFrame.ViewModels;
using ReactiveUI; 

namespace DigitalFrame.Views;

// TODO: Figure out Reactive.Fody, Reactive.Validation, Pharmacist
public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
        this.DataContextChanged += delegate(object? o, EventArgs args)
        {
            var vm = (DataContext as MainWindowViewModel);
            vm.NumberOfScreens = this.Screens.ScreenCount;
        };
        
        this.WhenActivated(disposableImage =>
        {
            this.BindCommand(ViewModel,
                vm => vm.PreviousImage,
                v => v.PreviousBtn
            ).DisposeWith(disposableImage);
            this.BindCommand(ViewModel,
                vm => vm.NextImage,
                v => v.NextBtn
            ).DisposeWith(disposableImage);
            
            this.BindCommand(ViewModel,
                vm => vm.ShowDisplay,
                v => v.ShowBtn
            ).DisposeWith(disposableImage);
            this.BindCommand(ViewModel,
                vm => vm.CloseDisplay,
                v => v.CloseBtn
            ).DisposeWith(disposableImage);
        });
        
    }


}