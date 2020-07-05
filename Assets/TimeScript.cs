using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class TimeScript : MonoBehaviour
{
    float mSurvivalTime = 0;
    bool mAlive = true;
    float mCurrentHighScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<UnityEngine.UI.Text>().text = "Survival Time: 00:00:00";
        mCurrentHighScore = PlayerPrefs.GetFloat("HighScore", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (mAlive)
        {
            mSurvivalTime += Time.deltaTime;

            float minutes = Mathf.FloorToInt(mSurvivalTime / 60);
            float seconds = Mathf.FloorToInt(mSurvivalTime / 1 - minutes * 60);
            float fractions = (mSurvivalTime - seconds - minutes * 60) * 100;

            if(mSurvivalTime > mCurrentHighScore && mCurrentHighScore > 0 && mSurvivalTime < mCurrentHighScore + 4)
            {
                gameObject.GetComponent<UnityEngine.UI.Text>().text = "Survival Time: " + minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + fractions.ToString("00") + "\nNEW HIGH SCORE!";
            }
            else
            {
                gameObject.GetComponent<UnityEngine.UI.Text>().text = "Survival Time: " + minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + fractions.ToString("00");
            }
        } else if (mCurrentHighScore == 0 || mSurvivalTime > mCurrentHighScore)
        {
            float minutes = Mathf.FloorToInt(mSurvivalTime / 60);
            float seconds = Mathf.FloorToInt(mSurvivalTime / 1 - minutes * 60);
            float fractions = (mSurvivalTime - seconds - minutes * 60) * 100;
            gameObject.GetComponent<UnityEngine.UI.Text>().text = "Survival Time: " + minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + fractions.ToString("00") + "\nNEW HIGH SCORE!";
        }
    }

    public void GameOver()
    {
        mAlive = false;
        if(mSurvivalTime > mCurrentHighScore)
        {
            PlayerPrefs.SetFloat("HighScore", mSurvivalTime);

            Social.ReportScore((long)(mSurvivalTime * 1000), "CgkIvanrz-kPEAIQAQ", (bool success) =>
            {
                ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI("CgkIvanrz-kPEAIQAQ");
            });
        }

        if(mSurvivalTime > 10 * 60)
        {
            PlayGamesPlatform.Instance.ReportProgress("CgkIvanrz-kPEAIQCw", 100.0f, res => { });
        }
    }
}
