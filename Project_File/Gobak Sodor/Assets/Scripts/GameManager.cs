using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int Round { get => round; set => round = value; }
    public int TotalArriveAtEndPoint { get => totalArriveAtEndPoint; set => totalArriveAtEndPoint = value; }
    public bool IsPaused { get => isPaused; set => isPaused = value; }

    private int round;
    private int totalArriveAtEndPoint;
    private bool isPaused;
    [SerializeField] private List<GameObject> listAllies = new List<GameObject>();
    [SerializeField] private List<GameObject> listEnemies = new List<GameObject>();
    [SerializeField] private GameObject uiManager;

    void Start()
    {
        IsPaused = false;
        round = 1;
        AlliesUpdateRound();
    }

    private void AlliesUpdateRound()
    {
        for (int i = 0; i < listAllies.Count; i++)
        {
            listAllies[i].GetComponent<Ally>().Round = round;
        }
    }

    public void CheckRotateFaceAllies()
    {
        if (CheckIsAllArriveAtEndPoint())
        {
            for (int i = 0; i < listAllies.Count; i++)
            {
                listAllies[i].transform.rotation = new Quaternion(0, -180, 0, 0);
            }
            for (int i = 0; i < listEnemies.Count; i++)
            {
                listEnemies[i].transform.rotation = new Quaternion(0, 0, 0, 0);
            }
            round = 2;
            totalArriveAtEndPoint = 0;
            AlliesUpdateRound();
            Debug.Log("round 1 complete");
            Debug.Log("now round " + round);
        }
    }

    public void CheckLevelComplete()
    {
        if (CheckIsAllArriveAtEndPoint())
        {
            Debug.Log("level complete");
            Pause();
            uiManager.GetComponent<UIManager>().ShowPanelVictory();
        }
    }

    private bool CheckIsAllArriveAtEndPoint()
    {
        if(totalArriveAtEndPoint == (listAllies.Count + 1))
        {
            return true;
        }
        return false;
    }

    public void Pause()
    {
        isPaused = true;
        foreach (var ally in listAllies)
        {
            ally.GetComponent<Ally>().IsPaused = true;
        }
        foreach (var enemy in listEnemies)
        {
            enemy.GetComponent<Enemy>().IsPaused = true;
        }
        EasySurvivalScripts.PlayerMovement player = FindObjectOfType<EasySurvivalScripts.PlayerMovement>();
        player.IsPaused = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
