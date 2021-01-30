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

	private float recordValue;

    public void OnPointerDown()
    {
		mNeedMove = false;
		recordValue = m_Scrollbar.value;
	}

    public void OnPointerUp()
    {
		float invate = 1.0f / (count - 1);
		float dir = m_Scrollbar.value - recordValue;
		if (dir > 0 && dir > invate / 3)
		{
			if (mTargetValue != 1.0f)
			{
				mTargetValue += invate;
			}
		}
		else if(dir < 0 && -dir > invate / 3)
		{
			if (mTargetValue != 0.0f)
			{
				mTargetValue -= invate;
			}
		}
		mNeedMove = true;
        mMoveSpeed = 0;
    }

    public void OnButtonClick(int value)
    {
		mTargetValue = 1.0f / (count - 1) * value;
        mNeedMove = true;
    }

    void Update()
    {
        if (mNeedMove)
        {
            if (Mathf.Abs(m_Scrollbar.value - mTargetValue) < 0.01f)
            {
                m_Scrollbar.value = mTargetValue;
                mNeedMove = false;
                return;
            }
            m_Scrollbar.value = Mathf.SmoothDamp(m_Scrollbar.value, mTargetValue, ref mMoveSpeed, SMOOTH_TIME);
        }
    }

}