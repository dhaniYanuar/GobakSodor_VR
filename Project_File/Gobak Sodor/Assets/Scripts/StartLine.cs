using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLine : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private void OnTriggerEnter(Collider other)
    {
        if (gameManager.Round == 2)
        {
            if (other.gameObject.tag == "Player")
            {
                gameManager.TotalArriveAtEndPoint++;
                Debug.Log("player arrive at start line " + gameManager.TotalArriveAtEndPoint);
                gameManager.CheckLevelComplete();
            }
            if (other.gameObject.tag == "Ally")
            {
                gameManager.TotalArriveAtEndPoint++;
                Debug.Log("player arrive at start line " + gameManager.TotalArriveAtEndPoint);
                gameManager.CheckLevelComplete();
            }
        }
    }
}
