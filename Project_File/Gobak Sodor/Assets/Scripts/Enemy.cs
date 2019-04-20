using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;

    private float enemySpeed = 9f;
    private float roamSpeed = 15f;
    private float minDist = 60f;
    [SerializeField]private int direction = 1;

    void Update()
    {
        Move();
    }

    private void Move()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < minDist)
        {
            transform.LookAt(player);
            transform.position += new Vector3(transform.forward.x, 0, 0) * enemySpeed * Time.deltaTime;
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
