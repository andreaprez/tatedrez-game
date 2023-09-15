using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Serializable]
    public enum PlayerId
    {
        None,
        Player1,
        Player2
    }
    
    public static GameManager Instance = null;
        
    [SerializeField] private BoardView boardView;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        LoadGame();
    }

    public void LoadGame()
    {
        InputHandler.ClearEvents();
            
        var boardModel = new BoardModel();
        new BoardMediator(boardModel, boardView);
    }
}
