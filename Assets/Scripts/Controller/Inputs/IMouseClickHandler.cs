using System;
using UnityEngine;

public interface IMouseClickHandler
{
    void ProcessInput(Vector3 inputPosition, GameObject selectedObject, Action onClick);
}