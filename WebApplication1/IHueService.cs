using System.Net.Http;
using System.Threading.Tasks;

namespace WebApplication1
{
    public interface IHueService
    {

        Task<HttpResponseMessage> TurnLight(string turnOn);
        void PerformCoolLightShow();
    }
}