using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Model
{
    public class Piece
    {
        private PieceType type;

        public PieceType Type { get; private set; }
        public Team Team { get; private set; }
        public Vector2Integer CurrentSquare { get; private set; }
        public Boolean IsAlive { get; private set; }
        public Boolean Selected { get; private set; }

        public IPieceObserver observer;

        public Piece(PieceType type, Team team, Vector2Integer currentSquare, Boolean isAlive = true)
        {
            this.Type = type;
            this.Team = team;
            this.CurrentSquare = currentSquare;
            this.Selected = false;
            this.IsAlive = isAlive;
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
            } else
            {
                Piece p = (Piece)obj;
                return (Type == p.Type) && (Team == p.Team) &&
                    (CurrentSquare == p.CurrentSquare);
            }
        }

        public void SetIsSelected(Boolean isSelected)
        {
            Selected = isSelected;
            observer?.UpdateSelection(isSelected);
        }

        public void SetIsAlive(Boolean isAlive)
        {
            IsAlive = isAlive;
            observer?.UpdateIsAlive(isAlive);
        }

        public void MoveTo(Vector2Integer destinationSquare)
        {
            CurrentSquare = destinationSquare;
            observer.UpdateSquareLocation(destinationSquare);
        }
    }
}