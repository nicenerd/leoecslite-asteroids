namespace EcsAsteroids.Client
{
    public static class AppConstants
    {
        public static class Tags
        {
            public const string Spaceship = "Spaceship";
            public const string Asteroid = "Asteroid";
            public const string Ufo = "Ufo";
            public const string Bullet = "Bullet";
            public const string Laser = "Laser";
        }

        public static class Worlds
        {
            public const string Events = "Events";
        }

        public const string SimulationGroupName = "SimulationSystems";

        public static class Ui
        {
            // pause menu
            public const string PauseMenuPopup = "PauseMenu";
            public const string ResumeBtn = "ResumeBtn";

            // lose menu
            public const string LoseMenuPopup = "LoseMenu";
            public const string ScoreText = "ScoreText";
            public const string RestartBtn = "RestartBtn";

            // ship HUD
            public const string ShipHudPosText = "ShipPosText";
            public const string ShipHudRotAngleText = "ShipRotAngleText";
            public const string ShipHudVelText = "ShipVelText";
            public const string ShipHudLaserChargesCounterText = "LaserChargesCounterText";
            public const string ShipHudLaserCooldownTimerText = "LaserCooldownTimerText";
        }
    }
}