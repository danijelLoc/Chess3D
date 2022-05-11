using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Model;
using Assets.Scripts.View;

namespace Assets.Scripts.Controller
{
    class ViewCreator : MonoBehaviour
    {
        [SerializeField] private PieceView[] piecesPrefabs;
        [SerializeField] private Material whitePieceMaterial;
        [SerializeField] private Material blackPieceMaterial;
        private Dictionary<Team, Material> teamToMaterial = new Dictionary<Team, Material>();
        private Dictionary<PieceType, PieceView> pieceTypeToGameObject = new Dictionary<PieceType, PieceView>();

        private void Awake()
        {
            teamToMaterial.Add(Team.Black, blackPieceMaterial);
            teamToMaterial.Add(Team.White, whitePieceMaterial);
            foreach (var piece in piecesPrefabs)
            {
                pieceTypeToGameObject.Add(piece.GetComponent<PieceType>(), piece);
            }
        }

        public PieceView createPieceGameObject(Piece piece)
        {
            PieceView pieceGameObject = pieceTypeToGameObject[piece.Type];
            pieceGameObject.SetMaterial(teamToMaterial[piece.Team]);
            return pieceGameObject;
        }
    }
}