using System.Collections;
using NUnit.Framework;
using Tatedrez.Board;
using Tatedrez.Board.Tatedrez.Board;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tatedrez.Tests
{
    public class PieceMovementTests
    {
        [UnityTest]
        public IEnumerator KnightMovementValid()
        {
            var piece = InitPiece(PieceType.Knight);

            var model = new BoardModel(new Score());

            var origin = new Vector2Int(0, 0);
            var target = new Vector2Int(1, 2);

            var valid = piece.IsValidMovement(origin, target, model.Cells);

            Assert.True(valid);
            yield return null;
        }

        [UnityTest]
        public IEnumerator KnightMovementInvalid()
        {
            var piece = InitPiece(PieceType.Knight);

            var model = new BoardModel(new Score());

            var origin = new Vector2Int(0, 0);
            var target = new Vector2Int(1, 1);

            var valid = piece.IsValidMovement(origin, target, model.Cells);

            Assert.True(!valid);
            yield return null;
        }

        [UnityTest]
        public IEnumerator RookMovementValid()
        {
            var piece = InitPiece(PieceType.Rook);

            var model = new BoardModel(new Score());

            var origin = new Vector2Int(0, 2);
            var target = new Vector2Int(0, 0);

            var valid = piece.IsValidMovement(origin, target, model.Cells);

            Assert.True(valid);
            yield return null;
        }

        [UnityTest]
        public IEnumerator RookMovementInvalid()
        {
            var piece = InitPiece(PieceType.Rook);

            var model = new BoardModel(new Score());

            var origin = new Vector2Int(0, 0);
            var target = new Vector2Int(2, 1);

            var valid = piece.IsValidMovement(origin, target, model.Cells);

            Assert.True(!valid);
            yield return null;
        }

        [UnityTest]
        public IEnumerator BishopMovementValid()
        {
            var piece = InitPiece(PieceType.Bishop);

            var model = new BoardModel(new Score());

            var origin = new Vector2Int(0, 2);
            var target = new Vector2Int(2, 0);

            var valid = piece.IsValidMovement(origin, target, model.Cells);

            Assert.True(valid);
            yield return null;
        }

        [UnityTest]
        public IEnumerator BishopMovementInvalid()
        {
            var piece = InitPiece(PieceType.Bishop);

            var model = new BoardModel(new Score());

            var origin = new Vector2Int(0, 0);
            var target = new Vector2Int(0, 1);

            var valid = piece.IsValidMovement(origin, target, model.Cells);

            Assert.True(!valid);
            yield return null;
        }

        [UnityTest]
        public IEnumerator RookMovementObstacle()
        {
            var piece = InitPiece(PieceType.Rook);

            var model = new BoardModel(new Score());

            var origin = new Vector2Int(0, 0);
            var target = new Vector2Int(0, 2);

            model.GetCell(0, 1).SetState(Cell.CellState.Occupied);

            var valid = piece.IsValidMovement(origin, target, model.Cells);

            Assert.True(!valid);
            yield return null;
        }

        [UnityTest]
        public IEnumerator BishopMovementObstacle()
        {
            var piece = InitPiece(PieceType.Bishop);

            var model = new BoardModel(new Score());

            var origin = new Vector2Int(0, 0);
            var target = new Vector2Int(2, 2);

            model.GetCell(1, 1).SetState(Cell.CellState.Occupied);

            var valid = piece.IsValidMovement(origin, target, model.Cells);

            Assert.True(!valid);
            yield return null;
        }

        private Piece InitPiece(PieceType type)
        {
            var gameobject = new GameObject();
            
            switch (type)
            {
                case PieceType.Knight:
                    return gameobject.AddComponent<Knight>();
                case PieceType.Rook:
                    return gameobject.AddComponent<Rook>();
                case PieceType.Bishop:
                    return gameobject.AddComponent<Bishop>();
            }
            return null;
        }
        
        private enum PieceType
        {
            Knight,
            Rook,
            Bishop
        }
    }
}