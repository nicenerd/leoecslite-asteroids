namespace EcsAsteroids.Client
{
    public struct BulletWeaponComponent
    {
        public bool isFireable;
        public float minShotInterval;
        public float timeSinceLastShot;
    }
}