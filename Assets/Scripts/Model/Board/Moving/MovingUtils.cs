using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Model
{
    public class MovingUtils
    {
        public static readonly List<Vector2Integer> diagonalDirections = new List<Vector2Integer> {
                new Vector2Integer(1, 1), new Vector2Integer(-1, -1),
                new Vector2Integer(1, -1), new Vector2Integer(-1, 1)
        };

        public static readonly List<Vector2Integer> lineDirections = new List<Vector2Integer> {
                new Vector2Integer(0, 1), new Vector2Integer(0, -1),
                new Vector2Integer(1, 0), new Vector2Integer(-1, 0)
        };

        public static readonly List<Vector2Integer> knightDirections = new List<Vector2Integer> {
                new Vector2Integer(2,1), new Vector2Integer(2,-1),
                new Vector2Integer(-2,1), new Vector2Integer(-2,-1),
                new Vector2Integer(1,2), new Vector2Integer(1,-2),
                new Vector2Integer(-1,2), new Vector2Integer(-1,-2),
        };

        public static int LeftCastlingKingXLocation = 2;
        public static int LeftCastlingRookXLocation = 3;
        public static int RightCastlingKingXLocation = 6;
        public static int RightCastlingRookXLocation = 5;

        public static Piece FirstPieceInDirection (Vector2Integer direction, Piece selectedPiece, Board board)
        {
            int i = 1;
            while (true) {
                Vector2Integer potentialSquare = selectedPiece.CurrentSquare + direction * i;
                if (!Board.IsSquareInsideBoard(potentialSquare))
                    break;

                var firstPiece = board.Layout[potentialSquare.X, potentialSquare.Y];
                if (firstPiece != null)
                    return firstPiece;
                i++;
            }
            return null;
        }

        public static int PawnYDirection(Team team)
        {
            return team == Team.White ? 1 : -1;
        }

        public static Vector2Integer PawnLineDirection(Team team)
        {
            return new Vector2Integer(0, PawnYDirection(team));
        }

        public static List<Vector2Integer> PawnAttackDirections(Team team)
        {
            return new List<Vector2Integer> { new Vector2Integer(-1, PawnYDirection(team)), new Vector2Integer(1, PawnYDirection(team)) };
        }

        public static int PawnTwoSquareAdvanceYLocation(Team team)
        {
            return team == Team.White ? 3 : 4;
        }

        public static int PawnPromotionYLocation(Team team)
        {
            return team == Team.White ? 7 : 0;
        }
    }
}
