using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace WebApplication1
{
    public class HueService : IHueService
    {
        private readonly ILogger<HueService> _logger;
        
        private static HttpClient client;

        // TODO de här borde bli en miljövariabel då eftersom de kan bytas likt de andra.
        private static readonly string USERNAME = "Rf7RBx4nC2TaSu9TKE1xeJnP49JCjflklG0earcn";

        public HueService(ILogger<HueService> logger, IHttpClientFactory clientFactory, Oauth2HueBearer oath2)
        {
            _logger = logger;
            _logger.LogInformation("The Service is now called?");
            client = clientFactory.CreateClient("ServiceHttpClient");
        }
        public async Task<HttpResponseMessage> TurnLight(string turnOn)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            client = new HttpClient(clientHandler);
            
            client.DefaultRequestHeaders.Accept.Clear();
            // client.DefaultRequestHeaders.Accept.Add(
            //     new MediaTypeWithQualityHeaderValue("application/json"));
            
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", "4y3TZNXe3x1xK8NGC5ALpaxmcGIc");
            
            string requestUri = $"https://api.meethue.com/bridge/{USERNAME}/lights/9/state";
            Console.WriteLine("URI:");
            Console.WriteLine(requestUri);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put,
                requestUri);
            
            
            string content = "{\"on\":" + turnOn + "}";
            request.Content = new StringContent(content);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            Console.WriteLine("Content:");
            Console.WriteLine(content);

            var response = await client.SendAsync(request);
            return response;
        }

        public void PerformCoolLightShow()
        {
            List<string> sendMessages = new List<string>()
            {
                "{\"hue\":46920, \"transitiontime\": 0}", "{\"hue\":630, \"transitiontime\": 0}",
                "{\"xy\": [0.2485, 0.0917], \"transitiontime\": 0}", "{\"on\":false, \"transitiontime\": 0}", "{\"on\":true, \"bri\":192, \"transitiontime\": 0, \"xy\": [0.2857, 0.2744]}",
                "{\"xy\": [0.167, 0.04], \"transitiontime\": 0}"
            };

            
            for (int j = 0; j < 5; j++)
            {
                for (int i = 0; i < sendMessages.Count; i++)
                {
                    Console.WriteLine("Sending: " + sendMessages[i]);
                    var lastResponse = SendMessages(sendMessages[i]);
                    Console.WriteLine("now sleeping");
                    Thread.Sleep(300);
                }
            }
        }

        private async Task<HttpResponseMessage> SendMessages(string sendMessage)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            client = new HttpClient(clientHandler);
            
            client.DefaultRequestHeaders.Accept.Clear();
            // client.DefaultRequestHeaders.Accept.Add(
            //     new MediaTypeWithQualityHeaderValue("application/json"));
            
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", "4y3TZNXe3x1xK8NGC5ALpaxmcGIc");
            
            string requestUri = $"https://api.meethue.com/bridge/{USERNAME}/groups/12/action";
            Console.WriteLine("URI:");
            Console.WriteLine(requestUri);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put,
                requestUri);
            
            request.Content = new StringContent(sendMessage);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            Console.WriteLine("Content:");
            Console.WriteLine(sendMessage);

            var response = await client.SendAsync(request);
            return response;
        }

        public void Test()
        {
            // using (var client = new HttpClient())
            // {
            //     client.BaseAddress = new Uri("http://api.vasttrafik.se");
            //     var content = new FormUrlEncodedContent(new[]
            //     {
            //         new KeyValuePair<string, string>("grant_type", "client_credentials"),
            //         new KeyValuePair<string, string>("scope", Injected.Instance.Platform.GetId())
            //     });
            //
            //     client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", System.Convert.ToBase64String(plainTextBytes));
            //
            //     content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            //     var result = await client.PostAsync("token",content);
            //     string resultContent = await result.Content.ReadAsStringAsync();
            //
            // }
        }
    }
}