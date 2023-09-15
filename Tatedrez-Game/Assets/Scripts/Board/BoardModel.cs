using UnityEngine;

public class BoardModel
{
    public readonly Vector2Int BoardSize;
    public readonly Cell[,] Cells;
    private GameManager.PlayerId activePlayer;
    private bool isPlacementMode;
    private bool isPieceSelected;
    private Piece selectedPiece;
    private Cell selectionCell;

    public GameManager.PlayerId ActivePlayer => activePlayer;
    public bool IsPlacementMode => isPlacementMode;
    public bool IsPieceSelected => isPieceSelected;
    public Piece SelectedPiece => selectedPiece;
    
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
        
        activePlayer = (GameManager.PlayerId)Random.Range(1, 3);
        isPlacementMode = true;
        isPieceSelected = false;
        selectedPiece = null;
        selectionCell = null;
    }
    
    public Cell GetCell(int x, int y)
    {
        return IsValidCell(x, y) ? Cells[x, y] : new Cell(x, y, false);
    }
    
    private bool IsValidCell(int x, int y)
    {
        return x >= 0 && x < BoardSize.x && y >= 0 && y < BoardSize.y;
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

    public bool Move(Piece piece, Cell target)
    {
        if (isPlacementMode)
        {
            target.SetState(Cell.CellState.Occupied);
            target.SetPiece(selectedPiece);
            return true;
        }
        
        var validMovement = piece.IsValidMovement(selectionCell.Position, target.Position, Cells);

        if (validMovement)
        {
            selectionCell.SetState(Cell.CellState.Empty);
            selectionCell.SetPiece(null);

            target.SetState(Cell.CellState.Occupied);
            target.SetPiece(selectedPiece);
        }

        return validMovement;
    }

    public GameManager.PlayerId FinishPlayerTurn()
    {
        switch (activePlayer)
        {
            case GameManager.PlayerId.Player1:
                activePlayer = GameManager.PlayerId.Player2;
                break;
            case GameManager.PlayerId.Player2:
                activePlayer = GameManager.PlayerId.Player1;
                break;
        }

        return activePlayer;
    }

    public void SetIsPlacementMode(bool value)
    {
        isPlacementMode = value;
    }
}
