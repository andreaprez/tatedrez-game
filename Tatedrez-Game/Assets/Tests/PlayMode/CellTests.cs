using System.Collections;
using NUnit.Framework;
using Tatedrez.Board;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tatedrez.Tests
{
    public class CellTests
    {

        [UnityTest]
        public IEnumerator SelectCell()
        {
            var piece = InitPiece();
            var model = new BoardModel();

            model.Select(piece);

            Assert.True(model.IsPieceSelected && model.SelectedPiece != null);
            yield return null;
        }

        [UnityTest]
        public IEnumerator ClearCell()
        {
            var piece = InitPiece();
            var model = new BoardModel();

            model.Select(piece);
            model.ClearSelection();

            Assert.True(!model.IsPieceSelected && model.SelectedPiece == null);
            yield return null;
        }

        [UnityTest]
        public IEnumerator MoveToCell()
        {
            var piece = InitPiece();
            var model = new BoardModel();

            var originCell = model.GetCell(0, 0);
            var targetCell = model.GetCell(1, 0);

            model.Select(piece, originCell);
            model.Move(targetCell);

            Assert.True(!targetCell.IsEmpty() && targetCell.CurrentPiece == piece && originCell.IsEmpty());
            yield return null;
        }

        private Piece InitPiece()
        {
            var gameobject = new GameObject();
            return gameobject.AddComponent<Knight>();
        }
    }
}