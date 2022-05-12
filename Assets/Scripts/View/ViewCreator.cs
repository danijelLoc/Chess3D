using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Model;

namespace Assets.Scripts.View
{
    class ViewCreator : MonoBehaviour
    {
        [SerializeField] private PieceView[] piecesPrefabs;
        [SerializeField] private Material whitePieceMaterial;
        [SerializeField] private Material blackPieceMaterial;
        [SerializeField] private BoardView boardView;
        private Dictionary<Team, Material> teamToMaterial = new Dictionary<Team, Material>();
        private Dictionary<PieceType, PieceView> pieceTypeToPrefab = new Dictionary<PieceType, PieceView>();

        private void Awake()
        {
            teamToMaterial.Add(Team.Black, blackPieceMaterial);
            teamToMaterial.Add(Team.White, whitePieceMaterial);
            foreach (var piecePrefab in piecesPrefabs)
            {
                pieceTypeToPrefab.Add(piecePrefab.PieceType, piecePrefab);
            }
        }

        public PieceView CreatePieceView(Piece piece)
        {
            PieceView piecePrefab = pieceTypeToPrefab[piece.Type];
            PieceView newPiece = Instantiate(piecePrefab);
            newPiece.SetMaterial(teamToMaterial[piece.Team]);
            newPiece.SetBoard(boardView);
            piece.observer = newPiece;
            return newPiece;
        }
    }
}