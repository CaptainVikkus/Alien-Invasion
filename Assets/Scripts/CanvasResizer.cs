using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasResizer : MonoBehaviour
{
    public TextMeshPro Score;
    private bool hasDeadZone = false;

    void Start()
    {
        hasDeadZone = !((Screen.width == Screen.safeArea.width) && (Screen.height == Screen.safeArea.height));
    }

    // Update is called once per frame
    void Update()
    {
        if (hasDeadZone)
        {
            switch (Input.deviceOrientation)
            {
                case DeviceOrientation.LandscapeLeft:
                    Score.rectTransform.anchoredPosition = new Vector2(200.0f, 50.0f);
                    break;
                case DeviceOrientation.LandscapeRight:
                    Score.rectTransform.anchoredPosition = new Vector2(175.0f, 50.0f);
                    break;
                case DeviceOrientation.Portrait:
                    Score.rectTransform.anchoredPosition = new Vector2(175.0f, 50.0f);
                    break;
                case DeviceOrientation.Unknown:
                    Debug.Log("Ooops, device orientation unkown");
                    break;
            }
        }
    }
}
