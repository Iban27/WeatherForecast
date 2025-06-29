using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WindowManagerSystem
{
    public class LoadingWindow : Window
    {
        private void Start()
        {
            SetPriority(9999);
        }
    }
}