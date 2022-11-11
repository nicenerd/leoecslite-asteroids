using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsAsteroids.Client
{
    public class UfoAiSystem : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<UfoComponent, AccelerationComponent, PositionComponent>> _ufoFilter = default;
        readonly EcsPoolInject<UfoComponent> _ufoComponentPool = default;
        readonly EcsPoolInject<AccelerationComponent> _accComponentPool = default;
        readonly EcsPoolInject<PositionComponent> _posComponentPool = default;

        readonly EcsFilterInject<Inc<InputByPlayer, PositionComponent>> _spaceshipFilter = default;
        readonly EcsPoolInject<PositionComponent> _spaceshipPosComponentPool = default;

        public void Run(IEcsSystems ecsSystems)
        {
            var dTime = Time.deltaTime;

            foreach (var spaceshipEntity in _spaceshipFilter.Value)
            {
                ref var spaceshipPosComponent = ref _spaceshipPosComponentPool.Value.Get(spaceshipEntity);
                var spaceshipPos = spaceshipPosComponent.pos;

                foreach (var entity in _ufoFilter.Value)
                {
                    ref var ufoComponent = ref _ufoComponentPool.Value.Get(entity);
                    ref var accComponent = ref _accComponentPool.Value.Get(entity);
                    ref var posComponent = ref _posComponentPool.Value.Get(entity);

                    var viewVec = spaceshipPos - posComponent.pos;
                    accComponent.value += viewVec * (ufoComponent.speed * dTime);
                    accComponent.value = Vector2.ClampMagnitude(accComponent.value, ufoComponent.speed);

                    posComponent.pos += accComponent.value;
                }
            }
        }
    }
}