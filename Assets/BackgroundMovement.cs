using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    Vector3 mSpeed;

    float BACKGROUND_MOVEMENT_SPEED = 1f;

    // Start is called before the first frame update
    void Start()
    {
        float zIndex = Random.Range(8f, 10f);
        Vector3 pos = transform.position;
        pos.z = zIndex;
        transform.position = pos;
        mSpeed = new Vector3(Random.Range(-1f, -0.5f), Random.Range(-1f, -0.5f), 0);
        transform.eulerAngles = new Vector3(270, 0, 0);
        transform.localScale = new Vector3(Random.Range(0.01f, 0.8f), 1, Random.Range(0.01f, 0.8f));

        Color c = Color.HSVToRGB((Time.fixedTime % 360 / 360f), Random.Range(0.2f, 5f), Random.Range(0.2f, 0.4f));
        gameObject.GetComponent<Renderer>().material.color = c;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + mSpeed * Time.deltaTime * BACKGROUND_MOVEMENT_SPEED;

        if(transform.position.x + transform.localScale.x < -10 || transform.position.y + transform.localScale.y < -5)
        {
            Destroy(gameObject);
        }
    }
}
