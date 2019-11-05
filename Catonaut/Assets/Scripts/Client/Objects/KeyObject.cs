using System;
using ECS.Components;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

namespace Client.Objects
{
    public class KeyObject : EntityRefObject
    {
        [SerializeField] private Color _red;
        [SerializeField] private Color _green;
        [SerializeField] private Color _blue;
        [SerializeField] private Renderer _meshRenderer;

        private KeyColor _keyColor;
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
                        _meshRenderer.material.SetColor(ColorCached, _red);
                        break;
                    case KeyColor.Green:
                        _meshRenderer.material.SetColor(ColorCached, _green);
                        break;
                    case KeyColor.Blue:
                        _meshRenderer.material.SetColor(ColorCached, _blue);
                        break;
                }
            }
        } 
    }
}
