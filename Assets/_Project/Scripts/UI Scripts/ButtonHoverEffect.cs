using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rectTransform;
    private Vector3 originalScale;
    public Vector3 hoverScale = new Vector3(0.9f, 0.9f, 0.9f);
    private AudioSource audioSource;
    
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;
        audioSource = GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rectTransform.localScale = hoverScale;

        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rectTransform.localScale = originalScale;
    }
}
