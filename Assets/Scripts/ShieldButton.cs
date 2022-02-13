using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShieldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent OnPress;
    public UnityEvent OnRelease;


    [SerializeField] private Image image;
    [SerializeField] private float timePress;
    private float currentTimePress;
    private bool isPress;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPress?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnRelease?.Invoke();
    }

    private void Awake()
    {
        OnPress.AddListener(Press);
        OnRelease.AddListener(Release);
    }
   

    private void Press()
    {
        isPress = true;
        currentTimePress = timePress;
    }

    private void Release()
    {
        isPress = false;
        image.fillAmount = 1;
    }

    private void Update()
    {
        if (isPress)
        {
            currentTimePress -= Time.deltaTime;
            image.fillAmount = currentTimePress/timePress;
            if (currentTimePress <= 0)
            {
                OnRelease?.Invoke();
            }
        }
    }
}
