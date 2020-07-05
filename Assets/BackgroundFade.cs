using UnityEngine;

public class BackgroundFade : MonoBehaviour
{
    private Color mStartColor = new Color(0, 0, 0, 0);
    private Color mEndColor = new Color(0, 0, 0, 0.5f);

    private Material mBackgroundFadeMaterial;

    private CanvasGroup mGameOverButtons;

    private float mStartTime;
    private float mEndTime;
    private bool mAlive = true;

    private float mButtonFade;

    // Start is called before the first frame update
    void Start()
    {
        mBackgroundFadeMaterial = GetComponent<Renderer>().material;
        mBackgroundFadeMaterial.color = mStartColor;

        mGameOverButtons = GameObject.Find("GameOver").GetComponent<CanvasGroup>();
        mGameOverButtons.alpha = 0;
        mGameOverButtons.interactable = false;
        mGameOverButtons.blocksRaycasts = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!mAlive)
        {
            if (Time.time < mEndTime)
            {
                float progress = (Time.time - mStartTime) / (mEndTime - mStartTime);
                mBackgroundFadeMaterial.color = Color.Lerp(mStartColor, mEndColor, progress);
            } else if (Time.time < mButtonFade)
            {
                float progress = (Time.time - mEndTime) / (mButtonFade - mEndTime);
                mGameOverButtons.alpha = progress;
            } else
            {
                mGameOverButtons.interactable = true;
                mGameOverButtons.blocksRaycasts = true;
            }
        }
    }

    public void GameOver()
    {
        mStartTime = Time.time;
        mEndTime = Time.time + 1.5f;
        mButtonFade = Time.time + 3f;
        mAlive = false;
    }
}
