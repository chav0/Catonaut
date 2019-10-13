using Client.Updaters;
using UnityEngine;

namespace Client.ViewStates
{
    public class BattleViewState : BaseViewState
    {
        private CameraUpdater _cameraUpdater;
        private PlayerUpdater _playerUpdater;

        public override void OnEnter()
        {
            _cameraUpdater = new CameraUpdater(Context.Camera, Context.AppModel.Settings);
            _playerUpdater = new PlayerUpdater();
        }
        
        public override void PreModelUpdate()
        {
            bool hasStick = false; 
            var moveStick = FillVectorByKeys(new Vector2(), ref hasStick);

            moveStick.Normalize();
			
            moveStick = GetRelativeMovementVector(moveStick);
            
            var input = new Input();
            input.Movement = moveStick;
            Context.AppModel.AddGameInput(input); 
        }

        public override void PostModelUpdate()
        {
            _playerUpdater.Update(Context.AppModel.World);
            _cameraUpdater.Update(Context.AppModel.World, 90f);
        }
        
        private static Vector2 FillVectorByKeys(Vector2 moveStick, ref bool hasStick)
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
