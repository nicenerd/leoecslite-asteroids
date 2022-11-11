using UnityEngine;

namespace EcsAsteroids.Client
{
    [CreateAssetMenu(fileName = "UfoConfig", menuName = "EcsPong/UfoConfig", order = 1100)]
    public sealed class UfoConfigSO : ScriptableObject
    {
        public GameObject ufoPrefab;
        public float speed;
    }
}