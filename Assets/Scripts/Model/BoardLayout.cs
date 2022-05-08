using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Model
{
    [CreateAssetMenu(menuName = "Scriptable Objects/BoardLayout")]
    public class BoardLayout : ScriptableObject
    {
        [SerializeField]
        public Piece[] pieces;
    }
}
