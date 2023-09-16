using UnityEngine;

namespace Tatedrez.Board
{
    public class Cell
    {
        public enum CellState
        {
            Empty,
            Occupied
        }

        public readonly Vector2Int Position;
        public readonly bool IsValid;

        private CellState state;
        private Piece currentPiece;

        public CellState State => state;
        public Piece CurrentPiece => currentPiece;

        public Cell(int posX, int posY, bool isValid = true)
        {
            Position = new Vector2Int(posX, posY);
            IsValid = isValid;
            state = CellState.Empty;
            currentPiece = null;
        }

        public void SetState(CellState state)
        {
            this.state = state;
        }

        public void SetPiece(Piece piece)
        {
            this.currentPiece = piece;
        }
    }
}