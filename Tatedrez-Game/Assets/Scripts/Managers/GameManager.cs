using Tatedrez.Board;
using Tatedrez.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Tatedrez.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private BoardView boardView;
        [SerializeField] private Button restartButton;

        private void Start()
        {
            restartButton.onClick.AddListener(() =>
            {
                AudioManager.PlaySound(AudioManager.Sound.RestartButton);

                ReloadGame();
            });
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

        private void ReloadGame()
        {
            boardView.Reset();
            LoadGame();
        }
    }
}