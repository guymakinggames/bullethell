using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const float BULLET_SPEED = 4f;
    private const float MAX_X = 10f;
    private const float MAX_Y = 5.5f;

    private const float HIT_DISTANCE = 0.275f;
    private const float PLAYER_DISTANCE_CENTER = 4.25f + HIT_DISTANCE;

    private Vector3? mDirection = null;

    private GameObject mPlayer;
    private GameObject mMain;
    private GameObject mSurvivalTime;

    private float mTimeLimit = -1;

    private bool mHit = false;

    // Start is called before the first frame update
    void Start()
    {
        mPlayer = GameObject.Find("Player");
        mMain = GameObject.Find("CenterObj");
        mSurvivalTime = GameObject.Find("SurvivalTime");
    }

    // Update is called once per frame
    void Update()
    {
        if(mDirection != null)
        {
            Vector3 direction = (Vector3)mDirection;
            transform.position = transform.position + direction * BULLET_SPEED * Time.deltaTime;

            float dist = Vector3.Distance(mPlayer.transform.position, transform.position);
            float distToCenter = Vector3.Distance(transform.position, new Vector3(0, 0, 0));
            
            if(mTimeLimit > 0 && Time.time > mTimeLimit)
            {
                hitEnd();
            }
            else if(dist < HIT_DISTANCE && distToCenter < PLAYER_DISTANCE_CENTER)
            {
                if(!mHit && Time.timeScale == 1)
                {
                    Time.timeScale = 0.01f;
                    mHit = true;
                    ParticleSystem main = GameObject.Find("Explosion").GetComponent<ParticleSystem>();
                    main.Play();
                    mPlayer.GetComponent<Player>().StartDeathAnimation();
                    mMain.GetComponent<GameMaster>().GameOver();

                    if (mSurvivalTime != null)
                    {
                        mSurvivalTime.GetComponent<TimeScript>().GameOver();
                    }

                    GameObject pauseButton = GameObject.Find("PauseButton");
                    if (pauseButton != null)
                    {
                        Destroy(pauseButton);
                    }

                    GameObject backgroundFade = GameObject.Find("BackgroundFade");
                    if (backgroundFade != null)
                    {
                        backgroundFade.GetComponent<BackgroundFade>().GameOver();
                    }

                    GameObject audioButton = GameObject.Find("AudioButton");
                    if (audioButton != null)
                    {
                        Destroy(audioButton);
                    }

                    mTimeLimit = Time.time + 0.0075f;
                }
            } else if (mHit && dist > HIT_DISTANCE)
            {
                hitEnd();
            }

            if (transform.position.x > MAX_X || transform.position.x < -MAX_X || transform.position.y > MAX_Y || transform.position.y < -MAX_Y)
            {
                Destroy(gameObject);
            }
        }
    }

    void hitEnd()
    {
        Time.timeScale = 1.01f;
        ParticleSystem main = GameObject.Find("Explosion").GetComponent<ParticleSystem>();
        main.Stop();
        mHit = false;
    }

    public void setDirection(Vector3 direction)
    {
        mDirection = direction;
    }
}
