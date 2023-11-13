using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    private static List<string> locationsVisited = new List<string>();

    public static Dictionary<string, float> scores = new Dictionary<string, float>();
    public static void ClearData()
    {
        locationsVisited.Clear();
        scores.Clear();
    }

    public static void OnLocationVisited(string location)
    {
        locationsVisited.Add(location);
    }

    public static bool HasBeenVisited(string location)
    {
        return locationsVisited.Contains(location);
    }
}
