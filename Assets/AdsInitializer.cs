using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Advertisement.Initialize("f316f7ea-2081-4e7d-b99d-8efefc72a897", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
