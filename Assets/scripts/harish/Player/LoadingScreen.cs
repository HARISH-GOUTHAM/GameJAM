using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    public GameObject loadingScreen;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(StopLoading),10);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void StopLoading()
    {
        loadingScreen.SetActive(false);
    }
}
