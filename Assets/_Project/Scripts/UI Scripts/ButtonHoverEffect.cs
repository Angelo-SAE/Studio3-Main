using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour
{
    [SerializeField] private RectTransform uiToScale;
    [SerializeField] private Vector3 hoverScale;
    [SerializeField] private float verticalCast, horizontalCast;
    private RectTransform rectTransform;
    private Vector3 originalScale;
    private AudioSource audioSource;
    private bool isInside;


    private void Start()
    {
        originalScale = uiToScale.localScale;
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(mouseWorldPos.x > transform.position.x - horizontalCast && mouseWorldPos.x < transform.position.x + horizontalCast && mouseWorldPos.y > transform.position.y - verticalCast && mouseWorldPos.y < transform.position.y + verticalCast)
        {
            if(!isInside)
            {
                OnPointerEnter();
            }
        } else if(isInside)
        {
            OnPointerExit();
        }
    }

    public void OnPointerEnter()
    {
        isInside = true;
        uiToScale.localScale = hoverScale;

        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    public void OnPointerExit()
    {
        isInside = false;
        uiToScale.localScale = originalScale;
    }
}
