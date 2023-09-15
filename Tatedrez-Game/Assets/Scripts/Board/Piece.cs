using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Piece : MonoBehaviour
{
    [Serializable]
    public enum PieceType
    {
        Knight,
        Rook,
        Bishop
    }

    [SerializeField] private GameManager.PlayerId owner;
    [SerializeField] private PieceType type;
    [SerializeField] private Image overlayImage;
    [SerializeField] private BoxCollider2D collider;

    private int positionOffset = 2;
    private bool isPlaced = false;
    private Color originalOverlayColor;

    public GameManager.PlayerId Owner => owner;
    public PieceType Type => type;
    public bool IsPlaced => isPlaced;

    private void Start()
    {
        switch (owner)
        {
            case GameManager.PlayerId.Player1:
                originalOverlayColor = Color.black;
                break;
            case GameManager.PlayerId.Player2:
                originalOverlayColor = Color.white;
                break;
        }
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
        
        switch (type)
        {
            case PieceType.Knight:
                return (distanceX == 2 && distanceY == 1) || (distanceX == 1 && distanceY == 2);
            
            case PieceType.Rook:
                var isHorizontalMovement = distanceX == 0 || distanceY == 0;
                if (!isHorizontalMovement) return false;
                
                if (target.x - origin.x > 1)
                {
                    for (int x = origin.x + 1; x < target.x; x++)
                    {
                        if (Cell.CellState.Occupied.Equals(cells[x, origin.y].State))
                        {
                            return false;
                        }
                    }
                }

                if (target.y - origin.y > 1)
                {
                    for (int y = origin.y + 1; y < target.y; y++)
                    {
                        if (Cell.CellState.Occupied.Equals(cells[origin.x, y].State))
                        {
                            return false;
                        }
                    }
                }
                
                return true;
            
            case PieceType.Bishop:
                var isDiagonalMovement = distanceX == distanceY;
                if (!isDiagonalMovement) return false;

                var movementDirection = target - origin;
                if (movementDirection.magnitude > 2)
                {
                    foreach (var cell in cells)
                    {
                        distanceX = Math.Abs(cell.Position.x - origin.x);
                        distanceY = Math.Abs(cell.Position.y - origin.y);
                        var cellIsInPath = (distanceX == distanceY) && cell.Position != target && cell.Position != origin;

                        if (cellIsInPath)
                        {
                            if (Cell.CellState.Occupied.Equals(cell.State))
                            {
                                return false;
                            }
                        }
                    }
                }

                return true;
        }

        return false;
    }
}
