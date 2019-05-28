using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;

    [SerializeField]private float enemySpeed = 0.5f;
    [SerializeField]private float roamSpeed = 15f;
    private const float minDist = 40f;
    private bool isPaused = false;
    [SerializeField]private int direction = 1;
    [SerializeField]private int type = 1;

    public bool IsPaused { get => isPaused; set => isPaused = value; }

    void Update()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        }
        
        if (IsPaused)
        {
            return;
        }
        Move();
    }

    private void Move()
    {
        float dist = Mathf.Abs(player.position.z - transform.position.z);
        if (type == 1)
        {
            HorizontalMove(dist);
        }
        else
        {
            VerticalMove();
        }
        
    }

    private void VerticalMove()
    {
        if (player.position.z > -26 && player.position.z < 85)
        {
            transform.LookAt(player.transform);
            transform.position = new Vector3(transform.position.x, transform.position.y, Vector3.Lerp(transform.position, player.transform.position, enemySpeed * Time.deltaTime).z);
        }
        else
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
            transform.Translate(new Vector3(0, 0, roamSpeed * Time.deltaTime * direction));
        }
    }

    private void HorizontalMove(float _dist)
    {
        if (_dist < minDist)
        {
            transform.position = new Vector3(Vector3.Lerp(transform.position, player.transform.position, enemySpeed * Time.deltaTime).x, transform.position.y, transform.position.z);
        }
        else
        {
            //transform.Translate(new Vector3(roamSpeed * Time.deltaTime * direction, 0, 0));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            direction *= -1;
        }
        else if(other.gameObject.tag == "Player")
        {
            EasySurvivalScripts.PlayerMovement player = FindObjectOfType<EasySurvivalScripts.PlayerMovement>();
            UIManager uiManager = FindObjectOfType<UIManager>();
            uiManager.ShowPanelFail();
            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.Pause();
        }
        else if ((other.gameObject.tag == "BatasAwal" || other.gameObject.tag == "BatasAkhir") && type == 2)
        {
            direction *= -1;
        }

    }
}
