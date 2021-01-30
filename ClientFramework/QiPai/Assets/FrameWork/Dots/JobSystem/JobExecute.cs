using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Burst;
using Unity.Jobs;

namespace Lantis
{
	//[BurstCompile(CompileSynchronously = true)]
	public struct JobExecute : IJob
	{
		private bool autoRelese;
		public void SetParamar(object paramar, Action<object> call)
		{
			var regist = new LantisJobRegist1();
			regist.releseCall = Relese;
			regist.paramar = paramar;
			regist.callFun = call;
			LantisJobSystem.AddRegistParamar(this, regist);
		}

		public void SetHandle(JobHandle handle)
		{
			LantisJobSystem.SetHaneld(this,handle);
		}

		public void Relese()
		{
			LantisJobSystem.RemoveRegistParamar(this);
		}

		public void Execute()
		{
			var regist = LantisJobSystem.GetRegistParamar(this);

			if (regist != null)
			{
				var regist1 = regist as LantisJobRegist1;

				if (regist1 != null && regist1.callFun != null)
				{
					regist1.callFun(regist1.paramar);
				}
			}
		}
	}
}
