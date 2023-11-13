using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocationSelect : MonoBehaviour
{
    private string sName;

    [SerializeField]
    private LocationData[] locations;

    void Start()
    {
        bool everyLocationVisited = true;
        foreach (LocationData l in locations)
        {
            if (GameData.HasBeenVisited(l.name))
            {
                l.locationButton.SetActive(false);
            }
            else
            {
                everyLocationVisited = false;
            }
        }

        if (everyLocationVisited)
        {
            SceneManager.LoadScene("LevelOrchid");
        }
    }

    public void LoadScene(string sceneName)
    {
        UIFade fader = FindObjectOfType<UIFade>();
        if (fader != null)
        {
            fader.FadeIn();
        }

        sName = sceneName;
        Invoke(nameof(LoadSceneAfterWait), 1);
    }

    private void LoadSceneAfterWait()
    {
        SceneManager.LoadScene(sName);
    }

    [Serializable]
    private struct LocationData
    {
        public GameObject locationButton;
        public string name;
    }
}
