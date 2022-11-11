using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedSystems;
using Leopotam.EcsLite.Unity.Ugui;

namespace EcsAsteroids.Client
{
    public class GameStartup : MonoBehaviour
    {
        [SerializeField] private GameConfigSO _gameConfig;
        [SerializeField] private EcsUguiEmitter _uguiEmitter;

        private EcsWorld _ecsWorld;
        private EcsWorld _globalEventsWorld;
        private IEcsSystems _updateSystems;

        private void Start()
        {
            //Random.InitState(42);
            Random.InitState(System.DateTime.UtcNow.GetHashCode());

            _ecsWorld = new EcsWorld();
            _globalEventsWorld = new EcsWorld();

            var gameData = new GameData
            {
                config = _gameConfig,
                input = new AsteroidsInput(),
                gameFieldBounds = GetGameFieldBounds(),
                score = 0,
                spawnAsteroidCooldown = 0f,
                spawnUfoCooldown = 0f
            };

            _updateSystems = new EcsSystems(_ecsWorld, gameData);
            _updateSystems
                .AddWorld(_globalEventsWorld, AppConstants.Worlds.Events)
                // init systems
                .Add(new SpaceshipInitSystem())
                .Add(new UsetHotkeysInitSystem())
                // update systems
                .AddGroup(AppConstants.SimulationGroupName, true, AppConstants.Worlds.Events,
                    new AsteroidSpawnSystem(),
                    new UfoSpawnSystem(),
                    new AgingSystem(),
                    new MaxAgeSystem(),
                    new DestroyingSystem(),
                    new PlayerInputSystem(),
                    new SpaceshipMovementSystem(),
                    new AsteroidMovementSystem(),
                    new UfoAiSystem(),
                    new HitSystem(),
                    new AsteroidSplittingSystem(),
                    new BulletWeaponSystem(),
                    new LaserWeaponSystem(),
                    new BulletSystem(),
                    new LaserSystem(),
                    new BoundsWrapSystem(),
                    new PositionRenderSystem(),
                    new RotationRenderSystem()
                )
                .Add(new UserUiButtonsInputSystem())
                .Add(new SpaceshipHudSystem())
                .Add(new PauseSystem())
                .Add(new RestartSystem())
                .Add(new ScoreSystem())
                // events
                .DelHere<HitEvent>()
                .DelHere<BulletInitComponent>()
                .DelHere<LaserInitComponent>()
                // global events
                .DelHere<PauseEvent>(AppConstants.Worlds.Events)
                .DelHere<RestartEvent>(AppConstants.Worlds.Events)
                .DelHere<LoseEvent>(AppConstants.Worlds.Events)
                .DelHere<ScoreEvent>(AppConstants.Worlds.Events)
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem(AppConstants.Worlds.Events))
#endif
                .Inject(gameData)
                .InjectUgui(_uguiEmitter, AppConstants.Worlds.Events)
                .Init();
        }

        private void Update()
        {
            _updateSystems?.Run();
        }

        private void OnDestroy()
        {
            if (_updateSystems != null)
            {
                _updateSystems.GetWorld(AppConstants.Worlds.Events).Destroy();
                _updateSystems.GetWorld().Destroy();
                _updateSystems.Destroy();
                _updateSystems = null;
            }
        }

        private Bounds GetGameFieldBounds()
        {
            var size = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            return new Bounds(Vector3.zero, new Vector3(size.x * 2, size.y * 2));
        }
    }
}