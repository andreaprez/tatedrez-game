using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Tatedrez.Board
{
    public abstract class Piece : MonoBehaviour
    {
        [SerializeField] private PlayerId owner;
        [SerializeField] private Image overlayImage;
        [SerializeField] private BoxCollider2D boxCollider;
        
        private int positionOffset = 2;
        private bool isPlaced = false;
        private bool isSelected = false;
        private Color originalOverlayColor;
        private Vector3 initialPosition;
        
        public PlayerId Owner => owner;
        public bool IsPlaced => isPlaced;

        public abstract bool IsValidMovement(Vector2Int origin, Vector2Int target, Cell[,] cells);
        
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
            isSelected = true;
        }
        
        public void ClearSelection()
        {
            overlayImage.color = originalOverlayColor;
            isSelected = false;
        }

        public void Move(Vector3 position)
        {
            position = new Vector3(position.x + positionOffset, position.y + positionOffset, position.z);
            transform.position = position;
        }

        public void InvalidMovement()
        {
            StartCoroutine(ShowInvalidMovement());
        }
        
        public void SetIsPlaced(bool value)
        {
            isPlaced = value;
            boxCollider.enabled = !isPlaced;
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

        private IEnumerator ShowInvalidMovement()
        {
            overlayImage.color = Color.red;

            yield return new WaitForSeconds(0.7f);

            if (isSelected)
            {
                overlayImage.color = Color.green;
            }
            else
            {
                overlayImage.color = originalOverlayColor;
            }
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
