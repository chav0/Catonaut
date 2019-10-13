using UnityEngine;

namespace ECS.Components
{
    public class Transform : IComponent
    {
        public Vector3 Position;
        public Vector3 Rotation;
        public float Speed; 
    }
}
