namespace ECS.Components
{
    public class Match : Component
    {
        public bool ItsVictory;
        public int NextStateTick;
        public MatchState State;
    }

    public enum MatchState : byte
    {
        InProgress,
        Complete
    }
}
