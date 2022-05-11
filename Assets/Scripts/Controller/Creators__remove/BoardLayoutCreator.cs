using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Model;

namespace Assets.Scripts.Controller
{
    [CreateAssetMenu(menuName = "Scriptable Objects/BoardLayoutCreator")]
    public class BoardLayoutCreator : ScriptableObject
    {
        public PieceCreator[] pieces;
    }
}
