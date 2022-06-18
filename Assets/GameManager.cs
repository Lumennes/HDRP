using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text qualityText;
    [SerializeField] GameObject media; //sinematic

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
