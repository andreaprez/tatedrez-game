using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

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

        public string PlayerTurn => playerTurn;
        public string NoMovesAvailable => noMovesAvailable;
        public string Winner => winner;
        public string RestartButton => restartButton;
    }
}