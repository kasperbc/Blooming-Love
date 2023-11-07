using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Ink.Runtime;
using UnityEngine.UI;
using UnityEngine.Events;
using Unity.Mathematics;

public class DialogueManager : MonoBehaviour
{
    // Ink
    [SerializeField, Header("Ink JSON")] 
    private TextAsset inkJson;
    private Story story;
    private List<GameObject> choices = new List<GameObject>();
    private List<TextMeshProUGUI> choiceTexts = new List<TextMeshProUGUI>();

    // State
    public enum GameState
    {
        Normal,
        Choice
    }
    private GameState currentState;

    // Refrences
    private GameObject canvas;
    private TextMeshProUGUI dialogueText;
    void Start()
    {
        // Find refrences
        canvas = GameObject.Find("Canvas");
        dialogueText = GameObject.Find("DialogueText").GetComponent<TextMeshProUGUI>();

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
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentState == GameState.Normal)
        {
            ContinueStory();
        }
    }

    void ContinueStory()
    {
        if (story.canContinue)
        {
            dialogueText.text = story.Continue();

            if (story.currentChoices.Count > 0)
            {
                DisplayChoices();
                currentState = GameState.Choice;
            }
        }
        else
        {
            dialogueText.text = "<i>End of story";
            print("No more story :(");
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
            choiceTexts[i].text = c.text;
        }
    }

    void HideChoices()
    {
        foreach (GameObject choice in choices) 
        {
            choice.SetActive(false); 
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        story.ChooseChoiceIndex(choiceIndex);

        currentState = GameState.Normal;

        HideChoices();
        ContinueStory();
    }
}
