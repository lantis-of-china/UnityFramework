using System;
using UnityEngine;
using System.Collections;
using ILRuntime.CLR.TypeSystem;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Stack;
using System.Collections.Generic;

public class ILRuntimeRegistAdapter
{
    public static List<Type> defineTypeList = new List<Type>()
    {
        typeof(UnityEngine.TrailRenderer),
        typeof(UnityEngine.SkinnedMeshRenderer),
        typeof(UnityEngine.MeshRenderer),
        typeof(UnityEngine.LineRenderer),
        typeof(RenderOrderLayerSet),
    };

    unsafe static public void RegisterAdapter(ILRuntime.Runtime.Enviorment.AppDomain _app)
    {
#if DEBUG && (UNITY_EDITOR || UNITY_ANDROID || UNITY_IPHONE)
        //由于Unity的Profiler接口只允许在主线程使用，为了避免出异常，需要告诉ILRuntime主线程的线程ID才能正确将函数运行耗时报告给Profiler
        _app.UnityMainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
#endif
        _app.RegisterValueTypeBinder(typeof(Vector3), new Vector3Binder());
        _app.RegisterValueTypeBinder(typeof(Quaternion), new QuaternionBinder());
        _app.RegisterValueTypeBinder(typeof(Vector2), new Vector2Binder());

        // register for delegate		
        _app.DelegateManager.RegisterMethodDelegate<List<ILTypeInstance>>();
        _app.DelegateManager.RegisterMethodDelegate<List<object>>();
        _app.DelegateManager.RegisterMethodDelegate<List<byte>>();
        _app.DelegateManager.RegisterMethodDelegate<List<char>>();
        _app.DelegateManager.RegisterMethodDelegate<List<short>>();
        _app.DelegateManager.RegisterMethodDelegate<List<int>>();
        _app.DelegateManager.RegisterMethodDelegate<List<long>>();
        _app.DelegateManager.RegisterMethodDelegate<List<float>>();
        _app.DelegateManager.RegisterMethodDelegate<List<double>>();
        _app.DelegateManager.RegisterMethodDelegate<List<Single>>();
        _app.DelegateManager.RegisterMethodDelegate<List<Action<object>>>();
        
        _app.DelegateManager.RegisterMethodDelegate<Dictionary<string, List<Action<object>>>>();
        _app.DelegateManager.RegisterMethodDelegate<Dictionary<string, ILTypeInstance>>();
        _app.DelegateManager.RegisterMethodDelegate<Dictionary<string, List<ILTypeInstance>>>();
        _app.DelegateManager.RegisterMethodDelegate<Dictionary<string, string>>();
        _app.DelegateManager.RegisterMethodDelegate<Dictionary<object,object>>();
        _app.DelegateManager.RegisterMethodDelegate<Dictionary<string, string>>();

        //_app.DelegateManager.RegisterMethodDelegate<LantisDictronaryList<string, List<Action<object>>>>();
        //_app.DelegateManager.RegisterMethodDelegate<LantisDictronaryList<string, ILTypeInstance>>();
        //_app.DelegateManager.RegisterMethodDelegate<LantisDictronaryList<string, List<ILTypeInstance>>>();
        //_app.DelegateManager.RegisterMethodDelegate<LantisDictronaryList<string, string>>();
        //_app.DelegateManager.RegisterMethodDelegate<LantisDictronaryList<object, object>>();
        //_app.DelegateManager.RegisterMethodDelegate<LantisDictronaryList<string, string>>();

        _app.DelegateManager.RegisterMethodDelegate<Action<object>>();
        _app.DelegateManager.RegisterMethodDelegate<Action<string>>();
		_app.DelegateManager.RegisterMethodDelegate<System.String, System.String, UnityEngine.LogType>();
		_app.DelegateManager.RegisterMethodDelegate<System.String>();
		_app.DelegateManager.RegisterMethodDelegate<System.Object>();
        _app.DelegateManager.RegisterMethodDelegate<System.Object, System.Object>();
        _app.DelegateManager.RegisterMethodDelegate<System.Object, System.Object, System.Object>();
        _app.DelegateManager.RegisterMethodDelegate<ILRuntime.Runtime.Intepreter.ILTypeInstance>();

		_app.DelegateManager.RegisterMethodDelegate<System.Single>();		_app.DelegateManager.RegisterDelegateConvertor<UnityEngine.Events.UnityAction<System.Single>>((act) =>
		{
			return new UnityEngine.Events.UnityAction<System.Single>((arg0) =>
			{
				((Action<System.Single>)act)(arg0);
			});
		});		_app.DelegateManager.RegisterMethodDelegate<System.Boolean>();
		_app.DelegateManager.RegisterDelegateConvertor<UnityEngine.Events.UnityAction<System.Boolean>>((act) =>
		{
			return new UnityEngine.Events.UnityAction<System.Boolean>((arg0) =>
			{
				((Action<System.Boolean>)act)(arg0);
			});
		});		_app.DelegateManager.RegisterFunctionDelegate<System.Int32, System.Int32, System.Int32>();		_app.DelegateManager.RegisterDelegateConvertor<System.Comparison<System.Int32>>((act) =>
		{
			return new System.Comparison<System.Int32>((x, y) =>
			{
				return ((Func<System.Int32, System.Int32, System.Int32>)act)(x, y);
			});
		});

		_app.DelegateManager.RegisterFunctionDelegate<ILRuntime.Runtime.Intepreter.ILTypeInstance, System.Boolean>();
		_app.DelegateManager.RegisterDelegateConvertor<System.Predicate<ILRuntime.Runtime.Intepreter.ILTypeInstance>>((act) =>
		{
			return new System.Predicate<ILRuntime.Runtime.Intepreter.ILTypeInstance>((obj) =>
			{
				return ((Func<ILRuntime.Runtime.Intepreter.ILTypeInstance, System.Boolean>)act)(obj);
			});
		});
		_app.DelegateManager.RegisterMethodDelegate<Application.LogCallback>();
		_app.DelegateManager.RegisterDelegateConvertor<UnityEngine.Application.LogCallback>((act) =>
		{
			return new UnityEngine.Application.LogCallback((condition, stackTrace, type) =>
			{
				((Action<System.String, System.String, UnityEngine.LogType>)act)(condition, stackTrace, type);
			});
		});		

		_app.DelegateManager.RegisterMethodDelegate<Action<System.String>>();
		_app.DelegateManager.RegisterDelegateConvertor<Action<System.String>>((act) =>
		{
			return new Action<System.String>((log) =>
			{
				((Action<System.String>)act)(log);
			});
		});
               
		_app.DelegateManager.RegisterMethodDelegate<CherishTween.ParamarCallFun>();
        _app.DelegateManager.RegisterDelegateConvertor<CherishTween.ParamarCallFun>((_action) =>
        {
            return new CherishTween.ParamarCallFun(delegate(object paramar)
            {
                ((System.Action<object>)_action)(paramar);
            });
        });             

        _app.DelegateManager.RegisterMethodDelegate<UnityEngine.Events.UnityAction>();
        _app.DelegateManager.RegisterDelegateConvertor<UnityEngine.Events.UnityAction>((_action) =>
        {
            return new UnityEngine.Events.UnityAction(delegate
            {
                ((System.Action)_action)();
            });
        });

        _app.DelegateManager.RegisterMethodDelegate<UnityEngine.EventSystems.BaseEventData>();
        _app.DelegateManager.RegisterDelegateConvertor<UnityEngine.Events.UnityAction<UnityEngine.EventSystems.BaseEventData>>((_action) =>
        {
            return new UnityEngine.Events.UnityAction<UnityEngine.EventSystems.BaseEventData>(delegate(UnityEngine.EventSystems.BaseEventData paramar)
            {
                ((System.Action<UnityEngine.EventSystems.BaseEventData>)_action)(paramar);
            });
        });

        _app.DelegateManager.RegisterMethodDelegate<System.Threading.ThreadStart>();
        _app.DelegateManager.RegisterDelegateConvertor<System.Threading.ThreadStart>((_action) =>
        {
            return new System.Threading.ThreadStart(delegate
            {
                ((System.Action)_action)();
            });
        });

        _app.DelegateManager.RegisterMethodDelegate<System.Threading.ParameterizedThreadStart>();
        _app.DelegateManager.RegisterDelegateConvertor<System.Threading.ParameterizedThreadStart>((_action) =>
        {
            return new System.Threading.ParameterizedThreadStart(delegate(object paramar)
            {
                ((System.Action<object>)_action)(paramar);
            });
        });

        _app.DelegateManager.RegisterFunctionDelegate<ILTypeInstance, ILTypeInstance, int>();
        _app.DelegateManager.RegisterDelegateConvertor<System.Comparison<ILTypeInstance>>((_action) =>
        {
            return new System.Comparison<ILTypeInstance>(delegate(ILTypeInstance p1, ILTypeInstance p2)
            {

                return ((System.Func<ILTypeInstance, ILTypeInstance, int>)_action)(p1, p2);
            });
        });

        _app.DelegateManager.RegisterFunctionDelegate<System.Object, System.Security.Cryptography.X509Certificates.X509Certificate, System.Security.Cryptography.X509Certificates.X509Chain, System.Net.Security.SslPolicyErrors, System.Boolean>();
        _app.DelegateManager.RegisterDelegateConvertor<System.Net.Security.RemoteCertificateValidationCallback>((_action) =>
        {
            return new System.Net.Security.RemoteCertificateValidationCallback(delegate(System.Object p1, System.Security.Cryptography.X509Certificates.X509Certificate p2, System.Security.Cryptography.X509Certificates.X509Chain p3, System.Net.Security.SslPolicyErrors p4)
            {

                return ((System.Func<System.Object, System.Security.Cryptography.X509Certificates.X509Certificate, System.Security.Cryptography.X509Certificates.X509Chain, System.Net.Security.SslPolicyErrors, System.Boolean>)_action)(p1, p2, p3, p4);
            });
        });

		_app.DelegateManager.RegisterMethodDelegate<UnityEngine.Vector2>();
		_app.DelegateManager.RegisterDelegateConvertor<UnityEngine.Events.UnityAction<UnityEngine.Vector2>>((act) =>
		{
			return new UnityEngine.Events.UnityAction<UnityEngine.Vector2>((arg0) =>
			{
				((Action<UnityEngine.Vector2>)act)(arg0);
			});
		});

		_app.DelegateManager.RegisterMethodDelegate<UnityEngine.Events.UnityAction<System.String>>();
		_app.DelegateManager.RegisterDelegateConvertor<UnityEngine.Events.UnityAction<System.String>>((act) =>
		{
			return new UnityEngine.Events.UnityAction<System.String>((arg0) =>
			{
				((Action<System.String>)act)(arg0);
			});
		});

		// for start cotoutine
		_app.RegisterCrossBindingAdaptor(new IEnumeratorObjectAdapter());
        _app.RegisterCrossBindingAdaptor(new IDisposableAdapter());
        _app.RegisterCrossBindingAdaptor(new IEnumeratorAdapter());
        _app.RegisterCrossBindingAdaptor(new QueueAdapter());
        _app.RegisterCrossBindingAdaptor(new MonoBehaviourAdapter());

        var mi = typeof(Debug).GetMethod("LogError", new System.Type[] { typeof(object) });
        _app.RegisterCLRMethodRedirection(mi, ClrRedirectionLogError);

        ILRuntime.Runtime.Generated.CLRBindings.Initialize(_app);

        Debug.Log("AppDom Prewarm Start");
        _app.Prewarm("UserNetWork");
        _app.Prewarm("NetDataManager");
        _app.Prewarm("TcpNetSend");              
        _app.Prewarm("UdpSubmit");
        _app.Prewarm("UdpLineParkTool");
        _app.Prewarm("Server.Process.MessageDriver");
        _app.Prewarm("ComplateRecorder");
        _app.Prewarm("UpResourceManager");
        _app.Prewarm("FrameWork.ServerLog");
        Debug.Log("AppDom Prewarm End");
    }

    unsafe static StackObject* ClrRedirectionLogError(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
    {
        //ILRuntime的调用约定为被调用者清理堆栈，因此执行这个函数后需要将参数从堆栈清理干净，并把返回值放在栈顶，具体请看ILRuntime实现原理文档
        ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
        StackObject* ptr_of_this_method;
        //这个是最后方法返回后esp栈指针的值，应该返回清理完参数并指向返回值，这里是只需要返回清理完参数的值即可
        StackObject* __ret = ILIntepreter.Minus(__esp, 1);
        //取Log方法的参数，如果有两个参数的话，第一个参数是esp - 2,第二个参数是esp -1, 因为Mono的bug，直接-2值会错误，所以要调用ILIntepreter.Minus
        ptr_of_this_method = ILIntepreter.Minus(__esp, 1);

        //这里是将栈指针上的值转换成object，如果是基础类型可直接通过ptr->Value和ptr->ValueLow访问到值，具体请看ILRuntime实现原理文档
        object message = StackObject.ToObject(ptr_of_this_method, __domain, __mStack);
        //所有非基础类型都得调用Free来释放托管堆栈
        __intp.Free(ptr_of_this_method);

        //在真实调用Debug.Log前，我们先获取DLL内的堆栈
        var stacktrace = __domain.DebugService.GetStackTrace(__intp);

        //我们在输出信息后面加上DLL堆栈
        UnityEngine.Debug.LogError(message + "\n" + stacktrace);

        return __ret;
    }

    public unsafe static StackObject* AddComponent_Redirection(ILIntepreter intp, StackObject* esp, List<object> mStack, CLRMethod method, bool isNewObj)
    {

        IType[] genericArguments = method.GenericArguments;

        if (genericArguments != null && genericArguments.Length == 1)
        {
            var t = genericArguments[0];

            if (t is ILType)
            {
                Debug.Log("AddComponent_Redirection entry");
            }
            else
            {
                StackObject* rtSobj = ILIntepreter.PushObject(esp, mStack, method.Invoke(intp, esp, mStack, isNewObj));
                return rtSobj;
            }
        }
        else
        {
            throw new System.EntryPointNotFoundException();
        }

        return null;
    }
}
