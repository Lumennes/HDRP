using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text qualityText;
    [SerializeField] GameObject media; //sinematic
#if !UNITY_EDITOR
    bool fullscreen;
#endif

    // Start is called before the first frame update
    void Start()
    {
        ShowQualityLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            SetQualityLevel(0);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            SetQualityLevel(1);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            SetQualityLevel(2);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (media)
                media.SetActive(!media.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
                UnityEditor.EditorWindow.focusedWindow.maximized = !UnityEditor.EditorWindow.focusedWindow.maximized;
#else
                //Screen.fullScreen = !Screen.fullScreen;
                fullscreen = !fullscreen;
                if (!fullscreen)
                    Screen.SetResolution(638, 460, false);
                else
                    Screen.SetResolution(1280, 1024, true);
#endif
        }

    }

    void SetQualityLevel(int qualityLevel)
    {
        QualitySettings.SetQualityLevel(qualityLevel, true);
        ShowQualityLevel();
    }

    void ShowQualityLevel()
    {
        var qualityName = QualitySettings.names[QualitySettings.GetQualityLevel()];
        Debug.Log(qualityName);
        if (qualityText)
            qualityText.text = qualityName;
    }
}
