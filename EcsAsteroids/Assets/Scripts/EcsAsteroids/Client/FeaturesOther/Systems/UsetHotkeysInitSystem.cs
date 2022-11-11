using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsAsteroids.Client
{
    public class UsetHotkeysInitSystem : IEcsInitSystem
    {
        readonly EcsCustomInject<GameData> _gameData = default;
        readonly EcsWorldInject _eventsWorld = AppConstants.Worlds.Events;
        readonly EcsPoolInject<PauseEvent> _pauseEventPool = AppConstants.Worlds.Events;

        public void Init(IEcsSystems ecsSystems)
        {
            _gameData.Value.input.Game.Pause.performed += (e) =>
            {
                var pause = _eventsWorld.Value.NewEntity();
                _pauseEventPool.Value.Add(pause);
            };
        }
    }
}