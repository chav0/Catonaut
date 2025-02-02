﻿using System.Linq;
using Client;
using ECS.Components;
using ECS.Physics;
using UnityEngine;

namespace ECS.Systems
{
    public class InsertingKeysInCapsuleSystem : SystemBase
    {
        private readonly GameSettings _gameSettings; 
    
        public InsertingKeysInCapsuleSystem(GameSettings settings)
        {
            _gameSettings = settings;
        }
    
        public override void Execute()
        {
            for (int i = 0; i < World.Capsules.Count; i++)
            {
                var entity = World.Capsules.EntityAt(i);
                var capsule = entity.Capsule;
                var capsuleBody = capsule.Body;
                var capsuleInventory = entity.Inventory; 
                
                var overlaps = PhysicsUtils.OverlapSphere(capsuleBody.transform.position, _gameSettings.CapsuleRadius, 
                    Layers.MovementMask);

                foreach (var entityOverlap in overlaps)
                {
                    var player = entityOverlap.Player;
                    var inventory = entityOverlap.Inventory; 
                        
                    if (player == null || inventory == null) 
                        continue;
                        
                    if(inventory.Keys.Count == 0)
                        continue;

                    MergeInventories(inventory, capsuleInventory);
                    SetKeysCompleted(capsule, capsuleInventory); 
                    CheckForVictory(capsule, capsuleInventory, entityOverlap);
                }
            }
        }

        private void MergeInventories(Inventory input, Inventory main)
        {
            main.Keys.AddRange(input.Keys);
            input.Keys.Clear();
        }

        private void SetKeysCompleted(Capsule capsule, Inventory capsuleInventory)
        {
            foreach (var keyObject in capsule.Body.KeyObjects)
            {
                var keyCompleted = capsuleInventory.Keys.Any(entity => entity.Key?.KeyColor == keyObject.KeyColor);
                keyObject.IsCompleted = keyCompleted; 
            }
        }
        
        private void CheckForVictory(Capsule capsule, Inventory capsuleInventory, Entity player)
        {
            foreach (var requiredKey in capsule.RequiredKeys)
            {
                var hasKeyColor = capsuleInventory.Keys.Any(entity => entity.Key?.KeyColor == requiredKey);
                if (!hasKeyColor)
                    return;
            }

            World.Match.Result = player == World.ClientEntity ? MatchResult.Victory : MatchResult.Defeat;
            World.Match.State = MatchState.Complete; 
            World.Match.NextStateTick = -1;
        }
    }
}
