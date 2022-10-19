using System.Threading.Tasks;
using Common.Services.Settings.Parameters;
using Windows.UI.Xaml;

namespace Common.Services.Settings
{
    public interface ISettingsServiceBase
    {
        IParameter<ElementTheme> Theme { get; }
    }
}