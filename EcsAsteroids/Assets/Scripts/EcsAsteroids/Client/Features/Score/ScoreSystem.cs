using UnityEngine;
using UnityEngine.UI;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Unity.Ugui;
using Leopotam.EcsLite.ExtendedSystems;

namespace EcsAsteroids.Client
{
    public class ScoreSystem : IEcsRunSystem
    {
        [EcsUguiNamed(AppConstants.Ui.LoseMenuPopup)]
        readonly GameObject _losePopupGO = default;
        [EcsUguiNamed(AppConstants.Ui.ScoreText)]
        readonly Text _scoreText = default;

        readonly EcsCustomInject<GameData> _gameData = default;
        readonly EcsFilterInject<Inc<ScoreEvent>> _scoreEventFilter = AppConstants.Worlds.Events;
        readonly EcsFilterInject<Inc<LoseEvent>> _loseEventFilter = AppConstants.Worlds.Events;
        readonly EcsFilterInject<Inc<RestartEvent>> _restartFilter = AppConstants.Worlds.Events;

        public void Run(IEcsSystems ecsSystems)
        {
            foreach (var entity in _scoreEventFilter.Value)
            {
                ref var scoreEvent = ref _scoreEventFilter.Pools.Inc1.Get(entity);
                _gameData.Value.score += scoreEvent.score;
            }

            foreach (var entity in _loseEventFilter.Value)
            {
                ref var loseEvent = ref _loseEventFilter.Pools.Inc1.Get(entity);

                _scoreText.text = $"Total score: {_gameData.Value.score}";
                _gameData.Value.isPaused = true;
                _losePopupGO.SetActive(true);

                // toggle simulation systems group
                var ecsWorld = ecsSystems.GetWorld(AppConstants.Worlds.Events);
                var updateSimGroupEntity = ecsWorld.NewEntity();
                ref var evt = ref ecsWorld.GetPool<EcsGroupSystemState>().Add(updateSimGroupEntity);
                evt.Name = AppConstants.SimulationGroupName;
                evt.State = !_gameData.Value.isPaused;
                break;
            }

            foreach (var restartEntity in _restartFilter.Value)
            {
                _gameData.Value.score = 0;
                _scoreText.text = "Total score: 0";
                _losePopupGO.SetActive(false);
                break;
            }
        }
    }
}