using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class UGUISpriteAnimation : MonoBehaviour
{
	public static UGUISpriteAnimation AddComp(GameObject target,bool clear)
	{
		UGUISpriteAnimation comp = target.GetComponent<UGUISpriteAnimation>();
		if (comp == null)
		{
			comp = target.AddComponent<UGUISpriteAnimation>();
		}

		if (clear)
		{
			comp.finishCall = null;
			comp.mDelta = 0;
			comp.mCurFrame = 0;
			comp.SpriteFrames.Clear();
		}

		return comp;
	}

	public Action finishCall;
    public Image ImageSource;
    private int mCurFrame = 0;
    private float mDelta = 0;

    public float FPS = 5;
    public List<Sprite> SpriteFrames = new List<Sprite>();
    public bool IsPlaying = false;
    public bool Foward = true;
    public bool AutoPlay = false;
    public bool Loop = false;

    public int FrameCount
    {
        get
        {
            return SpriteFrames.Count;
        }
    }

    void Awake()
    {
        if (ImageSource == null)
        {
            ImageSource = GetComponent<Image>();
        }
    }

    void OnEnable()
    {
        if (AutoPlay)
        {
            Play();
        }
        else
        {
            IsPlaying = false;
        }
    }

    public void SetSprite(int idx)
    {
        if (idx < SpriteFrames.Count)
        {
            ImageSource.overrideSprite = SpriteFrames[idx];
			ImageSource.sprite = ImageSource.overrideSprite;
			ImageSource.SetNativeSize();
        }
    }

    public void Play()
    {
        IsPlaying = true;
        Foward = true;
    }

    public void PlayReverse()
    {
        IsPlaying = true;
        Foward = false;
    }

    void Update()
    {
        if (!IsPlaying || 0 == FrameCount)
        {
            return;
        }

        mDelta += Time.deltaTime;
        if (mDelta > 1 / FPS)
        {
            mDelta = 0;
            if (Foward)
            {
                mCurFrame++;
            }
            else
            {
                mCurFrame--;
            }

            if (mCurFrame >= FrameCount)
            {
                if (Loop)
                {
                    mCurFrame = 0;
                }
                else
                {
					PlayEnd();
					return;
                }
            }
            else if (mCurFrame < 0)
            {
                if (Loop)
                {
                    mCurFrame = FrameCount - 1;
                }
                else
                {
					PlayEnd();
					return;
                }
            }

            SetSprite(mCurFrame);
        }
    }

    public void Pause()
    {
        IsPlaying = false;
    }

    public void Resume()
    {
        if (!IsPlaying)
        {
            IsPlaying = true;
        }
    }

    public void Stop()
    {
        mCurFrame = 0;
        SetSprite(mCurFrame);
        IsPlaying = false;
    }

    public void Rewind()
    {
        mCurFrame = 0;
        SetSprite(mCurFrame);
        Play();
    }


	public void PlayEnd()
	{
		IsPlaying = false;

		if (finishCall != null)
		{
			finishCall();
		}
	}
}