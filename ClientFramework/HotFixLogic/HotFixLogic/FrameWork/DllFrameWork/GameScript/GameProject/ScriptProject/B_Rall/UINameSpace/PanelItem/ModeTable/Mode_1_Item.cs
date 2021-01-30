using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace Rall
{
    public class Mode_1_Item : TablePanelItem
    {
		/// <summary>
		/// 获取面包信息
		/// </summary>
		public override void OnGetPanelInfo()
        {
            base.OnGetPanelInfo();			
        }

        /// <summary>
        /// 选中了这个Item
        /// </summary>
        public override void OnSelect()
        {
            base.OnSelect();
        }

        public override void OnUnSelect()
        {
            base.OnUnSelect();            
        }

        /// <summary>
        /// 外部调用接口
        /// </summary>
        /// <param name="parmaras"></param>
        public override void ExitCall(object parmaras)
        {
            base.ExitCall(parmaras);
        }
    }
}

