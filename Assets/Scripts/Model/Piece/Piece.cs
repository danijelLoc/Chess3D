using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.Model
{
    public class Piece
    {
        private int id;
        public PieceType Type { get; private set; }
        public Team Team { get; private set; }
        public Vector2Integer CurrentSquare { get; private set; }
        public int MoveCounter { get; private set; }
        public Boolean IsAlive { get; private set; }
        public Boolean Selected { get; private set; }

        public IPieceObserver observer;

        public Piece(PieceType type, Team team, Vector2Integer currentSquare, Boolean isAlive = true)
        {
            this.id = (int)team * 100 + (int)type + currentSquare.X + currentSquare.Y;
            this.Type = type;
            this.Team = team;
            this.CurrentSquare = currentSquare;
            this.Selected = false;
            this.IsAlive = isAlive;
            this.MoveCounter = 0;
        }

        public override int GetHashCode()
        {
            return (Type, Team, CurrentSquare).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            } 
            else
            {
                Piece p = (Piece)obj;
                return id == p.id;
            }
        }

        public void SetIsSelected(Boolean isSelected)
        {
            Selected = isSelected;
            observer?.UpdateSelection(isSelected);
        }

        public void Promote()
        {
            if (Type != PieceType.Pawn) { throw new Exception("Promotion from pawns is only allowed"); }
            Type = PieceType.Queen;
            observer.UpdatePieceType(this, Type);
            Debug.Log("changed type");
        }

        public void Demote()
        {
            if (Type != PieceType.Queen) { throw new Exception("Demotion from queens is only allowed"); }
            Type = PieceType.Pawn;
            observer.UpdatePieceType(this, Type);
            Debug.Log("changed type");
        }

        public void SetIsAlive(Boolean isAlive)
        {
            IsAlive = isAlive;
            observer?.UpdateIsAlive(isAlive);
        }

        public void MoveTo(Vector2Integer destinationSquare, Boolean isUndo = false)
        {
            CurrentSquare = destinationSquare;
            observer?.UpdateSquareLocation(destinationSquare);
            if (isUndo)
                MoveCounter--;
            else
                MoveCounter++;
        }
    }
}