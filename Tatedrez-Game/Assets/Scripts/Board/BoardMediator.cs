using System;
using System.Collections.Generic;
using Tatedrez.Utils;
using UnityEngine;

namespace Tatedrez.Board
{
    public class BoardMediator
    {
        private readonly BoardModel model;
        private readonly BoardView view;

        public BoardMediator(BoardModel model, BoardView view)
        {
            this.model = model;
            this.view = view;

            view.Setup(model.BoardSize, model.ActivePlayer);

            foreach (var cell in model.Cells)
            {
                view.DrawCell(cell.Position);
            }

            InputHandler.Instance.OnTouchEvent += HandleInput;
        }

        private void HandleInput(Vector3 position)
        {
            if (model.IsPlacementMode)
            {
                HandlePlacementMode(position);
            }
            else
            {
                HandleDynamicMode(position);
            }
        }

        private void HandlePlacementMode(Vector3 position)
        {
            var piece = view.ScreenToPiece(position);

            if (piece && piece.Owner.Equals(model.ActivePlayer) && piece != model.SelectedPiece)
            {
                if (model.IsPieceSelected)
                {
                    view.ClearSelection(model.SelectedPiece);
                    model.ClearSelection();
                }

                model.Select(piece);
                view.Select(piece);
            }
            else if (model.IsPieceSelected)
            {
                var cellPosition = view.ScreenToCell(position);
                var cell = model.GetCell(cellPosition.x, cellPosition.y);

                if (!cell.IsValid || Cell.CellState.Occupied.Equals(cell.State)) return;

                model.Move(cell);
                view.Move(model.SelectedPiece, cellPosition);

                model.SelectedPiece.SetIsPlaced(true);

                view.ClearSelection(model.SelectedPiece);
                model.ClearSelection();

                CheckPlacementMode();
                EndTurn();
            }
        }

        private void HandleDynamicMode(Vector3 position)
        {
            var cellPosition = view.ScreenToCell(position);
            var cell = model.GetCell(cellPosition.x, cellPosition.y);

            if (!cell.IsValid) return;

            if (Cell.CellState.Occupied.Equals(cell.State) && model.ActivePlayer.Equals(cell.CurrentPiece.Owner) && cell.CurrentPiece != model.SelectedPiece)
            {
                if (model.IsPieceSelected)
                {
                    view.ClearSelection(model.SelectedPiece);
                    model.ClearSelection();
                }

                model.Select(cell.CurrentPiece, cell);
                view.Select(cell.CurrentPiece);
            }
            else if (Cell.CellState.Empty.Equals(cell.State) && model.IsPieceSelected)
            {
                var validMovement = model.SelectedPiece.IsValidMovement(model.SelectionCell.Position, cell.Position, model.Cells);

                if (validMovement)
                {
                    model.Move(cell);
                    view.Move(model.SelectedPiece, cellPosition);

                    view.ClearSelection(model.SelectedPiece);
                    model.ClearSelection();

                    EndTurn();
                }
            }
        }

        private void CheckPlacementMode()
        {
            foreach (var piece in view.Pieces)
            {
                if (!piece.IsPlaced)
                {
                    return;
                }
            }

            model.ExitPlacementMode();
        }

        private bool CheckTicTacToe()
        {
            List<Vector2Int> playerPositions = new List<Vector2Int>();
            
            List<Cell> playerOwnedCells = model.GetPlayerOwnedCells(model.ActivePlayer);
            foreach (var cell in playerOwnedCells)
            {
                playerPositions.Add(cell.Position);
            }

            if (playerPositions.Count == 3)
            {
                var sameRow = playerPositions[0].y == playerPositions[1].y && playerPositions[1].y == playerPositions[2].y;
                if (sameRow) return true;

                var sameColumn = playerPositions[0].x == playerPositions[1].x && playerPositions[1].x == playerPositions[2].x;
                if (sameColumn) return true;

                var sameDiagonal = Math.Abs(playerPositions[0].x - playerPositions[1].x) == Math.Abs(playerPositions[0].y - playerPositions[1].y) &&
                                        Math.Abs(playerPositions[1].x - playerPositions[2].x) == Math.Abs(playerPositions[1].y - playerPositions[2].y) &&
                                        Math.Abs(playerPositions[0].x - playerPositions[2].x) == Math.Abs(playerPositions[0].y - playerPositions[2].y);
                if (sameDiagonal) return true;
            }

            return false;
        }

        private void EndTurn()
        {
            var gameover = CheckTicTacToe();

            if (gameover)
            {
                InputHandler.Instance.SetInputBlocked(true);
                view.GameOver(model.ActivePlayer);
            }
            else
            {
                var newActivePlayer = model.SwitchPlayerTurn();
                view.UpdatePlayerTurn(newActivePlayer);

                if (!model.IsPlacementMode) CheckPlayerCanMove();
            }
        }

        private void CheckPlayerCanMove()
        {
            List<Cell> playerOwnedCells = model.GetPlayerOwnedCells(model.ActivePlayer);
            
            List<Cell> emptyCells = model.GetEmptyCells();
            
            foreach (var playerCell in playerOwnedCells)
            {
                foreach (var emptyCell in emptyCells)
                {
                    var validMovement = playerCell.CurrentPiece.IsValidMovement(playerCell.Position, emptyCell.Position, model.Cells);
                    if (validMovement) return;
                }
            }
            
            InputHandler.Instance.SetInputBlocked(true);
            var newActivePlayer = model.SwitchPlayerTurn();
            view.NoMovesAvailable(newActivePlayer);

        }
    }
}