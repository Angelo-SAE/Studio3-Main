using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stress : MonoBehaviour
{
    [SerializeField] private FloatObject stress;
    [SerializeField] private Slider stressSlider;
    [SerializeField] private BoolObject inRestaurant;
    [SerializeField] private GameObjectObject frontCounter, cashier;
    private FrontCounter counter1;
    private Cashier counter2;
    [SerializeField] private float stressIncreaseCounters;
    [SerializeField] private Image stressBarBackground;
    [SerializeField] private RectTransform stressBarBackgroundScale;
    [SerializeField] private Color flashColor = Color.red;
    [SerializeField] private float flashDuration = 0.3f;
    private float previousStress = 0f;

    private bool isFlashing = false;

    public AudioSource audioSource;

    private float targetPitch;
    private float currentPitch;
    private float startingpitch;
    private float pitchLerpSpeed;
    public float audioSmoothTime;


    private void Start()
    {
      inRestaurant.value = false;
      stress.value = 0;
      UpdateSlider();
      counter1 = frontCounter.value.GetComponent<FrontCounter>();
      counter2 = cashier.value.GetComponent<Cashier>();
      StartCoroutine(UpdatePreviousStress());

      currentPitch = audioSource.pitch;
      targetPitch = audioSource.pitch;
      startingpitch = audioSource.pitch;
      pitchLerpSpeed = 1 / audioSmoothTime;

    }

    private void Update()
    {
      if(stressSlider.value != stress.value)
      {
        UpdateSlider();
      }

       
      stress.value += stressIncreaseCounters * Time.deltaTime;


      stress.value = Mathf.Max(0, stress.value);

        if (stress.value > previousStress && !isFlashing)
        {
            StartCoroutine(FlashStressBarBackground());
        }

        targetPitch = startingpitch + (stress.value * 0.0015f);
 
        currentPitch = Mathf.Lerp(currentPitch, targetPitch, pitchLerpSpeed * Time.deltaTime);

        audioSource.pitch = currentPitch;
    }

    private void UpdateSlider()
    {
      if(stress.value > stressSlider.maxValue)
      {
        stress.value = stressSlider.maxValue;
      } else if(stress.value < stressSlider.minValue)
      {
        stress.value = stressSlider.minValue;
      }
      stressSlider.value = stress.value;
    }

    private IEnumerator UpdatePreviousStress()
    {
        while (true)
        {
            previousStress = stress.value;
            yield return new WaitForSeconds(0.2f);
        }
    }

    private IEnumerator FlashStressBarBackground()
    {
        isFlashing = true;
        Color originalColor = stressBarBackground.color;
        Color targetColor = flashColor;
        targetColor.a = 1f;

        // Flash to red
        float elapsedTime = 0f;
        while (elapsedTime < flashDuration)
        {
            elapsedTime += Time.deltaTime;
            flashDuration = 0.5f + 0.25f * stress.value * 0.01f;
            stressBarBackground.color = Color.Lerp(targetColor, originalColor, elapsedTime / flashDuration);
            stressBarBackgroundScale.localScale = new Vector2(1 + (0.2f * stress.value * 0.01f), 1 + (0.05f * 0.01f * stress.value));
            yield return null;
        }

        
        stressBarBackground.color = originalColor;
        isFlashing = false;
    }
}
