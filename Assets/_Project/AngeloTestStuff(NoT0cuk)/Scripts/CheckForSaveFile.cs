using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class CheckForSaveFile : MonoBehaviour
{
    [SerializeField] private Button loadButton;
    [SerializeField] private SceneManage sceneManage;
    private string path;

    private void Awake()
    {
      path = Application.streamingAssetsPath + "/SavedGame/currentSave";
      if(File.Exists(path))
      {
        loadButton.interactable = true;
        sceneManage.ThereIsSave();
      }
    }
}
