﻿using UnityEngine;
using System.Collections.Generic;
using System;
using Assets.Scripts.Model;
using Assets.Scripts.View;

namespace Assets.Scripts.Controller
{
    public class BoardController : MonoBehaviour, IMouseClickHandler
    {
        private BoardView boardView;
        private GameManager gameManager;
        [SerializeField] private PieceViewCreator pieceViewCreator;


        private void Awake()
        {
            boardView = GetComponent<BoardView>();
        }

        private void Start()
        {
            InitialLayout();
        }

        public void ProcessInput(Vector3 inputPosition, GameObject selectedObject, Action onClick)
        {
            Vector2Integer squareLocation = boardView.SquareLocationFromPosition(inputPosition);
            gameManager.OnSquareSelected(squareLocation);
        }

        public void ShowLayout(Board boardLayout)
        {
            foreach (Piece piece in boardLayout.pieces)
            {
                PieceView pieceView = pieceViewCreator.CreatePieceView(piece);
                //pieceView.enabled = piece.Destoryed;
            }
        }

        private void InitialLayout()
        {
            Piece king = new Piece(PieceType.King, Team.Black, new Vector2Integer(2, 4));
            Piece pawn = new Piece(PieceType.Pawn, Team.White, new Vector2Integer(1, 1));
            List<Piece> pieces = new List<Piece> { king, pawn };
            Board board = new Board(pieces);
            board.boardMarkersObserver = boardView;
            gameManager = new GameManager(board);
            ShowLayout(board);
        }
    }
}