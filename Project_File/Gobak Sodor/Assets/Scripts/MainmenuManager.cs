using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainmenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField] private GameObject panel_mainmenu;
    [SerializeField] private GameObject panel_credits;
    [SerializeField] private GameObject panel_settings;
    [SerializeField] private GameObject panel_tutorial;
    [SerializeField] private GameObject panel_quit;
    [SerializeField] private Button btn_start;
    [SerializeField] private List<GameObject> listOfTutorialContent = new List<GameObject>();
    bool isOpenTutorial;
    int indexTutorial;
    void Start()
    {
        isOpenTutorial = false;
        indexTutorial = 0;
        panel_mainmenu.SetActive(true);
    }

    public void ShowPanelCredits(bool _active)
    {
        panel_mainmenu.SetActive(!_active);
        panel_credits.SetActive(_active);
        SetSelectableObject(panel_credits);
    }

    public void ShowPanelSettings(bool _active)
    {
        panel_mainmenu.SetActive(!_active);
        panel_settings.SetActive(_active);
        SetSelectableObject(panel_settings);
    }

    public void ShowPanelTutorial(bool _active)
    {
        isOpenTutorial = _active;
        panel_tutorial.SetActive(_active);
        if (_active)
        {
            SetSelectableObject(panel_tutorial);
        }
        else
        {
            SetSelectableObject(panel_settings);
        }
        
    }

    public void ShowPanelQuit(bool _active)
    {
        panel_mainmenu.SetActive(!_active);
        panel_quit.SetActive(_active);
        SetSelectableObject(panel_quit);
    }

    private void SetSelectableObject(GameObject _object)
    {
        int indexInteractableObject = -1;
        for (int i = 0; i < _object.transform.childCount; i++)
        {
            Button btn = _object.transform.GetChild(i).gameObject.GetComponent(typeof(Button)) as Button;
            Slider sld = _object.transform.GetChild(i).gameObject.GetComponent(typeof(Slider)) as Slider;
            if (btn != null || sld != null)
            {
                Debug.Log(_object.transform.GetChild(i).gameObject.name);
                //EventSystem.current.SetSelectedGameObject(_object.transform.GetChild(i).gameObject);
                indexInteractableObject = i;
                break;
            }
        }
        if (indexInteractableObject != -1)
        {
            EventSystem.current.SetSelectedGameObject(_object.transform.GetChild(indexInteractableObject).gameObject);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
        }

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void UpdateTutorialContent()
    {
        if(indexTutorial > listOfTutorialContent.Count - 1)
        {
            indexTutorial = 0;
        }
        else if(indexTutorial < 0)
        {
            indexTutorial = listOfTutorialContent.Count - 1;
        }
        for (int i = 0; i < listOfTutorialContent.Count; i++)
        {
            if(i == indexTutorial)
            {
                listOfTutorialContent[i].SetActive(true);
            }
            else
            {
                listOfTutorialContent[i].SetActive(false);
            }
        }
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("LEVEL"))
        {
            int level = PlayerPrefs.GetInt("LEVEL");
            UnityEngine.SceneManagement.SceneManager.LoadScene(level);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Cancel"))
        {
            ShowPanelCredits(false);
            ShowPanelQuit(false);
            ShowPanelSettings(false);
            ShowPanelTutorial(false);
            btn_start.Select();
        }
        else if (Input.GetAxis("Horizontal") > 0 && isOpenTutorial)
        {
            indexTutorial++;
            UpdateTutorialContent();
            return;
        }
        else if (Input.GetAxis("Horizontal") < 0 && isOpenTutorial)
        {
            indexTutorial--;
            UpdateTutorialContent();
            return;
        }
    }
}
