using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.View;
using Assets.Scripts.Model;

namespace Assets.Scripts.Controller
{

    [RequireComponent(typeof(ViewCreator))]
    public class Controller : MonoBehaviour
    {
        private ViewCreator viewCreator;

        private void Awake()
        {
            viewCreator = GetComponent<ViewCreator>();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}