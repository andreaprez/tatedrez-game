using System.Collections;
using System.Collections.Generic;
using Tatedrez.Libraries;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace Tatedrez.Board
{
    public class BoardView : MonoBehaviour
    {
        [SerializeField] private BoardLibrary boardLibrary;
        [SerializeField] private Tilemap tilemap;
        [SerializeField] private List<Piece> pieces;

        [SerializeField] private TextMeshProUGUI turnPlayer1;
        [SerializeField] private TextMeshProUGUI turnPlayer2;

        [SerializeField] private Camera mainCamera;
        
        [SerializeField] private GameObject gameOverLayout;
        [SerializeField] private TextMeshProUGUI gameOverText;
        [SerializeField] private Button restartButton;

        private readonly int tileSize = 36;
        private readonly int boardScale = 6;

        public List<Piece> Pieces => pieces;

        public void Setup(Vector2Int boardSize, PlayerId activePlayer)
        {
            ClearBoardTiles();

            var boardPosX = boardSize.x * tileSize * boardScale / -2;
            var boardPosY = boardSize.y * tileSize * boardScale / -2;
            tilemap.gameObject.transform.localPosition = new Vector3(boardPosX, boardPosY, 0);

            turnPlayer1.gameObject.SetActive(false);
            turnPlayer2.gameObject.SetActive(false);

            UpdatePlayerTurn(activePlayer);
        }

        public void Reset()
        {
            gameOverLayout.SetActive(false);

            foreach (var piece in pieces)
            {
                piece.Reset();
            }
        }

        private void ClearBoardTiles()
        {
            if (tilemap.size.x > 0 || tilemap.size.y > 0)
            {
                tilemap.ClearAllTiles();
            }
        }

        public void DrawCell(Vector2Int position)
        {
            var tile = boardLibrary.WhiteTile;

            if ((position.x % 2 == 0 && position.y % 2 == 0) || (position.x % 2 > 0 && position.y % 2 > 0))
            {
                tile = boardLibrary.BlackTile;
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

        public void Select(Piece piece)
        {
            piece.Select();
        }

        public void ClearSelection(Piece piece)
        {
            piece.ClearSelection();
        }

        public void Move(Piece piece, Vector2Int position)
        {
            Vector3Int vector = new Vector3Int(position.x, position.y);
            Vector3 newPosition = tilemap.CellToWorld(vector);
            piece.Move(newPosition);
        }

        public void UpdatePlayerTurn(PlayerId newActivePlayer)
        {
            turnPlayer1.gameObject.SetActive(false);
            turnPlayer2.gameObject.SetActive(false);

            switch (newActivePlayer)
            {
                case PlayerId.Player1:
                    turnPlayer1.gameObject.SetActive(true);
                    break;
                case PlayerId.Player2:
                    turnPlayer2.gameObject.SetActive(true);
                    break;
            }
        }
        
        public void GameOver(PlayerId winner)
        {
            var winnerName = "";
            switch (winner)
            {
                case PlayerId.Player1:
                    winnerName = "Player 1";
                    break;
                case PlayerId.Player2:
                    winnerName = "Player 2";
                    break;
            }
            gameOverText.text = winnerName + " wins!";

            restartButton.interactable = false;

            StartCoroutine(ShowGameOver());
        }

        private IEnumerator ShowGameOver()
        {
            yield return new WaitForSeconds(1f);

            restartButton.interactable = true;
            gameOverLayout.SetActive(true);
        }
    }
}