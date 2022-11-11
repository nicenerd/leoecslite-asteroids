using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Unity.Ugui;
using Leopotam.EcsLite.ExtendedSystems;

namespace EcsAsteroids.Client
{
    public class PauseSystem : IEcsRunSystem
    {
        [EcsUguiNamed(AppConstants.Ui.PauseMenuPopup)]
        readonly GameObject _pausePopupGO = default;
        [EcsUguiNamed(AppConstants.Ui.LoseMenuPopup)]
        readonly GameObject _losePopupGO = default;

        readonly EcsCustomInject<GameData> _gameData = default;
        readonly EcsFilterInject<Inc<PauseEvent>> _pauseEventFilter = AppConstants.Worlds.Events;

        public void Run(IEcsSystems ecsSystems)
        {
            foreach (var entity in _pauseEventFilter.Value)
            {
                if (_losePopupGO.activeSelf) { return; }

                _gameData.Value.isPaused = !_gameData.Value.isPaused;
                _pausePopupGO.SetActive(_gameData.Value.isPaused);

                // toggle simulation systems group
                var ecsWorld = ecsSystems.GetWorld(AppConstants.Worlds.Events);
                var updateSimGroupEntity = ecsWorld.NewEntity();
                ref var evt = ref ecsWorld.GetPool<EcsGroupSystemState>().Add(updateSimGroupEntity);
                evt.Name = AppConstants.SimulationGroupName;
                evt.State = !_gameData.Value.isPaused;
                break;
            }
        }
    }
}