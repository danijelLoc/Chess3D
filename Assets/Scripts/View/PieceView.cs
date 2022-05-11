using System;
using UnityEngine;

namespace Assets.Scripts.View
{
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(Transform))]
    class PieceView : MonoBehaviour
    {
        private MeshRenderer meshRenderer;
        private Transform _transform;

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
