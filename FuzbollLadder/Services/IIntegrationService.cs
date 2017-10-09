using System.Threading.Tasks;

namespace FuzbollLadder.Services
{
    public interface IIntegrationService
    {
        Task SendNotificationAsync(string text);
    }
}