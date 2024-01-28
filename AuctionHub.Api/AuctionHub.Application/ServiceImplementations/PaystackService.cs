using AuctionHub.Application.DTOs.Paystack;
using AuctionHub.Domain.Entities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace AuctionHub.Application.ServiceImplementations
{
    public class PaystackService : IDisposable
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;

        public PaystackService(IOptions<PaystackSettings> paystackSettings, HttpClient httpClient)
        {
            _apiKey = paystackSettings.Value.ApiKey;
            _httpClient = httpClient;
        }

        public async Task<PaystackTransactionResponse> InitializePaymentAsync(decimal amount, string email)
        {
            var request = new PaystackInitializeRequest
            {
                Amount = amount,
                Email = email,
                Currency = "NGN", 
                Reference = Guid.NewGuid().ToString("N")
            };

            var response = await _httpClient.PostAsJsonAsync("https://api.paystack.co/transaction/initialize", request);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PaystackTransactionResponse>(content);
        }

        public async Task<PaystackTransactionResponse> VerifyPaymentAsync(string reference)
        {
            var response = await _httpClient.GetAsync($"https://api.paystack.co/transaction/verify/{reference}");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PaystackTransactionResponse>(content);
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
