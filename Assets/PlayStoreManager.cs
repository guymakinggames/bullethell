using System.Collections;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class PlayStoreManager : MonoBehaviour
{
    bool mLoggedIn;
    public UnityEngine.UI.Text mText;

    // Start is called before the first frame update
    void Start()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) => {
            mLoggedIn = (result == SignInStatus.Success);
            updateLoginState();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void login()
    {
        if(mLoggedIn)
        {
            PlayGamesPlatform.Instance.SignOut();
            mLoggedIn = false;
            updateLoginState();
        } else
        {
            PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptAlways, (result) => {
                mLoggedIn = (result == SignInStatus.Success);
                updateLoginState();
            });
        }
    }

    void updateLoginState()
    {
        if(mLoggedIn)
        {
            mText.text = "Logout";
        } else
        {
            mText.text = "Login";
        }
    }

    public void showLeaderboard()
    {
        ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI("CgkIvanrz-kPEAIQAQ");
    }
}
