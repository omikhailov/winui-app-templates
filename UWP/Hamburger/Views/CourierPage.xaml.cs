using System;
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
using Windows.UI.WindowManagement;
using Common.ViewLayer.Pages;
using Common.ModelLayer;
using Hamburger.BL.ViewModels.Couriers;

namespace Hamburger.Views
{
    public sealed partial class CourierPage : SmartPage, IViewModelBoundPage<ICourierViewModel>, ISecondaryPage
    {
        public CourierPage()
        {
            this.InitializeComponent();
        }

        public ICourierViewModel ViewModel { get; set; }

        private AppWindow _window;

        public AppWindow Window
        {
            get
            {
                return _window;
            }
            set
            {
                this.Set(ref _window, value, OnPropertyChanged);
            }
        }

    }
}
