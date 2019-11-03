using Client.Objects;
using UnityEngine;

namespace ECS.Components
{
    public class Monster : Component
    {
        public int TargetPoint;
        public uint TargetId; 
        public MonsterObject Body;
        public int NextAttackTick;
        public bool Move; 
        public bool Attack;
        public bool HaveShield;
        public float DamageCoef;
        public int CurrentShieldHealth; 
    }
}
