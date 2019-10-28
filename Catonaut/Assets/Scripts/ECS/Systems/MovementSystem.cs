using Client;
using UnityEngine;

namespace ECS.Systems
{
    public class MovementSystem : SystemBase
    {
        private readonly GameSettings _gameSettings; 
    
        public MovementSystem(GameSettings settings)
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

                    transform.Position += deltaMove;
                    if (body.TryDepenetrate(transform.Position, out var delta))
                    {
                        transform.Position += delta;
                    }
                    
                    transform.Position = new Vector3(transform.Position.x, 0f, transform.Position.z);

                    transform.Rotation = Quaternion.Lerp(Quaternion.LookRotation(input.Aimed ? aimedDirection : direction,
                        Vector3.up), transform.Rotation, entity == World.ClientEntity ? _gameSettings.PlayerRotationLerp : _gameSettings.BotRotationLerp);
                }

                body.transform.SetPositionAndRotation(transform.Position, transform.Rotation); 
            }
        }
    }
}