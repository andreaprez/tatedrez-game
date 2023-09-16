using TMPro;
using UnityEngine;

namespace Tatedrez
{
    public class FrameRateHandler : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI fpsText;

        private float timer = 0.5f;
        private float elapsed = 0f;

        private void Awake()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }

        private void Update()
        {
            elapsed += Time.deltaTime;

            if (elapsed >= timer)
            {
                elapsed = 0f;
                
                int fps = (int)(1.0f / Time.deltaTime);
                fpsText.text = "FPS: " + fps;
            }
        }
    }
}