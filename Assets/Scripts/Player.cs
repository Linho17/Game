using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public enum StatusPlayer 
{
    None,
    Shielded
}

public class Player : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private ShieldButton shieldButton;

    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private Material shieldMaterial;
    [SerializeField] private Material playerMaterial;

    [SerializeField] private GameObject deathEffect;
    [SerializeField] private GameObject winEffect;


    public UnityEvent OnDie;
    public UnityEvent OnWin;
    bool isWin;

    private StatusPlayer statusPlayer;
    public StatusPlayer StatusPlayer
    {
        private set 
        { 
            statusPlayer = value;
            switch (statusPlayer)
            {
                case StatusPlayer.None:
                    renderer.material = playerMaterial;
                    break;
                case StatusPlayer.Shielded:
                    renderer.material = shieldMaterial;
                    break;
                default:
                    break;
            }
        }
        get => statusPlayer;
    }
    



    private void Awake()
    {
        shieldButton.OnPress.AddListener(() =>
        {
            StatusPlayer = StatusPlayer.Shielded;
        });


        shieldButton.OnRelease.AddListener(() =>
        {
            StatusPlayer = StatusPlayer.None;
        });

        OnDie.AddListener(Die);
        OnWin.AddListener(Win);

    }

    public void SetStartPosition(Vector3 position)
    {
        isWin = false;
        transform.position = position;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void SetTarget(Vector3 target)
    {
        
        agent.SetDestination(target);
    }


    private void OnTriggerEnter(Collider other)
    {
        
        PlateFloor plate = other.GetComponent<PlateFloor>();

        if (plate)
        {
            switch (plate.GetPlateState())
            {
                case PlateState.FinishZone:
                    if(!isWin)
                        OnWin?.Invoke();
                    break;
                case PlateState.DeathZone:
                    if (StatusPlayer == StatusPlayer.None)
                    {
                        OnDie?.Invoke();
                        
                    }

                    break;

            }
        }
    }

    private void Win()
    {
        
        isWin = true;
        Instantiate(winEffect, transform.position, Quaternion.identity);
    }

    private void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        
    }
}
