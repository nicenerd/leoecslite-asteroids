using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsAsteroids.Client
{
    public class UfoSpawnSystem : IEcsRunSystem
    {
        readonly EcsCustomInject<GameData> _gameData = default;
        readonly EcsPoolInject<UfoComponent> _ufoPool = default;
        readonly EcsPoolInject<AccelerationComponent> _accComponentPool = default;
        readonly EcsPoolInject<PositionComponent> _posComponentPool = default;
        readonly EcsPoolInject<TransformComponent> _transComponentPool = default;

        public void Run(IEcsSystems ecsSystems)
        {
            var dTime = Time.deltaTime;

            if (_gameData.Value.spawnUfoCooldown > 0)
            {
                _gameData.Value.spawnUfoCooldown -= dTime;
                return;
            }
            _gameData.Value.spawnUfoCooldown = _gameData.Value.config.ufoSpawnDelay;

            var ecsWorld = ecsSystems.GetWorld();
            var entity = ecsWorld.NewEntity();

            var ufoConfig = _gameData.Value.config.ufoConfig;

            var extents = new Vector2(_gameData.Value.gameFieldBounds.extents.x, _gameData.Value.gameFieldBounds.extents.y) * 1.2f;
            var spawnPos = Random.insideUnitCircle.normalized * extents.magnitude;

            var ufoView = Object.Instantiate(ufoConfig.ufoPrefab, spawnPos, Quaternion.identity);
            var ufoViewCollChecker = ufoView.GetComponent<CollisionCheckerView>();
            ufoViewCollChecker.EcsWorld = ecsWorld;
            ufoViewCollChecker.SelfEntity = ecsWorld.PackEntity(entity);

            _posComponentPool.Value.Add(entity);
            ref var posComponent = ref _posComponentPool.Value.Get(entity);
            posComponent.pos = spawnPos;

            _accComponentPool.Value.Add(entity);

            _transComponentPool.Value.Add(entity);
            ref var transComponent = ref _transComponentPool.Value.Get(entity);
            transComponent.trans = ufoView.transform;

            _ufoPool.Value.Add(entity);
            ref var ufoComponent = ref _ufoPool.Value.Get(entity);
            ufoComponent.speed = ufoConfig.speed;
        }
    }
}