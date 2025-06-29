using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace WindowManagerSystem
{
    public abstract class Window : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;
        private Canvas _canvas;

        public void Init(int priority)
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvas = GetComponent<Canvas>();
            SetPriority(priority);
        }

        public virtual void Show()
        {
            _canvasGroup.alpha = 0;
            gameObject.SetActive(true);
            _canvasGroup.DOFade(1, 0.5f);
        }

        public virtual void Hide() 
        {
            gameObject.SetActive(false);
            _canvasGroup.DOFade(0, 0.5f);
        }

        public void SetPriority(int priority)
        {
            _canvas.sortingOrder = priority;
        }
    }
}
