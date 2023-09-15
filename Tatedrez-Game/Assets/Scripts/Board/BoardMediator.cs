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
        if (!model.IsPieceSelected)
        {
            //Try Select
            var piece = view.ScreenToPiece(position);
            if (piece && piece.Owner.Equals(model.ActivePlayer))
            {
                model.Select(piece);
                view.Select(piece);
            }
        }
        else
        {
            //Try Move
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

                var newActivePlayer = model.FinishPlayerTurn();
                view.UpdatePlayerTurn(newActivePlayer);
            }
        }
    }
    
    private void HandleDynamicMode(Vector3 position)
    {
        var cellPosition = view.ScreenToCell(position);
        var cell = model.GetCell(cellPosition.x, cellPosition.y);

        if (!cell.IsValid) return;

        if (!model.IsPieceSelected)
        {
            //Try Select
            if (Cell.CellState.Occupied.Equals(cell.State) &&
                model.ActivePlayer.Equals(cell.CurrentPiece.Owner))
            {
                model.Select(cell.CurrentPiece, cell);
                view.Select(cell.CurrentPiece);
            }
        }
        else
        {
            //Try Move

            if (Cell.CellState.Occupied.Equals(cell.State)) return;
                
            var moveSuccessful = model.Move(model.SelectedPiece, cell);
            if (moveSuccessful)
            {
                view.Move(model.SelectedPiece, cellPosition);
                
                view.ClearSelection(model.SelectedPiece);
                model.ClearSelection();

                var newActivePlayer = model.FinishPlayerTurn();
                view.UpdatePlayerTurn(newActivePlayer);
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
        CheckTicTacToe();
    }

    private void CheckTicTacToe()
    {
        
    }
}
