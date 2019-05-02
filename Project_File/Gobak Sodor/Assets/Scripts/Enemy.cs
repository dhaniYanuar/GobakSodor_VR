using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;

    private float enemySpeed = 0.5f;
    private float roamSpeed = 15f;
    private const float minDist = 40f;
    [SerializeField]private int direction = 1;

    void Update()
    {
        
        Move();
    }

    private void Move()
    {
        float dist = Mathf.Abs(player.position.z - transform.position.z);
        if (dist < minDist)
        {
            transform.position = new Vector3(Vector3.Lerp(transform.position, player.transform.position, enemySpeed*Time.deltaTime).x, transform.position.y, transform.position.z);
        }
        else
        {
            transform.Translate(new Vector3(roamSpeed * Time.deltaTime * direction, 0, 0));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            direction *= -1;
        }else if(other.gameObject.tag == "Player")
        {
            EasySurvivalScripts.PlayerMovement player = FindObjectOfType<EasySurvivalScripts.PlayerMovement>();
            player.GameOver();
        }
    }
}
