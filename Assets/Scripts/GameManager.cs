using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private float heartPoints;

    [Header("Character sprites"), SerializeField]
    private List<SpriteNamePair> sprites = new List<SpriteNamePair>();
    private Dictionary<string, Sprite> spriteDict = new Dictionary<string, Sprite>();

    [Header("Backgrounds"), SerializeField]
    private List<SpriteNamePair> backgrounds = new List<SpriteNamePair>();
    private Dictionary<string, Sprite> backgroundDict = new Dictionary<string, Sprite>();

    [Header("Location"), SerializeField]
    private string locationName;

    // Refrences
    private Image characterImg;
    private Image backgroundImg;
    public float HeartPoints
    {
        get { return heartPoints; }
        set { heartPoints = value; }
    }

    void Start()
    {
        // Set singleton instance
        if (Instance != null)
        {
            Debug.LogWarning("Another GameManager found!");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Find refrences
        characterImg = GameObject.Find("Character").GetComponent<Image>();
        backgroundImg = GameObject.Find("Background").GetComponent<Image>();

        // Convert sprites to dict
        foreach (SpriteNamePair s in sprites)
        {
            spriteDict.Add(s.name, s.sprite);
        }

        foreach (SpriteNamePair b in backgrounds)
        {
            backgroundDict.Add(b.name, b.sprite);
        }

        // Set default sprite/background
        SetCharacterSprite(sprites[0].name);
        SetBackground(backgrounds[0].name);
    }

    public void SetCharacterSprite(string spriteName)
    {
        if (spriteDict.TryGetValue(spriteName, out Sprite result) == false)
        {
            Debug.LogError($"Sprite {spriteName} could not be found!");
            return;
        }

        characterImg.sprite = result;
    }

    public void SetBackground(string backgroundName)
    {
        if (backgroundDict.TryGetValue(backgroundName, out Sprite result) == false)
        {
            Debug.LogError($"Background {backgroundName} could not be found!");
            return;
        }

        backgroundImg.sprite = result;
    }

    private string sName;
    public void LoadScene(string sceneName)
    {
        UIFade fader = FindObjectOfType<UIFade>();
        if (fader != null)
        {
            fader.FadeIn();
        }
        GameData.OnLocationVisited(locationName);

        sName = sceneName;
        Invoke(nameof(LoadSceneAfterWait), 1);
    }

    private void LoadSceneAfterWait()
    {
        SceneManager.LoadScene(sName);
    }

    [Serializable]
    public struct SpriteNamePair
    {
        public string name;
        public Sprite sprite;
    }
}
