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
        public UIMessage DownPressed { get; } = new UIMessage();
        public UIMessage UpPressed { get; } = new UIMessage();

        public bool Pressed { get; set; }
		
        public void OnPointerDown(PointerEventData eventData)
        {
            DownPressed.Set();
            Pressed = true; 
            StartPosition = eventData.position;
            CurrentPosition = eventData.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            CurrentPosition = eventData.position;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            CurrentPosition = eventData.position;
            UpPressed.Set();
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
        }
        
        public void UpdateRotation(out Vector2 direction)
        {
            direction = CurrentPosition - StartPosition; 
        }
    }
}
