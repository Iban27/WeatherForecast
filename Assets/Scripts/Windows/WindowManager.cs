using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace WindowManagerSystem
{
    public class WindowManager : MonoBehaviour
    {
        private List<Window> createdWindows = new List<Window>();
        [SerializeField] private Window[] windowsPrefabs;
        public static WindowManager Instance;
        private int _priority;
        private void Awake()
        {
            Instance = this;
        }

        public Window Open<T>() where T : Window
        {
            if (TryGetFromWindowList<T>(out var window))
            {
                return OpenWindow(window);
            }
            else
            {
                string typeName = typeof(T).Name;
                Window prefab = TryGetFromWindowPrefab(typeName);
                return CreateWindow(prefab);
            }
        }

        public Window Close<T>() where T : Window
        {
            if (TryGetFromWindowList<T>(out var window))
            {
                return CloseWindow(window);
            }
            return null;
        }

        private Window CreateWindow(Window windowPrefab)
        {
            var window = Instantiate(windowPrefab, transform);
            createdWindows.Add(window);
            _priority++;
            window.Init(_priority);
            window.Show();
            return window;
        }

        private Window OpenWindow(Window window)
        {
            window.Show();
            return window;
        }

        private Window CloseWindow(Window window)
        {
            window.Hide();
            return window;
        }

        private bool TryGetFromWindowList<T>(out Window window) where T : Window
        {
            var windowType = typeof(T);
            foreach (var windowInList in createdWindows)
            {
                if (windowType.Name == windowInList.GetType().Name)
                {
                    window = windowInList;
                    return true;
                }
            }
            window = null;
            return false;
        }

        private Window TryGetFromWindowPrefab(string typeName)
        {
            foreach (var window in windowsPrefabs)
            {
                string windowName = window.GetType().Name;
                if (string.Equals(windowName, typeName))
                {
                    return window;
                }
            }
            throw new ArgumentException("Type name is incorrect");
        }
    }
}
