using UnityEngine;

namespace EcsAsteroids.Client
{
    [CreateAssetMenu(fileName = "LaserConfig", menuName = "EcsPong/LaserConfig", order = 1100)]
    public sealed class LaserConfigSO : ScriptableObject
    {
        public GameObject laserPrefab;
        public float maxAge;
    }
}