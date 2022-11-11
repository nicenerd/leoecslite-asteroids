using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsAsteroids.Client
{
    public class AsteroidSplittingSystem : IEcsRunSystem
    {
        readonly EcsCustomInject<GameData> _gameData = default;
        readonly EcsWorldInject _ecsWorld = default;
        readonly EcsFilterInject<Inc<AsteroidSplitComponent, AsteroidComponent,
            TransformComponent, AccelerationComponent, PositionComponent>> _filter = default;
        readonly EcsPoolInject<AsteroidComponent> _asteroidPool = default;
        readonly EcsPoolInject<AccelerationComponent> _accComponentPool = default;
        readonly EcsPoolInject<PositionComponent> _posComponentPool = default;
        readonly EcsPoolInject<TransformComponent> _transComponentPool = default;
        readonly EcsPoolInject<AgeComponent> _ageComponentPool = default;
        readonly EcsPoolInject<MaxAgeComponent> _maxAgeComponentPool = default;

        public void Run(IEcsSystems ecsSystems)
        {
            var asteroidConfig = _gameData.Value.config.asteroidConfig;

            foreach (var entity in _filter.Value)
            {
                ref var accComponent = ref _accComponentPool.Value.Get(entity);
                ref var posComponent = ref _posComponentPool.Value.Get(entity);
                ref var transComponent = ref _transComponentPool.Value.Get(entity);
                var sourcePos = posComponent.pos;
                
                Object.Destroy(transComponent.trans.gameObject);
                _ecsWorld.Value.DelEntity(entity);

                var partCount = Random.Range(asteroidConfig.smallPartsCountRange.x, asteroidConfig.smallPartsCountRange.y);
                for (int i = 0; i < partCount; i++)
                {
                    SpawnAsteroidPart(_ecsWorld.Value, asteroidConfig, sourcePos);
                }
            }
        }

        private void SpawnAsteroidPart(EcsWorld ecsWorld, AsteroidConfigSO asteroidConfig, Vector2 spawnPos)
        {
            var entity = ecsWorld.NewEntity();

            spawnPos += new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));

            var spawnSpeed = Random.Range(asteroidConfig.smallInitSpeedRange.x, asteroidConfig.smallInitSpeedRange.y);

            var spawnRotAcc = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            spawnRotAcc *= spawnSpeed;

            var asteroidView = Object.Instantiate(asteroidConfig.asteroidSmallPrefab, spawnPos, Quaternion.identity);
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
            asteroidComponent.type = AsteroidType.SMALL;

            _ageComponentPool.Value.Add(entity);

            _maxAgeComponentPool.Value.Add(entity);
            ref var maxAgeComponent = ref _maxAgeComponentPool.Value.Get(entity);
            maxAgeComponent.maxAge = asteroidConfig.maxAge;
        }
    }
}