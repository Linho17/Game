using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button continueButton;

    private void Awake()
    {
        pauseButton.onClick.AddListener(() =>
        {
            ToBlack(false);
           
            continueButton.gameObject.SetActive(true);
        });

        continueButton.onClick.AddListener(() =>
        {
            FromBlack();
            
            continueButton.gameObject.SetActive(false);
        });


    }

    public void GoTime()
    {
        Time.timeScale = 1;
    }

    public void StopTime()
    {
        Time.timeScale = 0;
    }

    public void FromBlack()
    {
        animator.SetTrigger("Show");
    }

    public void ToBlack(bool isWin)
    {
        if (isWin)
            return;
        animator.SetTrigger("Hide");
    }
}

