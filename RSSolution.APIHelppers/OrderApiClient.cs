using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RSSolution.Utilities.Constants;
using RSSolution.ViewModels.Common;
using RSSolution.ViewModels.Sales;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RSSolution.APIHelpers
{
    public class OrderApiClient : BaseApiClient, IOrderApiClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public OrderApiClient(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<bool> Create(CheckoutRequest request)
        {
            var sessions = _httpContextAccessor
                .HttpContext
                .Session
                .GetString(SystemConstants.AppSettings.Token);
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/orders", httpContent);
            return response.IsSuccessStatusCode;
        }
        public async Task<List<OrderViewModel>> GetAll()
        {
            return await GetListAsync<OrderViewModel>("/api/orders");
        }
        public async Task<List<OrderDetailVm>> GetAllOrderDetails()
        {
            return await GetListAsync<OrderDetailVm>("/api/orders/orderDetail");
        }
        public async Task<List<OrderDetailVm>> GetById(int orderId)
        {
            var data = await GetAsync<List<OrderDetailVm>>($"/api/orders/{orderId}");

            return data;
        }
        public async Task<List<GetOrderByDateVM>> GetByDate(TimeRequest time)
        {
            var sessions = _httpContextAccessor
                .HttpContext
                .Session
                .GetString(SystemConstants.AppSettings.Token);
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var json = JsonConvert.SerializeObject(time);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/orders/getbydate", httpContent);
            var body = await response.Content.ReadAsStringAsync();
            var orderdate = JsonConvert.DeserializeObject<List<GetOrderByDateVM>>(body);
            return orderdate;
        }
    }
}
