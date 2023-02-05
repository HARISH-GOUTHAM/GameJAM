using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;
    public int sceneIndexToLoad = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(UnityEngine.Input.GetKeyDown(KeyCode.Escape))
            OnPause();
    }

    public void OnPause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    
    public void OnResume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    
}
