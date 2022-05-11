using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Model;

namespace Assets.Scripts.Controller
{

    [Serializable]
    public class PieceCreator
    {
        [field: SerializeField]
        public PieceType Type { get; set; }
        [field: SerializeField]
        public Team Team { get; private set; }
        [field: SerializeField]
        public Vector2Int CurrentSquare { get; set; }

        public PieceCreator(PieceType type, Team team, Vector2Int currentSquare)
        {
            this.Type = type;
            this.Team = team;
            this.CurrentSquare = currentSquare;
        }
    }
}