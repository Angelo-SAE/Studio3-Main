using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    [SerializeField] private GameObject warningPanel;
    private bool isSaveFile;

    public void LoadScene(string sceneName)
    {
      SceneManager.LoadScene(sceneName);
    }

    public void CheckForLoadScene(string sceneName)
    {
      if(isSaveFile)
      {
        warningPanel.SetActive(true);
      } else {
        SceneManager.LoadScene(sceneName);
      }
    }

    public void QuitApplication()
    {
      Application.Quit();
    }

    public void ThereIsSave()
    {
      isSaveFile = true;
    }
}
