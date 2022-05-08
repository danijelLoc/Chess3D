using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Model;

namespace Assets.Scripts.View
{
    class ViewCreator : MonoBehaviour
    {
        [SerializeField] private PieceGameObject[] piecesPrefabs;
        [SerializeField] private Material whitePieceMaterial;
        [SerializeField] private Material blackPieceMaterial;
        private Dictionary<Team, Material> teamToMaterial = new Dictionary<Team, Material>();
        private Dictionary<PieceType, PieceGameObject> pieceTypeToGameObject = new Dictionary<PieceType, PieceGameObject>();

        private void Awake()
        {
            teamToMaterial.Add(Team.Black, blackPieceMaterial);
            teamToMaterial.Add(Team.White, whitePieceMaterial);
            foreach (var piece in piecesPrefabs)
            {
                pieceTypeToGameObject.Add(piece.GetComponent<PieceType>(), piece);
            }
        }

        public PieceGameObject createPieceGameObject(Piece piece)
        {
            PieceGameObject pieceGameObject = pieceTypeToGameObject[piece.Type];
            pieceGameObject.SetMaterial(teamToMaterial[piece.Team]);
            return pieceGameObject;
        }
    }
}