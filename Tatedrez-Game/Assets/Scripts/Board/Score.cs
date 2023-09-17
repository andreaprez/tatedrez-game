namespace Tatedrez.Board
{
    namespace Tatedrez.Board
    {
        public class Score
        {
            private int playerWhite;
            private int playerBlack;

            public int PlayerWhite => playerWhite;
            public int PlayerBlack => playerBlack;

            public Score()
            {
                playerWhite = 0;
                playerBlack = 0;
            }
            
            public void AddScore(PlayerId player)
            {
                switch (player)
                {
                    case PlayerId.White:
                        playerWhite++;
                        break;
                    case PlayerId.Black:
                        playerBlack++;
                        break;
                }
            }
        }
    }
}