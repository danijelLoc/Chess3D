using UnityEngine;
using System.Collections;
using Assets.Scripts.Model;

namespace Assets.Scripts.View
{
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(BoxCollider))]
    public class BoardView : MonoBehaviour
    {
        // Use this for initialization
        private MeshRenderer meshRenderer;
        public Transform bottomLeftSquare;
        private BoxCollider boxCollider; 
        public float Width { get { return boxCollider.size.x; } }
        public float SquareWidth { get { return bottomLeftSquare.localScale.x; } }

        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            boxCollider = GetComponent<BoxCollider>();
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public Vector2Integer SquareLocationFromPosition(Vector3 position)
        {
            int x = Mathf.FloorToInt((transform.InverseTransformPoint(position).x + Width / 2) / SquareWidth);
            int y = Mathf.FloorToInt((transform.InverseTransformPoint(position).z + Width / 2) / SquareWidth);
            return new Vector2Integer(x, y);
        }

        public Vector3 PositionFromSquareLocation(Vector2Integer squareLocation)
        {
            return bottomLeftSquare.position +
                new Vector3(squareLocation.X * SquareWidth,
                            0f, squareLocation.Y * SquareWidth);
        }
    }
}
