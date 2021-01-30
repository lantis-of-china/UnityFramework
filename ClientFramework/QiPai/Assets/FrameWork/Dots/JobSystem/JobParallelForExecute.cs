using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Jobs;

namespace Lantis
{
	//[BurstCompile(CompileSynchronously = true)]
	public struct JobParallelForExecute : IJobParallelFor
	{
		public void SetParamar(object paramar, Action<object,object> call)
		{
			var regist = new LantisJobRegist2();
			regist.releseCall = Relese;
			regist.paramar = paramar;
			regist.callFun = call;
			LantisJobSystem.AddRegistParamar(this, regist);
		}

		public void SetHandle(JobHandle handle)
		{
			LantisJobSystem.SetHaneld(this, handle);
		}

		public void Relese()
		{
			LantisJobSystem.RemoveRegistParamar(this);
		}

		public void Execute(int index)
		{
			var regist = LantisJobSystem.GetRegistParamar(this);

			if (regist != null)
			{
				var regist2 = regist as LantisJobRegist2;

				if (regist2 != null && regist2.callFun != null)
				{
					regist2.callFun(regist2.paramar, index);
				}
			}
		}
	}
}
