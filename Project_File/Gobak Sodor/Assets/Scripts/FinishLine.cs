using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            SceneLoader sceneLoader = FindObjectOfType<SceneLoader>();
            if(sceneLoader.GetSceneIndex() == 2)
            {
                sceneLoader.ChangeScene("MainMenu");
            }
            sceneLoader.ChangeScene("Level" + (sceneLoader.GetSceneIndex() + 1).ToString());
        }
    }
}
