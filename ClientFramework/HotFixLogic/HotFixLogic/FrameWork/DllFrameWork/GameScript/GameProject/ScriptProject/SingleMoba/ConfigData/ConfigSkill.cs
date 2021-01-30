using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SingleMoba
{
    public class ConfigSkillData
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
        /// 实体对象类型
        /// </summary>
        public int itemType;
        /// <summary>
        /// 名字
        /// </summary>
        public string name;
        /// <summary>
        /// 说明
        /// </summary>
        public string descaption;
        /// <summary>
        /// 技能开始特效
        /// </summary>
        public string startEffect;
        /// <summary>
        /// 命中特效
        /// </summary>
        public string hitEffect;
        /// <summary>
        /// 结束效果特效
        /// </summary>
        public string endTakeEffect;
        /// <summary>
        /// 生命周期计时
        /// </summary>
        public float lifeTime;
        /// <summary>
        /// 是否禁止输入
        /// </summary>
        public int disEnableInput;
        /// <summary>
        /// 冷却时间
        /// </summary>
        public float cdTime;
        /// <summary>
        /// 使用消耗
        /// </summary>
        public int consume;
        /// <summary>
        /// 移动类型
        /// </summary>
        public int moveType;
        /// <summary>
        /// 移动距离
        /// </summary>
        public float moveDistance;
        /// <summary>
        /// 移动命中类型
        /// </summary>
        public int moveHitType;
        /// <summary>
        /// 命中的阵营
        /// </summary>
        public int moveHitTeam;
        /// <summary>
        /// 移动命中范围
        /// </summary>
        public float moveHitRange;
        /// <summary>
        /// 移动停止命中数量
        /// </summary>
        public int moveHitEndCount;
        /// <summary>
        /// 技能结束生效范围
        /// </summary>
        public float endTakeRange;
        /// <summary>
        /// 技能开始时使用者触发的效果
        /// </summary>
        public List<int> startUserBuffKeys = new List<int>();
        /// <summary>
        /// 命中的己方玩家受到的触发效果
        /// </summary>
        public List<int> hitSelfBuffKeys = new List<int>();
        /// <summary>
        /// 命中的己方玩家受到的触发效果
        /// </summary>
        public List<int> hitEnemyBuffKeys = new List<int>();
        /// <summary>
        /// 技能结束的时候己方受到的触发效果
        /// </summary>
        public List<int> endTakeSelfBuffKeys = new List<int>();
        /// <summary>
        /// 技能结束的时候敌方受到的触发效果
        /// </summary>
        public List<int> endTakeEnemyBuffKeys = new List<int>();
        /// <summary>
        /// 结束生效延时时间
        /// </summary>
        public float endTakeEventTimer;
        /// <summary>
        /// 结束效果间隔触发时间
        /// </summary>
        public float endTakeEventInvateTimer;
        /// <summary>
        /// 结束效果最大触发次数
        /// </summary>
        public int endTakeEventTimesMax;
    }
    
    public class ConfigSkill : ConfigDataBase
	{
		public ConfigSkillData currentData;

		public System.Collections.Generic.Dictionary<int, ConfigSkillData> dataList = new System.Collections.Generic.Dictionary<int, ConfigSkillData>();

		public ConfigSkill(string _projectName, string _configName)
			: base(_projectName, _configName)
		{
		}

		public override void CreateData(string _key)
		{
			currentData = new ConfigSkillData();
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
                case "ItemType":
                {
                    currentData.itemType = int.Parse(_value);
                    break;
                }
                case "Name":
                {
                    currentData.name = _value;
                    break;
                }
				case "Descaption":
                {
                    currentData.descaption = _value;
                    break;
                }
                case "StartEffect":
                {
                    currentData.startEffect = _value;
                    break;
                }
                case "HitEffect":
                {
                    currentData.hitEffect = _value;
                    break;
                }
                case "EndTakeEffect":
                {
                    currentData.endTakeEffect = _value;
                    break;
                }
                case "LifeTime":
                {
                    currentData.lifeTime = float.Parse(_value);
                    break;
                }
                case "DisEnableInput":
                {
                    currentData.disEnableInput = int.Parse(_value);
                    break;
                }
                case "CDTime":
                {
                    currentData.cdTime = float.Parse(_value);
                    break;
                }
                case "Consume":
                {
                    currentData.consume = int.Parse(_value);
                    break;
                }
                case "MoveType":
                {
                    currentData.moveType = int.Parse(_value);
                    break;
                }
                case "MoveDistance":
                {
                    currentData.moveDistance = float.Parse(_value);
                    break;
                }
                case "MoveHitType":
                {
                    currentData.moveHitType = int.Parse(_value);
                    break;
                }
                case "MoveHitTeam":
                {
                    currentData.moveHitTeam = int.Parse(_value);
                    break;
                }
                case "MoveHitRange":
                {
                    currentData.moveHitRange = float.Parse(_value);
                    break;
                }
                case "MoveHitEndCount":
                {
                    currentData.moveHitEndCount = int.Parse(_value);
                    break;
                }
                case "EndTakeRange":
                {
                    currentData.endTakeRange = float.Parse(_value);
                    break;
                }
                case "StartUserBuffKeys":
                {
                    if (string.IsNullOrEmpty(_value))
                    {
                        return;
                    }

                    string[] strAry_1 = _value.Split(',');

                    for (int i = 0; i < strAry_1.Length; ++i)
                    {
                        currentData.startUserBuffKeys.Add(int.Parse(strAry_1[i]));
                    }

                    break;
                }
                case "HitSelfBuffKeys":
                {
                    if (string.IsNullOrEmpty(_value))
                    {
                        return;
                    }

                    string[] strAry_1 = _value.Split(',');

                    for (int i = 0; i < strAry_1.Length; ++i)
                    {
                        currentData.hitSelfBuffKeys.Add(int.Parse(strAry_1[i]));
                    }

                    break;
                }
                case "HitEnemyBuffKeys":
                {
                    if (string.IsNullOrEmpty(_value))
                    {
                        return;
                    }

                    string[] strAry_1 = _value.Split(',');

                    for (int i = 0; i < strAry_1.Length; ++i)
                    {
                        currentData.hitEnemyBuffKeys.Add(int.Parse(strAry_1[i]));
                    }

                    break;
                }
                case "EndTakeSelfBuffKeys":
                {
                    if (string.IsNullOrEmpty(_value))
                    {
                        return;
                    }

                    string[] strAry_1 = _value.Split(',');

                    for (int i = 0; i < strAry_1.Length; ++i)
                    {
                        currentData.endTakeSelfBuffKeys.Add(int.Parse(strAry_1[i]));
                    }

                    break;
                }
                case "EndTakeEnemyBuffKeys":
                {
                    if (string.IsNullOrEmpty(_value))
                    {
                        return;
                    }

                    string[] strAry_1 = _value.Split(',');

                    for (int i = 0; i < strAry_1.Length; ++i)
                    {
                        currentData.endTakeEnemyBuffKeys.Add(int.Parse(strAry_1[i]));
                    }

                    break;
                }
                case "EndTakeEventTimer":
                {
                    currentData.endTakeEventTimer = float.Parse(_value);
                    break;
                }
                case "EndTakeEventInvateTimer":
                {
                    currentData.endTakeEventInvateTimer = float.Parse(_value);
                    break;
                }
                case "EndTakeEventTimesMax":
                {
                    currentData.endTakeEventTimesMax = int.Parse(_value);
                    break;
                }
                default:
					break;
			}
		}

		public ConfigSkillData GetKey(int _key)
		{
            ConfigSkillData _outData = null;

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