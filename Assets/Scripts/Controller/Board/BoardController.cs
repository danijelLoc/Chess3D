using UnityEngine;
using System.Collections;
using System;
using Assets.Scripts.Model;
using Assets.Scripts.View;

[RequireComponent(typeof(BoardView))]
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
        throw new NotImplementedException(inputPosition.ToString());
    }
}