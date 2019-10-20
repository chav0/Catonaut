using System;
using Client.Objects;
using ECS.Components;
using JetBrains.Annotations;
using UnityEngine;
using Transform = ECS.Components.Transform;

namespace ECS
{
    public class Entity : IComparable<Entity>, IEquatable<Entity>
    {
        private World _world; 
        public uint Id { get; }

        public Entity(uint id, World world)
        {
            Id = id;
            _world = world;
        }

        public static bool operator ==(Entity entity1, Entity entity2) => entity1.Id == entity2.Id;

        public static bool operator !=(Entity entity1, Entity entity2) => entity1.Id != entity2.Id;

        public override int GetHashCode()
        {
            return (int) Id;
        }

        public int CompareTo(Entity other)
        {
            return (int) (Id - other.Id);
        }
        
        public bool Equals(Entity other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            return obj is Entity other && Equals(other);
        }

        public Player Player;
        public Transform Transform;
        public Input Input;
        public Flashlight Flashlight;
        public Inventory Inventory;

        public Key Key;
        public Capsule Capsule; 

        public Player AddPlayer()
        {
            var player = new Player();
            Player = player; 
            _world.Players.CreateAt(this, player);
            return player; 
        }
        
        public Transform AddTransform()
        {
            var transform = new Transform();
            Transform = transform; 
            _world.Transrofms.CreateAt(this, transform);
            return transform; 
        }
        
        public Input AddInput()
        {
            var input = new Input();
            Input = input; 
            _world.Input.CreateAt(this, input);
            return input; 
        }
        
        public Flashlight AddFlashlight()
        {
            var flashlight = new Flashlight();
            Flashlight = flashlight; 
            _world.Flashlights.CreateAt(this, flashlight);
            return flashlight; 
        }
        
        public Inventory AddInventory()
        {
            var inventory = new Inventory();
            Inventory = inventory; 
            _world.Inventories.CreateAt(this, inventory);
            return inventory; 
        }
        
        public Key AddKey()
        {
            var key = new Key();
            Key = key; 
            _world.Keys.CreateAt(this, key);
            return key; 
        }
        
        public Capsule AddCapsule()
        {
            var capsule = new Capsule();
            Capsule = capsule; 
            _world.Capsules.CreateAt(this, capsule);
            return capsule; 
        }
    }
}
