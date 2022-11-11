using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsAsteroids.Client
{
    public class MaxAgeSystem : IEcsRunSystem
    {
        readonly EcsWorldInject _ecsWorld = default;
        readonly EcsFilterInject<Inc<AgeComponent, MaxAgeComponent>> _filter = default;
        readonly EcsPoolInject<AgeComponent> _ageComponentPool = default;
        readonly EcsPoolInject<MaxAgeComponent> _maxAgeComponentPool = default;
        readonly EcsPoolInject<DestroyComponent> _destroyComponentPool = default;

        public void Run(IEcsSystems ecsSystems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var ageComponent = ref _ageComponentPool.Value.Get(entity);
                ref var maxAgeComponent = ref _maxAgeComponentPool.Value.Get(entity);
                if (ageComponent.age >= maxAgeComponent.maxAge)
                {
                    _destroyComponentPool.Value.Add(entity);
                }
            }
        }
    }
}