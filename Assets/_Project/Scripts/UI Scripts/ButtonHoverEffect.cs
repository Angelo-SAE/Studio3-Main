using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private RectTransform uiToScale;
    [SerializeField] private Vector3 hoverScale;
    private RectTransform rectTransform;
    private Vector3 originalScale;
    private AudioSource audioSource;

    private void Start()
    {
        originalScale = uiToScale.localScale;
        audioSource = GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        uiToScale.localScale = hoverScale;

        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        uiToScale.localScale = originalScale;
    }
}
