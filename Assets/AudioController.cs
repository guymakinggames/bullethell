using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private bool mAudioEnabled;
    public UnityEngine.UI.Text mText;

    // Start is called before the first frame update
    void Start()
    {
        mAudioEnabled = (PlayerPrefs.GetInt("AudioEnabled", 1) == 1);
        GetComponent<AudioSource>().mute = !mAudioEnabled;
        updateText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void updateText()
    {
        mText.text = "Music: " + (mAudioEnabled ? "On" : "Off");
    }

    public void toggleAudio()
    {
        mAudioEnabled = !mAudioEnabled;
        GetComponent<AudioSource>().mute = !mAudioEnabled;
        PlayerPrefs.SetInt("AudioEnabled", mAudioEnabled ? 1 : 0);
        updateText();
    }
}
