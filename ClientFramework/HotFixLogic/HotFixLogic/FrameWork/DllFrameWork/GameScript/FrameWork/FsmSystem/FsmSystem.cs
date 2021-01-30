namespace FsmSystem
{
    /// <summary>
    /// ״̬������
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FsmSystemDiv
    {
        public enum StateFsm { }
        public enum TranslateFsm { }

        public FsmSystem FsmManager = new FsmSystem();

        private static System.Collections.Generic.List<FsmSystemDiv> fsmSystemDivList = new System.Collections.Generic.List<FsmSystemDiv>();

        public FsmSystemDiv()
        {
            fsmSystemDivList.Add(this);

            InitFsmStates();
        }

        public void Dispose()
        {
            fsmSystemDivList.Remove(this);
        }

        public virtual void InitFsmStates()
        { }



        public static void Update()
        {
            for (int loop = fsmSystemDivList.Count - 1; loop >= 0; --loop)
            {
                FsmSystemDiv fsd = fsmSystemDivList[loop];

                if (fsd.FsmManager != null)
                {
                    fsd.FsmManager.CurrentState.OnListener();
                    fsd.FsmManager.CurrentState.OnUpdate();
                }
            }
        }
    }

    /// <summary>
    /// Fsm״̬ϵͳ
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FsmSystem
    {
        public System.Collections.Generic.List<FsmState> FsmStateList = new System.Collections.Generic.List<FsmState>();

        public FsmState CurrentState = null;

        public void AddState(FsmState fsmState)
        {
            FsmState findState = GetFsmState(fsmState.fsmStateId);

            if (findState != null)
            {
                UnityEngine.Debug.LogError(fsmState.GetType() + " �ظ������������");

                return;
            }

            fsmState.fsmSystem = this;

            FsmStateList.Add(fsmState);

            if (CurrentState == null)
            {
                TranslateState(fsmState.fsmStateId);
            }
        }

        public void TranslateState(int translateStateFsmId)
        {
            FsmState fsmState = GetFsmState(translateStateFsmId);

            if (fsmState == null)
            {
                UnityEngine.Debug.LogError("��ǰ������ ��ת��״̬ -> " + translateStateFsmId.ToString());
                return;
            }

            if (CurrentState != null)
            {
                CurrentState.OnLeave();
            }

            CurrentState = fsmState;

            CurrentState.OnEntry();
        }

        private FsmState GetFsmState(int translateStateFsmId)
        {
            for (int loop = 0; loop < FsmStateList.Count; loop++)
            {
                if (FsmStateList[loop].fsmStateId == translateStateFsmId)
                {
                    return FsmStateList[loop];
                }
            }

            return null;
        }
    }

    /// <summary>
    /// ״̬ϵͳ
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FsmState
    {
        /// <summary>
        /// ״̬�����ڵ�ϵͳ
        /// </summary>
        public FsmSystem fsmSystem;

        /// <summary>
        /// ��״̬ID
        /// </summary>
        public int fsmStateId;

        /// <summary>
        /// <FsmSystem<T>.TranslateFsm,FsmSystem<T>.StateFsm>
        /// </summary>
        public System.Collections.Generic.Dictionary<int, int> stateMap = new System.Collections.Generic.Dictionary<int, int>();

        /// <summary>
        /// ���ñ�״̬��Id
        /// </summary>
        /// <param name="fsmStateId"></param>
        public virtual void SetStateFsmId(int fsmStateId)
        {
            this.fsmStateId = fsmStateId;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="translateFsmId"></param>
        /// <param name="fsmStateId"></param>
        public virtual void AddTranslateStateBind(int translateFsmId, int fsmStateId)
        {
            if (isContainsTranslateState(translateFsmId))
            {
                UnityEngine.Debug.LogError("�Ѿ����� " + translateFsmId.ToString() + " ��ת��״̬����");

                return;
            }

            stateMap.Add(translateFsmId, fsmStateId);
        }

        public bool isContainsTranslateState(int translateFsm)
        {
            return stateMap.ContainsKey(translateFsm);
        }

        /// <summary>
        /// ͨ��ת��ȥ�ҵ���û�ж�Ӧ��״̬
        /// </summary>
        /// <param name="translate"></param>
        public void TranslateState(int translate)
        {
            foreach (var keyValue in stateMap)
            {
                if (int.Equals(translate, keyValue.Key))
                {
                    fsmSystem.TranslateState(keyValue.Value);
                    break;
                }
            }


        }

        public virtual void OnEntry()
        {

        }

        public virtual void OnLeave()
        {

        }

        public virtual void OnListener()
        {

        }

        public virtual void OnUpdate()
        {

        }

        public virtual void OnEvent(int eventId)
        {

        }
    }
}