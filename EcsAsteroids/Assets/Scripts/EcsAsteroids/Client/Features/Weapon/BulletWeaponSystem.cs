using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsAsteroids.Client
{
    public class BulletWeaponSystem : IEcsRunSystem
    {
        readonly EcsWorldInject _ecsWorld = default;
        readonly EcsCustomInject<GameData> _gameData = default;

        readonly EcsFilterInject<Inc<InputByPlayer, SpaceshipInputComponent, BulletWeaponComponent,
            PositionComponent, RotationComponent>> _spaceshipBulletWpnFilter = default;
        readonly EcsPoolInject<SpaceshipInputComponent> _spaceshipInputComponentPool = default;
        readonly EcsPoolInject<BulletWeaponComponent> _bulletWpnComponentPool = default;
        readonly EcsPoolInject<PositionComponent> _spaceshipPosComponentPool = default;
        readonly EcsPoolInject<RotationComponent> _spaceshipRotComponentPool = default;
        readonly EcsPoolInject<BulletInitComponent> _bulletInitComponentPool = default;

        readonly EcsFilterInject<Inc<BulletWeaponComponent>> _bulletWpnFilter = default;

        public void Run(IEcsSystems ecsSystems)
        {
            var dTime = Time.deltaTime;

            #region bullet wpn cooldown

            foreach (var entity in _bulletWpnFilter.Value)
            {
                ref var bulletWpnComponent = ref _bulletWpnComponentPool.Value.Get(entity);
                bulletWpnComponent.timeSinceLastShot -= dTime;
                bulletWpnComponent.isFireable = bulletWpnComponent.timeSinceLastShot <= 0;
            }
            #endregion


            #region player bullet wpn

            foreach (var entity in _spaceshipBulletWpnFilter.Value)
            {
                ref var spaceshipInputComponent = ref _spaceshipInputComponentPool.Value.Get(entity);

                if (spaceshipInputComponent.primaryWeapon != 0)
                {
                    ref var bulletWpnComponent = ref _bulletWpnComponentPool.Value.Get(entity);
                    if (!bulletWpnComponent.isFireable) { break; }
                    bulletWpnComponent.timeSinceLastShot = bulletWpnComponent.minShotInterval;

                    ref var posComponent = ref _spaceshipPosComponentPool.Value.Get(entity);
                    ref var rotComponent = ref _spaceshipRotComponentPool.Value.Get(entity);

                    var bulletInitEnt = _ecsWorld.Value.NewEntity();
                    _bulletInitComponentPool.Value.Add(bulletInitEnt);
                    ref var bulletInitComponent = ref _bulletInitComponentPool.Value.Get(bulletInitEnt);
                    var spaceshipConfig = _gameData.Value.config.spaceshipConfig;
                    Vector2 fwd = Quaternion.Euler(0, 0, rotComponent.rotation) * Vector3.up;
                    fwd *= spaceshipConfig.spawnBulletsFwdOffset;
                    bulletInitComponent.pos = posComponent.pos + fwd;
                    bulletInitComponent.rotation = rotComponent.rotation;
                    break;
                }
            }
            #endregion
        }
    }
}