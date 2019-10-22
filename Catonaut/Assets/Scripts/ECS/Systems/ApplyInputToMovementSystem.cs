using Client;
using UnityEngine;

namespace ECS.Systems
{
    public class ApplyInputToMovementSystem : SystemBase
    {
        private readonly GameSettings _gameSettings; 
    
        public ApplyInputToMovementSystem(GameSettings settings)
        {
            _gameSettings = settings;
        }
    
        public override void Execute()
        {
            for (int i = 0; i < World.Input.Count; i++)
            {
                var entity = World.Input.EntityAt(i);

                var input = entity.Input;
                var body = entity.Player.PlayerObject; 
                var transform = entity.Transform;

                if (input == null || transform == null || body == null) 
                    continue;

                var direction = new Vector3(input.Movement.x, 0f, input.Movement.y);
                var aimedDirection = new Vector3(input.Direction.x, 0f, input.Direction.y);
                var modificator = input.Aimed ? _gameSettings.AimSpeedModificator : 1f; 
                var deltaMove = modificator * input.Speed * _gameSettings.MaxSpeed * direction / TickRate; 
            
                if (input.Speed >= 0.1f || input.Aimed)
                {
                    body.transform.position = transform.Position;
                    body.CharacterController.Move(deltaMove);
                
                    transform.Position = body.transform.position;

                    transform.Rotation = Quaternion.Lerp(Quaternion.LookRotation(input.Aimed ? Quaternion.Euler(0f, 225f, 0f) * aimedDirection : direction,
                        Vector3.up), transform.Rotation, _gameSettings.CameraRotationLerp);
                }
                
                body.transform.position = transform.Position;
                body.transform.rotation = transform.Rotation; 
            }
        }
    }
}
