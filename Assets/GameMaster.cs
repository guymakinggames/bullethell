using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.Advertisements;

enum STATE
{
    IDLE,

    //Simple movement
    SINGLE_PATTERN_1,

    //Adding the enemy
    ADDING_ENEMY,

    /*
     * MULTI UNIT SHARED
     */
    SPREAD,
    SAME_DIRECTION_1,
    SAME_DIRECTION_2,
    SAME_DIRECTION_3,
    SAME_DIRECTION_4,

    /*
     * 2 UNITS
     */
    //Moving in opposite directions
    DOUBLE_PATTERN_ACTION_2_1,
    DOUBLE_PATTERN_ACTION_2_2,

    /*
     * 3 UNITS
     */
    //First one moving in one direction, the others in the other direction
    TRIPLE_PATTERN_ACTION_3_1,
    TRIPLE_PATTERN_ACTION_3_2,

    //Second one moving in one direction, the others in the other direction
    TRIPLE_PATTERN_ACTION_3_3,
    TRIPLE_PATTERN_ACTION_3_4,

    //Third one moving in one direction, the others in the other direction
    TRIPLE_PATTERN_ACTION_3_5,
    TRIPLE_PATTERN_ACTION_3_6,

}

public class GameMaster : MonoBehaviour
{
    public EnemyPuppet mEnemy;

    private List<EnemyPuppet> enemies = new List<EnemyPuppet>();

    private STATE mState = STATE.IDLE;
    private float mCurrentStateStartTime;
    private float mNextTransitionTime;

    private float mSpeedModifier = 160f;
    private float mFireRateModifier = 1f;

    private float mGameStartTime;

    private float PHASE_2_START_TIME = 15f;
    private float PHASE_3_START_TIME = 45f;
    private float PHASE_4_START_TIME = 90f;
    private float PHASE_5_START_TIME = 180f;
    private float PHASE_6_START_TIME = 240f;
    private float PHASE_7_START_TIME = 300f;
    private float PHASE_8_START_TIME = 360f;

    private float MIN_ANGLE = 40f;

    private float mNextSpreadTime = -1;

    private bool mAlive = true;
    float mTimeScale = -1f;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        mGameStartTime = Time.time;
        mCurrentStateStartTime = Time.time;
        mNextTransitionTime = Time.time + 1;
        EnemyPuppet enemy = Instantiate(mEnemy, new Vector3(1, 0, 0), Quaternion.identity);
        enemies.Add(enemy);
    }

    // Update is called once per frame
    void Update()
    {
        if (mAlive)
        {
            if (Time.time > mNextTransitionTime)
            {
                transitionToNextstate();
            }

            handleCurrentState();
        } else
        {
            handleStateGameOver();
        }
    }

    private void handleCurrentState()
    {
        switch (mState)
        {
            case STATE.IDLE:
                handleStateIdle();
                break;
            case STATE.SINGLE_PATTERN_1:
                handleSinglePattern1();
                break;
            case STATE.ADDING_ENEMY:
            case STATE.SPREAD:
                break;
            case STATE.SAME_DIRECTION_1:
                handleMoveSameDirection(true);
                break;
            case STATE.SAME_DIRECTION_2:
                handleMoveSameDirection(false);
                break;
            case STATE.SAME_DIRECTION_3:
                handleMoveSameDirectionAlternateBulletSpeed(true);
                break;
            case STATE.SAME_DIRECTION_4:
                handleMoveSameDirectionAlternateBulletSpeed(false);
                break;
            case STATE.DOUBLE_PATTERN_ACTION_2_1:
                handleDoubleStatePattern2(true);
                break;
            case STATE.DOUBLE_PATTERN_ACTION_2_2:
                handleDoubleStatePattern2(false);
                break;
            case STATE.TRIPLE_PATTERN_ACTION_3_1:
                handleTripleStatePattern(true, 0);
                break;
            case STATE.TRIPLE_PATTERN_ACTION_3_2:
                handleTripleStatePattern(false, 0);
                break;
            case STATE.TRIPLE_PATTERN_ACTION_3_3:
                handleTripleStatePattern(true, 1);
                break;
            case STATE.TRIPLE_PATTERN_ACTION_3_4:
                handleTripleStatePattern(false, 1);
                break;
            case STATE.TRIPLE_PATTERN_ACTION_3_5:
                handleTripleStatePattern(true, 2);
                break;
            case STATE.TRIPLE_PATTERN_ACTION_3_6:
                handleTripleStatePattern(false, 2);
                break;
        }
    }

    private void transitionToNextstate()
    {
        if (mState == STATE.IDLE)
        {
            if (Time.time - mGameStartTime > PHASE_2_START_TIME && enemies.Count == 1)
            {
                PlayGamesPlatform.Instance.ReportProgress("CgkIvanrz-kPEAIQBA", 100.0f, res => { });
                mState = STATE.ADDING_ENEMY;
                mNextTransitionTime = Time.time + 3;
            }
            else if (Time.time - mGameStartTime > PHASE_3_START_TIME && enemies.Count == 2)
            {
                PlayGamesPlatform.Instance.ReportProgress("CgkIvanrz-kPEAIQBQ", 100.0f, res => { });
                mState = STATE.ADDING_ENEMY;
                mNextTransitionTime = Time.time + 3;
            }
            else if (Time.time - mGameStartTime > PHASE_4_START_TIME && enemies.Count == 3)
            {
                PlayGamesPlatform.Instance.ReportProgress("CgkIvanrz-kPEAIQBg", 100.0f, res => { });
                mState = STATE.ADDING_ENEMY;
                mNextTransitionTime = Time.time + 3;
            }
            else if (Time.time - mGameStartTime > PHASE_5_START_TIME && enemies.Count == 4)
            {
                PlayGamesPlatform.Instance.ReportProgress("CgkIvanrz-kPEAIQBw", 100.0f, res => { });
                mState = STATE.ADDING_ENEMY;
                mNextTransitionTime = Time.time + 3;
            }
            else if (Time.time - mGameStartTime > PHASE_6_START_TIME && enemies.Count == 5)
            {
                PlayGamesPlatform.Instance.ReportProgress("CgkIvanrz-kPEAIQCA", 100.0f, res => { });
                mState = STATE.ADDING_ENEMY;
                mNextTransitionTime = Time.time + 3;
            }
            else if (Time.time - mGameStartTime > PHASE_7_START_TIME && enemies.Count == 6)
            {
                PlayGamesPlatform.Instance.ReportProgress("CgkIvanrz-kPEAIQCQ", 100.0f, res => { });
                mState = STATE.ADDING_ENEMY;
                mNextTransitionTime = Time.time + 3;
            }
            else if (Time.time - mGameStartTime > PHASE_8_START_TIME && enemies.Count == 7)
            {
                PlayGamesPlatform.Instance.ReportProgress("CgkIvanrz-kPEAIQCg", 100.0f, res => { });
                mState = STATE.ADDING_ENEMY;
                mNextTransitionTime = Time.time + 3;
            }
            else if (enemies.Count == 1)
            {
                transitionToNextSingleState();
            }
            else if (enemies.Count == 2)
            {
                transitionToNextFirstPhaseDoubleState();
            }
            else if (enemies.Count == 3)
            {
                transitionToNextFirstPhaseThirdState();
            }
            else if (enemies.Count == 4)
            {
                transitionToNextFirstPhaseFourthState();
            }
            else if (enemies.Count == 5)
            {
                transitionToNextFirstPhaseFifthState();
            }
            else if (enemies.Count == 6)
            {
                transitionToNextFirstPhaseSixthState();
            }
            else if (enemies.Count == 7)
            {
                transitionToNextFirstPhaseSeventhState();
            }
            else if (enemies.Count == 8)
            {
                transitionToNextFirstPhaseEighthState();
            }
        }
        else if (mState == STATE.ADDING_ENEMY)
        {
            mState = STATE.IDLE;
            float transitionTime = Random.Range(1f, 2.5f);
            mNextTransitionTime = Time.time + transitionTime;
            EnemyPuppet enemy = Instantiate(mEnemy, new Vector3(1, 0, 0), Quaternion.identity);
            int totalEnemies = enemies.Count + 1;
            float angle = 360f / totalEnemies;
            enemy.OverrideAngle(360 - angle);
            enemies.Add(enemy);
            handleAddingEnemy(transitionTime);
        }
        else if (mState == STATE.SAME_DIRECTION_1 || mState == STATE.SAME_DIRECTION_3 || mState == STATE.DOUBLE_PATTERN_ACTION_2_1 || mState == STATE.TRIPLE_PATTERN_ACTION_3_1 ||
            mState == STATE.TRIPLE_PATTERN_ACTION_3_3 || mState == STATE.TRIPLE_PATTERN_ACTION_3_5)
        {
            if (enemies.Count == 2)
            {
                transitionToNextSecondPhaseDoubleState();
            }
            else if (enemies.Count == 3)
            {
                transitionToNextSecondPhaseThirdState();
            }
            else if (enemies.Count == 4)
            {
                transitionToNextSecondPhaseFourthState();
            }
            else if (enemies.Count == 5)
            {
                transitionToNextSecondPhaseFifthState();
            }
            else if (enemies.Count == 6)
            {
                transitionToNextSecondPhaseSixthState();
            }
            else if (enemies.Count == 7)
            {
                transitionToNextSecondPhaseSeventhState();
            }
            else if (enemies.Count == 8)
            {
                transitionToNextSecondPhaseEigthState();
            }
        }
        else
        {
            mState = STATE.IDLE;
            mNextTransitionTime = Time.time + Random.Range(0f, 1.5f/enemies.Count);
        }

        mCurrentStateStartTime = Time.time;
    }

    private void transitionToNextSingleState()
    {
        mState = STATE.SINGLE_PATTERN_1;
        mNextTransitionTime = Time.time + 5;

        if (Random.Range(0f, 1f) < 0.5f)
        {
            mSpeedModifier = Random.Range(140f, 200f);
            mFireRateModifier = Random.Range(1f, 4f);
        }
        else
        {
            mSpeedModifier = Random.Range(-200f, -140f);
            mFireRateModifier = Random.Range(1f, 4f);
        }
    }

    private void transitionToNextFirstPhaseDoubleState()
    {
        if (Random.Range(0f, 1f) < 0.2f && (mNextSpreadTime == -1 || Time.time > mNextSpreadTime))
        {
            float transitionTime = Random.Range(1f, 2.5f);
            mNextTransitionTime = Time.time + transitionTime;
            mState = STATE.SPREAD;
            handleSpread(transitionTime);
            mNextSpreadTime = Time.time + 10;
        }
        else
        {
            float nextVal = Random.Range(0, 3);
            switch (nextVal)
            {
                case 0:
                    mState = STATE.SAME_DIRECTION_1;
                    break;
                case 1:
                    mState = STATE.SAME_DIRECTION_3;
                    break;
                case 2:
                    mState = STATE.DOUBLE_PATTERN_ACTION_2_1;
                    break;
            }
            mNextTransitionTime = Time.time + Random.Range(3f, 8f);

            if (Random.Range(0f, 1f) < 0.5f)
            {
                mSpeedModifier = Random.Range(100f, 150f);
                mFireRateModifier = Random.Range(1f, 1.5f);
            }
            else
            {
                mSpeedModifier = Random.Range(-150f, -100f);
                mFireRateModifier = Random.Range(1f, 1.5f);
            }
        }
    }

    private void transitionToNextFirstPhaseThirdState()
    {
        if (Random.Range(0f, 1f) < 0.4f && (mNextSpreadTime == -1 || Time.time > mNextSpreadTime))
        {
            float transitionTime = Random.Range(1f, 2.5f);
            mNextTransitionTime = Time.time + transitionTime;
            mState = STATE.SPREAD;
            handleSpread(transitionTime);
            mNextSpreadTime = Time.time + 10;
        }
        else
        {
            float nextVal = Random.Range(0, 10);
            if(nextVal < 4)
            {
                mState = STATE.SAME_DIRECTION_1;
            } else if (nextVal < 7)
            {
                mState = STATE.SAME_DIRECTION_3;
            }
            else if (nextVal == 7)
            {
                mState = STATE.TRIPLE_PATTERN_ACTION_3_1;
            }
            else if (nextVal == 8)
            {
                mState = STATE.TRIPLE_PATTERN_ACTION_3_3;
            }
            else if (nextVal == 9)
            {
                mState = STATE.TRIPLE_PATTERN_ACTION_3_5;
            }

            mNextTransitionTime = Time.time + Random.Range(3f, 8f);

            if (Random.Range(0f, 1f) < 0.5f)
            {
                mSpeedModifier = Random.Range(100f, 150f);
                mFireRateModifier = Random.Range(1f, 1.5f);
            }
            else
            {
                mSpeedModifier = Random.Range(-150f, -100f);
                mFireRateModifier = Random.Range(1f, 1.5f);
            }
        }
    }
    
    private void transitionToNextSecondPhaseDoubleState()
    {
        float nextVal = Random.Range(0, 3);
        switch (nextVal)
        {
            case 0:
                mState = STATE.SAME_DIRECTION_2;
                break;
            case 1:
                mState = STATE.SAME_DIRECTION_4;
                break;
            case 2:
                mState = STATE.DOUBLE_PATTERN_ACTION_2_2;
                break;
        }
        mNextTransitionTime = Time.time + Random.Range(3f, 8f);

        if (Random.Range(0f, 1f) < 0.5f)
        {
            mSpeedModifier = Random.Range(100f, 150f);
            mFireRateModifier = Random.Range(1f, 1.5f);
        }
        else
        {
            mSpeedModifier = Random.Range(-150f, -100f);
            mFireRateModifier = Random.Range(1f, 1.5f);
        }
    }

    private void transitionToNextSecondPhaseThirdState()
    {
        float nextVal = Random.Range(0, 10);
        if (nextVal < 4)
        {
            mState = STATE.SAME_DIRECTION_2;
        }
        else if (nextVal < 7)
        {
            mState = STATE.SAME_DIRECTION_4;
        }
        else if (nextVal == 7)
        {
            mState = STATE.TRIPLE_PATTERN_ACTION_3_2;
        }
        else if (nextVal == 8)
        {
            mState = STATE.TRIPLE_PATTERN_ACTION_3_4;
        }
        else if (nextVal == 9)
        {
            mState = STATE.TRIPLE_PATTERN_ACTION_3_6;
        }

        mNextTransitionTime = Time.time + Random.Range(3f, 8f);

        if (Random.Range(0f, 1f) < 0.5f)
        {
            mSpeedModifier = Random.Range(100f, 150f);
            mFireRateModifier = Random.Range(1f, 1.5f);
        }
        else
        {
            mSpeedModifier = Random.Range(-150f, -100f);
            mFireRateModifier = Random.Range(1f, 1.5f);
        }
    }

    private void transitionToNextFirstPhaseFourthState()
    {
        if (Random.Range(0f, 1f) < 0.5f && (mNextSpreadTime == -1 || Time.time > mNextSpreadTime))
        {
            float transitionTime = Random.Range(1f, 2.5f);
            mNextTransitionTime = Time.time + transitionTime;
            mState = STATE.SPREAD;
            handleSpread(transitionTime);
            mNextSpreadTime = Time.time + 5;
        }
        else
        {
            float nextVal = Random.Range(0, 2);
            if (nextVal == 0)
            {
                mState = STATE.SAME_DIRECTION_1;
            }
            else
            {
                mState = STATE.SAME_DIRECTION_3;
            }

            mNextTransitionTime = Time.time + Random.Range(3f, 8f);

            if (Random.Range(0f, 1f) < 0.5f)
            {
                mSpeedModifier = Random.Range(100f, 150f);
                mFireRateModifier = Random.Range(1f, 1.5f);
            }
            else
            {
                mSpeedModifier = Random.Range(-150f, -100f);
                mFireRateModifier = Random.Range(1f, 1.5f);
            }
        }
    }
    
    private void transitionToNextSecondPhaseFourthState()
    {
        float nextVal = Random.Range(0, 2);
        if (nextVal == 1)
        {
            mState = STATE.SAME_DIRECTION_2;
        }
        else
        {
            mState = STATE.SAME_DIRECTION_4;
        }
        
        mNextTransitionTime = Time.time + Random.Range(3f, 8f);

        if (Random.Range(0f, 1f) < 0.5f)
        {
            mSpeedModifier = Random.Range(100f, 150f);
            mFireRateModifier = Random.Range(1f, 1.5f);
        }
        else
        {
            mSpeedModifier = Random.Range(-150f, -100f);
            mFireRateModifier = Random.Range(1f, 1.5f);
        }
    }

    private void transitionToNextFirstPhaseFifthState()
    {
        if (Random.Range(0f, 1f) < 0.5f && (mNextSpreadTime == -1 || Time.time > mNextSpreadTime))
        {
            float transitionTime = Random.Range(1f, 2.5f);
            mNextTransitionTime = Time.time + transitionTime;
            mState = STATE.SPREAD;
            handleSpread(transitionTime);
            mNextSpreadTime = Time.time + 5;
        }
        else
        {
            float nextVal = Random.Range(0, 2);
            if (nextVal == 0)
            {
                mState = STATE.SAME_DIRECTION_1;
            }
            else
            {
                mState = STATE.SAME_DIRECTION_3;
            }

            mNextTransitionTime = Time.time + Random.Range(3f, 8f);

            if (Random.Range(0f, 1f) < 0.5f)
            {
                mSpeedModifier = Random.Range(100f, 150f);
                mFireRateModifier = Random.Range(1f, 1.5f);
            }
            else
            {
                mSpeedModifier = Random.Range(-150f, -100f);
                mFireRateModifier = Random.Range(1f, 1.5f);
            }
        }
    }

    private void transitionToNextSecondPhaseFifthState()
    {
        float nextVal = Random.Range(0, 2);
        if (nextVal == 1)
        {
            mState = STATE.SAME_DIRECTION_2;
        }
        else
        {
            mState = STATE.SAME_DIRECTION_4;
        }

        mNextTransitionTime = Time.time + Random.Range(3f, 8f);

        if (Random.Range(0f, 1f) < 0.5f)
        {
            mSpeedModifier = Random.Range(100f, 150f);
            mFireRateModifier = Random.Range(1f, 1.5f);
        }
        else
        {
            mSpeedModifier = Random.Range(-150f, -100f);
            mFireRateModifier = Random.Range(1f, 1.5f);
        }
    }

    private void transitionToNextFirstPhaseSixthState()
    {
        if (Random.Range(0f, 1f) < 0.5f && (mNextSpreadTime == -1 || Time.time > mNextSpreadTime))
        {
            float transitionTime = Random.Range(1f, 2.5f);
            mNextTransitionTime = Time.time + transitionTime;
            mState = STATE.SPREAD;
            handleSpread(transitionTime);
            mNextSpreadTime = Time.time + 5;
        }
        else
        {
            float nextVal = Random.Range(0, 2);
            if (nextVal == 0)
            {
                mState = STATE.SAME_DIRECTION_1;
            }
            else
            {
                mState = STATE.SAME_DIRECTION_3;
            }

            mNextTransitionTime = Time.time + Random.Range(3f, 8f);

            if (Random.Range(0f, 1f) < 0.5f)
            {
                mSpeedModifier = Random.Range(100f, 150f);
                mFireRateModifier = Random.Range(1f, 1.5f);
            }
            else
            {
                mSpeedModifier = Random.Range(-150f, -100f);
                mFireRateModifier = Random.Range(1f, 1.5f);
            }
        }
    }

    private void transitionToNextSecondPhaseSixthState()
    {
        float nextVal = Random.Range(0, 2);
        if (nextVal == 1)
        {
            mState = STATE.SAME_DIRECTION_2;
        }
        else
        {
            mState = STATE.SAME_DIRECTION_4;
        }

        mNextTransitionTime = Time.time + Random.Range(3f, 8f);

        if (Random.Range(0f, 1f) < 0.5f)
        {
            mSpeedModifier = Random.Range(100f, 150f);
            mFireRateModifier = Random.Range(1f, 1.5f);
        }
        else
        {
            mSpeedModifier = Random.Range(-150f, -100f);
            mFireRateModifier = Random.Range(1f, 1.5f);
        }
    }

    private void transitionToNextFirstPhaseSeventhState()
    {
        if (Random.Range(0f, 1f) < 0.5f && (mNextSpreadTime == -1 || Time.time > mNextSpreadTime))
        {
            float transitionTime = Random.Range(1f, 2.5f);
            mNextTransitionTime = Time.time + transitionTime;
            mState = STATE.SPREAD;
            handleSpread(transitionTime);
            mNextSpreadTime = Time.time + 5;
        }
        else
        {
            float nextVal = Random.Range(0, 2);
            if (nextVal == 0)
            {
                mState = STATE.SAME_DIRECTION_1;
            }
            else
            {
                mState = STATE.SAME_DIRECTION_3;
            }

            mNextTransitionTime = Time.time + Random.Range(3f, 8f);

            if (Random.Range(0f, 1f) < 0.5f)
            {
                mSpeedModifier = Random.Range(100f, 150f);
                mFireRateModifier = Random.Range(1f, 1.5f);
            }
            else
            {
                mSpeedModifier = Random.Range(-150f, -100f);
                mFireRateModifier = Random.Range(1f, 1.5f);
            }
        }
    }

    private void transitionToNextSecondPhaseSeventhState()
    {
        float nextVal = Random.Range(0, 2);
        if (nextVal == 1)
        {
            mState = STATE.SAME_DIRECTION_2;
        }
        else
        {
            mState = STATE.SAME_DIRECTION_4;
        }

        mNextTransitionTime = Time.time + Random.Range(3f, 8f);

        if (Random.Range(0f, 1f) < 0.5f)
        {
            mSpeedModifier = Random.Range(100f, 150f);
            mFireRateModifier = Random.Range(1f, 1.5f);
        }
        else
        {
            mSpeedModifier = Random.Range(-150f, -100f);
            mFireRateModifier = Random.Range(1f, 1.5f);
        }
    }

    private void transitionToNextFirstPhaseEighthState()
    {
        if (Random.Range(0f, 1f) < 0.5f && (mNextSpreadTime == -1 || Time.time > mNextSpreadTime))
        {
            float transitionTime = Random.Range(1f, 2.5f);
            mNextTransitionTime = Time.time + transitionTime;
            mState = STATE.SPREAD;
            handleSpread(transitionTime);
            mNextSpreadTime = Time.time + 5;
        }
        else
        {
            float nextVal = Random.Range(0, 2);
            if (nextVal == 0)
            {
                mState = STATE.SAME_DIRECTION_1;
            }
            else
            {
                mState = STATE.SAME_DIRECTION_3;
            }

            mNextTransitionTime = Time.time + Random.Range(3f, 8f);

            if (Random.Range(0f, 1f) < 0.5f)
            {
                mSpeedModifier = Random.Range(100f, 150f + (Time.time - PHASE_8_START_TIME)/5);
            }
            else
            {
                mSpeedModifier = Random.Range(-150f - (Time.time - PHASE_8_START_TIME) / 5, -100f);
            }
            mFireRateModifier = Random.Range(1f, 1.5f + (Time.time - PHASE_8_START_TIME) / 25f);
        }
    }

    private void transitionToNextSecondPhaseEigthState()
    {
        float nextVal = Random.Range(0, 2);
        if (nextVal == 1)
        {
            mState = STATE.SAME_DIRECTION_2;
        }
        else
        {
            mState = STATE.SAME_DIRECTION_4;
        }

        mNextTransitionTime = Time.time + Random.Range(3f, 8f);

        if (Random.Range(0f, 1f) < 0.5f)
        {
            mSpeedModifier = Random.Range(100f, 150f);
            mFireRateModifier = Random.Range(1f, 1.5f);
        }
        else
        {
            mSpeedModifier = Random.Range(-150f, -100f);
            mFireRateModifier = Random.Range(1f, 1.5f);
        }
    }

    private void handleStateGameOver()
    {
        enemies.ForEach(enemy =>
        {
            enemy.SetRotationSpeed(0f);
            enemy.FireBulletRate(0.0f);
        });
    }

    private void handleStateIdle()
    {
        enemies.ForEach(enemy =>
        {
            enemy.FireBulletRate(0.0f);
        });
    }

    private void handleSinglePattern1()
    {
        enemies.ForEach(enemy =>
        {
            float progress = (Time.time - mCurrentStateStartTime) / (mNextTransitionTime - mCurrentStateStartTime);
            if (progress > 0.5f)
            {
                float speed = Mathf.Lerp(mSpeedModifier, 0, 2 * (progress - 0.5f));
                enemy.SetRotationSpeed(speed);
                enemy.FireBulletRate((progress - 0.5f) / mFireRateModifier);
            }
            else
            {
                float speed = Mathf.Lerp(0, mSpeedModifier, 2 * progress);
                enemy.SetRotationSpeed(speed);
                enemy.FireBulletRate((0.5f - progress) / mFireRateModifier);
            }

        });
    }

    private void handleAddingEnemy(float time)
    {
        int totalEnemies = enemies.Count;
        float angle = 360f / totalEnemies;
        for (int i = 0; i < enemies.Count; i++)
        {
            EnemyPuppet enemy = enemies[i];
            enemy.SetTargetAngle(angle * i, time);
            enemy.FireBulletRate(0);
        }
    }

    private void handleSpread(float time)
    {
        float referenceAngle = Random.Range(0, 360);

        enemies.ForEach(enemy =>
        {
            enemy.FireBulletRate(0);
        });

        if (enemies.Count == 2)
        {
            float type = Random.Range(0, 6);
            switch (type)
            {
                case 0:
                    enemies[0].SetTargetAngle(referenceAngle, time);
                    enemies[1].SetTargetAngle(referenceAngle + 35, time);
                    break;
                case 1:
                    enemies[0].SetTargetAngle(referenceAngle, time);
                    enemies[1].SetTargetAngle(referenceAngle - 35, time);
                    break;
                case 2:
                    enemies[0].SetTargetAngle(referenceAngle, time);
                    enemies[1].SetTargetAngle(referenceAngle + 90, time);
                    break;
                case 3:
                    enemies[0].SetTargetAngle(referenceAngle, time);
                    enemies[1].SetTargetAngle(referenceAngle - 90, time);
                    break;
                case 4:
                    enemies[0].SetTargetAngle(referenceAngle, time);
                    enemies[1].SetTargetAngle(referenceAngle + 180, time);
                    break;
                case 5:
                    enemies[0].SetTargetAngle(referenceAngle, time);
                    enemies[1].SetTargetAngle(referenceAngle - 180, time);
                    break;
            }
        }
        else if (enemies.Count == 3)
        {
            float type = Random.Range(0, 4);
            switch (type)
            {
                case 0:
                    enemies[0].SetTargetAngle(referenceAngle, time);
                    enemies[1].SetTargetAngle(referenceAngle + 35, time);
                    enemies[2].SetTargetAngle(referenceAngle - 35, time);
                    break;
                case 1:
                    enemies[0].SetTargetAngle(referenceAngle, time);
                    enemies[1].SetTargetAngle(referenceAngle + 40, time);
                    enemies[2].SetTargetAngle(referenceAngle + 200, time);
                    break;
                case 2:
                    enemies[0].SetTargetAngle(referenceAngle, time);
                    enemies[1].SetTargetAngle(referenceAngle + 120, time);
                    enemies[2].SetTargetAngle(referenceAngle + 240, time);
                    break;
                case 3:
                    enemies[0].SetTargetAngle(referenceAngle, time);
                    enemies[1].SetTargetAngle(referenceAngle + 90, time);
                    enemies[2].SetTargetAngle(referenceAngle - 90, time);
                    break;
            }
        }
        else if (enemies.Count == 4)
        {
            float type = Random.Range(0, 7);
            switch (type)
            {
                case 0:
                    enemies[0].SetTargetAngle(referenceAngle, time);
                    enemies[1].SetTargetAngle(referenceAngle + 90, time);
                    enemies[2].SetTargetAngle(referenceAngle - 90, time);
                    enemies[3].SetTargetAngle(referenceAngle + 180, time);
                    break;
                case 1:
                    enemies[0].SetTargetAngle(referenceAngle, time);
                    enemies[1].SetTargetAngle(referenceAngle + 45, time);
                    enemies[2].SetTargetAngle(referenceAngle - 45, time);
                    enemies[3].SetTargetAngle(referenceAngle + 90, time);
                    break;
                case 2:
                    enemies[0].SetTargetAngle(referenceAngle, time);
                    enemies[1].SetTargetAngle(referenceAngle + 45, time);
                    enemies[2].SetTargetAngle(referenceAngle - 45, time);
                    enemies[3].SetTargetAngle(referenceAngle - 90, time);
                    break;
                case 3:
                    enemies[0].SetTargetAngle(referenceAngle, time);
                    enemies[1].SetTargetAngle(referenceAngle + 35, time);
                    enemies[2].SetTargetAngle(referenceAngle + 180, time);
                    enemies[3].SetTargetAngle(referenceAngle - 145, time);
                    break;
                case 4:
                    enemies[0].SetTargetAngle(referenceAngle, time);
                    enemies[1].SetTargetAngle(referenceAngle + 35, time);
                    enemies[2].SetTargetAngle(referenceAngle - 35, time);
                    enemies[3].SetTargetAngle(referenceAngle + 180, time);
                    break;
                case 5:
                    enemies[0].SetTargetAngle(referenceAngle, time);
                    enemies[1].SetTargetAngle(referenceAngle + 90, time);
                    enemies[2].SetTargetAngle(referenceAngle - 90, time);
                    enemies[3].SetTargetAngle(referenceAngle + 135, time);
                    break;
                case 6:
                    enemies[0].SetTargetAngle(referenceAngle, time);
                    enemies[1].SetTargetAngle(referenceAngle + 90, time);
                    enemies[2].SetTargetAngle(referenceAngle - 90, time);
                    enemies[3].SetTargetAngle(referenceAngle - 135, time);
                    break;
            }
        }
        else if (enemies.Count == 5)
        {
            float type = Random.Range(0, 6);
            switch (type)
            {
                case 0:
                    enemies[0].SetTargetAngle(referenceAngle, time);
                    enemies[1].SetTargetAngle(referenceAngle + 72, time);
                    enemies[2].SetTargetAngle(referenceAngle - 72, time);
                    enemies[3].SetTargetAngle(referenceAngle + 144, time);
                    enemies[4].SetTargetAngle(referenceAngle - 144, time);
                    break;
                case 1:
                    enemies[0].SetTargetAngle(referenceAngle, time);
                    enemies[1].SetTargetAngle(referenceAngle + 35, time);
                    enemies[2].SetTargetAngle(referenceAngle - 35, time);
                    enemies[3].SetTargetAngle(referenceAngle + 144, time);
                    enemies[4].SetTargetAngle(referenceAngle - 144, time);
                    break;
                case 2:
                    enemies[0].SetTargetAngle(referenceAngle, time);
                    enemies[1].SetTargetAngle(referenceAngle + 35, time);
                    enemies[2].SetTargetAngle(referenceAngle - 35, time);
                    enemies[3].SetTargetAngle(referenceAngle + 215, time);
                    enemies[4].SetTargetAngle(referenceAngle + 145, time);
                    break;
                case 3:
                    enemies[0].SetTargetAngle(referenceAngle, time);
                    enemies[1].SetTargetAngle(referenceAngle + 35, time);
                    enemies[2].SetTargetAngle(referenceAngle - 35, time);
                    enemies[3].SetTargetAngle(referenceAngle + 70, time);
                    enemies[4].SetTargetAngle(referenceAngle - 70, time);
                    break;
                case 4:
                    enemies[0].SetTargetAngle(referenceAngle, time);
                    enemies[1].SetTargetAngle(referenceAngle + 35, time);
                    enemies[2].SetTargetAngle(referenceAngle - 35, time);
                    enemies[3].SetTargetAngle(referenceAngle + 70, time);
                    enemies[4].SetTargetAngle(referenceAngle + 180, time);
                    break;
                case 5:
                    enemies[0].SetTargetAngle(referenceAngle, time);
                    enemies[1].SetTargetAngle(referenceAngle + 60, time);
                    enemies[2].SetTargetAngle(referenceAngle - 60, time);
                    enemies[3].SetTargetAngle(referenceAngle + 120, time);
                    enemies[4].SetTargetAngle(referenceAngle + 180, time);
                    break;
            }
        }
        else if (enemies.Count == 6)
        {
            float type = Random.Range(0, 8);
            switch (type)
            {
                case 0:
                    enemies[0].SetTargetAngle(referenceAngle, time);
                    enemies[1].SetTargetAngle(referenceAngle + 60, time);
                    enemies[2].SetTargetAngle(referenceAngle - 60, time);
                    enemies[3].SetTargetAngle(referenceAngle + 120, time);
                    enemies[4].SetTargetAngle(referenceAngle - 120, time);
                    enemies[5].SetTargetAngle(referenceAngle + 180, time);
                    break;
                case 1:
                    enemies[0].SetTargetAngle(referenceAngle, time);
                    enemies[1].SetTargetAngle(referenceAngle + 35, time);
                    enemies[2].SetTargetAngle(referenceAngle - 35, time);
                    enemies[3].SetTargetAngle(referenceAngle + 70, time);
                    enemies[4].SetTargetAngle(referenceAngle - 70, time);
                    enemies[5].SetTargetAngle(referenceAngle + 105, time);
                    break;
                case 2:
                    enemies[0].SetTargetAngle(referenceAngle, time);
                    enemies[1].SetTargetAngle(referenceAngle + 35, time);
                    enemies[2].SetTargetAngle(referenceAngle - 35, time);
                    enemies[3].SetTargetAngle(referenceAngle + 70, time);
                    enemies[4].SetTargetAngle(referenceAngle - 70, time);
                    enemies[5].SetTargetAngle(referenceAngle - 105, time);
                    break;
                case 3:
                    enemies[0].SetTargetAngle(referenceAngle, time);
                    enemies[1].SetTargetAngle(referenceAngle + 35, time);
                    enemies[2].SetTargetAngle(referenceAngle - 35, time);
                    enemies[3].SetTargetAngle(referenceAngle + 70, time);
                    enemies[4].SetTargetAngle(referenceAngle - 70, time);
                    enemies[5].SetTargetAngle(referenceAngle + 180, time);
                    break;
                case 4:
                    enemies[0].SetTargetAngle(referenceAngle, time);
                    enemies[1].SetTargetAngle(referenceAngle + 45, time);
                    enemies[2].SetTargetAngle(referenceAngle - 45, time);
                    enemies[3].SetTargetAngle(referenceAngle + 90, time);
                    enemies[4].SetTargetAngle(referenceAngle - 90, time);
                    enemies[5].SetTargetAngle(referenceAngle + 135, time);
                    break;
                case 5:
                    enemies[0].SetTargetAngle(referenceAngle, time);
                    enemies[1].SetTargetAngle(referenceAngle + 45, time);
                    enemies[2].SetTargetAngle(referenceAngle - 45, time);
                    enemies[3].SetTargetAngle(referenceAngle + 90, time);
                    enemies[4].SetTargetAngle(referenceAngle - 90, time);
                    enemies[5].SetTargetAngle(referenceAngle - 135, time);
                    break;
                case 6:
                    enemies[0].SetTargetAngle(referenceAngle, time);
                    enemies[1].SetTargetAngle(referenceAngle + 35, time);
                    enemies[2].SetTargetAngle(referenceAngle + 120, time);
                    enemies[3].SetTargetAngle(referenceAngle + 155, time);
                    enemies[4].SetTargetAngle(referenceAngle + 240, time);
                    enemies[5].SetTargetAngle(referenceAngle + 275, time);
                    break;
                case 7:
                    enemies[0].SetTargetAngle(referenceAngle, time);
                    enemies[1].SetTargetAngle(referenceAngle + 35, time);
                    enemies[2].SetTargetAngle(referenceAngle - 35, time);
                    enemies[3].SetTargetAngle(referenceAngle + 145, time);
                    enemies[4].SetTargetAngle(referenceAngle + 180, time);
                    enemies[5].SetTargetAngle(referenceAngle + 215, time);
                    break;
            }
        }
        else if (enemies.Count == 7)
        {
            float type = Random.Range(0, 5);
            switch (type)
            {
                case 0:
                    enemies[0].SetTargetAngle(referenceAngle, time);
                    enemies[1].SetTargetAngle(referenceAngle + 51, time);
                    enemies[2].SetTargetAngle(referenceAngle - 51, time);
                    enemies[3].SetTargetAngle(referenceAngle + 102, time);
                    enemies[4].SetTargetAngle(referenceAngle - 102, time);
                    enemies[5].SetTargetAngle(referenceAngle + 153, time);
                    enemies[6].SetTargetAngle(referenceAngle - 153, time);
                    break;
                case 1:
                    enemies[0].SetTargetAngle(referenceAngle, time);
                    enemies[1].SetTargetAngle(referenceAngle + 35, time);
                    enemies[2].SetTargetAngle(referenceAngle - 35, time);
                    enemies[3].SetTargetAngle(referenceAngle + 70, time);
                    enemies[4].SetTargetAngle(referenceAngle - 70, time);
                    enemies[5].SetTargetAngle(referenceAngle + 105, time);
                    enemies[6].SetTargetAngle(referenceAngle - 105, time);
                    break;
                case 2:
                    enemies[0].SetTargetAngle(referenceAngle + 18, time);
                    enemies[1].SetTargetAngle(referenceAngle - 18, time);
                    enemies[2].SetTargetAngle(referenceAngle + 53, time);
                    enemies[3].SetTargetAngle(referenceAngle - 53, time);
                    enemies[4].SetTargetAngle(referenceAngle + 180, time);
                    enemies[5].SetTargetAngle(referenceAngle + 215, time);
                    enemies[6].SetTargetAngle(referenceAngle + 145, time);
                    break;
                case 3:
                    enemies[0].SetTargetAngle(referenceAngle + 18, time);
                    enemies[1].SetTargetAngle(referenceAngle - 18, time);
                    enemies[2].SetTargetAngle(referenceAngle + 53, time);
                    enemies[3].SetTargetAngle(referenceAngle - 53, time);
                    enemies[4].SetTargetAngle(referenceAngle + 180, time);
                    enemies[5].SetTargetAngle(referenceAngle + 88, time);
                    enemies[6].SetTargetAngle(referenceAngle - 88, time);
                    break;
                case 4:
                    enemies[0].SetTargetAngle(referenceAngle + 18, time);
                    enemies[1].SetTargetAngle(referenceAngle - 18, time);
                    enemies[2].SetTargetAngle(referenceAngle + 90 + 18, time);
                    enemies[3].SetTargetAngle(referenceAngle + 90 - 18, time);
                    enemies[4].SetTargetAngle(referenceAngle + 180, time);
                    enemies[5].SetTargetAngle(referenceAngle - 90 + 18, time);
                    enemies[6].SetTargetAngle(referenceAngle - 90 - 18, time);
                    break;
            }
        }
        else if (enemies.Count == 8)
        {
            float type = Random.Range(0, 4);
            switch (type)
            {
                case 0:
                    enemies[0].SetTargetAngle(referenceAngle, time);
                    enemies[1].SetTargetAngle(referenceAngle + 45, time);
                    enemies[2].SetTargetAngle(referenceAngle - 45, time);
                    enemies[3].SetTargetAngle(referenceAngle + 90, time);
                    enemies[4].SetTargetAngle(referenceAngle - 90, time);
                    enemies[5].SetTargetAngle(referenceAngle + 135, time);
                    enemies[6].SetTargetAngle(referenceAngle - 135, time);
                    enemies[7].SetTargetAngle(referenceAngle + 180, time);
                    break;
                case 1:
                    enemies[0].SetTargetAngle(referenceAngle + 18, time);
                    enemies[1].SetTargetAngle(referenceAngle - 18, time);
                    enemies[2].SetTargetAngle(referenceAngle + 90 + 18, time);
                    enemies[3].SetTargetAngle(referenceAngle + 90 - 18, time);
                    enemies[4].SetTargetAngle(referenceAngle - 90 + 18, time);
                    enemies[5].SetTargetAngle(referenceAngle - 90 - 18, time);
                    enemies[6].SetTargetAngle(referenceAngle + 180 - 18, time);
                    enemies[7].SetTargetAngle(referenceAngle + 180 + 18, time);
                    break;
                case 2:
                    enemies[0].SetTargetAngle(referenceAngle, time);
                    enemies[1].SetTargetAngle(referenceAngle + 35, time);
                    enemies[2].SetTargetAngle(referenceAngle + 70, time);
                    enemies[3].SetTargetAngle(referenceAngle + 105, time);
                    enemies[4].SetTargetAngle(referenceAngle - 35, time);
                    enemies[5].SetTargetAngle(referenceAngle - 70, time);
                    enemies[6].SetTargetAngle(referenceAngle - 105, time);
                    enemies[7].SetTargetAngle(referenceAngle + 140, time);
                    break;
                case 3:
                    enemies[0].SetTargetAngle(referenceAngle - 18, time);
                    enemies[1].SetTargetAngle(referenceAngle + 18, time);
                    enemies[2].SetTargetAngle(referenceAngle - 18 - 35, time);
                    enemies[3].SetTargetAngle(referenceAngle + 18 + 35, time);
                    enemies[4].SetTargetAngle(referenceAngle + 180 - 18, time);
                    enemies[5].SetTargetAngle(referenceAngle + 180 + 18, time);
                    enemies[6].SetTargetAngle(referenceAngle + 180 - 18 - 35, time);
                    enemies[7].SetTargetAngle(referenceAngle + 180 + 18 + 35, time);
                    break;

            }
        }
    }

    //Move in the same direction
    private void handleMoveSameDirection(bool firstHalf)
    {
        float progress = (Time.time - mCurrentStateStartTime) / (mNextTransitionTime - mCurrentStateStartTime);
        if (!firstHalf)
        {
            progress = 1.0f - progress;
        }

        float speed = Mathf.Lerp(0, mSpeedModifier, progress);

        enemies.ForEach(enemy =>
        {
            enemy.SetRotationSpeed(speed);
            enemy.FireBulletRate((1f - progress) / mFireRateModifier);
        });
    }

    //Move in the same direction, bullet speed wave
    private void handleMoveSameDirectionAlternateBulletSpeed(bool firstHalf)
    {
        float progress = (Time.time - mCurrentStateStartTime) / (mNextTransitionTime - mCurrentStateStartTime);
        if (!firstHalf)
        {
            progress = 1.0f - progress;
        }

        float speed = Mathf.Lerp(0, mSpeedModifier, progress);
        float modifiedProgress = Mathf.Abs(Mathf.Cos(5 * progress));

        enemies.ForEach(enemy =>
        {
            enemy.SetRotationSpeed(speed);
            enemy.FireBulletRate(modifiedProgress / mFireRateModifier);
        });
    }

    //Move in opposite directions
    private void handleDoubleStatePattern2(bool firstHalf)
    {
        float progress = (Time.time - mCurrentStateStartTime) / (mNextTransitionTime - mCurrentStateStartTime);
        if (!firstHalf)
        {
            progress = 1.0f - progress;
        }

        float speed = Mathf.Lerp(0, mSpeedModifier, progress);
        enemies[0].SetRotationSpeed(speed);
        enemies[1].SetRotationSpeed(-speed);
        enemies[0].FireBulletRate((1f - progress) / mFireRateModifier);
        enemies[1].FireBulletRate((1f - progress) / mFireRateModifier);

        checkEnemyProximity();
    }

    //One in one direction, the others in teh other
    private void handleTripleStatePattern(bool firstHalf, int index)
    {
        float progress = (Time.time - mCurrentStateStartTime) / (mNextTransitionTime - mCurrentStateStartTime);
        if (!firstHalf)
        {
            progress = 1.0f - progress;
        }

        float speed = Mathf.Lerp(0, mSpeedModifier, progress);
        for (int i=0; i<enemies.Count; i++)
        {
            if(i==index)
            {
                enemies[i].SetRotationSpeed(speed);
            } else
            {
                enemies[i].SetRotationSpeed(-speed);
            }
            enemies[i].FireBulletRate((1f - progress) / mFireRateModifier);
        }

        checkEnemyProximity();
    }

    private void skipToNextPhase()
    {
        mNextTransitionTime = Time.time;
        enemies.ForEach(enemy =>
        {
            enemy.SetRotationSpeed(0);
        });
    }

    private void checkEnemyProximity()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            for (int j = 0; j < enemies.Count; j++)
            {
                float angleDiff = Mathf.Abs(enemies[i].GetAngle() - enemies[j].GetAngle());
                if (angleDiff < MIN_ANGLE || angleDiff > 360 - MIN_ANGLE)
                {
                    float firstSpeed = enemies[i].GetRotationSpeed();
                    float secondSpeed = enemies[j].GetRotationSpeed();

                    if (firstSpeed * secondSpeed < 0)
                    {
                        if (firstSpeed < 0)
                        {
                            if (enemies[i].GetAngle() > enemies[j].GetAngle())
                            {
                                skipToNextPhase();
                            }
                            else if (enemies[j].GetAngle() > enemies[i].GetAngle() && enemies[j].GetAngle() - enemies[i].GetAngle() > 180)
                            {
                                skipToNextPhase();
                            }
                        }
                        else
                        {
                            if (enemies[i].GetAngle() < enemies[j].GetAngle())
                            {
                                skipToNextPhase();
                            }
                            else if (enemies[j].GetAngle() < enemies[i].GetAngle() && enemies[i].GetAngle() - enemies[j].GetAngle() > 180)
                            {
                                skipToNextPhase();
                            }
                        }
                    }
                }
            }
        }
    }

    public void GameOver()
    {
        mAlive = false;
        PlayGamesPlatform.Instance.ReportProgress("CgkIvanrz-kPEAIQAw", 100.0f, res => { });
    }

    public void TogglePause()
    {
        GameObject pauseText = GameObject.Find("PauseText");

        if (mTimeScale == -1)
        {
            mTimeScale = Time.timeScale;
            Time.timeScale = 0.0f;
            pauseText.GetComponent<UnityEngine.UI.Text>().text = ">";

        }
        else
        {
            pauseText.GetComponent<UnityEngine.UI.Text>().text = "||";
            Time.timeScale = mTimeScale;
            mTimeScale = -1;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadLevel()
    {
        int playsBeforeAd = PlayerPrefs.GetInt("Ads", 5);
        playsBeforeAd--;
        if(playsBeforeAd == 0)
        {
            Advertisement.Show();
            PlayerPrefs.SetInt("Ads", 5);
        } else
        {
            PlayerPrefs.SetInt("Ads", playsBeforeAd);
        }

        PlayGamesPlatform.Instance.ReportProgress("CgkIvanrz-kPEAIQAg", 100.0f, res => { });

        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void showLeaderboard()
    {
        ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI("CgkIvanrz-kPEAIQAQ");
    }
}
