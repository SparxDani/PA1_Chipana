using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UImanager : MonoBehaviour
{
    [Header("Health UI")]
    public Image healthBar;
    public int maxHealth = 100;

    [Header("Points UI")]
    public TMP_Text pointsText;
    private int currentPoints;

    [Header("Results Screen UI")]
    public GameObject resultsScreen;
    public TMP_Text resultText;
    public GameObject retryButton;
    public GameObject mainMenuButton;

    private void OnEnable()
    {
        PlayerController.OnHealthUpdated += UpdateHealthBar;
        PlayerController.OnPointsUpdated += UpdatePointsUI;
        PlayerController.OnPlayerWon += ShowVictoryScreen;
        PlayerController.OnPlayerDefeated += ShowDefeatScreen;
    }

    private void OnDisable()
    {
        PlayerController.OnHealthUpdated -= UpdateHealthBar;
        PlayerController.OnPointsUpdated -= UpdatePointsUI;
        PlayerController.OnPlayerWon -= ShowVictoryScreen;
        PlayerController.OnPlayerDefeated -= ShowDefeatScreen;
    }

    void Start()
    {
        resultsScreen.SetActive(false);
        currentPoints = 0;
        UpdatePointsUI(currentPoints);
    }

    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        healthBar.fillAmount = (float)currentHealth / maxHealth;
    }

    public void UpdatePointsUI(int points)
    {
        currentPoints = points;
        pointsText.text = "Points: " + currentPoints.ToString();
    }

    private void ShowVictoryScreen()
    {
        ShowResultsScreen("LEVEL CLEAR");
        retryButton.SetActive(true);
        mainMenuButton.SetActive(true);
    }

    private void ShowDefeatScreen()
    {
        ShowResultsScreen("BAD");
        retryButton.SetActive(true);
        mainMenuButton.SetActive(true);
    }

    private void ShowResultsScreen(string result)
    {
        resultsScreen.SetActive(true);
        resultText.text = result;
    }

}
