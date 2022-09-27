using Battleship.Logic;
using Battleship.Logic.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Battleship.Tests.Unit
{
    public class BoardTests
    {
        private const int BOARD_SIZE = 10;

        [Fact]
        public void Should()
        {
            var board = new Board(BOARD_SIZE);
        }
    }
}
