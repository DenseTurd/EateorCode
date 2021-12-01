public enum GoalType
{
    Collect,
    TimedCollect,
    Avoid
}

public enum CollectibleType
{
    Rock,
    Powerup,
    HungerBGone,
    Slowmo,
    Gravity,
    CosmicBurger,
    Danger,
    Meteor,
    Splooge,
    Bonk
}

public class BaseDaily
{
    public GoalType Type { get; set; }
    public CollectibleType Collectible { get; set; }
    public int RequiredValue { get; set; }
    public int CurrenValue { get; set; }
    public float TimeLimit { get; set; }
    public float CurrentTime { get; set; }
}


