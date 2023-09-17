using UnityEngine;

namespace Tatedrez.Libraries
{
    [CreateAssetMenu(menuName = "Tatedrez/Texts Library", fileName = "TextsLibrary")]
    public class TextsLibrary : ScriptableObject
    {
        [Header("Texts")] 
        [SerializeField] private string playerTurn;
        [SerializeField] private string noMovesAvailable;
        [SerializeField] private string winner;
        [SerializeField] private string restartButton;
        [SerializeField] private string scores;

        public string PlayerTurn => playerTurn;
        public string NoMovesAvailable => noMovesAvailable;
        public string Winner => winner;
        public string RestartButton => restartButton;
        public string Scores => scores;
    }
}