using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private void OnTriggerEnter(Collider other)
    {
        if(gameManager.Round == 1)
        {
            if (other.gameObject.tag == "Player")
            {
                gameManager.TotalArriveAtEndPoint++;
                Debug.Log("player arrive at finish line " + gameManager.TotalArriveAtEndPoint);
                gameManager.CheckRotateFaceAllies();
            }
            if (other.gameObject.tag == "Ally")
            {
                gameManager.TotalArriveAtEndPoint++;
                Debug.Log("player arrive at finish line " + gameManager.TotalArriveAtEndPoint);
                other.GetComponent<Ally>().BackToStartLine();
                gameManager.CheckRotateFaceAllies();
            }
        }
    }
}
