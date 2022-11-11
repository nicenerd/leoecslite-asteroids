using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsAsteroids.Client
{
    public class LaserWeaponSystem : IEcsRunSystem
    {
        readonly EcsWorldInject _ecsWorld = default;
        readonly EcsCustomInject<GameData> _gameData = default;

        readonly EcsFilterInject<Inc<InputByPlayer, SpaceshipInputComponent, BulletWeaponComponent,
            PositionComponent, RotationComponent>> _spaceshipLaserWpnFilter = default;
        readonly EcsPoolInject<SpaceshipInputComponent> _spaceshipInputComponentPool = default;
        readonly EcsPoolInject<LaserWeaponComponent> _laserWpnComponentPool = default;
        readonly EcsPoolInject<PositionComponent> _spaceshipPosComponentPool = default;
        readonly EcsPoolInject<RotationComponent> _spaceshipRotComponentPool = default;
        readonly EcsPoolInject<LaserInitComponent> _laserInitComponentPool = default;

        readonly EcsFilterInject<Inc<LaserWeaponComponent>> _laseerWpnFilter = default;

        public void Run(IEcsSystems ecsSystems)
        {
            var dTime = Time.deltaTime;

            #region laser wpn cooldown

            foreach (var entity in _laseerWpnFilter.Value)
            {
                ref var laserWpnComponent = ref _laserWpnComponentPool.Value.Get(entity);
                laserWpnComponent.timeSinceLastShot -= dTime;

                if (laserWpnComponent.rechargeCooldown > 0)
                {
                    laserWpnComponent.rechargeCooldown -= dTime;
                }
                else if (laserWpnComponent.laserCharges < laserWpnComponent.maxLaserChargeCount)
                {
                    laserWpnComponent.laserCharges += 1;
                    laserWpnComponent.rechargeCooldown = laserWpnComponent.minRechargeInterval;
                }

                laserWpnComponent.isFireable = laserWpnComponent.timeSinceLastShot <= 0 && laserWpnComponent.laserCharges > 0;
            }
            #endregion


            #region player laser wpn

            foreach (var entity in _spaceshipLaserWpnFilter.Value)
            {
                ref var spaceshipInputComponent = ref _spaceshipInputComponentPool.Value.Get(entity);

                if (spaceshipInputComponent.secondaryWeapon != 0)
                {
                    ref var laserWpnComponent = ref _laserWpnComponentPool.Value.Get(entity);
                    if (!laserWpnComponent.isFireable) { break; }
                    laserWpnComponent.timeSinceLastShot = laserWpnComponent.minShotInterval;
                    laserWpnComponent.laserCharges -= 1;

                    ref var posComponent = ref _spaceshipPosComponentPool.Value.Get(entity);
                    ref var rotComponent = ref _spaceshipRotComponentPool.Value.Get(entity);

                    var laserInitEnt = _ecsWorld.Value.NewEntity();
                    _laserInitComponentPool.Value.Add(laserInitEnt);
                    ref var laserInitComponent = ref _laserInitComponentPool.Value.Get(laserInitEnt);
                    var spaceshipConfig = _gameData.Value.config.spaceshipConfig;
                    Vector2 fwd = Quaternion.Euler(0, 0, rotComponent.rotation) * Vector3.up;
                    fwd *= spaceshipConfig.spawnBulletsFwdOffset;
                    laserInitComponent.pos = posComponent.pos + fwd;
                    laserInitComponent.rotation = rotComponent.rotation;
                    break;
                }
            }
            #endregion
        }
    }
}