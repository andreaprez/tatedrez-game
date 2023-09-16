using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tatedrez.Board
{
    public class BoardModel
    {
        public readonly Vector2Int BoardSize;
        public readonly Cell[,] Cells;
        private PlayerId activePlayer;
        private bool isPlacementMode;
        private bool isPieceSelected;
        private Piece selectedPiece;
        private Cell selectionCell;

        public PlayerId ActivePlayer => activePlayer;
        public bool IsPlacementMode => isPlacementMode;
        public bool IsPieceSelected => isPieceSelected;
        public Piece SelectedPiece => selectedPiece;
        public Cell SelectionCell => selectionCell;

        public BoardModel()
        {
            BoardSize = new Vector2Int(3, 3);
            Cells = new Cell[BoardSize.x, BoardSize.y];

            for (var x = 0; x < BoardSize.x; x++)
            {
                for (var y = 0; y < BoardSize.y; y++)
                {
                    Cells[x, y] = new Cell(x, y);
                }
            }

            activePlayer = (PlayerId)Random.Range(1, 3);
            isPlacementMode = true;
            isPieceSelected = false;
            selectedPiece = null;
            selectionCell = null;
        }

        public Cell GetCell(int x, int y)
        {
            return IsValidCell(x, y) ? Cells[x, y] : new Cell(x, y, false);
        }

        public void Select(Piece piece, Cell cell = null)
        {
            isPieceSelected = true;
            selectedPiece = piece;
            selectionCell = cell;
        }

        public void ClearSelection()
        {
            isPieceSelected = false;
            selectedPiece = null;
            selectionCell = null;
        }

        public void Move(Cell target)
        {
            target.SetState(Cell.CellState.Occupied);
            target.SetPiece(selectedPiece);

            if (!isPlacementMode)
            {
                selectionCell.SetState(Cell.CellState.Empty);
                selectionCell.SetPiece(null);
            }
        }

        public PlayerId SwitchPlayerTurn()
        {
            switch (activePlayer)
            {
                case PlayerId.Player1:
                    activePlayer = PlayerId.Player2;
                    break;
                case PlayerId.Player2:
                    activePlayer = PlayerId.Player1;
                    break;
            }

            return activePlayer;
        }

        public void ExitPlacementMode()
        {
            isPlacementMode = false;
        }

        public void GameOver()
        {
            activePlayer = PlayerId.None;
        }

        private bool IsValidCell(int x, int y)
        {
            return x >= 0 && x < BoardSize.x && y >= 0 && y < BoardSize.y;
        }
    }
}