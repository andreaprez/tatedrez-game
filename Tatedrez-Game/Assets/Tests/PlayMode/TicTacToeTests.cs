using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Tatedrez.Board;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tatedrez.Tests
{
    public class TicTacToeTests
    {
        [UnityTest]
        public IEnumerator TicTacToeHorizontal()
        {
            var model = new BoardModel();
            var view = InitBoardView();
            var mediator = new BoardMediator(model, view);
            
            var posA = new Vector2Int(0, 0);
            var posB = new Vector2Int(1, 0);
            var posC = new Vector2Int(2, 0);

            PlacePieces(model, posA, posB, posC);

            Assert.True(mediator.TicTacToeTest());
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator TicTacToeVertical()
        {
            var model = new BoardModel();
            var view = InitBoardView();
            var mediator = new BoardMediator(model, view);
            
            var posA = new Vector2Int(0, 0);
            var posB = new Vector2Int(0, 1);
            var posC = new Vector2Int(0, 2);

            PlacePieces(model, posA, posB, posC);
            
            Assert.True(mediator.TicTacToeTest());
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator TicTacToeDiagonal()
        {
            var model = new BoardModel();
            var view = InitBoardView();
            var mediator = new BoardMediator(model, view);
            
            var posA = new Vector2Int(0, 0);
            var posB = new Vector2Int(1, 1);
            var posC = new Vector2Int(2, 2);

            PlacePieces(model, posA, posB, posC);
            
            Assert.True(mediator.TicTacToeTest());
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator TicTacToeNone()
        {
            var model = new BoardModel();
            var view = InitBoardView();

            var mediator = new BoardMediator(model, view);
            
            var posA = new Vector2Int(0, 0);
            var posB = new Vector2Int(1, 1);
            var posC = new Vector2Int(0, 2);

            PlacePieces(model, posA, posB, posC);
            
            Assert.True(!mediator.TicTacToeTest());
            yield return null;
        }
        
        private Piece InitPiece(PlayerId activePlayer)
        {
            var gameobject = new GameObject();
            var piece = gameobject.AddComponent<Piece>();
            return piece.SetOwner(activePlayer);
        }
        
        private BoardView InitBoardView()
        {
            var gameobject = new GameObject();
            return gameobject.AddComponent<BoardView>();
        }

        private void PlacePieces(BoardModel model, Vector2Int a, Vector2Int b, Vector2Int c)
        {
            var pieceA = InitPiece(model.ActivePlayer);
            var pieceB = InitPiece(model.ActivePlayer);
            var pieceC = InitPiece(model.ActivePlayer);
            
            model.Select(pieceA);
            model.Move(model.GetCell(a.x, a.y));

            model.Select(pieceB);
            model.Move(model.GetCell(b.x, b.y));
            
            model.Select(pieceC);
            model.Move(model.GetCell(c.x, c.y));
        }
    }
}