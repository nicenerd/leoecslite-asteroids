using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsAsteroids.Client
{
    public class AsteroidMovementSystem : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<AsteroidComponent, AccelerationComponent, PositionComponent>> _filter = default;
        readonly EcsPoolInject<AsteroidComponent> _asteroidComponentPool = default;
        readonly EcsPoolInject<AccelerationComponent> _accComponentPool = default;
        readonly EcsPoolInject<PositionComponent> _posComponentPool = default;

        public void Run(IEcsSystems ecsSystems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var asteroidComponent = ref _asteroidComponentPool.Value.Get(entity);
                ref var accComponent = ref _accComponentPool.Value.Get(entity);
                ref var posComponent = ref _posComponentPool.Value.Get(entity);

                posComponent.pos += accComponent.value;
            }
        }
    }
}