using UnityEngine;

namespace EcsAsteroids.Client
{
    [CreateAssetMenu(fileName = "SpaceshipConfig", menuName = "EcsPong/SpaceshipConfig", order = 1100)]
    public sealed class SpaceshipConfigSO : ScriptableObject
    {
        public GameObject spaceshipPrefab;
        public Vector2 spawnPos;

        public float maxSpeed;
        public float speed;
        public float timeToStop;

        public float rotDegPerSecond;

        [Header("Weapons")]
        public float spawnBulletsFwdOffset;
        public float bulletWpnDelay;
        public float laserWpnDelay;
        public int laserWpnMaxCharges;
        public float laserWpnMinRechargeInterval;
    }
}