using System.Collections;
using System.Collections.Generic;
using Client;
using ECS;
using UnityEngine;

public class ApplyInputToMovementSystem : SystemBase
{
    private readonly GameSettings _gameSettings; 
    
    public ApplyInputToMovementSystem(GameSettings settings)
    {
        _gameSettings = settings;
    }
    
    public override void Simulate()
    {
        for (int i = 0; i < World.Input.Count; i++)
        {
            var entity = World.Input.EntityAt(i);

            var input = entity.Input;
            var body = entity.Player.PlayerObject; 
            var transform = entity.Transform;

            if (input == null || transform == null || body == null) 
                continue;
            
            if (input.Movement.magnitude >= 0.1f)
            {
                transform.Position += _gameSettings.MaxSpeed * new Vector3(input.Movement.x, 0f, input.Movement.y) / TickRate;
            }
                
            body.transform.position = transform.Position;
            body.transform.eulerAngles = transform.Rotation; 

            transform.Speed = input.Movement.magnitude * _gameSettings.MaxSpeed;
        }
    }
}
