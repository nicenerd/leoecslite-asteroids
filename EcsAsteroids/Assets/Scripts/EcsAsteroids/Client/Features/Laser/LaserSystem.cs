using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsAsteroids.Client
{
    public class LaserSystem : IEcsRunSystem
    {
        readonly EcsCustomInject<GameData> _gameData = default;
        readonly EcsFilterInject<Inc<LaserComponent, AgeComponent, MaxAgeComponent,
            PositionComponent, RotationComponent>> _laserFilter = default;
        readonly EcsPoolInject<LaserComponent> _laserComponentPool = default;
        readonly EcsPoolInject<PositionComponent> _posComponentPool = default;
        readonly EcsPoolInject<RotationComponent> _rotComponentPool = default;
        readonly EcsPoolInject<TransformComponent> _transComponentPool = default;
        readonly EcsPoolInject<AgeComponent> _ageComponentPool = default;
        readonly EcsPoolInject<MaxAgeComponent> _maxAgeComponentPool = default;

        readonly EcsFilterInject<Inc<LaserInitComponent>> _laserInitFilter = default;
        readonly EcsPoolInject<LaserInitComponent> _laserInitComponentPool = default;

        public void Run(IEcsSystems ecsSystems)
        {
            var laserConfig = _gameData.Value.config.spaceshipLaserConfig;
            var dTime = Time.deltaTime;

            #region Create lasers

            foreach (var laserInitEnt in _laserInitFilter.Value)
            {
                ref var laserInitComponent = ref _laserInitComponentPool.Value.Get(laserInitEnt);

                var ecsWorld = ecsSystems.GetWorld();
                var entity = ecsWorld.NewEntity();

                _laserComponentPool.Value.Add(entity);
                _ageComponentPool.Value.Add(entity);

                var laserView = Object.Instantiate(laserConfig.laserPrefab, laserInitComponent.pos,
                    Quaternion.Euler(0, 0, laserInitComponent.rotation));

                _transComponentPool.Value.Add(entity);
                ref var transComponent = ref _transComponentPool.Value.Get(entity);
                transComponent.trans = laserView.transform;

                _posComponentPool.Value.Add(entity);
                ref var posComponent = ref _posComponentPool.Value.Get(entity);
                posComponent.pos = laserInitComponent.pos;

                _rotComponentPool.Value.Add(entity);
                ref var rotComponent = ref _rotComponentPool.Value.Get(entity);
                rotComponent.rotation = laserInitComponent.rotation;

                _maxAgeComponentPool.Value.Add(entity);
                ref var maxAgeComponent = ref _maxAgeComponentPool.Value.Get(entity);
                maxAgeComponent.maxAge = laserConfig.maxAge;
            }
            #endregion
        }
    }
}