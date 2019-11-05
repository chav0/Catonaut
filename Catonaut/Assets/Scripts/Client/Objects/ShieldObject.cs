using DG.Tweening;
using UnityEngine;

namespace Client.Objects
{
    public class ShieldObject : MonoBehaviour
    {
        public float Coef;
        public int Health; 
        
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
                .InsertCallback(.3f, () =>
                {
                    MeshRenderer.material.SetColor(ColorCached, StandartColor);
                })
                .Play(); 
        }
    }
}
