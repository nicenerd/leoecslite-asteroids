using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsAsteroids.Client
{
    public class HitSystem : IEcsRunSystem
    {
        readonly EcsCustomInject<GameData> _gameData = default;
        readonly EcsWorldInject _ecsWorld = default;

        readonly EcsFilterInject<Inc<HitEvent>> _hitFilter = default;
        readonly EcsPoolInject<HitEvent> _hitComponentPool = default;

        readonly EcsWorldInject _eventsWorld = AppConstants.Worlds.Events;
        readonly EcsPoolInject<LoseEvent> _loseEventPool = AppConstants.Worlds.Events;
        readonly EcsPoolInject<DestroyComponent> _destroyComponentPool = default;
        readonly EcsPoolInject<ScoreEvent> _scoreEventPool = AppConstants.Worlds.Events;
        readonly EcsPoolInject<AsteroidComponent> _asteroidPool = default;
        readonly EcsPoolInject<AsteroidSplitComponent> _asteroidSplitComponentPool = default;

        public void Run(IEcsSystems ecsSystems)
        {
            var asteroidScorePoints = _gameData.Value.config.asteroidScorePoints;
            var ufoScorePoints = _gameData.Value.config.ufoScorePoints;

            foreach (var hitEntity in _hitFilter.Value)
            {
                ref var hitComponent = ref _hitComponentPool.Value.Get(hitEntity);

                if (hitComponent.from.CompareTag(AppConstants.Tags.Spaceship))
                {
                    if (hitComponent.to.CompareTag(AppConstants.Tags.Asteroid) ||
                        hitComponent.to.CompareTag(AppConstants.Tags.Ufo))
                    {
                        var loseEvent = _eventsWorld.Value.NewEntity();
                        _loseEventPool.Value.Add(loseEvent);
                        break;
                    }
                }
                else if (hitComponent.from.CompareTag(AppConstants.Tags.Asteroid))
                {
                    if (hitComponent.to.CompareTag(AppConstants.Tags.Laser))
                    {
                        var setScoreEvent = _eventsWorld.Value.NewEntity();
                        _scoreEventPool.Value.Add(setScoreEvent);
                        ref var scoreEvent = ref _scoreEventPool.Value.Get(setScoreEvent);
                        scoreEvent.score = asteroidScorePoints;

                        if (hitComponent.fromEnt.Unpack(_ecsWorld.Value, out int unpackedFromEnt))
                        {
                            if (!_destroyComponentPool.Value.Has(unpackedFromEnt))
                            {
                                _destroyComponentPool.Value.Add(unpackedFromEnt);
                            }
                        }
                    }
                    else if (hitComponent.to.CompareTag(AppConstants.Tags.Bullet))
                    {
                        if (hitComponent.fromEnt.Unpack(_ecsWorld.Value, out int unpackedFromEnt))
                        {
                            ref var asteroidComponent = ref _asteroidPool.Value.Get(unpackedFromEnt);
                            if (asteroidComponent.type == AsteroidType.LARGE)
                            {
                                _asteroidSplitComponentPool.Value.Add(unpackedFromEnt);
                            }
                            else
                            {
                                var setScoreEvent = _eventsWorld.Value.NewEntity();
                                _scoreEventPool.Value.Add(setScoreEvent);
                                ref var scoreEvent = ref _scoreEventPool.Value.Get(setScoreEvent);
                                scoreEvent.score = asteroidScorePoints;

                                if (!_destroyComponentPool.Value.Has(unpackedFromEnt))
                                {
                                    _destroyComponentPool.Value.Add(unpackedFromEnt);
                                }
                            }
                        }
                        if (hitComponent.toEnt.Unpack(_ecsWorld.Value, out int unpackedToEnt))
                        {
                            if (!_destroyComponentPool.Value.Has(unpackedToEnt))
                            {
                                _destroyComponentPool.Value.Add(unpackedToEnt);
                            }
                        }
                    }
                }
                else if (hitComponent.from.CompareTag(AppConstants.Tags.Ufo))
                {
                    if (hitComponent.to.CompareTag(AppConstants.Tags.Laser))
                    {
                        var setScoreEvent = _eventsWorld.Value.NewEntity();
                        _scoreEventPool.Value.Add(setScoreEvent);
                        ref var scoreEvent = ref _scoreEventPool.Value.Get(setScoreEvent);
                        scoreEvent.score = ufoScorePoints;

                        if (hitComponent.fromEnt.Unpack(_ecsWorld.Value, out int unpackedFromEnt))
                        {
                            if (!_destroyComponentPool.Value.Has(unpackedFromEnt))
                            {
                                _destroyComponentPool.Value.Add(unpackedFromEnt);
                            }
                        }
                    }
                    else if (hitComponent.to.CompareTag(AppConstants.Tags.Bullet))
                    {
                        var setScoreEvent = _eventsWorld.Value.NewEntity();
                        _scoreEventPool.Value.Add(setScoreEvent);
                        ref var scoreEvent = ref _scoreEventPool.Value.Get(setScoreEvent);
                        scoreEvent.score = ufoScorePoints;

                        if (hitComponent.fromEnt.Unpack(_ecsWorld.Value, out int unpackedFromEnt))
                        {
                            if (!_destroyComponentPool.Value.Has(unpackedFromEnt))
                            {
                                _destroyComponentPool.Value.Add(unpackedFromEnt);
                            }
                        }
                        if (hitComponent.toEnt.Unpack(_ecsWorld.Value, out int unpackedToEnt))
                        {
                            if (!_destroyComponentPool.Value.Has(unpackedToEnt))
                            {
                                _destroyComponentPool.Value.Add(unpackedToEnt);
                            }
                        }
                    }
                }
            }
        }
    }
}