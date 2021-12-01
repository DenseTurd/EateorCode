using System;
using UnityEngine;

public class DailiesManager : MonoBehaviour
{
    DateTime LastPlayDate;
    DailyGenerator dailyGenerator;
    BaseDaily daily;
    void Start()
    {
        DateTime.TryParse(PlayerPrefs.GetString("LastPlayDate"), out LastPlayDate);

        if (LastPlayDate != DateTime.Today)
        {
            Debug.Log("New day, reset dailies");
            LastPlayDate = DateTime.Today;
            PlayerPrefs.SetString("LastPlayDate", LastPlayDate.ToString());
            CreateDaily();
        }
        else
        {
            Debug.Log("It's still today dont reset the dailies");
        }
    }

    void CreateDaily()
    {
        dailyGenerator = new DailyGenerator();
        daily = dailyGenerator.Create();
    }
}
