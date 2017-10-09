using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentScheduler;
using FuzbollLadder.Models;

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

        private void SendPlayerDeltasJob()
        {
            // Prepare deltas
            var playerDeltas = new List<PlayerDelta>();

            // Get current ladder
            var players = _dataService.GetAllPlayers().ToArray();

            // Compare with last ladder and add deltas
            foreach (var player in players)
            {
                var lastPlayer = _lastPlayers?.FirstOrDefault(p => p.Id == player.Id);
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
            {
                textBuffer.AppendLine($"*{playerDelta.Player.Name}*: {playerDelta.Delta:+0;-#;}");
            }

            // Send payload
            if (textBuffer.Length > 0)
            {
                _integrationService.SendNotificationAsync(textBuffer.ToString()).GetAwaiter().GetResult();
            }
        }

        public void Initialize()
        {
            var registry = new Registry();

            // Jobs
            registry.Schedule(() => SendPlayerDeltasJob()).NonReentrant().ToRunEvery(1).Days().At(18, 00);

            JobManager.Initialize(registry);
        }
    }
}