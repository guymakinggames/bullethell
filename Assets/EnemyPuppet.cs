using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPuppet : MonoBehaviour
{
    private const float DISTANCE_CENTER = 1f;
    private const float DISTANCE_EDGE = 0.25f;
    private const float AUTO_MOVEMENT_SPEED = 100f;

    public Bullet mBullet;
    private float mAngle;
    private float mRotationSpeed;
    private float mFireBulletRate;
    private float mNextFireTime = -1;
    private float mTargetAngle = -1;

    private float mTargetTime = -1;
    private float mStartTime = -1;
    private float mStartAngle = -1;


    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localScale.x < 0.5f)
        {
            float newVal = transform.localScale.x * 2 + Time.deltaTime;
            if(newVal > 1)
            {
                newVal = 1;
            }

            transform.localScale = new Vector3(newVal*0.5f, newVal*0.5f, newVal*0.2f);
        }

        bool skipPos = false;

        if(mTargetTime > Time.time)
        {
            float progress = (Time.time - mStartTime) / (mTargetTime - mStartTime);
            progress = Mathf.Sin(progress * Mathf.PI / 2);
            mAngle = Mathf.LerpAngle(mStartAngle, mTargetAngle, progress);

            float dist = Mathf.Abs(mTargetAngle - mStartAngle);
            if(dist > 90 && dist < 270)
            {
                float returnToZeroProgress = Mathf.Sin(progress * Mathf.PI);
                float targetAngle = mTargetAngle / 360f * 2 * Mathf.PI;
                float startAngle = mStartAngle / 360f * 2 * Mathf.PI;

                Vector3 endPos = new Vector3(Mathf.Cos(targetAngle) * DISTANCE_CENTER, Mathf.Sin(targetAngle) * DISTANCE_CENTER, 0);
                Vector3 startPos = new Vector3(Mathf.Cos(startAngle) * DISTANCE_CENTER, Mathf.Sin(startAngle) * DISTANCE_CENTER, 0);

                Vector3 updatedPos = new Vector3(Mathf.Lerp(startPos.x, endPos.x, progress), Mathf.Lerp(startPos.y, endPos.y, progress), - returnToZeroProgress * DISTANCE_CENTER);
                transform.position = updatedPos;
                skipPos = true;
            }

        } else
        {
            mAngle = mAngle + (Time.deltaTime * mRotationSpeed);
        }

        if (mAngle < 0)
        {
            mAngle = mAngle + 360;
        }
        else if (mAngle > 360)
        {
            mAngle = mAngle - 360;
        }

        if (!skipPos)
        {
            float angleRads = mAngle / 360f * 2 * Mathf.PI;
            Vector3 pos = new Vector3(Mathf.Cos(angleRads) * DISTANCE_CENTER, Mathf.Sin(angleRads) * DISTANCE_CENTER, 0);
            transform.position = pos;
        }
        transform.eulerAngles = new Vector3(0, 0, mAngle + 270);

        if (mFireBulletRate > 0 && mNextFireTime < 0)
        {
            mNextFireTime = Time.time + mFireBulletRate;
        }

        if(Time.time > mNextFireTime && mNextFireTime > 0 && Time.timeScale > 0.5f)
        {
            FireBullet();
            mNextFireTime = -1;
        }
    }

    //>0 for clockwise, <0 for counter-clockwise
    public void SetRotationSpeed(float speed)
    {
        mRotationSpeed = speed;
    }

    public float GetRotationSpeed()
    {
        return mRotationSpeed;
    }

    public void FireBulletRate(float fireRate)
    {
        mFireBulletRate = fireRate;
    }

    public void SetTargetAngle(float angle, float targetTime)
    {
        if(angle < 0)
        {
            angle += 360;
        } else if (angle > 360)
        {
            angle -= 360;
        }

        mTargetAngle = angle;
        mTargetTime = Time.time + targetTime;
        mStartTime = Time.time;
        mStartAngle = mAngle;
    }

    public void OverrideAngle(float angle)
    {
        mAngle = angle;
    }

    public float GetAngle()
    {
        return mAngle;
    }

    private void FireBullet()
    {
        float angleRads = mAngle / 360f * 2 * Mathf.PI;
        Vector3 startPosition = transform.position + new Vector3(Mathf.Cos(angleRads) * DISTANCE_EDGE, Mathf.Sin(angleRads) * DISTANCE_EDGE, 0);
        Bullet bullet = Instantiate(mBullet, startPosition, Quaternion.identity);
        bullet.setDirection(new Vector3(Mathf.Cos(angleRads), Mathf.Sin(angleRads), 0));
    }

}
