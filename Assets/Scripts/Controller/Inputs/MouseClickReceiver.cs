using UnityEngine;

public class MouseClickReceiver : MonoBehaviour
{ 
    [SerializeField] protected IMouseClickHandler[] clickHandlers;

    public void OnInputRecieved(Vector3 clickPosition)
    {
        foreach (var clickHandler in clickHandlers)
        {
            clickHandler.ProcessInput(clickPosition, null, null);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                OnInputRecieved(hit.point);
            }
        }
    }
}