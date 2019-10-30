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
        private Vector3 _randomRotation;
        private bool _moveToEnemy;
        
        public BotBehaviourSystem(GameSettings settings)
        {
            _gameSettings = settings;
            _randomRotation = new Vector3(0f, Random.Range(-_gameSettings.BotRandomRotation, _gameSettings.BotRandomRotation),  0f); 
        }
        
        public override void Execute()
        {
            for (int i = 0; i < World.Players.Count; i++)
            {
                var bot = World.Players.EntityAt(i); 
                
                if(bot == World.ClientEntity)
                    continue;

                bot.Input = new Input();
                ChooseTargetPosition(bot); 
                Shoot(bot);
                Run(bot); 
            }
        }

        private void ChooseTargetPosition(Entity bot)
        {
            var capsuleEntity = World.Capsules.EntityAt(0); 
            var capsule = capsuleEntity.Capsule;
            var capsuleInventory = capsuleEntity.Inventory.Keys.Select(x => x.Key.KeyColor);
            var botInventory = bot.Inventory.Keys.Select(x => x.Key.KeyColor); 
            
            var anyOneNeed = false;

            for (int i = 0; i < capsule.RequiredKeys.Count; i++)
            {
                var key = capsule.RequiredKeys[i];
                
                if(capsuleInventory.Contains(key))
                    continue;
                
                if (!botInventory.Contains(key))
                {
                    anyOneNeed = true;
                    break;
                }
            }

            if(anyOneNeed)
            {
                if ((bot.Transform.Position - _currentTargetPosition).sqrMagnitude < 0.25f || _moveToEnemy || _currentTargetPosition == Vector3.zero)
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
            var path = new NavMeshPath();
            bool found = NavMesh.CalculatePath(bot.Transform.Position, _currentTargetPosition,
                NavMesh.AllAreas, path);

            if (found)
            {
                var botPath = path.corners[1];
                var direction = (bot.Transform.Position - botPath).normalized;
                var inputDirection = new Vector2(-direction.x, -direction.z);
                bot.Input.Movement = inputDirection;
                
                if (!bot.Input.Aimed)
                    bot.Input.Direction = inputDirection; 
                
                bot.Input.Speed = bot.Input.Movement.sqrMagnitude > 0.25f ? 0.3f : 0f; 
            }
        }

        private void Shoot(Entity bot)
        {
            var player = World.ClientEntity;
            var distance = player.Transform.Position - bot.Transform.Position; 
            if (distance.magnitude < _gameSettings.ProjectileRange / 1.5f)
            {
                _randomRotation = Vector3.Lerp(_randomRotation, _randomRotation + new Vector3(0f, Random.Range(-10f, 10f),  0f), 0.1f);
                var randomDirection = (Quaternion.Euler(_randomRotation) * distance); 
                var inputDirection = new Vector2(randomDirection.x, randomDirection.z);

                bot.Input.Direction = Vector2.Lerp(inputDirection, bot.Input.Direction, _gameSettings.BotRotationLerp);
                bot.Input.Aimed = true;
                bot.Input.Aim = true;
                if (bot.Weapon.WeaponState == WeaponState.Ready && bot.Weapon.RandomNextBotShootingTick < World.Tick)
                {
                    bot.Weapon.RandomNextBotShootingTick = World.Tick + Random.Range(30, 60); 
                    _randomRotation = new Vector3(0f, Random.Range(-_gameSettings.BotRandomRotation, _gameSettings.BotRandomRotation),  0f); 
                }
            }
            
            bot.Input.Attack = bot.Weapon.WeaponState == WeaponState.Ready && bot.Weapon.RandomNextBotShootingTick == World.Tick;
        }
    }
}
