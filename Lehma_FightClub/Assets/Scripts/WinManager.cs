using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class WinManager : MonoBehaviour
{
    public static WinManager Instance;

    [Header("UI Elementit")]
    public Image winImage;                    // Puolil‰pin‰kyv‰ taustakuva
    public TextMeshProUGUI player1WinText;    // Pelaajan 1 voittoteksti
    public TextMeshProUGUI player2WinText;    // Pelaajan 2 voittoteksti

    private bool isGameOver = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        // Piilotetaan kaikki voittoruudun elementit pelin alussa
        if (winImage != null) winImage.gameObject.SetActive(false);
        if (player1WinText != null) player1WinText.gameObject.SetActive(false);
        if (player2WinText != null) player2WinText.gameObject.SetActive(false);
    }

    public void TriggerWin(int winningPlayer)
    {
        if (isGameOver) return;
        isGameOver = true;

        Time.timeScale = 0f; // Pys‰ytet‰‰n peli

        // N‰ytet‰‰n taustakuva
        if (winImage != null)
            winImage.gameObject.SetActive(true);

        // N‰ytet‰‰n kumman pelaajan teksti tahansa voitti
        if (winningPlayer == 1 && player1WinText != null)
        {
            player1WinText.gameObject.SetActive(true);
        }
        else if (winningPlayer == 2 && player2WinText != null)
        {
            player2WinText.gameObject.SetActive(true);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}