using JSAM;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MinigameManager : MonoBehaviour
{
    public static MinigameManager instance;

    private float playerHealth;
    public float PlayerHealth
    {
        get => playerHealth;
        set
        {
            playerHealth = Mathf.Clamp(value, 0, playerMaxHealth);
        }
    }

    [SerializeField, Header("Player Health")]
    private float playerMaxHealth = 10;
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
        playerHealth = playerMaxHealth;

        healthBar = GameObject.Find("HealthBar").GetComponent<Image>();
        timer = GameObject.Find("MinigameTimer").GetComponent<TextMeshProUGUI>();
        spawnerParent = transform.Find("Spawners").gameObject;

        timeSinceLastDamage = playerIFrames;

        Invoke(nameof(StartMinigame), 3);
    }

    void Update()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, PlayerHealth / playerMaxHealth, 0.015f);

        timeSinceLastDamage += Time.deltaTime;
        
        if (minigameActive)
        {
            currentMinigameTime += Time.deltaTime;
        }

        UpdateTimer();
        if (currentMinigameTime > minigameLength)
        {
            EndMinigame();
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
    }

    private void StartMinigame()
    {
        spawnerParent.SetActive(true);
        minigameActive = true;
    }

    private void EndMinigame()
    {
        spawnerParent.SetActive(false);
        minigameActive = false;
        GameManager.Instance.OnMinigameEnd();
    }
}
