using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tatedrez.Libraries
{
    [CreateAssetMenu(menuName = "Tatedrez/Board Library", fileName = "BoardLibrary")]
    public class BoardLibrary : ScriptableObject
    {
        [Header("Tiles")] [SerializeField] private Tile whiteTile;
        [SerializeField] private Tile blackTile;

        public Tile WhiteTile => whiteTile;
        public Tile BlackTile => blackTile;
    }
}