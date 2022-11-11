using UnityEngine;

namespace EcsAsteroids.Client
{
    [CreateAssetMenu(fileName = "AsteroidConfig", menuName = "EcsPong/AsteroidConfig", order = 1100)]
    public sealed class AsteroidConfigSO : ScriptableObject
    {
        public GameObject asteroidPrefab;
        public GameObject asteroidSmallPrefab;
        public Vector2 initSpeedRange;          // min/max
        public Vector2 smallInitSpeedRange;     // min/max
        public float maxAge;
        public Vector2 smallPartsCountRange;    // min/max
    }
}