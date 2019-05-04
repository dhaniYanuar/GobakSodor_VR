using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(1f, 15f)]
    public float walkSpeed;
    [Range(1f, 15f)]
    public float runSpeed;
    [Range(0f, 15f)]
    public float JumpForce;
    public Transform FootLocation;
    [SerializeField]
    private List<GameObject> listEnemy = new List<GameObject>();
    [SerializeField]
    private GameObject finishLane;
    [SerializeField]
    private GameObject startLane;
    private int direction = 1;
    private int round;
    private bool isPaused;

    public int Round { get => round; set => round = value; }
    public bool IsPaused { get => isPaused; set => isPaused = value; }

    void Start()
    {
        IsPaused = false;
    }

    void MoveForward()
    {
        //Debug.Log("Move forward");
        transform.Translate(new Vector3(0, 0, walkSpeed * Time.deltaTime * direction));
    }

    void MoveToFinishLane()
    {
        if (round == 1)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, Vector3.Lerp(transform.position, finishLane.transform.position, walkSpeed / 4 * Time.deltaTime).z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, Vector3.Lerp(transform.position, startLane.transform.position, walkSpeed / 4 * Time.deltaTime).z);
        }
    }

    public int GetEnemyInFront()
    {
        int indexEnemy = -1;
        for (int i = 0; i < listEnemy.Count; i++)
        {
            if (round == 1)
            {
                if (listEnemy[i].transform.position.z > transform.position.z)
                {
                    indexEnemy = i;
                    break;
                }
            }
            else
            {
                if (listEnemy[i].transform.position.z < transform.position.z)
                {
                    indexEnemy = i;
                    break;
                }
            }
            
        }
        return indexEnemy;
    }

    public void BackToStartLine()
    {
        transform.position = transform.position;
        listEnemy.Reverse();
        Debug.Log("Player standby");
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPaused)
        {
            return;
        }
        if(GetEnemyInFront() != -1)
        {
            float distanceWithNearesEnemyX = listEnemy[GetEnemyInFront()].transform.position.x - transform.position.x;
            float distanceWithNearesEnemyZ = listEnemy[GetEnemyInFront()].transform.position.z - transform.position.z;
            //Debug.Log("enemy : " + (GetEnemyInFront() + 1) + " ,distance x : " + distanceWithNearesEnemyX + "distance z : " + distanceWithNearesEnemyZ);
            if (Mathf.Abs(distanceWithNearesEnemyX) > 20)
            {
                MoveForward();
            }else if(Mathf.Abs(distanceWithNearesEnemyZ) > 15)
            {
                MoveForward();
            }else if((Mathf.Abs(distanceWithNearesEnemyX) < 20 && Mathf.Abs(distanceWithNearesEnemyX) > 10) && Mathf.Abs(distanceWithNearesEnemyZ) < 5)
            {
                MoveForward();
            }
        }
        else
        {
            MoveToFinishLane();
        }
    }
}
