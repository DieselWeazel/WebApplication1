using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication1
{
    interface IOAuth2
    {
        string Token { get; set; }
    }
    public class Oauth2HueBearer : DelegatingHandler, IOAuth2
    {
        public string Token { get; set; }
        
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Add OAuth2 stuff to request
            var token = Token;

            return await base.SendAsync(request, cancellationToken);
        }
    }
}