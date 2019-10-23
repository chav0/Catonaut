using Client;
using UnityEngine;
using UnityEngine.AI;

namespace ECS.Systems
{
    public class BotBehaviourSystem : SystemBase
    {
        private readonly GameSettings _gameSettings; 
        public BotBehaviourSystem(GameSettings settings)
        {
            _gameSettings = settings;
        }
        
        public override void Execute()
        {
            for (int i = 0; i < World.Players.Count; i++)
            {
                var player = World.Players.EntityAt(i); 
                
                if(player == World.ClientEntity)
                    continue;

                Run(player); 
            }
        }

        private void Run(Entity bot)
        {
            bot.Input = new Input();
            var path = new NavMeshPath();
            bool found = NavMesh.CalculatePath(bot.Transform.Position, World.Keys[2].Body.transform.position,
                NavMesh.AllAreas, path);

            if (found)
            {
                var botPath = path.corners[1];
                var direction = (bot.Transform.Position - botPath).normalized; 
                Debug.Log(direction);
                var inputDirection = new Vector2(-direction.x, -direction.z);
                bot.Input.Movement = inputDirection;
                bot.Input.Direction = inputDirection; 
                bot.Input.Speed = bot.Input.Movement.magnitude > 0f ? 1f : 0f; 
            }
        }
    }
}
