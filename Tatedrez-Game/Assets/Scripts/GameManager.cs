using Tatedrez.Board;
using Tatedrez.Utils;
using UnityEngine;

namespace Tatedrez
{
    public class GameManager : MonoBehaviour
    {
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

        private void LoadGame()
        {
            InputHandler.Instance.SetInputBlocked(false);
            InputHandler.Instance.ClearEvents();

            var boardModel = new BoardModel();
            new BoardMediator(boardModel, boardView);
        }

        public void ReloadGame()
        {
            boardView.Reset();
            LoadGame();
        }
    }
}