using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderControl : MonoBehaviour
{
	public int count = 5;
    public Scrollbar m_Scrollbar;
    public ScrollRect m_ScrollRect;

    private float mTargetValue;

    private bool mNeedMove = false;

    private const float MOVE_SPEED = 1F;

    private const float SMOOTH_TIME = 0.2F;

    private float mMoveSpeed = 0f;
	/// <summary>
	/// 按下时候的进度值
	/// </summary>
	private float recordValue;
	/// <summary>
	/// 是否按下
	/// </summary>
	private bool isTouch;
	/// <summary>
	/// 时间间隔
	/// </summary>
	private float invateTime;
	/// <summary>
	/// 当前轮询
	/// </summary>
	private int curIndex;
	/// <summary>
	/// 向后
	/// </summary>
	private bool add;

	void Awake()
	{
		isTouch = false;
		invateTime = 0.0f;
		add = true;
		OnButtonClick(0);
	}


    public void OnPointerDown()
    {
		isTouch = true;
		mNeedMove = false;
		recordValue = m_Scrollbar.value;
	}

    public void OnPointerUp()
    {
		float invate = 1.0f / (count - 1);
		float dir = m_Scrollbar.value - recordValue;
		if (dir > 0.0f && dir > invate / 3.0f)
		{
			if (mTargetValue != 1.0f)
			{
				mTargetValue += invate;
			}

			add = true;
		}
		else if(dir < 0.0f && dir < invate / 3.0f)
		{
			if (mTargetValue != 0.0f)
			{
				mTargetValue -= invate;
			}

			add = false;
		}

		isTouch = false;
		mNeedMove = true;
        mMoveSpeed = 0;
    }

    public void OnButtonClick(int value)
    {
		mTargetValue = 1.0f / (count - 1) * value;

		if (mTargetValue > 1.0f) {
			mTargetValue = 1.0f;
		} else if (mTargetValue < 0.0f) {
			mTargetValue = 0.0f;
		}
        mNeedMove = true;		
    }

	
	public void UPAnimation()
	{		
		//自动移动
		invateTime += Time.deltaTime;
		if (invateTime > 5.0f) {
			invateTime = 0.0f;
			if (add) 
			{
				if (curIndex == (count-1)) 
				{
					add = false;
					curIndex -= 1;
				} 
				else 
				{
					curIndex += 1;
				}
			} 
			else 
			{
				if (curIndex == 0) 
				{		
					add = true;
					curIndex += 1;
				} else 
				{
					curIndex -= 1;
				}
			}

			OnButtonClick (curIndex);
		}

	}

    void Update()
    {
		if (mNeedMove)
		{
			if (Mathf.Abs(m_Scrollbar.value - mTargetValue) < 0.01f)
			{
				invateTime = 0.0f;
				m_Scrollbar.value = mTargetValue;
				mNeedMove = false;

				curIndex = (int)(mTargetValue / (1.0f / (count-1)));
				return;
			}
			m_Scrollbar.value = Mathf.SmoothDamp(m_Scrollbar.value, mTargetValue, ref mMoveSpeed, SMOOTH_TIME);
		}
		else if(!isTouch)
		{
			UPAnimation ();
		}
    }

}