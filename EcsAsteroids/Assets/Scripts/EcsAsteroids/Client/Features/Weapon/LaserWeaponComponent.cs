namespace EcsAsteroids.Client
{
    public struct LaserWeaponComponent
    {
        public bool isFireable;
        public float minShotInterval;
        public float timeSinceLastShot;

        public int laserCharges;
        public int maxLaserChargeCount;
        public float rechargeCooldown;
        public float minRechargeInterval;
    }
}