﻿using UnityEngine;

namespace ECS.Components
{
    public class Projectile : Component
    {
        public Entity Owner;
        public Vector3 Position;
        public Vector3 Direction;
        public int Damage;
        public float SpeedPerTick;
        public int RemainingLifetime;
        public bool IsDead; 
    }
}
