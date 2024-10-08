using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private BoolObject gamePause, pause;
    [SerializeField] private GameObject pauseMenuObj, optionsMenuObj;
    private bool pauseActive, inOptions;

    private void Update()
    {
      if(Input.GetKeyDown(KeyCode.Escape) && !pause.value && !gamePause.value && !pauseActive)
      {
        PauseGame();
      } else if(Input.GetKeyDown(KeyCode.Escape) && pauseActive)
      {
        if(inOptions)
        {
          CloseOptions();
        } else {
          UnPauseGame();
        }
      }
    }

    public void PauseGame()
    {
      Time.timeScale = 0;
      gamePause.SetTrue();
      pause.SetTrue();
      pauseActive = true;
      pauseMenuObj.SetActive(true);
    }

    public void UnPauseGame()
    {
      Time.timeScale = 1;
      gamePause.SetFalse();
      pause.SetFalse();
      pauseActive = false;
      pauseMenuObj.SetActive(false);
    }

    public void OpenOptions()
    {
      inOptions = true;
      optionsMenuObj.SetActive(true);
    }

    public void CloseOptions()
    {
      optionsMenuObj.SetActive(false);
      inOptions = false;
    }
}
