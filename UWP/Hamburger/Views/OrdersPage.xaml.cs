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
using Common.ViewLayer.Pages;
using Hamburger.BL.ViewModels.Orders;

namespace Hamburger.Views
{
    public sealed partial class OrdersPage : SmartPage, IViewModelBoundPage<IOrdersViewModel>
    {
        public OrdersPage()
        {
            this.InitializeComponent();
        }

        public IOrdersViewModel ViewModel { get; set; }
    }
}
