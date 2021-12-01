using UnityEngine;

public static class Extensions
{
    public static void PlayerPrefsIncrementInt(string str)
    {
        PlayerPrefs.SetInt(str, PlayerPrefs.GetInt(str) + 1);
    }

    public static void PlayerPrefsSetBool(string str, bool val)
    {
            PlayerPrefs.SetInt(str, val ? 1 : 0);
    }

    public static bool PlayerPrefsGetBool(string str)
    {
        return PlayerPrefs.GetInt(str) == 1 ? true : false;
    }

    public static Vector3 With(this Vector3 original, float? x = null, float? y = null, float? z = null)
    {
        return new Vector3(x ?? original.x, y ?? original.y, z ?? original.z);
    }
}
