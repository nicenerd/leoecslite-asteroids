using UnityEngine.Scripting;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Unity.Ugui;

namespace EcsAsteroids.Client
{
    sealed class UserUiButtonsInputSystem : EcsUguiCallbackSystem
    {
        readonly EcsWorldInject _eventsWorld = AppConstants.Worlds.Events;
        readonly EcsPoolInject<PauseEvent> _pauseEventPool = AppConstants.Worlds.Events;
        readonly EcsPoolInject<RestartEvent> _restartEventPool = AppConstants.Worlds.Events;

        [Preserve]
        [EcsUguiClickEvent(AppConstants.Ui.ResumeBtn, AppConstants.Worlds.Events)]
        void OnClickResumeBtn(in EcsUguiClickEvent e)
        {
            var pause = _eventsWorld.Value.NewEntity();
            _pauseEventPool.Value.Add(pause);
        }

        [Preserve]
        [EcsUguiClickEvent(AppConstants.Ui.RestartBtn, AppConstants.Worlds.Events)]
        void OnClickRestartBtn(in EcsUguiClickEvent e)
        {
            var restart = _eventsWorld.Value.NewEntity();
            _restartEventPool.Value.Add(restart);
        }
    }
}