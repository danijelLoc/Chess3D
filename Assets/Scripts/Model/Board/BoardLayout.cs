using System;
using System.Collections.Generic;

namespace Assets.Scripts.Model
{
    public class BoardLayout
    {
        public List<Piece> pieces;
        public Piece[,] Board
        {
            get
            {
                Piece[,] board = new Piece[8, 8];
                foreach (Piece piece in pieces)
                {
                    if (piece.IsAlive)
                        board[piece.CurrentSquare.X, piece.CurrentSquare.Y] = piece;
                }
                return board;
            }
        }

        public BoardLayout(List<Piece> pieces)
        {
            this.pieces = pieces;
        }
    }
}
