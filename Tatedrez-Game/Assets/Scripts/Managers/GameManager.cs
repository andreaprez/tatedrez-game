using Tatedrez.Board;
using Tatedrez.Utils;
using UnityEngine;

namespace Tatedrez
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private BoardView boardView;

        private void Start()
        {
            LoadGame();
        }

        private void LoadGame()
        {
            InputHandler.Instance.SetInputBlocked(false);
            InputHandler.Instance.ClearEvents();

            var boardModel = new BoardModel();
            var boardMediator = new BoardMediator(boardModel, boardView);
            boardMediator.Init();
        }

        public void ReloadGame()
        {
            boardView.Reset();
            LoadGame();
        }
    }
}