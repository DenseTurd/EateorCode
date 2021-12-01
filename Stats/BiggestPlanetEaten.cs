using UnityEngine;

public class BiggestPlanetEaten : Stat
{
    public BiggestPlanetEaten()
    {
        Name = "Biggest planet eaten";
        StringValue = PlanetDictionary.planetDictionary[PlayerPrefs.GetInt(Name)];
        StatType = StatType.String;
    }
}
