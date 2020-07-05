using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public GameObject mBackgroundUnit;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<50; i++)
        {
            Instantiate(mBackgroundUnit, new Vector3(Random.Range(-25, 25), Random.Range(-25, 25), 0), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Random.Range(0, 10) == 0)
        {
            Instantiate(mBackgroundUnit, new Vector3(Random.Range(0, 25), Random.Range(15, 35), 0), Quaternion.identity);
        }
    }
}
