using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueTextWriter : MonoBehaviour
{
    private char[] currentDialogue;
    private int currentLetterIndex;

    private bool writing;
    private float lastWrite;

    private TextMeshProUGUI textRef;

    [Tooltip("How many seconds per letter")]
    public float writeSpeed;

    private void Start()
    {
        textRef = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (writing)
        {
            WriteNext();
        }
    }

    void WriteNext()
    {
        float timeSinceLastWrite = Time.time - lastWrite;
        float lettersSinceLastWrite = timeSinceLastWrite / writeSpeed;

        int letterCount = Mathf.FloorToInt(lettersSinceLastWrite);
        if (letterCount <= 0)
        {
            return;
        }

        float extraTime = 0;
        string newText = string.Empty;
        for (int i = currentLetterIndex; i < Mathf.Min(currentLetterIndex + letterCount, currentDialogue.Length); i++)
        {
            newText += currentDialogue[i];
            
            if ((currentDialogue[i] == '.' || currentDialogue[i] == '!' || currentDialogue[i] == '?') && 
                i < currentDialogue.Length - 1 &&
                currentDialogue[i + 1] == ' ')
            {
                extraTime += Mathf.Max(writeSpeed * 4, 0.25f);
            }
        }

        currentLetterIndex += letterCount;
        textRef.text += newText;

        if (currentLetterIndex >= currentDialogue.Length)
        {
            writing = false;
            return;
        }

        lastWrite = Time.time + extraTime;
    }

    public void WriteDialogue(string dialogue)
    {
        writing = true;

        currentDialogue = dialogue.ToCharArray();
        currentLetterIndex = 0;

        textRef.text = string.Empty;
        lastWrite = Time.time;
    }
}
