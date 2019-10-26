using System.Collections.Generic;
using System.Linq;
using Client;
using ECS.Components;
using UnityEngine;
using UnityEngine.AI;

namespace ECS.Systems
{
    public class BotBehaviourSystem : SystemBase
    {
        private readonly GameSettings _gameSettings;
        private Vector3 _currentTargetPosition;
        private bool _moveToEnemy; 
        
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

                ChooseTargetPosition(player); 
                Run(player); 
            }
        }

        private void ChooseTargetPosition(Entity bot)
        {
            var capsule = World.Capsules[0];
            var anyOneNeed = false;

            for (int i = 0; i < capsule.RequiredKeys.Count; i++)
            {
                var key = capsule.RequiredKeys[i];
                if (!bot.Inventory.Keys.Select(x => x.Key.KeyColor).Contains(key))
                {
                    anyOneNeed = true;
                    break;
                }
            }

            if(anyOneNeed)
            {
                if ((bot.Transform.Position - _currentTargetPosition).sqrMagnitude < 0.25f || _moveToEnemy)
                {
                    if (_moveToEnemy)
                    {
                        _currentTargetPosition = World.ClientEntity.Player.PlayerObject.transform.position; 
                    }
                    else
                    {
                        var keyEntity = FindRandomKey(bot, capsule);
                        _moveToEnemy = keyEntity.Key.HasOwner;
                        _currentTargetPosition = keyEntity.Key.Body.transform.position; 
                    }
                }
            }
            else
            {
                var position = capsule.Body.transform.position;
                _currentTargetPosition = new Vector3(position.x, 0f, position.z);
            }
        }

        private Entity FindRandomKey(Entity bot, Capsule capsule)
        {
            var haveNoKeys = new List<Entity>();
            for (int i = 0; i < World.Keys.Count; i++)
            {
                var key = World.Keys.EntityAt(i);
                if (!bot.Inventory.Keys.Select(x => x.Key.KeyColor).Contains(key.Key.KeyColor))
                {
                    haveNoKeys.Add(key);
                }
            }

            var randomKey = Random.Range(0, haveNoKeys.Count);
            return haveNoKeys[randomKey]; 
        }

        private void Run(Entity bot)
        {
            bot.Input = new Input();
            var path = new NavMeshPath();
            bool found = NavMesh.CalculatePath(bot.Transform.Position, _currentTargetPosition,
                NavMesh.AllAreas, path);

            if (found)
            {
                var botPath = path.corners[1];
                var direction = (bot.Transform.Position - botPath).normalized;
                var inputDirection = new Vector2(-direction.x, -direction.z);
                bot.Input.Movement = inputDirection;
                bot.Input.Direction = inputDirection; 
                bot.Input.Speed = bot.Input.Movement.sqrMagnitude > 0.25f ? 0.2f : 0f; 
            }
        }
    }
}
