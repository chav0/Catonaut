namespace ECS.Physics
{
    public static class Layers
    {
        public const int Geometry = 0;
        public const int Movement = 8;
        
        public const int GeometryMask = 1 << Geometry;
        public const int MovementMask = 1 << Movement;
        
    }
}