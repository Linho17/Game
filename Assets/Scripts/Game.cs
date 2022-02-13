using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private PhantomPlayer phantomPlayer;
    private Transform startPlate;
    private Transform finishPlate;
    //[SerializeField] private Walls walls;
    //[SerializeField] private Plates plates;
    [SerializeField] private Field field;
 
    [SerializeField] private NavMeshSurface surface;
    [SerializeField] private UIController uIController;
    private float animationDuration = 1;

    bool isGame;

    private IEnumerator NewGame()
    {
        isGame = false;
        while (true)
        {
            field.GenerateField(out startPlate, out finishPlate);
            surface.BuildNavMesh();
            phantomPlayer.SetStartPosition(startPlate.position);
            phantomPlayer.SetTarget(finishPlate.position);
           
            yield return new WaitForSeconds(3);
            if (phantomPlayer.CanGetThroughTheMaze() || Input.GetKeyDown(KeyCode.Escape))
            {
                isGame = true;
                RespawnPlayer();
                break;
            }
        }
       
    }

    private void RespawnPlayer()
    {
        player.SetStartPosition(startPlate.position);
        SetStartPlate();
        Invoke(nameof(SetFinishPlate), 2f);
    }

    private void SetFinishPlate()
    {
        player.SetTarget(finishPlate.position);
    }
    private void SetStartPlate()
    {
        player.SetTarget(startPlate.position);
    }

    private IEnumerator Start()
    {
        
        StartCoroutine(NewGame());
        yield return new WaitUntil(() => isGame==true);
        uIController.FromBlack();
        
        player.OnDie.AddListener(RespawnPlayer);
        player.OnWin.AddListener(()=>
        {
            Invoke(nameof(WinGame), animationDuration*2);
        });
    }

    private void WinGame()
    {
        uIController.ToBlack(true);
        Invoke(nameof(ReloadLevel), animationDuration);
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(0);
    }



}
