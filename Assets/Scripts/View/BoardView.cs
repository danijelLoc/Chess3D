using UnityEngine;
using System.Collections;


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
    }
}
