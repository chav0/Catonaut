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
        private static readonly int Attack = Animator.StringToHash("Attack");
        
        public void SetAnimations(float speed, bool attack)
        {
            Animator.SetFloat(BlendSpeed, speed);
            if(speed > 0.5f)
                WalkSound.enabled = true;
            else
            {
                WalkSound.enabled = false;
            }

            if (attack)
            {
                Animator.SetTrigger(Attack);
            }
        }
    }
}
