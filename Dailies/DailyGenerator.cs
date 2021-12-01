using UnityEngine;
using System.Collections.Generic;
public class DailyGenerator 
{
    Dictionary<int, GoalType> goalDict = new Dictionary<int, GoalType>
    {
        { 0, GoalType.Collect },
        { 1, GoalType.TimedCollect },
        { 2, GoalType.Avoid }
    };

    Dictionary<int, CollectibleType> collectibleDict = new Dictionary<int, CollectibleType>
    {
        { 0, CollectibleType.Rock },
        { 1, CollectibleType.Powerup },
        { 2, CollectibleType.HungerBGone },
        { 3, CollectibleType.Slowmo },
        { 4, CollectibleType.Gravity },
        { 5, CollectibleType.CosmicBurger },
        { 6, CollectibleType.Danger },
        { 7, CollectibleType.Meteor },
        { 8, CollectibleType.Splooge },
        { 9, CollectibleType.Bonk }
    };

    BaseDaily daily;

    public BaseDaily Create()
    {
        daily = new BaseDaily
        {
            Type = goalDict[Random.Range(0, 3)],
            Collectible = collectibleDict[Random.Range(0, 10)],
            RequiredValue = Random.Range(1, 100),
            CurrenValue = 0,            
            TimeLimit = Random.Range(30, 120),
            CurrentTime = 0
        };


        string logString = "Daily type: " + daily.Type.ToString();
        logString += "\nCollectible: " + daily.Collectible.ToString();
        logString += "\nRequiredValue: " + daily.RequiredValue.ToString();
        if(daily.Type == GoalType.TimedCollect)
        {
            logString += "\nTimeLimit: " + daily.TimeLimit.ToString();
        }
        Debug.Log(logString);
        return daily;
    }
}
