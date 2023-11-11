using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    private static List<string> locationsVisited = new List<string>();

    public static void ClearData()
    {
        locationsVisited.Clear();
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
