using System;
using Client.Objects;
using ECS.Components;
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
    }
}
