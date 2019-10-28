using UnityEngine;

namespace Client.Objects
{
    public class MonsterObject : EntityRefObject
    {
        public Animator Animator;
        public AudioSource WalkSound;
        public Transform[] Points;
        public int Health; 
        private static readonly int BlendSpeed = Animator.StringToHash("BlendSpeed");
        
        public void SetAnimations(float speed)
        {
            Animator.SetFloat(BlendSpeed, speed);
            if(speed > 0.5f)
                WalkSound.enabled = true;
            else
            {
                WalkSound.enabled = false;
            }
        }
    }
}
