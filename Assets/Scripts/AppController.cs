using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using WindowManagerSystem;

public class AppController : MonoBehaviour
{
    async void Start()
    {
        //WindowManager.Instance.Open<LoadingWindow>();
        //await Task.Delay(10);
        //WindowManager.Instance.Close<LoadingWindow>();
        WindowManager.Instance.Open<StartMenuWindow>();
    }
}
