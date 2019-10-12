using System;
using ECS.Components;

namespace ECS
{
    public class Entity : IComparable<Entity>, IEquatable<Entity>
    {
        private World _world; 
        public uint Id { get; set; }

        public Entity(uint id, World world)
        {
            Id = id;
            _world = world;
        }

        public static bool operator ==(Entity entity1, Entity entity2) => entity1.Id == entity2.Id;

        public static bool operator !=(Entity entity1, Entity entity2) => entity1.Id != entity2.Id;
		                     
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

        public Player AddPlayer()
        {
            var player = new Player();
            _world.Players.CreateAt(Id, player);
            return player; 
        }
    }
}
