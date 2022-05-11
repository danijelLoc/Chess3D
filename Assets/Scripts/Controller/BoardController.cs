using UnityEngine;
using System.Collections;
using System;
using Assets.Scripts.Model;


public class BoardController : MonoBehaviour, IMouseClickHandler
{
    [SerializeField] private BoardInteraction boardInteraction;

    public void ProcessInput(Vector3 inputPosition, GameObject selectedObject, Action onClick)
    {
        throw new NotImplementedException();
    }
}