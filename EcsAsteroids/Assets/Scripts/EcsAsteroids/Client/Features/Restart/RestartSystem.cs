using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedSystems;

namespace EcsAsteroids.Client
{
    public class RestartSystem : IEcsRunSystem
    {
        readonly EcsCustomInject<GameData> _gameData = default;
        readonly EcsWorldInject _ecsWorld = default;
        readonly EcsWorldInject _eventsWorld = AppConstants.Worlds.Events;

        readonly EcsFilterInject<Inc<RestartEvent>> _restartFilter = AppConstants.Worlds.Events;
        readonly EcsFilterInject<Inc<SpaceshipComponent, InputByPlayer, BulletWeaponComponent, LaserWeaponComponent,
            AccelerationComponent, PositionComponent, RotationComponent>> _spaceshipFilter = default;
        readonly EcsFilterInject<Inc<AsteroidComponent, TransformComponent>> _asteroidFilter = default;
        readonly EcsFilterInject<Inc<UfoComponent, TransformComponent>> _ufoFilter = default;
        readonly EcsFilterInject<Inc<BulletComponent, TransformComponent>> _bulletFilter = default;
        readonly EcsFilterInject<Inc<LaserComponent, TransformComponent>> _laserFilter = default;

        readonly EcsPoolInject<TransformComponent> _transComponentPool = default;
        readonly EcsPoolInject<AccelerationComponent> _accComponentPool = default;
        readonly EcsPoolInject<PositionComponent> _posComponentPool = default;
        readonly EcsPoolInject<RotationComponent> _rotComponentPool = default;
        readonly EcsPoolInject<BulletWeaponComponent> _bulletWeaponComponentPool = default;
        readonly EcsPoolInject<LaserWeaponComponent> _laserWeaponComponentPool = default;

        public void Run(IEcsSystems ecsSystems)
        {
            foreach (var restartEntity in _restartFilter.Value)
            {
                // reset player spaceship
                foreach (var spaceshipEnt in _spaceshipFilter.Value)
                {
                    ref var accComponent = ref _accComponentPool.Value.Get(spaceshipEnt);
                    ref var posComponent = ref _posComponentPool.Value.Get(spaceshipEnt);
                    ref var rotComponent = ref _rotComponentPool.Value.Get(spaceshipEnt);
                    ref var bulletWpnComponent = ref _bulletWeaponComponentPool.Value.Get(spaceshipEnt);
                    ref var laserWpnComponent = ref _laserWeaponComponentPool.Value.Get(spaceshipEnt);

                    accComponent.value = Vector2.zero;
                    posComponent.pos = Vector2.zero;
                    rotComponent.rotation = 0f;
                    bulletWpnComponent.timeSinceLastShot = 0f;
                    laserWpnComponent.laserCharges = laserWpnComponent.maxLaserChargeCount;
                    laserWpnComponent.timeSinceLastShot = 0f;
                    laserWpnComponent.rechargeCooldown = 0f;

                    break;
                }

                // remove asteroids
                foreach (var asteroidEnt in _asteroidFilter.Value)
                {
                    DeleteTransEntity(asteroidEnt);
                }
                // remove UFOs
                foreach (var ufoEnt in _ufoFilter.Value)
                {
                    DeleteTransEntity(ufoEnt);
                }
                // remove bullets
                foreach (var bulletEnt in _bulletFilter.Value)
                {
                    DeleteTransEntity(bulletEnt);
                }
                // remove lasers
                foreach (var laserEnt in _laserFilter.Value)
                {
                    DeleteTransEntity(laserEnt);
                }

                // reset game state
                _gameData.Value.spawnAsteroidCooldown = 0f;
                _gameData.Value.spawnUfoCooldown = 0f;
                _gameData.Value.isPaused = false;

                // toggle simulation systems group
                var updateSimGroupEntity = _eventsWorld.Value.NewEntity();
                ref var evt = ref _eventsWorld.Value.GetPool<EcsGroupSystemState>().Add(updateSimGroupEntity);
                evt.Name = AppConstants.SimulationGroupName;
                evt.State = true;
                break;
            }
        }

        private void DeleteTransEntity(int entity)
        {
            ref var transComponent = ref _transComponentPool.Value.Get(entity);
            Object.Destroy(transComponent.trans.gameObject);
            _ecsWorld.Value.DelEntity(entity);
        }
    }
}