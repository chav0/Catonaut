using UnityEngine;
using DG.Tweening;
using ECS.Components;

namespace Client.Objects
{
    public class DamageZoneObject : EntityRefObject
    {
        public int Damage;
        public int Health;
        public float Radius;
        
        private Sequence _deathColoring;
        public MeshRenderer MeshRenderer;
        public Color32 StandartColor;
        public int LastHealth { get; set; }
        private static readonly int ColorCached = Shader.PropertyToID("_BaseColor");
        
        public void SetDamageImpact()
        {
            _deathColoring?.Kill();
            _deathColoring = DOTween.Sequence();
            _deathColoring.AppendCallback(() => MeshRenderer.material.SetColor(ColorCached, Color.yellow))
                .InsertCallback(.15f, () =>
                {
                    MeshRenderer.material.SetColor(ColorCached, StandartColor);
                })
                .Play(); 
        }
    }
}
