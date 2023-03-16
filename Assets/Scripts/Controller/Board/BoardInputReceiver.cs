using UnityEngine;

namespace Assets.Scripts.Controller
{
    [RequireComponent(typeof(BoxCollider))]
    public class BoardInputReceiver : MonoBehaviour
    {
        protected IMouseClickHandler mouseClickHandler;
        protected IUndoRedoKeysHandler undoRedoInputHandler;
        protected BoxCollider targetBoxCollider;

        private void Awake()
        {
            mouseClickHandler = GetComponent<IMouseClickHandler>();
            undoRedoInputHandler = GetComponent<IUndoRedoKeysHandler>();
            targetBoxCollider = GetComponent<BoxCollider>();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    // if player click on upper side of board
                    if (hit.collider == targetBoxCollider &&
                        hit.point.y >= targetBoxCollider.bounds.max.y - 1e-4)
                        mouseClickHandler.ProcessLeftMouseInput(hit.point, null, null);
                }
            }
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Z)) { // TODO: Unity shortcut ctrl+z cannot be used in game ....
                if (Input.GetKey(KeyCode.A)) {
                    undoRedoInputHandler.ProcessRedoCommandInput();
                } else {
                    undoRedoInputHandler.ProcessUndoCommandInput();
                }
                
            }
        }
    }
}