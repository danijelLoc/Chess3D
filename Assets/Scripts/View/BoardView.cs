using UnityEngine;
using System.Collections;


namespace Assets.Scripts.View
{
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(Transform))]
    public class BoardView : MonoBehaviour
    {
        // Use this for initialization
        private MeshRenderer meshRenderer;
        private Transform _transform;

        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            _transform = GetComponent<Transform>();
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
