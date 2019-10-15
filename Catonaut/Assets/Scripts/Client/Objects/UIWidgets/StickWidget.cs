using UnityEngine;
using UnityEngine.EventSystems;

namespace Client.Objects.UIWidgets
{
    public class StickWidget : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        public float MinRadius;
        public float MaxRadius; 
        public Vector2 StartPosition { get; private set; }
        public Vector2 CurrentPosition { get; private set; }
        public bool Pressed { get; private set; }
        public Vector2 LastDelta { get; private set; }
		
        public void OnPointerDown(PointerEventData eventData)
        {
            Pressed = true;
            StartPosition = eventData.position;
            CurrentPosition = eventData.position;
            LastDelta = Vector2.zero;
        }

        public void OnDrag(PointerEventData eventData)
        {
            CurrentPosition = eventData.position;
            LastDelta = eventData.delta;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            CurrentPosition = eventData.position;
            LastDelta = Vector2.zero;
            Pressed = false;
        }
        
        public void UpdateMoving(out Vector2 moving, out float speed)
        {
            if (Pressed)
            {
                var deltaPos = CurrentPosition - StartPosition;
                var tilt = deltaPos.magnitude;
                speed = tilt < MinRadius ? 0f : Mathf.Clamp((tilt - MinRadius) / (MaxRadius - MinRadius), 0.4f, 1f); 
                moving = deltaPos;
            }
            else
            {
                moving = Vector2.zero;
                speed = 0f; 
            }
            
            LastDelta = Vector2.zero;
        }
        
        public void UpdateRotation(out Vector2 direction)
        {
            direction = LastDelta; 
            LastDelta = Vector2.zero;
        }
    }
}
