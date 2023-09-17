namespace Tatedrez.Board
{
    namespace Tatedrez.Board
    {
        public class Score
        {
            private int player1;
            private int player2;

            public int Player1 => player1;
            public int Player2 => player2;

            public Score()
            {
                player1 = 0;
                player2 = 0;
            }
            
            public void AddScore(PlayerId player)
            {
                switch (player)
                {
                    case PlayerId.Player1:
                        player1++;
                        break;
                    case PlayerId.Player2:
                        player2++;
                        break;
                }
            }
        }
    }
}