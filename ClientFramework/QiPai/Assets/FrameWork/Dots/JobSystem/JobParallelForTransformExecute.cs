using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Jobs;
using UnityEngine.Jobs;

namespace Lantis
{
	//[BurstCompile(CompileSynchronously = true)]
	public struct JobParallelForTransformExecute : IJobParallelForTransform
	{
		public void SetParamar(object paramars, Action<object, object, object> call)
		{
			var regist = new LantisJobRegist3();
			regist.releseCall = Relese;
			regist.paramar = paramars;
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

		public void Execute(int index, TransformAccess transform)
		{
			var regist = LantisJobSystem.GetRegistParamar(this);

			if (regist != null)
			{
				var regist3 = regist as LantisJobRegist3;

				if (regist3 != null && regist3.callFun != null)
				{
					regist3.callFun(transform, regist3.paramar, index);
				}
			}
		}
	}
}
