using System.Collections.Generic;
using Tatedrez.Board.Tatedrez.Board;
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
        private Score scores;

        public PlayerId ActivePlayer => activePlayer;
        public bool IsPlacementMode => isPlacementMode;
        public bool IsPieceSelected => isPieceSelected;
        public Piece SelectedPiece => selectedPiece;
        public Cell SelectionCell => selectionCell;
        public Score Scores => scores;

        public BoardModel(Score scores)
        {
            this.scores = scores;
            
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

        public List<Cell> GetPlayerOwnedCells(PlayerId player)
        {
            List<Cell> playerOwnedCells = new List<Cell>();

            foreach (var cell in Cells)
            {
                if (!cell.IsEmpty() && cell.CurrentPiece != null && player.Equals(cell.CurrentPiece.Owner))
                {
                    playerOwnedCells.Add(cell);
                }
            }

            return playerOwnedCells;
        }

        public List<Cell> GetEmptyCells()
        {
            List<Cell> emptyCells = new List<Cell>();

            foreach (var cell in Cells)
            {
                if (cell.IsEmpty())
                {
                    emptyCells.Add(cell);
                }
            }

            return emptyCells;
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
                case PlayerId.White:
                    activePlayer = PlayerId.Black;
                    break;
                case PlayerId.Black:
                    activePlayer = PlayerId.White;
                    break;
            }

            return activePlayer;
        }

        public void ExitPlacementMode()
        {
            isPlacementMode = false;
        }

        public void UpdateScore(PlayerId winner)
        {
            scores.AddScore(winner);
        }
        
        private bool IsValidCell(int x, int y)
        {
            return x >= 0 && x < BoardSize.x && y >= 0 && y < BoardSize.y;
        }
    }
}