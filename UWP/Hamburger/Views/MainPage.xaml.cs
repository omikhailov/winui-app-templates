﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Common.ViewLayer;
using Common.ViewLayer.Pages;
using Hamburger.BL.ViewModels.Main;

namespace Hamburger.Views
{
    public sealed partial class MainPage : SmartPage, IViewModelBoundPage<IMainViewModel>
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        public IMainViewModel ViewModel { get; set; }
    }
}
