using ECS;
using ECS.Physics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.GlobalIllumination;

namespace Client.Objects
{
    public class PlayerObject : EntityRefObject
    {
        public Animator Animator;
        public Light Flashlight;
        public SphereCollider Collider; 
        public GameObject Laser;  
        private static readonly int RightBlend = Animator.StringToHash("RightBlend");
        private static readonly int ForwardBlend = Animator.StringToHash("ForwardBlend");
        private static readonly int BlendSpeed = Animator.StringToHash("BlendSpeed");
        public Transform HealthBar;
        
        private static readonly Collider[] Buffer = new Collider[20];

        public void SetAnimations(float speed)
        {
            Animator.SetFloat(BlendSpeed, speed);
        }

        public void SetFlashLight(float intensity, float yOffset, float range)
        {
            Flashlight.intensity = intensity; 
            Flashlight.transform.localPosition = new Vector3(0f, yOffset, 0f);
            Flashlight.range = range; 
        }

        public void SetLaser(bool active)
        {
            if (Laser.activeSelf != active)
            {
                Laser.SetActive(active);
            }
        }
        
        public bool TryDepenetrate(Vector3 position, out Vector3 depVector)
        {
            depVector = Vector3.zero;
            var count = Physics.OverlapSphereNonAlloc(position, Collider.radius, Buffer, Layers.GeometryMask);
            var result = false;
            for (int i = 0; i < count; i++)
            {
                var overlap = Buffer[i];
                if (Physics.ComputePenetration(Collider, position, Quaternion.identity,
                    overlap, overlap.transform.position, overlap.transform.rotation,
                    out var vector, out var distance))
                {
                    depVector += vector * distance;
                    result = true;
                }
            }
            return result;
        }
    }
}
