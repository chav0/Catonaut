using Client.Objects;
using UnityEngine;

namespace ECS.Components
{
    public class DamageZone : Component
    {
        public Vector3 Position;
        public int Damage;
        public DamageZoneObject Body;
        public float Radius;
    }
}
