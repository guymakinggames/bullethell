using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColorGeneration : MonoBehaviour
{
    public ParticleSystem mSystem;
    public Light mLight;

    ParticleSystem.MainModule mMainModule;
    Color mCurrentColor;
    Color mNextColor;

    float mNextTransitionTime;
    float mStartTransitionTime;
    bool mChangingColor = false;

    // Start is called before the first frame update
    void Start()
    {
        mMainModule = mSystem.main;
        mCurrentColor = new Color(1f, 1f, 1f);
        mNextTransitionTime = Time.time + 10;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > mNextTransitionTime)
        {
            if(mChangingColor)
            {
                mChangingColor = false;
                mCurrentColor = mNextColor;
            } else
            {
                mChangingColor = true;
                mNextColor = Color.HSVToRGB(Random.Range(0f, 1f), Random.Range(0.6f, 1f), Random.Range(0.6f, 1f));
                mStartTransitionTime = Time.time;
            }

            mNextTransitionTime = Time.time + 10;
        }

        if(mChangingColor)
        {
            float progress = (Time.time - mStartTransitionTime) / (mNextTransitionTime - mStartTransitionTime);
            mMainModule.startColor = Color.Lerp(mCurrentColor, mNextColor, progress);
            mLight.color = Color.Lerp(mCurrentColor, mNextColor, progress);
        }

    }
}
