using System;
using UnityEngine;

namespace Assets.Scripts.Controller
{
    public interface IUndoRedoKeysHandler
    {
        void ProcessUndoCommandInput();
        void ProcessRedoCommandInput();
    }
}