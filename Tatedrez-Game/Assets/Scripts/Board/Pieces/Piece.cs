using System;
using UnityEngine;
using UnityEngine.UI;

namespace Tatedrez.Board
{
    public abstract class Piece : MonoBehaviour
    {
        [Serializable]
        public enum PieceType
        {
            Knight,
            Rook,
            Bishop
        }

        [SerializeField] private PlayerId owner;
        [SerializeField] private Image overlayImage;
        [SerializeField] private BoxCollider2D collider;
        
        private int positionOffset = 2;
        private bool isPlaced = false;
        private Color originalOverlayColor;
        private Vector3 initialPosition;

        protected PieceType type;
        
        public PlayerId Owner => owner;
        public bool IsPlaced => isPlaced;

        public abstract bool IsValidMovement(Vector2Int origin, Vector2Int target, Cell[,] cells);
        
        protected virtual void Start()
        {
            switch (owner)
            {
                case PlayerId.Player1:
                    originalOverlayColor = Color.black;
                    break;
                case PlayerId.Player2:
                    originalOverlayColor = Color.white;
                    break;
            }

            initialPosition = transform.position;
        }

        public void Reset()
        {
            ClearSelection();
            SetIsPlaced(false);
            transform.position = initialPosition;
        }
        
        public void Select()
        {
            overlayImage.color = Color.green;
        }
        
        public void ClearSelection()
        {
            overlayImage.color = originalOverlayColor;
        }

        public void Move(Vector3 position)
        {
            position = new Vector3(position.x + positionOffset, position.y + positionOffset, position.z);
            transform.position = position;
        }

        public void SetIsPlaced(bool value)
        {
            isPlaced = value;
            collider.enabled = !isPlaced;
        }

        protected bool CellObstructingPath(Cell cell, Vector2Int origin, Vector2Int movementVector)
        {
            var movementVectorToCell = cell.Position - origin;

            Vector2 originalDirection = movementVector;
            originalDirection.Normalize();
            Vector2 directionToCell = movementVectorToCell;
            directionToCell.Normalize();

            var cellIsInPath = originalDirection == directionToCell;

            return cellIsInPath && !cell.IsEmpty();
        }
        
#if UNITY_EDITOR
        public Piece SetOwner(PlayerId player)
        {
            this.owner = player;
            return this;
        }
#endif
    }
}
