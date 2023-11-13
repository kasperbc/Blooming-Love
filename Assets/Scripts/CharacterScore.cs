using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterScore : MonoBehaviour
{
    public string characterKey;
    public float debugScore = -1;
    public float maxScore;

    private TextMeshProUGUI tmpro;
    void Start()
    {
        tmpro = GetComponent<TextMeshProUGUI>();

        float score = 0;
        if (!GameData.scores.TryGetValue(characterKey, out score))
        {
            Debug.LogError($"Score not found for {characterKey}!");
        }
        
        if (debugScore != -1)
            score = debugScore;

        int roundedScore = Mathf.RoundToInt((score / maxScore) * 5);

        string scoreStr = string.Empty;
        for (int i = 0; i < roundedScore; i++)
        {
            scoreStr += "B";
        }

        tmpro.text = scoreStr;
    }
}
