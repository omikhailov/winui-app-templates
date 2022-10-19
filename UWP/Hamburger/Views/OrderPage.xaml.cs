using System;
using System.ComponentModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.WindowManagement;
using Common.ModelLayer;
using Common.ViewLayer.Pages;
using Hamburger.BL.ViewModels.Orders;

namespace Hamburger.Views
{
    public sealed partial class OrderPage : SmartPage, IViewModelBoundPage<IOrderViewModel>, ISecondaryPage
    {
        public OrderPage()
        {
            this.InitializeComponent();
        }

        public IOrderViewModel ViewModel { get; set; }

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
