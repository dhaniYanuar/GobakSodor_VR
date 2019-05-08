using System.Collections;
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
    public GameObject game;
    public Camera cam;

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

    public void ShowPanelVictory()
    {
        panel_victory.SetActive(true);
        EventSystem.current.SetSelectedGameObject(panel_victory.transform.GetChild(panel_victory.transform.childCount - 1).gameObject);
    }

    public void ShowPanelFail()
    {
        panel_fail.SetActive(true);
        EventSystem.current.SetSelectedGameObject(panel_fail.transform.GetChild(panel_fail.transform.childCount - 1).gameObject);
    }

    public void ContinueLevel()
    {
        if(SceneManager.GetActiveScene().name == "Level1")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
        
    }

    public void Save()
    {
        GameManager gm = FindObjectOfType<GameManager>();
        PlayerPrefs.SetInt("LEVEL", gm.Round);
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
