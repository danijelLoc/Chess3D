using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public enum PieceType
    {
        Pawn, Knight, Bishop, Rook, Queen, King
    }

    public enum Team
    {
        White, Black
    }

    [Serializable]
    public class Piece
    {
        [field: SerializeField]
        public PieceType Type { get; set; }
        [field: SerializeField]
        public Team Team { get; private set; }
        [field: SerializeField]
        public Vector2Int CurrentSquare { get; set; }

        public Piece(PieceType type, Team team, Vector2Int currentSquare)
        {
            this.Type = type;
            this.Team = team;
            this.CurrentSquare = currentSquare;
        }
    }
}