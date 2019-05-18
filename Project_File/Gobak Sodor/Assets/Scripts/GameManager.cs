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
    public static int characterSelected;
    private bool isPaused;
    [SerializeField] private List<GameObject> listAllies = new List<GameObject>();
    [SerializeField] private List<GameObject> listEnemies = new List<GameObject>();
    [SerializeField] private GameObject game;
    [SerializeField] private GameObject uiManager;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject allySpawnPoint;
    [SerializeField] private GameObject[] player;
    [SerializeField] private GameObject[] ally;

    void Start()
    {
        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 1)
        {
            Time.timeScale = 0;
        }
        else
        {
            if(characterSelected == 0)
            {
                SpawnPlayer(0);
                SpawnAlly(1);
            }
            else
            {
                SpawnPlayer(1);
                SpawnAlly(0);
            }
        }

        IsPaused = false;
        round = 1;
        if (!PlayerPrefs.HasKey("LEVEL"))
        {
            PlayerPrefs.SetInt("LEVEL", round);
        }
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
            game.SetActive(false);
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

    public void SpawnPlayer(int _index)
    {
        Instantiate(player[_index], spawnPoint.transform.position, Quaternion.identity);
        uiManager.GetComponent<UIManager>().MakeCanvasAsChild();
        if (_index == 0)
        {
            SpawnAlly(1);
        }
        else
        {
            SpawnAlly(0);
        }
    }

    public void SpawnAlly(int _index)
    {
        GameObject ally3 = Instantiate(ally[_index], allySpawnPoint.transform.position, Quaternion.identity);
        listAllies.Add(ally3);
        ally3.tag = "Ally";
        ally3.AddComponent<Ally>();
        for (int i = 0; i < listEnemies.Count; i++)
        {
            ally3.GetComponent<Ally>().listEnemy.Add(listEnemies[i]);
        }
        ally3.GetComponent<Ally>().Round = Round;
        ally3.GetComponent<Ally>().walkSpeed = 9f;
        ally3.GetComponent<Ally>().runSpeed = 10f;
        ally3.GetComponent<Ally>().JumpForce = 0.2f;
        StartCoroutine(AddStartFinishAlly(ally3));
    }

    IEnumerator AddStartFinishAlly(GameObject ally3)
    {
        yield return new WaitForSeconds(1f);
        ally3.transform.parent = GameObject.Find("Game").transform;
        ally3.GetComponent<Ally>().startLane = FindObjectOfType<StartLine>().gameObject;
        ally3.GetComponent<Ally>().finishLane = FindObjectOfType<FinishLine>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
