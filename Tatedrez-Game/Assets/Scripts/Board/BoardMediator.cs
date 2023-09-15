using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

        InputHandler.OnTouchEvent += HandleInput;
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
                
            var moveSuccessful = model.Move(model.SelectedPiece, cell);
            if (moveSuccessful)
            {
                view.Move(model.SelectedPiece, cellPosition);
                
                model.SelectedPiece.SetIsPlaced(true);
                
                view.ClearSelection(model.SelectedPiece);
                model.ClearSelection();
                
                CheckPlacementMode();
                EndTurn();
            }
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
            var moveSuccessful = model.Move(model.SelectedPiece, cell);
            if (moveSuccessful)
            {
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

        model.SetIsPlacementMode(false);
    }

    private bool CheckTicTacToe(GameManager.PlayerId activePlayer)
    {
        List<Vector2Int> playerPositions = new List<Vector2Int>();

        foreach (var cell in model.Cells)
        {
            if (Cell.CellState.Occupied.Equals(cell.State) && cell.CurrentPiece != null && cell.CurrentPiece.Owner.Equals(activePlayer))
            {
                playerPositions.Add(cell.Position);
            }
        }

        if (playerPositions.Count == 3)
        {
            var sameRow = playerPositions[0].y == playerPositions[1].y && playerPositions[1].y == playerPositions[2].y;
            if (sameRow) return true;
            
            var sameColumn = playerPositions[0].x == playerPositions[1].x && playerPositions[1].x == playerPositions[2].x;
            if (sameColumn) return true;

            var sameDiagonal = Math.Abs(playerPositions[0].x - playerPositions[1].x) == Math.Abs(playerPositions[0].y - playerPositions[1].y) &&
                                    Math.Abs(playerPositions[1].x - playerPositions[2].x) == Math.Abs(playerPositions[1].y - playerPositions[2].y);
            if (sameDiagonal) return true;
        }
        
        return false;
    }

    private void EndTurn()
    {
        var gameover = CheckTicTacToe(model.ActivePlayer);

        if (gameover)
        {
            UIManager.Instance.GameOver(model.ActivePlayer);
            model.GameOver();
        }
        else
        {
            var newActivePlayer = model.SwitchPlayerTurn();
            view.UpdatePlayerTurn(newActivePlayer);
        }
    }
}
