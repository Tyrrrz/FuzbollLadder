using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentScheduler;
using FuzbollLadder.Models;
using Tyrrrz.Extensions;

namespace FuzbollLadder.Services
{
    public class JobService : IJobService
    {
        private readonly IDataService _dataService;
        private readonly IIntegrationService _integrationService;

        private IReadOnlyList<Player> _lastPlayers;

        public JobService(IDataService dataService, IIntegrationService integrationService)
        {
            _dataService = dataService;
            _integrationService = integrationService;
        }

        private async Task SendPlayerDeltasJobAsync()
        {
            // Prepare deltas
            var playerDeltas = new List<PlayerDelta>();

            // Get current ladder
            var players = _dataService.GetAllPlayers().ToArray();

            // Compare with last ladder and add deltas
            foreach (var player in players)
            {
                var lastPlayer = _lastPlayers.FirstOrDefault(p => p.Id == player.Id);
                if (lastPlayer != null && Math.Abs(player.Rating - lastPlayer.Rating) > 10e-5)
                {
                    playerDeltas.Add(new PlayerDelta
                    {
                        Player = player,
                        Delta = player.Rating - lastPlayer.Rating
                    });
                }
            }

            // Store last ladder
            _lastPlayers = players;

            // Compose payload
            var textBuffer = new StringBuilder();
            foreach (var playerDelta in playerDeltas)
                textBuffer.AppendLine($"*{playerDelta.Player.Name}*: {playerDelta.Delta:+0;-#;}");

            // Send notification
            var text = textBuffer.ToString().Trim();
            if (text.IsNotBlank())
                await _integrationService.SendNotificationAsync(text);
        }

        public void Initialize()
        {
            var registry = new Registry();

            // Pre-cache ladder
            _lastPlayers = _dataService.GetAllPlayers().ToArray();

            // Jobs
            registry.Schedule(() => SendPlayerDeltasJobAsync().GetAwaiter().GetResult())
                .NonReentrant()
                .ToRunOnceAt(18, 00)
                .AndEvery(1).Days().At(18, 00);

            JobManager.Initialize(registry);
        }
    }
}