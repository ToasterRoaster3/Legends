using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevel : MonoBehaviour
{
    public GameObject uiPanel;          
    
    public float delayBeforeLoad = 3f;   
    public string nextSceneName;        

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;

            if (uiPanel != null)
            {
                uiPanel.SetActive(true); 
                Debug.Log("UI Panel activated.");
            }
            else
            {
                Debug.LogWarning("UI Panel is not assigned!");
            }

            Invoke(nameof(LoadNextScene), delayBeforeLoad);
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
