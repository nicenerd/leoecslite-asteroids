using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsAsteroids.Client
{
    public class PlayerInputSystem : IEcsRunSystem
    {
        readonly EcsCustomInject<GameData> _gameData = default;
        readonly EcsFilterInject<Inc<SpaceshipInputComponent, InputByPlayer>> _filter = default;
        readonly EcsPoolInject<SpaceshipInputComponent> _spaceshipInputPool = default;

        public void Run(IEcsSystems ecsSystems)
        {
            var gameInput = _gameData.Value.input.Game;

            foreach (var entity in _filter.Value)
            {
                ref var spaceshipInput = ref _spaceshipInputPool.Value.Get(entity);

                spaceshipInput.moveFwd = gameInput.MoveFwd.ReadValue<float>();
                spaceshipInput.rotation = gameInput.Rotation.ReadValue<float>();
                spaceshipInput.primaryWeapon = gameInput.PrimaryWeapon.ReadValue<float>();
                spaceshipInput.secondaryWeapon = gameInput.SecondaryWeapon.ReadValue<float>();
            }
        }
    }
}