using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChair : Interactable
{
    [SerializeField] private FloatObject stress;
    [SerializeField] private BoolObject paused;
    [SerializeField] private GameObjectObject player;
    [SerializeField] private Vector2 chairPosition, exitPositionRight, exitPositionDown, exitPositionLeft;
    [SerializeField] private float stressReduction, increasedStressReduction;
    public bool playerInChair;

    public AudioSource audioSource;

    private float originalVolume;

    private void Start()
    {
        // Store the original volume of the audio source
        originalVolume = audioSource.volume;
    }

    public override void Interact()
    {
      if(!paused.value)
      {
        paused.SetTrue();
        AddPlayerToChair();
      }
    }

    public override void AltInteract() {}

    public void UpgradeChair()
    {
      stressReduction = increasedStressReduction;
    }

    private void Update()
    {
      if(playerInChair)
      {
        stress.value -= stressReduction * Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.D))
        {
          RemovePlayerFromChair(0);
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
          RemovePlayerFromChair(1);
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
          RemovePlayerFromChair(2);
        }
      }
    }

    private void AddPlayerToChair()
    {
      player.value.GetComponent<Collider2D>().enabled = false;
      player.value.transform.position = chairPosition;
      playerInChair = true;

      StartCoroutine(FadeOutAudio(2f));
    }

    private void RemovePlayerFromChair(int direction)
    {
      switch(direction)
      {
        case(0):
        player.value.transform.position = exitPositionRight;
        break;
        case(1):
        player.value.transform.position = exitPositionDown;
        break;
        case(2):
        player.value.transform.position = exitPositionLeft;
        break;
      }

      player.value.GetComponent<Collider2D>().enabled = true;
      paused.SetFalse();
      playerInChair = false;

      StartCoroutine(FadeInAudio(2f));
    }

    private IEnumerator FadeOutAudio(float duration)
    {
        float startVolume = audioSource.volume;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, elapsedTime / duration);
            yield return null;
        }

        audioSource.volume = 0f;
    }

    private IEnumerator FadeInAudio(float duration)
    {
        float startVolume = audioSource.volume;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, originalVolume, elapsedTime / duration);
            yield return null;
        }

        audioSource.volume = originalVolume;
    }



}
