using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FrameWork
{
    /// <summary>
    /// UI特效状态
    /// </summary>
    public class UIEffectState
    {
        public GameObject particleAction;

        public bool enableState;
    }

    public class UIEffectStateKeys
    {
        /// <summary>
        /// 绑定的UI
        /// </summary>
        public GameObject bindUi;
        /// <summary>
        /// 特效状态列表
        /// </summary>
        public List<UIEffectState> effectStateList = new List<UIEffectState>();
        
        /// <summary>
        /// 激活显示全部的特效
        /// </summary>
        public void ActiveAll(bool enable)
        {
            for (int i = 0; i < effectStateList.Count; ++i)
            {
                UIEffectState effectState = effectStateList[i];

                if (enable)
                {
                    effectState.particleAction.SetActive(effectState.enableState);
                }
                else
                {
                    effectState.particleAction.SetActive(false);
                }
            }
        }
    }

    public class UIEffectManager
    {
		private static bool isOpen = false;
        public static Dictionary<GameObject, UIEffectStateKeys> effectMap = new Dictionary<GameObject, UIEffectStateKeys>();

        /// <summary>
        /// 注册UI
        /// </summary>
        /// <param name="bindUI"></param>
        public static void RegistUIEffect(GameObject bindUI)
        {
			if (!isOpen)
			{
				return;
			}
            ParticleSystem[] particileSystemList = bindUI.GetComponentsInChildren<ParticleSystem>(true);
            if (particileSystemList != null && particileSystemList.Length > 0)
            {
                if (!effectMap.ContainsKey(bindUI))
                {
                    UIEffectStateKeys effectStateKey = new UIEffectStateKeys();
                    effectMap.Add(bindUI, effectStateKey);
                    effectStateKey.bindUi = bindUI;

                    for (int i = 0; i < particileSystemList.Length; ++i)
                    {
                        UIEffectState effectState = new UIEffectState();
                        effectState.particleAction = particileSystemList[i].gameObject;
                        effectState.enableState = particileSystemList[i].gameObject.activeSelf;

                        effectStateKey.effectStateList.Add(effectState);
                    }

                    effectStateKey.ActiveAll(false);
                }
            }
        }

        /// <summary>
        /// 释放UI节点
        /// </summary>
        /// <param name="bindUI"></param>
        public static void UnRegistUIEffect(GameObject bindUI)
        {
			if (!isOpen)
			{
				return;
			}
			if (effectMap.ContainsKey(bindUI))
            {
                UIEffectStateKeys effectStateKey = effectMap[bindUI];
                effectMap.Remove(bindUI);
            }
        }

        /// <summary>
        /// 关闭所有的特效
        /// </summary>
        public static void CloseAllEffect()
        {
			if (!isOpen)
			{
				return;
			}

			foreach (var kv in effectMap)
            {
                kv.Value.ActiveAll(false);
            }
        }

        /// <summary>
        /// 关闭其他所有的
        /// </summary>
        public static void UpTopEffect(GameObject showUI)
        {
			if (!isOpen)
			{
				return;
			}

			if (effectMap.ContainsKey(showUI))
            {
                CloseAllEffect();

                UIEffectStateKeys effectStateKey = effectMap[showUI];

                effectStateKey.ActiveAll(true);
            }
        }
    }
}
