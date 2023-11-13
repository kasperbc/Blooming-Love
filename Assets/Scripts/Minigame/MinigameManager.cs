using JSAM;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MinigameManager : MonoBehaviour
{
    public static MinigameManager instance;

    private int playerHealth;
    public int PlayerHealth
    {
        get => playerHealth;
        set
        {
            playerHealth = Mathf.Clamp(value, 0, playerMaxHealth);
        }
    }

    [SerializeField, Header("Player Health")]
    private int playerMaxHealth = 20;
    [SerializeField]
    public float playerIFrames = 1f;
    public float timeSinceLastDamage { get; private set; }

    [SerializeField, Header("Minigame")]
    private float minigameLength;
    private float currentMinigameTime;
    private bool minigameActive;

    // Refs
    private Image healthBar;
    private TextMeshProUGUI timer;
    private GameObject spawnerParent;
    private GameObject endText;

    private bool minigameEnding;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
    }

    void Start()
    {
        playerHealth = playerMaxHealth / 2;

        healthBar = GameObject.Find("HealthBar").GetComponent<Image>();
        timer = GameObject.Find("MinigameTimer").GetComponent<TextMeshProUGUI>();
        spawnerParent = transform.Find("Spawners").gameObject;
        endText = GameObject.Find("EndText");

        endText.SetActive(false);

        timeSinceLastDamage = playerIFrames;

        StartCoroutine(StartMinigame());
    }

    void Update()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, (float)PlayerHealth / playerMaxHealth, 0.015f);

        timeSinceLastDamage += Time.deltaTime;
        
        if (minigameActive)
        {
            currentMinigameTime += Time.deltaTime;
        }

        UpdateTimer();
        if (currentMinigameTime > minigameLength && !minigameEnding)
        {
            StartCoroutine(EndMinigame());
        }
    }

    private void UpdateTimer()
    {
        float timeLeft = minigameLength - currentMinigameTime;
        timeLeft = Mathf.Clamp(timeLeft, 0, minigameLength);

        timer.text = $"<size=48>Time:\n<size=62>{timeLeft:00.0}";
    }

    public void DamagePlayer()
    {
        if (timeSinceLastDamage < playerIFrames)
        {
            return;
        }

        PlayerHealth--;
        timeSinceLastDamage = 0;
        AudioManager.PlaySound(GameplayLibrarySounds.Hurt);
    }

    public void HealPlayer()
    {
        PlayerHealth++;
        AudioManager.PlaySound(GameplayLibrarySounds.Heart);
    }

    private IEnumerator StartMinigame()
    {
        TextMeshProUGUI startTimer = GameObject.Find("StartTimer").GetComponent<TextMeshProUGUI>();

        startTimer.text = "Starts in 3...";
        yield return new WaitForSeconds(1);

        startTimer.text = "Starts in 2...";
        yield return new WaitForSeconds(1);

        startTimer.text = "Starts in 1...";
        yield return new WaitForSeconds(1);

        startTimer.transform.parent.gameObject.SetActive(false);

        spawnerParent.SetActive(true);
        minigameActive = true;
    }

    private IEnumerator EndMinigame()
    {
        if (minigameEnding) yield break;

        spawnerParent.SetActive(false);
        minigameActive = false;
        minigameEnding = true;

        float score = (float)PlayerHealth / playerMaxHealth;

        endText.SetActive(true);
        endText.GetComponent<TextMeshProUGUI>().text = $"Your score: {score:P0}";
        //transform.Find("Minigame Music Player").gameObject.SetActive(false);

        yield return new WaitForSeconds(3);

        GameManager.Instance.OnMinigameEnd(score);
    }
}
