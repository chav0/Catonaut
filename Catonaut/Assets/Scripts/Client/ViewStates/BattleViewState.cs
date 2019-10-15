using Client.Updaters;
using UnityEngine;

namespace Client.ViewStates
{
    public class BattleViewState : BaseViewState
    {
        private CameraUpdater _cameraUpdater;
        private PlayerUpdater _playerUpdater;
        private Vector2 _viewYAngle;

        public override void OnEnter()
        {
            _cameraUpdater = new CameraUpdater(Context.Camera, Context.AppModel.Settings);
            _playerUpdater = new PlayerUpdater();
        }
        
        public override void PreModelUpdate()
        {
            var hud = Context.Screens.BattleHud;
            
            bool hasStick = hud.LeftStickWidget.Pressed; 
            
            hud.LeftStickWidget.UpdateMoving(out var moveStick, out var speed);
            hud.RightStickWidget.UpdateRotation(out var rotation);
            
            _viewYAngle = rotation * Screen.dpi / Context.AppModel.Settings.CameraRotationSpeed;
            
            moveStick = FillVectorByKeys(moveStick, ref speed, ref hasStick);

            moveStick.Normalize();
			
            moveStick = GetRelativeMovementVector(moveStick);
            

            var input = new Input();
            input.Movement = moveStick;
            input.Speed = speed;
            Context.AppModel.AddGameInput(input); 
        }

        public override void PostModelUpdate()
        {
            _playerUpdater.Update(Context.AppModel.World);
            _cameraUpdater.Update(Context.AppModel.World, _viewYAngle);
        }
             
        private static Vector2 FillVectorByKeys(Vector2 moveStick, ref float speed, ref bool hasStick)
        {
            bool firstKey = false;

            if (UnityEngine.Input.GetKey("a"))
            {
                if (!firstKey)
                {
                    firstKey = true;
                    moveStick = Vector2.zero;
                }

                hasStick = true;
                moveStick += Vector2.left;
                speed = 1f; 
            }

            if (UnityEngine.Input.GetKey("d"))
            {
                if (!firstKey)
                {
                    firstKey = true;
                    moveStick = Vector2.zero;
                }

                hasStick = true;
                moveStick += Vector2.right;
                speed = 1f; 
            }

            if (UnityEngine.Input.GetKey("w"))
            {
                if (!firstKey)
                {
                    firstKey = true;
                    moveStick = Vector2.zero;
                }

                hasStick = true;
                moveStick += Vector2.up;
                speed = 1f; 
            }

            if (UnityEngine.Input.GetKey("s"))
            {
                if (!firstKey)
                {
                    firstKey = true;
                    moveStick = Vector2.zero;
                }

                hasStick = true;
                moveStick += Vector2.down;
                speed = 1f; 
            }

            return moveStick;
        }
        
        private Vector2 GetRelativeMovementVector(Vector2 movement)
        {
            var battleCameraTransform = Context.Camera.transform;
            var battleCameraTransformRight = battleCameraTransform.right;
            var battleCameraTransformForward = battleCameraTransform.forward;
            var moveX = (movement.x * battleCameraTransformRight.x) + (movement.y * battleCameraTransformForward.x);
            var moveZ = (movement.x * battleCameraTransformRight.z) + (movement.y * battleCameraTransformForward.z);
            return new Vector2(moveX, moveZ);
        }
    }
}
