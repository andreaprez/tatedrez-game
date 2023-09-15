using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance = null;

    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private Button gameScreenRestartButton;

    private void Awake()
    {
        Instance = this;
    }

    public void GameOver(GameManager.PlayerId winner)
    {
        var winnerName = "";
        switch (winner)
        {
            case GameManager.PlayerId.Player1:
                winnerName = "Player 1";
                break;
            case GameManager.PlayerId.Player2:
                winnerName = "Player 2";
                break;
        }
        gameOverText.text = winnerName + " wins!";

        StartCoroutine(ShowGameOver());
    }

    public void RestartGame()
    {
        gameOverScreen.SetActive(false);
        GameManager.Instance.ReloadGame();
    }
    
    private IEnumerator ShowGameOver()
    {
        gameScreenRestartButton.interactable = false;
        
        yield return new WaitForSeconds(1f);
        
        gameScreenRestartButton.interactable = true;
        gameOverScreen.SetActive(true);
    }
}
