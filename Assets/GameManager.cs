using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.HighDefinition;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text qualityText;
    [SerializeField] GameObject media; // CutScene
    bool fullscreen;

    [SerializeField] HDAdditionalCameraData _camera;
    [SerializeField] GameObject volumes;

    [SerializeField] GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        ShowQualityLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetQualityLevel(0);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetQualityLevel(1);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetQualityLevel(2);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            player.SetActive(media.activeSelf);
            if (media)
                media.SetActive(!media.activeSelf);
            
            Debug.Log($"CutScene: {(media.activeSelf ? "ON" : "OFF")}");
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            fullscreen = !fullscreen;
#if UNITY_EDITOR
            UnityEditor.EditorWindow.focusedWindow.maximized = fullscreen;//!UnityEditor.EditorWindow.focusedWindow.maximized;
#else
            //Screen.fullScreen = !Screen.fullScreen;
            if (!fullscreen)
                Screen.SetResolution(638, 460, false);
            else
                Screen.SetResolution(638, 460, true);//1280, 1024                
#endif
            Debug.Log($"FullScreen: {(fullscreen ? "ON" : "OFF")}");
        }

        // включаем/отклчаем сглаживание
        if (Input.GetKeyDown(KeyCode.N))
        {
            _camera.antialiasing =
                _camera.antialiasing == HDAdditionalCameraData.AntialiasingMode.TemporalAntialiasing ?
                HDAdditionalCameraData.AntialiasingMode.None :
                HDAdditionalCameraData.AntialiasingMode.TemporalAntialiasing;
            Debug.Log($"AntiAliasing: {(_camera.antialiasing == HDAdditionalCameraData.AntialiasingMode.TemporalAntialiasing ? "ON" : "OFF")}");
            RefreshText();
        }

        // вкл/выкл пост-процессинг через отключение Volumes
        if (Input.GetKeyDown(KeyCode.P))
        {
            volumes.SetActive(!volumes.activeSelf);
            Debug.Log($"PostProcessing: {(volumes.activeSelf ? "ON" : "OFF")}");
            RefreshText();
        }

        if (/*(Input.GetKeyDown(KeyCode.LeftControl)|| Input.GetKeyDown(KeyCode.RightControl)) && */Input.GetKeyDown(KeyCode.F11))
        {
            //if(!string.IsNullOrEmpty(qualityText.text))
            RefreshText(string.IsNullOrEmpty(qualityText.text));
            Debug.Log("ctrl+f11");
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
        RefreshText();
    }

    void RefreshText(bool show = true)
    {
        if (qualityText)
        {
            var text = string.Empty;
            if (show)
            {
                text = $"[1 - 3] {(QualitySettings.names[QualitySettings.GetQualityLevel()]).ToUpper()} \n" +
                  $"[P] PostProcessing: {(volumes.activeSelf ? "ON" : "OFF")} \n" +
                  $"[N] AntiAliasing: {(_camera.antialiasing == HDAdditionalCameraData.AntialiasingMode.TemporalAntialiasing ? "ON" : "OFF")}";
            }

            qualityText.text = text;
        }
    }
}
