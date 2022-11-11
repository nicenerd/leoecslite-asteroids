using Leopotam.EcsLite;
using UnityEngine;

namespace EcsAsteroids.Client
{
    public struct HitEvent
    {
        public GameObject from;
        public GameObject to;
        public EcsPackedEntity fromEnt;
        public EcsPackedEntity toEnt;
    }
}