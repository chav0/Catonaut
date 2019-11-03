using UnityEngine;
using DG.Tweening;

namespace Client.Objects
{
    public class MonsterObject : EntityRefObject
    {
        public Animator Animator;
        public AudioSource WalkSound;
        public Transform[] Points;
        public int Health;
        public ShieldObject Shield; 
        private static readonly int BlendSpeed = Animator.StringToHash("BlendSpeed");
        private static readonly int Attack = Animator.StringToHash("Attack");
        
        private Sequence _deathColoring;
        public SkinnedMeshRenderer MeshRenderer;
        public Color32 StandartColor;
        public Light Flashlight;
        public int LastHealth { get; set; }
        private static readonly int ColorCached = Shader.PropertyToID("_BaseColor");
        
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
        public void SetDamageImpact()
        {
            _deathColoring?.Kill();
            _deathColoring = DOTween.Sequence();
            _deathColoring.AppendCallback(() =>
                {
                    MeshRenderer.material.SetColor(ColorCached, Color.red);
                    Flashlight.intensity = 100;
                    Flashlight.color = Color.red;
                })
                .InsertCallback(.3f, () =>
                {
                    MeshRenderer.material.SetColor(ColorCached, StandartColor);
                    Flashlight.intensity = 50;
                    Flashlight.color = Color.white;
                })
                .Play(); 
            
        }
    }
}
