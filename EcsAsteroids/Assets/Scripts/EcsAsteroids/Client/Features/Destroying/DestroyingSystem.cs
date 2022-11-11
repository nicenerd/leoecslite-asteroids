using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsAsteroids.Client
{
    public class DestroyingSystem : IEcsRunSystem
    {
        readonly EcsWorldInject _ecsWorld = default;
        readonly EcsFilterInject<Inc<DestroyComponent, TransformComponent>> _filter = default;
        readonly EcsPoolInject<TransformComponent> _transComponentPool = default;

        public void Run(IEcsSystems ecsSystems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var transComponent = ref _transComponentPool.Value.Get(entity);
                Object.Destroy(transComponent.trans.gameObject);
                _ecsWorld.Value.DelEntity(entity);
            }
        }
    }
}