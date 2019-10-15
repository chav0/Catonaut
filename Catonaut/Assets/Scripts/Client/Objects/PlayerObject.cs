using ECS;
using UnityEngine;

namespace Client.Objects
{
    public class PlayerObject : MonoBehaviour
    {
        public Animator Animator;
        public CharacterController CharacterController; 
        public Rigidbody Rigidbody; 
        private static readonly int RightBlend = Animator.StringToHash("RightBlend");
        private static readonly int ForwardBlend = Animator.StringToHash("ForwardBlend");
        private static readonly int BlendSpeed = Animator.StringToHash("BlendSpeed");

        public void SetAnimations(float speed)
        {
            Animator.SetFloat(BlendSpeed, speed);
        }
        
        public void SetEntity(Entity player)
        {
            
        }
    }
}
