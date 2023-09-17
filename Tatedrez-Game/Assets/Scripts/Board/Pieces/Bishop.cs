using System;
using UnityEngine;

namespace Tatedrez.Board
{
    public class Bishop: Piece
    {
        protected override void Start()
        {
            type = PieceType.Bishop;
            base.Start();
        }
        
        public override bool IsValidMovement(Vector2Int origin, Vector2Int target, Cell[,] cells)
        {
            var distanceX = Math.Abs(target.x - origin.x);
            var distanceY = Math.Abs(target.y - origin.y);
            var movementVector = target - origin;
            
            var isDiagonalMovement = distanceX == distanceY;
            if (!isDiagonalMovement) return false;

            if (movementVector.magnitude > 2)
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