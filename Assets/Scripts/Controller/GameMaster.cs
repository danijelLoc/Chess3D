using System;
using UnityEngine;
using Assets.Scripts.Model;

namespace Assets.Scripts.Controller
{
    public class GameMaster: MonoBehaviour
    {
        private GameManager gameManager;

        public GameMaster()
        {
            gameManager = new GameManager();
        }
    }
}
