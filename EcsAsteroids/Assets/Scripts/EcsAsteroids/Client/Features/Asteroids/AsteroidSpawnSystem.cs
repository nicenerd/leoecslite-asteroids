using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsAsteroids.Client
{
    public class AsteroidSpawnSystem : IEcsRunSystem
    {
        readonly EcsCustomInject<GameData> _gameData = default;
        readonly EcsPoolInject<AsteroidComponent> _asteroidPool = default;
        readonly EcsPoolInject<AccelerationComponent> _accComponentPool = default;
        readonly EcsPoolInject<PositionComponent> _posComponentPool = default;
        readonly EcsPoolInject<TransformComponent> _transComponentPool = default;
        readonly EcsPoolInject<AgeComponent> _ageComponentPool = default;
        readonly EcsPoolInject<MaxAgeComponent> _maxAgeComponentPool = default;

        public void Run(IEcsSystems ecsSystems)
        {
            var dTime = Time.deltaTime;

            if (_gameData.Value.spawnAsteroidCooldown > 0)
            {
                _gameData.Value.spawnAsteroidCooldown -= dTime;
                return;
            }
            _gameData.Value.spawnAsteroidCooldown = _gameData.Value.config.asteroidSpawnDelay;

            var ecsWorld = ecsSystems.GetWorld();
            var entity = ecsWorld.NewEntity();
            var asteroidConfig = _gameData.Value.config.asteroidConfig;

            var extents = new Vector2(_gameData.Value.gameFieldBounds.extents.x, _gameData.Value.gameFieldBounds.extents.y) * 1.2f;
            var spawnPos = Random.insideUnitCircle.normalized * extents.magnitude;

            var spawnSpeed = Random.Range(asteroidConfig.initSpeedRange.x, asteroidConfig.initSpeedRange.y);

            var spawnRotAcc = (new Vector2(Random.value, Random.value) - spawnPos).normalized;
            spawnRotAcc *= spawnSpeed;

            var asteroidView = Object.Instantiate(asteroidConfig.asteroidPrefab, spawnPos, Quaternion.identity);
            var asteroidViewCollChecker = asteroidView.GetComponent<CollisionCheckerView>();
            asteroidViewCollChecker.EcsWorld = ecsWorld;
            asteroidViewCollChecker.SelfEntity = ecsWorld.PackEntity(entity);

            _posComponentPool.Value.Add(entity);
            ref var posComponent = ref _posComponentPool.Value.Get(entity);
            posComponent.pos = spawnPos;

            _accComponentPool.Value.Add(entity);
            ref var accComponent = ref _accComponentPool.Value.Get(entity);
            accComponent.value = spawnRotAcc;

            _transComponentPool.Value.Add(entity);
            ref var transComponent = ref _transComponentPool.Value.Get(entity);
            transComponent.trans = asteroidView.transform;

            _asteroidPool.Value.Add(entity);
            ref var asteroidComponent = ref _asteroidPool.Value.Get(entity);
            asteroidComponent.speed = spawnSpeed;
            asteroidComponent.type = AsteroidType.LARGE;

            _ageComponentPool.Value.Add(entity);

            _maxAgeComponentPool.Value.Add(entity);
            ref var maxAgeComponent = ref _maxAgeComponentPool.Value.Get(entity);
            maxAgeComponent.maxAge = asteroidConfig.maxAge;
        }
    }
}