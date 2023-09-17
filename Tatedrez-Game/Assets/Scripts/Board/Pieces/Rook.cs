using System;
using UnityEngine;

namespace Tatedrez.Board
{
    public class Rook: Piece
    {
        protected override void Start()
        {
            type = PieceType.Rook;
            base.Start();
        }
        
        public override bool IsValidMovement(Vector2Int origin, Vector2Int target, Cell[,] cells)
        {
            var distanceX = Math.Abs(target.x - origin.x);
            var distanceY = Math.Abs(target.y - origin.y);
            var movementVector = target - origin;
            
            var isHorizontalMovement = distanceX == 0 || distanceY == 0;
            if (!isHorizontalMovement) return false;

            if (movementVector.magnitude > 1)
            {
                foreach (var cell in cells)
                {
                    if (CellObstructingPath(cell, origin, movementVector))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}