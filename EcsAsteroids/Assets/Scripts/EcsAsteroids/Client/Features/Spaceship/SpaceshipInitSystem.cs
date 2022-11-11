using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Unity.Ugui;

namespace EcsAsteroids.Client
{
    public class SpaceshipInitSystem : IEcsInitSystem
    {
        [EcsUguiNamed(AppConstants.Ui.PauseMenuPopup)]
        readonly GameObject _pauseMenuPopupGO = default;
        [EcsUguiNamed(AppConstants.Ui.LoseMenuPopup)]
        readonly GameObject _loseMenuPopupGO = default;

        readonly EcsCustomInject<GameData> _gameData = default;
        readonly EcsPoolInject<InputByPlayer> _inputByPlayerPool = default;
        readonly EcsPoolInject<SpaceshipComponent> _spaceshipPool = default;
        readonly EcsPoolInject<SpaceshipInputComponent> _spaceshipInputPool = default;
        readonly EcsPoolInject<AccelerationComponent> _accComponentPool = default;
        readonly EcsPoolInject<PositionComponent> _posComponentPool = default;
        readonly EcsPoolInject<RotationComponent> _rotComponentPool = default;
        readonly EcsPoolInject<BoundsComponent> _boundsComponentPool = default;
        readonly EcsPoolInject<TransformComponent> _transComponentPool = default;
        readonly EcsPoolInject<BulletWeaponComponent> _bulletWeaponComponentPool = default;
        readonly EcsPoolInject<LaserWeaponComponent> _laserWeaponComponentPool = default;

        public void Init(IEcsSystems ecsSystems)
        {
            var ecsWorld = ecsSystems.GetWorld();
            var entity = ecsWorld.NewEntity();

            _inputByPlayerPool.Value.Add(entity);
            _spaceshipInputPool.Value.Add(entity);
            _accComponentPool.Value.Add(entity);
            _posComponentPool.Value.Add(entity);
            _rotComponentPool.Value.Add(entity);
            _boundsComponentPool.Value.Add(entity);

            var spaceshipConfig = _gameData.Value.config.spaceshipConfig;
            var spaceshipView = Object.Instantiate(spaceshipConfig.spaceshipPrefab, spaceshipConfig.spawnPos, Quaternion.identity);
            var spaceshipViewCollChecker = spaceshipView.GetComponent<CollisionCheckerView>();
            spaceshipViewCollChecker.EcsWorld = ecsWorld;
            spaceshipViewCollChecker.SelfEntity = ecsWorld.PackEntity(entity);

            _transComponentPool.Value.Add(entity);
            ref var transComponent = ref _transComponentPool.Value.Get(entity);
            transComponent.trans = spaceshipView.transform;

            _bulletWeaponComponentPool.Value.Add(entity);
            ref var bulletWpnComponent = ref _bulletWeaponComponentPool.Value.Get(entity);
            bulletWpnComponent.minShotInterval = spaceshipConfig.bulletWpnDelay;
            bulletWpnComponent.timeSinceLastShot = 0f;

            _laserWeaponComponentPool.Value.Add(entity);
            ref var laserWpnComponent = ref _laserWeaponComponentPool.Value.Get(entity);
            laserWpnComponent.minShotInterval = spaceshipConfig.laserWpnDelay;
            laserWpnComponent.maxLaserChargeCount = spaceshipConfig.laserWpnMaxCharges;
            laserWpnComponent.minRechargeInterval = spaceshipConfig.laserWpnMinRechargeInterval;
            laserWpnComponent.laserCharges = laserWpnComponent.maxLaserChargeCount;
            laserWpnComponent.timeSinceLastShot = 0f;
            laserWpnComponent.rechargeCooldown = 0f;

            _spaceshipPool.Value.Add(entity);
            ref var spaceshipComponent = ref _spaceshipPool.Value.Get(entity);
            spaceshipComponent.speed = spaceshipConfig.speed;
            spaceshipComponent.maxSpeed = spaceshipConfig.maxSpeed;
            spaceshipComponent.timeToStop = spaceshipConfig.timeToStop;
            spaceshipComponent.rotSpeed = spaceshipConfig.rotDegPerSecond;

            _pauseMenuPopupGO.SetActive(false);
            _loseMenuPopupGO.SetActive(false);
            _gameData.Value.input.Enable();
        }
    }
}