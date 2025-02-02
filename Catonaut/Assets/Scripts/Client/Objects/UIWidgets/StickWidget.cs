﻿using UnityEngine;
using UnityEngine.EventSystems;

namespace Client.Objects.UIWidgets
{
    public class StickWidget : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        public float MinRadius;
        public float MaxRadius;
        public AudioSource ShotSound;
        public Vector2 StartPosition { get; private set; }
        public Vector2 CurrentPosition { get; private set; }
        public UIMessage DownPressed { get; } = new UIMessage();
        public UIMessage UpPressed { get; } = new UIMessage();
        public RectTransform Stick; 
        
        public bool Pressed { get; set; }
        public bool Dragged { get; set; }
		
        public void OnPointerDown(PointerEventData eventData)
        {
            DownPressed.Set();
            Pressed = true; 
            StartPosition = eventData.position;
            CurrentPosition = eventData.position;
            if(ShotSound != null)
                ShotSound.enabled = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            CurrentPosition = eventData.position;
            Dragged = true; 
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            CurrentPosition = eventData.position;
            UpPressed.Set();
            Pressed = false;
            Dragged = false;
            if(ShotSound != null)
                ShotSound.enabled = true;
            Stick.anchoredPosition = Vector2.zero;
        }        
        
        public void UpdateMoving(out Vector2 moving, out float speed)
        {
            if (Pressed)
            {
                var deltaPos = CurrentPosition - StartPosition;
                var tilt = deltaPos.magnitude;
                speed = tilt < MinRadius ? 0f : Mathf.Clamp((tilt - MinRadius) / (MaxRadius - MinRadius), 0.4f, 1f); 
                moving = deltaPos;
                Stick.anchoredPosition = deltaPos.normalized * Mathf.Clamp(deltaPos.magnitude, 0f, 50f);
            }
            else
            {
                moving = Vector2.zero;
                Stick.anchoredPosition = Vector2.zero;
                speed = 0f; 
            }
        }
        
        public void UpdateRotation(out Vector2 direction)
        {
            direction = CurrentPosition - StartPosition;
            
            if (Dragged)
            {
                Stick.anchoredPosition = direction.normalized * Mathf.Clamp(direction.magnitude, 0f, 50f);
            }
            else
            {
                Stick.anchoredPosition = Vector2.zero;
            }
        }
    }
}
