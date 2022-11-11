using UnityEngine;

namespace EcsAsteroids.Client
{
    public class GameData
    {
        public GameConfigSO config;
        public AsteroidsInput input;
        public bool isPaused;
        public Bounds gameFieldBounds;
        public int score;
        public float spawnAsteroidCooldown;
        public float spawnUfoCooldown;
    }
}