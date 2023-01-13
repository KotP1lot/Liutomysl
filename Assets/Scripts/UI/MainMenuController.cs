using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class MainMenuController : MonoBehaviour
{
    public TextMeshProUGUI topButtonText;
    public GameObject deleteSaveButton;
    public SceneTransition sceneTransition;
    public string saveFileToCheck;

    // Start is called before the first frame update
    void Start()
    {
        if (SaveFileExists())
        {
            topButtonText.text = "Продовжити гру";
            deleteSaveButton.SetActive(true);
        }
        else 
        {
            topButtonText.text = "Нова гра";
            deleteSaveButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool SaveFileExists()
    {
        var path = Path.Combine(Application.persistentDataPath, saveFileToCheck);
        return File.Exists(path);
    }

    public void DeleteSaveFile()
    {
        if (SaveFileExists())
        {
            var path = Path.Combine(Application.persistentDataPath, saveFileToCheck);
            File.Delete(path);

            sceneTransition.LoadNextLevel(0);
        }
    }
}
