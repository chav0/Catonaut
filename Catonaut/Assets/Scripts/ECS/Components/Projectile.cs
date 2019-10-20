using UnityEngine;

namespace ECS.Components
{
    public class Projectile : Component
    {
        public Entity Owner;
        public Vector3 Origin;
        public Vector3 Position;
        public Vector3 Direction;
        public float SpeedPerTick;
        public int RemainingLifetime;
    }
}
