using UnityEngine;
using System.Collections;
using System;
using Assets.Scripts.Model;
using Assets.Scripts.View;

public class BoardController : MonoBehaviour, IMouseClickHandler
{
    private BoardView boardView;
    private BoardInteractionManager interactionManager;

    private void Awake()
    {
        boardView = GetComponent<BoardView>();
        interactionManager = new BoardInteractionManager();
    }

    public void ProcessInput(Vector3 inputPosition, GameObject selectedObject, Action onClick)
    {
        Vector2Integer squareLocation = SquareLocationFromPosition(inputPosition);
        Debug.Log(squareLocation);
    }

    public Vector2Integer SquareLocationFromPosition(Vector3 position)
    {
        int x = Mathf.FloorToInt((transform.InverseTransformPoint(position).x + boardView.Width / 2) / boardView.SquareWidth);
        int y = Mathf.FloorToInt((transform.InverseTransformPoint(position).z + boardView.Width / 2) / boardView.SquareWidth);
        return new Vector2Integer(x, y);
    }

    public Vector3 PositionFromSquareLocation(Vector2Integer squareLocation)
    {
        return boardView.bottomLeftSquare.position +
            new Vector3(squareLocation.X * boardView.SquareWidth,
                        0f, squareLocation.Y * boardView.SquareWidth);
    }
}