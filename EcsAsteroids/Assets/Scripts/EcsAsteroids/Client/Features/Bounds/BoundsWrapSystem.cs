using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsAsteroids.Client
{
    public class BoundsWrapSystem : IEcsRunSystem
    {
        readonly EcsCustomInject<GameData> _gameData = default;
        readonly EcsFilterInject<Inc<BoundsComponent, PositionComponent>> _filter = default;
        readonly EcsPoolInject<PositionComponent> _posComponentPool = default;

        public void Run(IEcsSystems ecsSystems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var posComponent = ref _posComponentPool.Value.Get(entity);

                var bounds = _gameData.Value.gameFieldBounds;
                var pos = posComponent.pos;

                if (pos.x < bounds.min.x)
                    posComponent.pos = new Vector2(pos.x + bounds.size.x, pos.y);

                if (pos.x > bounds.max.x)
                    posComponent.pos = new Vector2(pos.x - bounds.size.x, pos.y);

                if (pos.y < bounds.min.y)
                    posComponent.pos = new Vector2(pos.x, pos.y + bounds.size.y);

                if (pos.y > bounds.max.y)
                    posComponent.pos = new Vector2(pos.x, pos.y - bounds.size.y);
            }
        }
    }
}