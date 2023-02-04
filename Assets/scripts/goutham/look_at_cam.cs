using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class look_at_cam : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = Camera.main.transform.position - transform.GetChild(0).position;//health UI STUFF
        v.x = v.z = 0.0f;//health UI STUFF
        transform.GetChild(0).LookAt(Camera.main.transform.position);//health UI STUFF

    }
}
