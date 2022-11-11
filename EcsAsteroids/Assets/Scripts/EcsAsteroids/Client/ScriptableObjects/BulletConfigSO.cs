using UnityEngine;

namespace EcsAsteroids.Client
{
    [CreateAssetMenu(fileName = "BulletConfig", menuName = "EcsPong/BulletConfig", order = 1100)]
    public sealed class BulletConfigSO : ScriptableObject
    {
        public GameObject bulletPrefab;

        public float speed;
        public float maxAge;
    }
}