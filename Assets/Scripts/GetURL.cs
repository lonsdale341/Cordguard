using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GetURL : MonoBehaviour
{
    private float time;

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime);
        time += Time.deltaTime;
        Debug.Log(time);
    }

    // Display the changing position of the sphere.
    
}