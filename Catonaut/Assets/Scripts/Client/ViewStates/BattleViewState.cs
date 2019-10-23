using Client.Updaters;
using UnityEngine;

namespace Client.ViewStates
{
    public class BattleViewState : BaseViewState
    {
        private CameraUpdater _cameraUpdater;
        private PlayerUpdater _playerUpdater;
        private FxUpdater _fxUpdater;
        private HealthBarUpdater _healthBarUpdater; 

        public override void OnEnter()
        {
            _cameraUpdater = new CameraUpdater(Context.Camera, Context.AppModel.Settings);
            _playerUpdater = new PlayerUpdater();
            _fxUpdater = new FxUpdater(Context.Resources);
            _healthBarUpdater = new HealthBarUpdater();
        }
        
        public override void PreModelUpdate()
        {
            var hud = Context.Screens.BattleHud;
            
            bool hasStick = hud.MovementStick.Pressed; 
            
            hud.MovementStick.UpdateMoving(out var moveStick, out var speed);
            hud.AttackStick.UpdateRotation(out var rotation);
            
            moveStick = FillVectorByKeys(moveStick, ref speed, ref hasStick);

            moveStick.Normalize();
			
            moveStick = GetRelativeMovementVector(moveStick);
            
            var input = new Input();
            input.Movement = moveStick;
            input.Speed = speed;
            input.Attack = hud.AttackStick.UpPressed.TryGet(); 
            input.Aimed = hud.AttackStick.Dragged;
            input.Aim = hud.AttackStick.DownPressed.TryGet();
            input.Direction = rotation; 
            Context.AppModel.AddGameInput(input); 
        }

        public override void PostModelUpdate()
        {
            _playerUpdater.Update(Context.AppModel.World);
            _cameraUpdater.Update(Context.AppModel.World);
            _fxUpdater.Update(Context.AppModel.World);
            _healthBarUpdater.Update(Context.AppModel.World);
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
