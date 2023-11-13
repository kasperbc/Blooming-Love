using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Ink.Runtime;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using JSAM;

public class DialogueManager : MonoBehaviour
{
    // Ink
    [SerializeField, Header("Ink JSON")] 
    private TextAsset inkJson;
    private Story story;
    private List<GameObject> choices = new List<GameObject>();
    private List<TextMeshProUGUI> choiceTexts = new List<TextMeshProUGUI>();

    [SerializeField, Header("End of story")]
    private UnityEvent endOfStoryBehaviour;

    // State
    public enum GameState
    {
        Normal,
        Writing,
        Minigame
    }
    private GameState currentState;
    public GameState CurrentState
    {
        get { return currentState; }
        set { currentState = value; }
    }

    // Refrences
    private GameObject canvas;
    private DialogueTextWriter dialogueText;
    private TextMeshProUGUI speakerText;
    private MusicPlayer musicPlayer;
    void Start()
    {
        // Find refrences
        canvas = GameObject.Find("Canvas");
        dialogueText = GameObject.Find("DialogueText").GetComponent<DialogueTextWriter>();
        speakerText = GameObject.Find("SpeakerText").GetComponent<TextMeshProUGUI>();
        musicPlayer = GameObject.Find("Music Player").GetComponent<MusicPlayer>();

        // Choice refrences
        Transform dialogueChoicesParent = GameObject.Find("DialogueChoices").transform;
        for (int i = 0; i < dialogueChoicesParent.childCount; i++)
        {
            Transform child = dialogueChoicesParent.GetChild(i);

            choices.Add(child.gameObject);
            choiceTexts.Add(child.Find("ChoiceText").GetComponent<TextMeshProUGUI>());
        }
        
        // Create Ink story
        story = new Story(inkJson.text);

        HideChoices();
        ContinueStory();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (currentState)
            {
                case GameState.Normal:
                    ContinueStory();
                    break;
                case GameState.Writing:
                    dialogueText.JumpDialogue();
                    break;
                case GameState.Minigame:
                    break;
            }
                
        }
    }

    public void ContinueStory()
    {
        if (story.currentChoices.Count > 0)
        {
            return;
        }

        if (story.canContinue)
        {
            if (story.currentTags.Count > 0)
            {
                HandleTags();
            }

            if (currentState != GameState.Minigame)
            {
                dialogueText.WriteDialogue(story.Continue());
            }

            if (story.currentChoices.Count > 0)
            {
                DisplayChoices();
            }
        }
        else
        {
            endOfStoryBehaviour.Invoke();
        }
    }

    void DisplayChoices()
    {
        List<Choice> currentChoices = story.currentChoices;
        
        if (currentChoices.Count > choices.Count)
        {
            Debug.LogError("Too many choices!");
            return;
        }

        for (int i = 0; i < currentChoices.Count; i++)
        {
            Choice c = currentChoices[i];

            int x = i;

            choices[i].SetActive(true);
            choices[i].GetComponent<Button>().onClick.AddListener(new UnityAction(() => MakeChoice(x)));
            choiceTexts[i].text = c.text.Replace("_", "");

            if (c.text[0] == '_')
            {
                choices[i].GetComponent<Image>().color = new Color(0.4811321f, 0.1883677f, 0.281041f);
            }
            else
            {
                choices[i].GetComponent<Image>().color = new Color(0.2117647f, 0.2117647f, 0.2117647f);
            }
        }
    }

    void HideChoices()
    {
        foreach (GameObject choice in choices) 
        {
            choice.SetActive(false); 
        }
    }
    void HandleTags()
    {
        foreach (string tag in story.currentTags)
        {
            string[] tagSplit = tag.Split(' ');

            switch (tagSplit[0].ToLower())
            {
                // Like / Dislike
                case "like":
                    GameManager.Instance.HeartPoints++;
                    break;
                case "dislike":
                    GameManager.Instance.HeartPoints--;
                    break;
                // Change sprite
                case "sprite":
                case "s":
                    GameManager.Instance.SetCharacterSprite(tagSplit[1]);
                    break;
                // Change background
                case "background":
                case "b":
                    GameManager.Instance.SetBackground(tagSplit[1]);
                    break;
                // Change speaker
                case "speaker":
                case "p":
                    speakerText.text = tagSplit[1].Replace('_', ' ');
                    break;
                // Load scene
                case "loadscene":
                    SceneManager.LoadScene(tagSplit[1]);
                    break;
                case "playmusic":
                    musicPlayer.Play();
                    break;
                case "minigame":
                    GameManager.Instance.PlayMinigame();
                    break;
            }
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        story.ChooseChoiceIndex(choiceIndex);

        HideChoices();
        ContinueStory();
    }

}
