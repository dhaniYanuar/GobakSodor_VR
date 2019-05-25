﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject panel_victory;
    [SerializeField] private GameObject panel_fail;
    [SerializeField] private GameObject panel_character_selection;
    public Canvas canvas;
    public GameObject game;
    public GameObject cam;

    
    public void SelectCharacter(int _index)
    {
        GameManager gm = FindObjectOfType<GameManager>();
        GameManager.characterSelected = _index;
        gm.SpawnPlayer(_index);
        panel_character_selection.SetActive(false);
        Destroy(cam);
        game.SetActive(true);
        Time.timeScale = 1;
    }

    public void MakeCanvasAsChild()
    {
        canvas.transform.parent = GameObject.FindGameObjectsWithTag("Player")[0].GetComponentInChildren<Camera>().transform;
    }

    public void ShowPanelVictory()
    {
        panel_victory.SetActive(true);
        EventSystem.current.SetSelectedGameObject(panel_victory.transform.GetChild(panel_victory.transform.childCount - 1).gameObject);
    }

    public void ShowPanelFail()
    {
        game.SetActive(false);
        panel_fail.SetActive(true);
        EventSystem.current.SetSelectedGameObject(panel_fail.transform.GetChild(panel_fail.transform.childCount - 1).gameObject);
    }

    public void ContinueLevel()
    {
        if(SceneManager.GetActiveScene().name == "Level1")
        {
            Debug.Log("continue level 1");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if(SceneManager.GetActiveScene().name == "Trial1")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (SceneManager.GetActiveScene().name == "Trial2")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            Debug.Log("back to main menu");
            SceneManager.LoadScene("MainMenu");
        }
        
    }

    public void Save()
    {
        GameManager gm = FindObjectOfType<GameManager>();
        PlayerPrefs.SetInt("LEVEL", gm.Level);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
