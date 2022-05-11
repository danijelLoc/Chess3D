using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Model
{
    public class Piece
    {
        public PieceType Type { get; set; }
        public Team Team { get; private set; }
        public Vector2Integer CurrentSquare { get; set; }

        public Piece(PieceType type, Team team, Vector2Integer currentSquare)
        {
            this.Type = type;
            this.Team = team;
            this.CurrentSquare = currentSquare;
        }
    }
}