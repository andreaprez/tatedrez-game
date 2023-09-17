using System;
using UnityEngine;

namespace Tatedrez.Board
{
    public class Knight : Piece
    {
        public override bool IsValidMovement(Vector2Int origin, Vector2Int target, Cell[,] cells)
        {
            var distanceX = Math.Abs(target.x - origin.x);
            var distanceY = Math.Abs(target.y - origin.y);
            
            return (distanceX == 2 && distanceY == 1) || (distanceX == 1 && distanceY == 2);
        }
    }
}