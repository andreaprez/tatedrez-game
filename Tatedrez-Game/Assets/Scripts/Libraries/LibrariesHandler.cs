using UnityEngine;

namespace Tatedrez.Libraries
{
    public static class LibrariesHandler
    {
        private static BoardLibrary boardLibrary;
        private static AudioLibrary audioLibrary;

        public static BoardLibrary GetBoardLibrary()
        {
            if (boardLibrary == null)
            {
                boardLibrary = Resources.Load(nameof(BoardLibrary)) as BoardLibrary;
            }

            return boardLibrary;
        }

        public static AudioLibrary GetAudioLibrary()
        {
            if (audioLibrary == null)
            {
                audioLibrary = Resources.Load(nameof(AudioLibrary)) as AudioLibrary;
            }

            return audioLibrary;
        }
    }
}