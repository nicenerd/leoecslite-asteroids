using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsAsteroids.Client
{
    public class PositionRenderSystem : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<TransformComponent, PositionComponent>> _filter = default;
        readonly EcsPoolInject<TransformComponent> _tranComponentPool = default;
        readonly EcsPoolInject<PositionComponent> _posComponentPool = default;

        public void Run(IEcsSystems ecsSystems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var tranComponent = ref _tranComponentPool.Value.Get(entity);
                ref var posComponent = ref _posComponentPool.Value.Get(entity);

                tranComponent.trans.position = posComponent.pos;
            }
        }
    }
}