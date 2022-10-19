using System.ComponentModel;
using Windows.UI.WindowManagement;

namespace Common.ViewLayer.Pages
{
    public interface ISecondaryPage : INotifyPropertyChanged
    {
        AppWindow Window { get; set; }
    }
}
