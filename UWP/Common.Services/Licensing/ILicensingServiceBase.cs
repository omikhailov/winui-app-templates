using System.Threading.Tasks;

namespace Common.Services.Licensing
{
    public interface ILicensingServiceBase
    {
        string AppName { get; }

        string AppVersion { get; }

        Task ReviewApp();
    }
}