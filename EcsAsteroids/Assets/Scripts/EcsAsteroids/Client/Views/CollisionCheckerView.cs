using UnityEngine;
using Leopotam.EcsLite;

namespace EcsAsteroids.Client
{
    public class CollisionCheckerView : MonoBehaviour
    {
        public EcsWorld EcsWorld { get; set; }
        public EcsPackedEntity SelfEntity { get; set; }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var hit = EcsWorld.NewEntity();

            var hitEventPool = EcsWorld.GetPool<HitEvent>();
            hitEventPool.Add(hit);

            ref var hitEvent = ref hitEventPool.Get(hit);
            hitEvent.from = transform.root.gameObject;
            hitEvent.to = collision.gameObject;

            hitEvent.fromEnt = SelfEntity;

            var _transFilter = EcsWorld.Filter<TransformComponent>().End();
            var _transComponentPool = EcsWorld.GetPool<TransformComponent>();
            foreach (var ent in _transFilter)
            {
                ref var transComponent = ref _transComponentPool.Get(ent);
                if (transComponent.trans.gameObject == hitEvent.to)
                {
                    hitEvent.toEnt = EcsWorld.PackEntity(ent);
                }
            }
        }
    }
}