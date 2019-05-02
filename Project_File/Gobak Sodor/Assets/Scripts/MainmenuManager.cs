using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainmenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject panel_mainmenu;
    [SerializeField] private GameObject panel_credits;
    [SerializeField] private GameObject panel_settings;
    [SerializeField] private GameObject panel_tutorial;
    [SerializeField] private GameObject panel_quit;
    void Start()
    {
        panel_mainmenu.SetActive(true);
    }

    public void ShowPanelCredits(bool _active)
    {
        panel_mainmenu.SetActive(!_active);
        panel_credits.SetActive(_active);
    }

    public void ShowPanelSettings(bool _active)
    {
        panel_mainmenu.SetActive(!_active);
        panel_settings.SetActive(_active);
    }

    public void ShowPanelTutorial(bool _active)
    {
        panel_tutorial.SetActive(_active);
    }

    public void ShowPanelQuit(bool _active)
    {
        panel_mainmenu.SetActive(!_active);
        panel_quit.SetActive(_active);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
