using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SingleMoba
{
    public class ConfigSkillBuffData
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int key;
        /// <summary>
        /// 图标
        /// </summary>
        public string icon;
        /// <summary>
        /// 实体资源
        /// </summary>
        public string item;
        /// <summary>
        /// 名字
        /// </summary>
        public string name;
        /// <summary>
        /// 说明
        /// </summary>
        public string descarption;
        /// <summary>
        /// 生命周期计时
        /// </summary>
        public float lifeTime;
		/// <summary>
		/// 是否叠加
		/// </summary>
		public int repeated;
        /// <summary>
        /// 事件类型
        /// </summary>
        public int eventType;
        /// <summary>
        /// 事件参数一
        /// </summary>
        public int eventParamar1;
        /// <summary>
        /// 事件参数二
        /// </summary>
        public int eventParamar2;
        /// <summary>
        /// 事件参数三
        /// </summary>
        public int eventParamar3;
        /// <summary>
        /// 事件参数四
        /// </summary>
        public float eventParamar4;
        /// <summary>
        /// 记录数值类型
        /// </summary>
        public List<int> recordTypes = new List<int>();
        /// <summary>
        /// 条件类型
        /// </summary>
        public List<int> conditionTypes = new List<int>();
        /// <summary>
        /// 条件的值
        /// </summary>
        public List<int> conditionValues = new List<int>();
        /// <summary>
        /// 触发延时时间
        /// </summary>
        public float eventTimer;
        /// <summary>
        /// 下一次触发间隔
        /// </summary>
        public float eventInvateTimer;
        /// <summary>
        /// 最大触发次数
        /// </summary>
        public int eventTimesMax;
    }
    
    public class ConfigSkillBuff : ConfigDataBase
	{
		public ConfigSkillBuffData currentData;

		public System.Collections.Generic.Dictionary<int, ConfigSkillBuffData> dataList = new System.Collections.Generic.Dictionary<int, ConfigSkillBuffData>();

		public ConfigSkillBuff(string _projectName, string _configName)
			: base(_projectName, _configName)
		{
		}

		public override void CreateData(string _key)
		{
			currentData = new ConfigSkillBuffData();
			currentData.key = int.Parse(_key);
			dataList.Add(currentData.key, currentData);
		}

		public override void AppendAttribute(string _key, string _name, string _value)
		{
			switch (_name)
			{
				case "Key":
                {
                    currentData.key = int.Parse(_value);
                    break;
                }
                case "Icon":
                {
                    currentData.icon = _value;
                    break;
                }
                case "Item":
                {
                    currentData.item = _value;
                    break;
                }
                case "Name":
                {
                    currentData.name = _value;
                    break;
                }
				case "Descaption":
                {
                    currentData.descarption = _value;
                    break;
                }
                case "LifeTime":
                {
                    currentData.lifeTime = float.Parse(_value);
                    break;
                }
                case "Repeated":
                {
                    currentData.repeated = int.Parse(_value);
                    break;
                }
                case "EventType":
                {
                    currentData.eventType = int.Parse(_value);
                    break;
                }
                case "EventParamar1":
                {
                    currentData.eventParamar1 = int.Parse(_value);
                    break;
                }
                case "EventParamar2":
                {
                    currentData.eventParamar2 = int.Parse(_value);
                    break;
                }
                case "EventParamar3":
                {
                    currentData.eventParamar3 = int.Parse(_value);
                    break;
                }
                case "EventParamar4":
                {
                    currentData.eventParamar4 = float.Parse(_value);
                    break;
                }
                case "RecordTypes":
                {
                    if (string.IsNullOrEmpty(_value))
                    {
                        return;
                    }

                    string[] strAry_1 = _value.Split(',');

                    for (int i = 0; i < strAry_1.Length; ++i)
                    {
                        currentData.recordTypes.Add(int.Parse(strAry_1[i]));
                    }

                    break;
                }
                case "ConditionTypes":
                {
                    if (string.IsNullOrEmpty(_value))
                    {
                        return;
                    }

                    string[] strAry_1 = _value.Split(',');

                    for (int i = 0; i < strAry_1.Length; ++i)
                    {
                        currentData.conditionTypes.Add(int.Parse(strAry_1[i]));
                    }

                    break;
                }
                case "ConditionValues":
                {
                    if (string.IsNullOrEmpty(_value))
                    {
                        return;
                    }

                    string[] strAry_1 = _value.Split(',');

                    for (int i = 0; i < strAry_1.Length; ++i)
                    {
                        currentData.conditionValues.Add(int.Parse(strAry_1[i]));
                    }

                    break;
                }
                case "EventTimer":
                {
                    currentData.eventTimer = float.Parse(_value);
                    break;
                }
                case "EventInvateTimer":
                {
                    currentData.eventInvateTimer = float.Parse(_value);
                    break;
                }
                case "EventTimesMax":
                {
                    currentData.eventTimesMax = int.Parse(_value);
                    break;
                }
                default:
					break;
			}
		}

		public ConfigSkillBuffData GetKey(int _key)
		{
            ConfigSkillBuffData _outData = null;

            if (dataList.TryGetValue(_key, out _outData))
            {

                return _outData;
            }
            else
            {
                DebugLoger.LogError("查找失败 BufId:" + _key);
                return null;
            }
		}
	}
}