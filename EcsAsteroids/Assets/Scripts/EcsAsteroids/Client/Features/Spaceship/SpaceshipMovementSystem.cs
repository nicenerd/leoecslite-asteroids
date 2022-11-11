using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsAsteroids.Client
{
    public class SpaceshipMovementSystem : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<SpaceshipComponent, SpaceshipInputComponent, InputByPlayer,
            AccelerationComponent, PositionComponent, RotationComponent>> _filter = default;
        readonly EcsPoolInject<SpaceshipComponent> _spaceshipComponentPool = default;
        readonly EcsPoolInject<SpaceshipInputComponent> _spaceshipInputComponentPool = default;
        readonly EcsPoolInject<AccelerationComponent> _accComponentPool = default;
        readonly EcsPoolInject<PositionComponent> _posComponentPool = default;
        readonly EcsPoolInject<RotationComponent> _rotComponentPool = default;

        public void Run(IEcsSystems ecsSystems)
        {
            var dTime = Time.deltaTime;

            foreach (var entity in _filter.Value)
            {
                ref var spaceshipComponent = ref _spaceshipComponentPool.Value.Get(entity);
                ref var spaceshipInputComponent = ref _spaceshipInputComponentPool.Value.Get(entity);
                ref var accComponent = ref _accComponentPool.Value.Get(entity);
                ref var posComponent = ref _posComponentPool.Value.Get(entity);
                ref var rotComponent = ref _rotComponentPool.Value.Get(entity);

                // rotation
                var rotDir = spaceshipInputComponent.rotation;
                if (rotDir != 0)
                {
                    rotComponent.rotation += rotDir * dTime * spaceshipComponent.rotSpeed;
                }

                // move
                if (spaceshipInputComponent.moveFwd == 0)
                {
                    if (spaceshipComponent.timeToStop > 0)
                        accComponent.value -= accComponent.value * (dTime / spaceshipComponent.timeToStop);
                }
                else
                {
                    Vector2 fwd = Quaternion.Euler(0, 0, rotComponent.rotation) * Vector3.up;
                    accComponent.value += fwd * (spaceshipComponent.speed * dTime);
                    accComponent.value = Vector2.ClampMagnitude(accComponent.value, spaceshipComponent.maxSpeed);
                }
                posComponent.pos += accComponent.value;
            }
        }
    }
}