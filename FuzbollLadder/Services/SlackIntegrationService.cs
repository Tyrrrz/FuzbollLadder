﻿using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FuzbollLadder.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Tyrrrz.Extensions;

namespace FuzbollLadder.Services
{
    public class SlackIntegrationService : IIntegrationService, IDisposable
    {
        private readonly IntegrationOptions _options;
        private readonly HttpClient _client;

        public SlackIntegrationService(IOptions<IntegrationOptions> optionsAccessor)
        {
            _options = optionsAccessor.Value;
            _client = new HttpClient();
        }

        public async Task SendNotificationAsync(string text)
        {
            if (_options.WebhookUrl.IsBlank())
                return;
            if (text.IsBlank())
                return;

            // Format
            var data = JsonConvert.SerializeObject(new {text});
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            // Send
            using (var response = await _client.PostAsync(_options.WebhookUrl, content))
            {
                response.EnsureSuccessStatusCode();
            }
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}