using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        private void SendPlayerDeltasJob()
        {
            // Prepare deltas
            var playerDeltas = new List<PlayerDelta>();

            // Get current ladder
            var players = _dataService.GetAllPlayers().ToArray();

            // Compare with last ladder
            foreach (var lastPlayer in _lastPlayers.EmptyIfNull())
            {
                var player = players.FirstOrDefault(p => p.Id == lastPlayer.Id);

                // New player
                if (player == null)
                {
                    playerDeltas.Add(new PlayerDelta
                    {
                        Player = lastPlayer,
                        Delta = lastPlayer.Rating
                    });
                }
                // Old player with different rating
                else if (Math.Abs(player.Rating - lastPlayer.Rating) > 10e-5)
                {
                    playerDeltas.Add(new PlayerDelta
                    {
                        Player = lastPlayer,
                        Delta = lastPlayer.Rating - player.Rating
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
            _integrationService.SendNotificationAsync(textBuffer.ToString()).GetAwaiter().GetResult();
        }

        public void Initialize()
        {
            var registry = new Registry();
            JobManager.Initialize(registry);

            // Jobs
            registry.Schedule(() => SendPlayerDeltasJob()).NonReentrant().ToRunEvery(1).Days().At(18, 00);
        }
    }
}