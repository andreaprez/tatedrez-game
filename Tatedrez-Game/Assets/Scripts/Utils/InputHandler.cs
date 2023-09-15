using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public static Action<Vector3> OnTouchEvent;

    public static void ClearEvents()
    {
        OnTouchEvent = null;
    }
    
    private void Update()
    {
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
}
