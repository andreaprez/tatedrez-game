using System;
using UnityEngine;
using UnityEngine.UI;

namespace Tatedrez.Board
{
    public class Piece : MonoBehaviour
    {
        [Serializable]
        public enum PieceType
        {
            Knight,
            Rook,
            Bishop
        }

        [SerializeField] private PlayerId owner;
        [SerializeField] private PieceType type;
        [SerializeField] private Image overlayImage;
        [SerializeField] private BoxCollider2D collider;

        private int positionOffset = 2;
        private bool isPlaced = false;
        private Color originalOverlayColor;
        private Vector3 initialPosition;

        public PlayerId Owner => owner;
        public bool IsPlaced => isPlaced;

        private void Start()
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

        public bool IsValidMovement(Vector2Int origin, Vector2Int target, Cell[,] cells)
        {
            var distanceX = Math.Abs(target.x - origin.x);
            var distanceY = Math.Abs(target.y - origin.y);
            var movementVector = target - origin;
            
            switch (type)
            {
                case PieceType.Knight:
                    return (distanceX == 2 && distanceY == 1) || (distanceX == 1 && distanceY == 2);
                
                case PieceType.Rook:
                    var isHorizontalMovement = distanceX == 0 || distanceY == 0;
                    if (!isHorizontalMovement) return false;

                    if (movementVector.magnitude > 1)
                    {
                        foreach (var cell in cells)
                        {
                            if (CellObstructingPath(cell, origin, movementVector))
                            {
                                return false;
                            }
                        }
                    }
                    return true;
                
                case PieceType.Bishop:
                    var isDiagonalMovement = distanceX == distanceY;
                    if (!isDiagonalMovement) return false;

                    if (movementVector.magnitude > 2)
                    {
                        foreach (var cell in cells)
                        {
                            if (CellObstructingPath(cell, origin, movementVector))
                            {
                                return false;
                            }
                        }
                    }
                    return true;
                
            }
            return false;
        }

        private bool CellObstructingPath(Cell cell, Vector2Int origin, Vector2Int movementVector)
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
        public Piece SetType(PieceType type)
        {
            this.type = type;
            return this;
        }
        
        public Piece SetOwner(PlayerId player)
        {
            this.owner = player;
            return this;
        }
#endif
    }
}
