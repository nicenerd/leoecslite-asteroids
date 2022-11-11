using UnityEngine;
using UnityEngine.UI;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Unity.Ugui;

namespace EcsAsteroids.Client
{
    public class SpaceshipHudSystem : IEcsRunSystem
    {
        [EcsUguiNamed(AppConstants.Ui.ShipHudPosText)]
        readonly Text _posText = default;
        [EcsUguiNamed(AppConstants.Ui.ShipHudRotAngleText)]
        readonly Text _rotAngleText = default;
        [EcsUguiNamed(AppConstants.Ui.ShipHudVelText)]
        readonly Text _velText = default;
        [EcsUguiNamed(AppConstants.Ui.ShipHudLaserChargesCounterText)]
        readonly Text _laserChargesCounterText = default;
        [EcsUguiNamed(AppConstants.Ui.ShipHudLaserCooldownTimerText)]
        readonly Text _laserCooldownTimerText = default;

        readonly EcsFilterInject<Inc<InputByPlayer, LaserWeaponComponent,
            AccelerationComponent, PositionComponent, RotationComponent>> _spaceshipFilter = default;
        readonly EcsPoolInject<AccelerationComponent> _accComponentPool = default;
        readonly EcsPoolInject<PositionComponent> _posComponentPool = default;
        readonly EcsPoolInject<RotationComponent> _rotComponentPool = default;
        readonly EcsPoolInject<LaserWeaponComponent> _laserComponentPool = default;

        public void Run(IEcsSystems ecsSystems)
        {
            foreach (var entity in _spaceshipFilter.Value)
            {
                ref var posComponent = ref _posComponentPool.Value.Get(entity);
                ref var rotComponent = ref _rotComponentPool.Value.Get(entity);
                ref var accComponent = ref _accComponentPool.Value.Get(entity);
                ref var laserComponent = ref _laserComponentPool.Value.Get(entity);

                _posText.text = $"Position: {posComponent.pos}";
                //_rotAngleText.text = $"Rot angle: {Quaternion.Euler(0, 0, rotComponent.rotation).eulerAngles.z}";
                _rotAngleText.text = $"Rot angle: {Mathf.Repeat(rotComponent.rotation, 360)}";
                _velText.text = $"Speed: {Mathf.RoundToInt(accComponent.value.magnitude * 1000)}";
                _laserChargesCounterText.text = $"Laser charge count: {laserComponent.laserCharges}";
                _laserCooldownTimerText.text = $"Laser reload cooldown: {Mathf.RoundToInt(laserComponent.rechargeCooldown)}";
            }
        }
    }
}