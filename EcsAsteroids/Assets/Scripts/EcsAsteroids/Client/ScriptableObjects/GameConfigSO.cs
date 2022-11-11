using UnityEngine;

namespace EcsAsteroids.Client
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "EcsPong/GameConfig", order = 1100)]
    public sealed class GameConfigSO : ScriptableObject
    {
        [Header("Presets")]
        public SpaceshipConfigSO spaceshipConfig;
        public BulletConfigSO spaceshipBulletConfig;
        public LaserConfigSO spaceshipLaserConfig;
        public AsteroidConfigSO asteroidConfig;
        public UfoConfigSO ufoConfig;

        [Header("Game Config")]
        public float asteroidSpawnDelay;
        public float ufoSpawnDelay;
        public int asteroidScorePoints;
        public int ufoScorePoints;
    }
}