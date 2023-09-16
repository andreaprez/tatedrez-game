using System;
using UnityEngine;

namespace Tatedrez.Utils
{
    public class InputHandler : MonoBehaviour
    {
        public static InputHandler Instance = null;
        
        public Action<Vector3> OnTouchEvent;

        private bool inputBlocked;
        
        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            if (inputBlocked) return;
            
#if UNITY_ANDROID || UNITY_IOS || UNITY_IPHONE
            if (Input.touchCount > 0)
            {
                switch (Input.touches[0].phase)
                {
                    case TouchPhase.Ended:
                        OnTouchEvent?.Invoke(Input.touches[0].position);
                        break;
                }
            }
#endif

#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                OnTouchEvent?.Invoke(Input.mousePosition);
            }
#endif
        }
        
        public void SetInputBlocked(bool value)
        {
            inputBlocked = value;
        }
        
        public void ClearEvents()
        {
            OnTouchEvent = null;
        }
    }
}