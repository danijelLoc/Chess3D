using System;
using UnityEngine;
using Assets.Scripts.Model;

namespace Assets.Scripts.View
{
    [RequireComponent(typeof(MeshRenderer))]
    class PieceView : MonoBehaviour, IPieceObserver
    {
        private MeshRenderer meshRenderer;
        private BoardView boardView;
        private PieceViewCreator pieceViewCreator;
        private Color originaMateriallColor;
        public PieceType PieceType;

        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        public void SetMaterial(Material material)
        {
            meshRenderer.material = material;
            originaMateriallColor = material.color;
        }

        public void SetBoard(BoardView boardView)
        {
            this.boardView = boardView;
        }

        public void SetPieceViewCreator(PieceViewCreator pieceViewCreator)
        {
            this.pieceViewCreator = pieceViewCreator;
        }

        public void UpdateSelection(Boolean selected)
        {
            meshRenderer.material.color = selected ? Color.green : originaMateriallColor;
        }

        public void UpdatePieceType(Piece piece, PieceType newType)
        {
            pieceViewCreator.ChangeTypeOfPiece(piece, newType);
        }

        public void UpdateSquareLocation(Vector2Integer newSquareLocation)
        {
            Vector3 destination = boardView.PositionFromSquareLocation(newSquareLocation);
            transform.position = destination;
        }

        public void UpdateIsAlive(bool isAlive)
        {
            Debug.Log("Destroy");
            gameObject.SetActive(isAlive);
        }
    }
}
