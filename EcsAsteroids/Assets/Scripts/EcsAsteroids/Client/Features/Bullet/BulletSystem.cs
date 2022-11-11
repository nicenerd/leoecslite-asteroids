using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsAsteroids.Client
{
    public class BulletSystem : IEcsRunSystem
    {
        readonly EcsCustomInject<GameData> _gameData = default;
        readonly EcsFilterInject<Inc<BulletComponent, AgeComponent, MaxAgeComponent,
            AccelerationComponent, PositionComponent, RotationComponent>> _bulletFilter = default;
        readonly EcsPoolInject<BulletComponent> _bulletComponentPool = default;
        readonly EcsPoolInject<PositionComponent> _posComponentPool = default;
        readonly EcsPoolInject<RotationComponent> _rotComponentPool = default;
        readonly EcsPoolInject<TransformComponent> _transComponentPool = default;
        readonly EcsPoolInject<AccelerationComponent> _accComponentPool = default;
        readonly EcsPoolInject<AgeComponent> _ageComponentPool = default;
        readonly EcsPoolInject<MaxAgeComponent> _maxAgeComponentPool = default;

        readonly EcsFilterInject<Inc<BulletInitComponent>> _bulletInitFilter = default;
        readonly EcsPoolInject<BulletInitComponent> _bulletInitComponentPool = default;

        public void Run(IEcsSystems ecsSystems)
        {
            var bulletConfig = _gameData.Value.config.spaceshipBulletConfig;
            var dTime = Time.deltaTime;
            
            #region Create bullets

            foreach (var bulletInitEnt in _bulletInitFilter.Value)
            {
                ref var bulletInitComponent = ref _bulletInitComponentPool.Value.Get(bulletInitEnt);

                var ecsWorld = ecsSystems.GetWorld();
                var entity = ecsWorld.NewEntity();

                _bulletComponentPool.Value.Add(entity);
                _ageComponentPool.Value.Add(entity);

                var bulletView = Object.Instantiate(bulletConfig.bulletPrefab, bulletInitComponent.pos,
                    Quaternion.Euler(0, 0, bulletInitComponent.rotation));

                _transComponentPool.Value.Add(entity);
                ref var transComponent = ref _transComponentPool.Value.Get(entity);
                transComponent.trans = bulletView.transform;

                _posComponentPool.Value.Add(entity);
                ref var posComponent = ref _posComponentPool.Value.Get(entity);
                posComponent.pos = bulletInitComponent.pos;

                _rotComponentPool.Value.Add(entity);
                ref var rotComponent = ref _rotComponentPool.Value.Get(entity);
                rotComponent.rotation = bulletInitComponent.rotation;

                _maxAgeComponentPool.Value.Add(entity);
                ref var maxAgeComponent = ref _maxAgeComponentPool.Value.Get(entity);
                maxAgeComponent.maxAge = bulletConfig.maxAge;

                _accComponentPool.Value.Add(entity);
            }
            #endregion

            
            #region Process bullets

            foreach (var bulletEnt in _bulletFilter.Value)
            {
                // move bullets
                ref var accComponent = ref _accComponentPool.Value.Get(bulletEnt);
                ref var posComponent = ref _posComponentPool.Value.Get(bulletEnt);
                ref var rotComponent = ref _rotComponentPool.Value.Get(bulletEnt);

                // move
                Vector2 fwd = Quaternion.Euler(0, 0, rotComponent.rotation) * Vector3.up;
                accComponent.value += fwd * (bulletConfig.speed * dTime);
                accComponent.value = Vector2.ClampMagnitude(accComponent.value, bulletConfig.speed);

                posComponent.pos += accComponent.value;
            }
            #endregion
        }
    }
}