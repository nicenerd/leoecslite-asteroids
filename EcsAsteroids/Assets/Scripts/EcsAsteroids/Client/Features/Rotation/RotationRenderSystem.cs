using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsAsteroids.Client
{
    public class RotationRenderSystem : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<TransformComponent, RotationComponent>> _filter = default;
        readonly EcsPoolInject<TransformComponent> _tranComponentPool = default;
        readonly EcsPoolInject<RotationComponent> _rotComponentPool = default;

        public void Run(IEcsSystems ecsSystems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var tranComponent = ref _tranComponentPool.Value.Get(entity);
                ref var rotComponent = ref _rotComponentPool.Value.Get(entity);

                //tranComponent.trans.rotation = Quaternion.Euler(
                //    tranComponent.trans.rotation.x, tranComponent.trans.rotation.y, rotComponent.rotation);
                tranComponent.trans.rotation = Quaternion.Euler(0, 0, rotComponent.rotation);
            }
        }
    }
}