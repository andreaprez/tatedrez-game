using NUnit.Framework;
using Tatedrez.Board;

namespace Tatedrez.Tests
{
    public class CellTests
    {
        [Test]
        public void CellIsValid()
        {
            var model = new BoardModel();
            var cell = model.GetCell(0, 0);

            Assert.True(cell.IsValid);
        }
        
        [Test]
        public void CellIsInvalid()
        {
            var model = new BoardModel();
            var cell = model.GetCell(-1, -1);

            Assert.True(!cell.IsValid);
        }
    }
}