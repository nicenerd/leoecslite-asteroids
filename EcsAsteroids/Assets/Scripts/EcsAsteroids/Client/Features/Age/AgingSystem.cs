using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsAsteroids.Client
{
    public class AgingSystem : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<AgeComponent>> _filter = default;
        readonly EcsPoolInject<AgeComponent> _ageComponentPool = default;

        public void Run(IEcsSystems ecsSystems)
        {
            var dTime = Time.deltaTime;

            foreach (var entity in _filter.Value)
            {
                ref var ageComponent = ref _ageComponentPool.Value.Get(entity);
                ageComponent.age += dTime;
            }
        }
    }
}