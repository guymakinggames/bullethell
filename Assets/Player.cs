using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const float DISTANCE_CENTER = 4.25f;
    private const float ANGLE_CHANGE_SPEED = 100f;
    private const float SELF_ANGLE_CHANGE_SPEED = 100f;
    private const float RETURN_TO_NORMAL_SPEED = 175f;
    private const float MAX_ANGLE = 50f;
    private const float DEATH_ROTATION_SPEED = 100f;

    private float mAngle = 270f;
    private float mSelfRotationAngle = 0f;

    private bool mAlive = true;
    private Vector3 mDeathPosition = new Vector3(0,0,0);
    private Vector3 mDeathRotationAngle = new Vector3(0, 0, 0);
    private float mDeathEndTime = -1;
    private float mDeathStartTime = -1;

    //Dummy Mode Variables
    public bool mDummyMode = true;
    private int mDummyAction = 0; //-1 left, 0 idle, 1 right
    private float mNextSwitchTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mAlive)
        {
            if (mDummyMode)
            {
                if(Time.time > mNextSwitchTime)
                {
                    mDummyAction = Random.Range(0, 3) - 1;
                    mNextSwitchTime = Time.time + Random.Range(1f, 10f);
                }

                switch(mDummyAction)
                {
                    case -1:
                        moveLeft();
                        break;
                    case 0:
                        returnToNeutral();
                        break;
                    case 1:
                        moveRight();
                        break;
                }
            }
            else
            {
                if (Input.GetKey("left"))
                {
                    moveLeft();
                }
                else if (Input.GetKey("right"))
                {
                    moveRight();
                }
                else if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    if (touch.position.x < 0.3f * Screen.width)
                    {
                        moveLeft();
                    }
                    else if (touch.position.x > 0.7f * Screen.width)
                    {
                        moveRight();
                    }
                    else
                    {
                        returnToNeutral();
                    }
                }
                else
                {
                    returnToNeutral();
                }
            }

            keepAnglesInBounds();

            float angleRads = mAngle / 360f * 2 * Mathf.PI;

            Vector3 pos = new Vector3(Mathf.Cos(angleRads) * DISTANCE_CENTER, Mathf.Sin(angleRads) * DISTANCE_CENTER, 0);
            transform.position = pos;

            transform.eulerAngles = new Vector3(Mathf.Cos(angleRads) * mSelfRotationAngle, Mathf.Sin(angleRads) * mSelfRotationAngle, mAngle - 270);
        } else
        {
            float progress = (Time.time - mDeathStartTime) / (mDeathEndTime - mDeathStartTime);
            transform.position = Vector3.Lerp(mDeathPosition, new Vector3(0, 0, 0), progress);
            transform.Rotate(mDeathRotationAngle, Time.deltaTime * DEATH_ROTATION_SPEED);
        }
    }

    private void moveLeft()
    {
        mAngle -= Time.deltaTime * ANGLE_CHANGE_SPEED;
        if (mSelfRotationAngle > 0)
        {
            mSelfRotationAngle -= Time.deltaTime * RETURN_TO_NORMAL_SPEED;
        }
        else
        {
            mSelfRotationAngle -= Time.deltaTime * SELF_ANGLE_CHANGE_SPEED;
        }
    }

    private void moveRight()
    {
        mAngle += Time.deltaTime * ANGLE_CHANGE_SPEED;
        if (mSelfRotationAngle < 0)
        {
            mSelfRotationAngle += Time.deltaTime * RETURN_TO_NORMAL_SPEED;
        }
        else
        {
            mSelfRotationAngle += Time.deltaTime * SELF_ANGLE_CHANGE_SPEED;
        }
    }

    private void returnToNeutral()
    {
        if (mSelfRotationAngle > 0)
        {
            mSelfRotationAngle -= Time.deltaTime * RETURN_TO_NORMAL_SPEED;
            if (mSelfRotationAngle < 0)
            {
                mSelfRotationAngle = 0;
            }
        }
        else
        {
            mSelfRotationAngle += Time.deltaTime * RETURN_TO_NORMAL_SPEED;
            if (mSelfRotationAngle > 0)
            {
                mSelfRotationAngle = 0;
            }
        }
    }

    private void keepAnglesInBounds()
    {
        if (mSelfRotationAngle > MAX_ANGLE)
        {
            mSelfRotationAngle = MAX_ANGLE;
        }
        else if (mSelfRotationAngle < -MAX_ANGLE)
        {
            mSelfRotationAngle = -MAX_ANGLE;
        }

        if (mAngle > 360f)
        {
            mAngle = mAngle - 360f;
        }
        else if (mAngle < 0f)
        {
            mAngle = mAngle + 360f;
        }
    }

    public void StartDeathAnimation()
    {
        mDeathStartTime = Time.time;
        mDeathEndTime = Time.time + 5f;
        mDeathPosition = transform.position;
        mDeathRotationAngle = new Vector3(transform.position.x, transform.position.y, Random.Range(2f, 5f));
        mAlive = false;
    }
}
