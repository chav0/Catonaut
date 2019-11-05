using ECS.Components;
using UnityEngine;

namespace Client.Objects
{
    public class CapsuleKeyObject : MonoBehaviour
    {
        [SerializeField] private Color _red;
        [SerializeField] private Color _green;
        [SerializeField] private Color _blue;
        [SerializeField] private Renderer _firstKey;
        [SerializeField] private Renderer _secondKey;
        
        private KeyColor _keyColor;
        private bool _isCompleted;
        private static readonly int ColorCached = Shader.PropertyToID("_BaseColor");

        public KeyColor KeyColor
        {
            get => _keyColor;
            set
            {
                _keyColor = value;
                
                switch (value)
                {
                    case KeyColor.Red:
                        _firstKey.material.SetColor(ColorCached, _red);
                        _secondKey.material.SetColor(ColorCached, _red);
                        break;
                    case KeyColor.Green:
                        _firstKey.material.SetColor(ColorCached, _green);
                        _secondKey.material.SetColor(ColorCached, _green);
                        break;
                    case KeyColor.Blue:
                        _firstKey.material.SetColor(ColorCached, _blue);
                        _secondKey.material.SetColor(ColorCached, _blue);
                        break;
                }
            }
        } 
        
        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                _isCompleted = value;
                _secondKey.gameObject.SetActive(_isCompleted);
            }
        }
    }
}
