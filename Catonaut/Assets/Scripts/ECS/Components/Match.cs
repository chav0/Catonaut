namespace ECS.Components
{
    public class Match : Component
    {
        public int NextStateTick;
        public MatchState State;
        public MatchResult Result; 
    }

    public enum MatchState : byte
    {
        InProgress,
        Complete
    }
    
    public enum MatchResult : byte
    {
        None,
        Victory,
        Defeat
    }
}
