using System.Collections;
using System.Collections.Generic;
using Tatedrez.Board.Tatedrez.Board;
using Tatedrez.Libraries;
using Tatedrez.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tatedrez.Board
{
    public class BoardView : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        
        [Header("Board")]
        [SerializeField] private Tilemap tilemap;
        [SerializeField] private List<Piece> pieces;

        [Header("Players Layout")]
        [SerializeField] private TextMeshProUGUI turnPlayer1Text;
        [SerializeField] private TextMeshProUGUI turnPlayer2Text;
        [SerializeField] private TextMeshProUGUI scoresText;
        
        [Header("Game Layout")]
        [SerializeField] private TextMeshProUGUI noMovesAvailableText;

        [Header("GameOver Layout")]
        [SerializeField] private GameObject winnerPlayer1Layout;
        [SerializeField] private GameObject winnerPlayer2Layout;
        [SerializeField] private TextMeshProUGUI winnerPlayer1Text;
        [SerializeField] private TextMeshProUGUI winnerPlayer2Text;

        [Header("Restart Button")]
        [SerializeField] private TextMeshProUGUI restartButtonText;
        
        private readonly int tileSize = 216;
        
        public List<Piece> Pieces => pieces;

        public void Setup(Vector2Int boardSize, PlayerId activePlayer, Cell[,] cells, Score scores)
        {
            ClearBoardTiles();

            var boardPosX = boardSize.x * tileSize / -2;
            var boardPosY = boardSize.y * tileSize / -2;
            tilemap.gameObject.transform.localPosition = new Vector3(boardPosX, boardPosY, 0);

            foreach (var cell in cells)
            {
                DrawCell(cell.Position);
            }
            
            SetTexts(scores);

            UpdatePlayerTurn(activePlayer);
        }

        public void Reset()
        {
            winnerPlayer1Layout.gameObject.SetActive(false);
            winnerPlayer2Layout.gameObject.SetActive(false);

            foreach (var piece in pieces)
            {
                piece.Reset();
            }
        }

        private void DrawCell(Vector2Int position)
        {
            var tile = LibrariesHandler.GetBoardLibrary().WhiteTile;

            if ((position.x % 2 == 0 && position.y % 2 == 0) || (position.x % 2 > 0 && position.y % 2 > 0))
            {
                tile = LibrariesHandler.GetBoardLibrary().BlackTile;
            }

            tilemap.SetTile((Vector3Int)position, tile);
        }

        public Piece ScreenToPiece(Vector3 position)
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(position), Vector2.zero);
            if (hitInfo)
            {
                hitInfo.transform.gameObject.TryGetComponent(out Piece piece);
                return piece;
            }

            return null;
        }

        public Vector2Int ScreenToCell(Vector3 position)
        {
            var worldPosition = mainCamera.ScreenToWorldPoint(position);
            var cellPosition = tilemap.WorldToCell(worldPosition);
            return (Vector2Int)cellPosition;
        }

        public void Move(Piece piece, Vector2Int position)
        {
            Vector3Int vector = new Vector3Int(position.x, position.y);
            Vector3 newPosition = tilemap.CellToWorld(vector);
            piece.Move(newPosition);
        }

        public void UpdatePlayerTurn(PlayerId newActivePlayer)
        {
            turnPlayer1Text.gameObject.SetActive(false);
            turnPlayer2Text.gameObject.SetActive(false);

            switch (newActivePlayer)
            {
                case PlayerId.Player1:
                    turnPlayer1Text.gameObject.SetActive(true);
                    break;
                case PlayerId.Player2:
                    turnPlayer2Text.gameObject.SetActive(true);
                    break;
            }
        }

        public void NoMovesAvailable(PlayerId newActivePlayer)
        {
            StartCoroutine(ShowNoMovesAvailable(newActivePlayer));
        }
        
        public void GameOver(PlayerId winner, Score scores)
        {
            SetScores(scores);

            turnPlayer1Text.gameObject.SetActive(false);
            turnPlayer2Text.gameObject.SetActive(false);

            switch (winner)
            {
                case PlayerId.Player1:
                    winnerPlayer1Layout.gameObject.SetActive(true);
                    break;
                case PlayerId.Player2:
                    winnerPlayer2Layout.gameObject.SetActive(true);
                    break;
            }
        }

        private void SetTexts(Score scores)
        {
            SetScores(scores);
            
            winnerPlayer1Text.text = LibrariesHandler.GetTextsLibrary().Winner.Replace("%", "1");
            winnerPlayer2Text.text = LibrariesHandler.GetTextsLibrary().Winner.Replace("%", "2");
            
            turnPlayer1Text.text = LibrariesHandler.GetTextsLibrary().PlayerTurn.Replace("%", "1");
            turnPlayer2Text.text = LibrariesHandler.GetTextsLibrary().PlayerTurn.Replace("%", "2");

            noMovesAvailableText.text = LibrariesHandler.GetTextsLibrary().NoMovesAvailable;

            restartButtonText.text = LibrariesHandler.GetTextsLibrary().RestartButton;
        }

        private void SetScores(Score scores)
        {
            var score1 = scores.Player1.ToString();
            var score2 = scores.Player2.ToString();
            scoresText.text = LibrariesHandler.GetTextsLibrary().Scores.Replace("%1", score1).Replace("%2", score2);
        }
        
        private void ClearBoardTiles()
        {
            if (tilemap.size.x > 0 || tilemap.size.y > 0)
            {
                tilemap.ClearAllTiles();
            }
        }
        
        private IEnumerator ShowNoMovesAvailable(PlayerId newActivePlayer)
        {
            yield return new WaitForSeconds(1f);

            noMovesAvailableText.gameObject.SetActive(true);
            
            yield return new WaitForSeconds(2f);

            noMovesAvailableText.gameObject.SetActive(false);
            UpdatePlayerTurn(newActivePlayer);
            InputHandler.Instance.SetInputBlocked(false);
        }
    }
}