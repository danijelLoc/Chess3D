using System;
using UnityEngine;
using Assets.Scripts.Model;

namespace Assets.Scripts.View
{
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(Transform))]
    class PieceView : MonoBehaviour
    {
        private MeshRenderer meshRenderer;
        private Transform _transform;
        [SerializeField] private PieceType pieceType;
        public PieceType PieceType
        {
            get { return pieceType; }
            set
            {
// TODO change mesh
                pieceType = value;
            }
        }

        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            _transform = GetComponent<Transform>();
        }

        public void SetMaterial(Material material)
        {
            meshRenderer.material = material;
        }
    }
}
