using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace UINameSpace
{
    public class UIGpsInfo : UIObject
    {
        /// <summary>
        /// 反射调用的注册方法
        /// </summary>
        /// <param name="_className"></param>
        public static int RegistSystem(string _className)
        {
            FrameWorkDrvice.UiManagerInstance.RegistFunctionCallFun(Rall.UIDefineName.UIGpsInfo_Rall, _className);
            return 1;
        }

        public UIGpsInfo()
        {
            assetsName = Rall.UIDefineName.UIGpsInfo_Rall;
        }

		public class UserGpsData
		{
			public string headUrl;
			public byte sex;
			public string name;
			public string id;
			public string ip;
			public float latitude;
			public float longitude;

			public static List<UserGpsData> gpsData = new List<UserGpsData>();

			public static UserGpsData selfGpsData;

			public static void ClearGpsData()
			{
				gpsData.Clear();
			}

			public static void AddGps(string _headUrl,byte _sex,string _name,string _id,string _ip,float _latitude,float _longitude)
			{
				UserGpsData ugd = new UserGpsData();
				gpsData.Add(ugd);

				ugd.headUrl = _headUrl;
				ugd.sex = _sex;
				ugd.name = _name;
				ugd.id = _id;
				ugd.ip = _ip;
				ugd.latitude = _latitude;
				ugd.longitude = _longitude;

				if (ugd.id == GoableData.userValiadateInfor.DatingNumber)
				{
					selfGpsData = ugd;
					DebugLoger.Log("存在自己的定位成员信息!");
				}
			}
		}

		/// <summary>
		/// 选中的信息
		/// </summary>
		public class SelectInfo
		{
			public static GameObject itemNode;
			public static CircleImage img_head;
			public static Text txt_name;
			public static Text txt_id;
			public static Text txt_ip;
			public static Text txt_gps;

			public static void GetUI(GameObject target)
			{
				itemNode = target;
				img_head = GenericityTool.GetComponentByPath<CircleImage>(itemNode, "img_head");
				txt_name = GenericityTool.GetComponentByPath<Text>(itemNode, "txt_name");
				txt_id = GenericityTool.GetComponentByPath<Text>(itemNode, "txt_id");
				txt_ip = GenericityTool.GetComponentByPath<Text>(itemNode, "txt_ip");
				txt_gps = GenericityTool.GetComponentByPath<Text>(itemNode, "txt_gps");
			}

			public static void ShowSelect(UserGpsData showInfo)
			{
				if (!string.IsNullOrEmpty(showInfo.headUrl))
				{
					SetCircleImageForHttpbytes.SetCircleImageFromUrl(img_head, showInfo.headUrl);
				}
				else
				{
					if (showInfo.sex == 1)
					{
						///男
						AssetsParkManager.SetCircleImageThingIcon(Rall.ConfigProject.iconsName, img_head, "GameEnd10");
					}
					else
					{
						///女
						AssetsParkManager.SetCircleImageThingIcon(Rall.ConfigProject.iconsName, img_head, "GameEnd9");
					}
				}

				if (showInfo.name.Length >= 6)
				{
					txt_name.text = showInfo.name.Substring(0, 5) + "..";
				}
				else
				{
					txt_name.text = showInfo.name;
				}

				txt_id.text = "ID " + showInfo.id;
				txt_ip.text = showInfo.ip;
				if (showInfo.longitude == 0.0f && showInfo.latitude == 0.0f)
				{
					txt_gps.text = "定位失败";
				}
				else
				{
					double distance = FrameWork.GpsTools.GetDistance(showInfo.latitude, showInfo.longitude, UserGpsData.selfGpsData.latitude, UserGpsData.selfGpsData.longitude);

					txt_gps.text = distance.ToString("0.00") + "/米";
				}

				DistanceCompaer.Compaer(showInfo);
			}
		}

		public class UserGpsInfo
		{
			public class UserGpsInfoItem
			{
				public GameObject itemNode;
				public CircleImage img_head;
				public Text txt_name;
				public GameObject obj_state;
				public Button btn_select;
				public UserGpsData bindData;

				public void GetUI(GameObject itemObj)
				{
					itemNode = itemObj;
					img_head = GenericityTool.GetComponentByPath<CircleImage>(itemNode, "img_icon");
					txt_name = GenericityTool.GetComponentByPath<Text>(itemNode, "txt_name");
					obj_state = GenericityTool.GetObjectByPath(itemNode, "img_state");
					btn_select = GenericityTool.GetComponentByPath<Button>(itemNode, "img_icon");
					btn_select.onClick.AddListener(OnSelect);
				}

				public void SetInfo(UserGpsData data)
				{
					bindData = data;

					if (bindData.name.Length >= 6)
					{
						txt_name.text = bindData.name.Substring(0, 5) + "..";
					}
					else
					{
						txt_name.text = bindData.name;
					}

					if (!string.IsNullOrEmpty(bindData.headUrl))
					{
						SetCircleImageForHttpbytes.SetCircleImageFromUrl(img_head, bindData.headUrl);
					}
					else
					{
						if (bindData.sex == 1)
						{
							///男
							AssetsParkManager.SetCircleImageThingIcon(Rall.ConfigProject.iconsName, img_head, "GameEnd10");
						}
						else
						{
							///女
							AssetsParkManager.SetCircleImageThingIcon(Rall.ConfigProject.iconsName, img_head, "GameEnd9");
						}
					}
				}

				public void SetParent(Transform parent)
				{
					itemNode.transform.SetParent(parent);
					itemNode.transform.localPosition = Vector3.zero;
					itemNode.transform.localScale = Vector3.one;
					itemNode.SetActive(true);
				}

				public void Destory()
				{
					GameObject.Destroy(itemNode);
				}

				public void OnSelect()
				{
					FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
					UserGpsInfo.AllNotSelect();

					obj_state.SetActive(true);

					SelectInfo.ShowSelect(bindData);
				}

				public void NotSelect()
				{
					obj_state.SetActive(false);
				}
			}
			
			public static GameObject itemNode;
			public static GameObject itemSource;
			public static List<UserGpsInfoItem> userGpsList = new List<UserGpsInfoItem>();

			public static void GetUI(GameObject itemObj)
			{
				itemNode = itemObj;
				itemSource = GenericityTool.GetObjectByPath(itemNode, "ItemSource");
				itemSource.SetActive(false);
			}

			public static void ShowItems()
			{
				DeleteItems();

				for (int i = 0; i < UserGpsData.gpsData.Count; ++i)
				{
					UserGpsData userGpsData = UserGpsData.gpsData[i];

					GameObject item = GameObject.Instantiate(itemSource);
					UserGpsInfoItem ugii = new UserGpsInfoItem();
					userGpsList.Add(ugii);

					ugii.GetUI(item);
					ugii.SetInfo(userGpsData);
					ugii.SetParent(itemSource.transform.parent);
					ugii.NotSelect();
				}

				userGpsList[0].OnSelect();
			}

			public static void DeleteItems()
			{
				for (int i = 0; i < userGpsList.Count; ++i)
				{
					userGpsList[i].Destory();
				}
				userGpsList.Clear();
			}

			public static void AllNotSelect()
			{
				for (int i = 0; i < userGpsList.Count; ++i)
				{
					userGpsList[i].NotSelect();
				}
			}
		}

		public class DistanceCompaer
		{
			public class DistanceCompaerItem
			{
				public GameObject itemNode;
				public CircleImage img_head;
				public Text txt_name;
				public Text txt_id;

				public CircleImage img_select_head;
				public Text txt_select_name;
				public Text txt_select_id;

				public Text txt_distance;

				public void GetUI(GameObject itemObj)
				{
					itemNode = itemObj;
					img_head = GenericityTool.GetComponentByPath<CircleImage>(itemNode, "userNode/img_icon");
					txt_name = GenericityTool.GetComponentByPath<Text>(itemNode, "userNode/txt_name");
					txt_id = GenericityTool.GetComponentByPath<Text>(itemNode, "userNode/txt_id");

					img_select_head = GenericityTool.GetComponentByPath<CircleImage>(itemNode, "selectNode/img_icon");
					txt_select_name = GenericityTool.GetComponentByPath<Text>(itemNode, "selectNode/txt_name");
					txt_select_id = GenericityTool.GetComponentByPath<Text>(itemNode, "selectNode/txt_id");

					txt_distance = GenericityTool.GetComponentByPath<Text>(itemNode, "txt_distance");
				}

				public void ShowInfo(UserGpsData userData, UserGpsData selectData)
				{
					if (!string.IsNullOrEmpty(userData.headUrl))
					{
						SetCircleImageForHttpbytes.SetCircleImageFromUrl(img_head, userData.headUrl);
					}
					else
					{
						if (userData.sex == 1)
						{
							///男
							AssetsParkManager.SetCircleImageThingIcon(Rall.ConfigProject.iconsName, img_head, "GameEnd10");
						}
						else
						{
							///女
							AssetsParkManager.SetCircleImageThingIcon(Rall.ConfigProject.iconsName, img_head, "GameEnd9");
						}
					}

					if (userData.name.Length >= 6)
					{
						txt_name.text = userData.name.Substring(0, 5) + "..";
					}
					else
					{
						txt_name.text = userData.name;
					}

					txt_id.text = "ID " + userData.id;

					if (!string.IsNullOrEmpty(selectData.headUrl))
					{
						SetCircleImageForHttpbytes.SetCircleImageFromUrl(img_select_head, selectData.headUrl);
					}
					else
					{
						if (selectData.sex == 1)
						{
							///男
							AssetsParkManager.SetCircleImageThingIcon(Rall.ConfigProject.iconsName, img_select_head, "GameEnd10");
						}
						else
						{
							///女
							AssetsParkManager.SetCircleImageThingIcon(Rall.ConfigProject.iconsName, img_select_head, "GameEnd9");
						}
					}
					if (selectData.name.Length >= 6)
					{
						txt_select_name.text = selectData.name.Substring(0, 5) + "..";
					}
					else
					{
						txt_select_name.text = selectData.name;
					}
					txt_select_id.text = selectData.id;


					if ((userData.longitude == 0.0f && userData.latitude == 0.0f) || (selectData.longitude == 0.0f && selectData.latitude == 0.0f))
					{
						txt_distance.text = "无法测距";
						txt_distance.color = Color.red;
					}
					else
					{
						double distance = FrameWork.GpsTools.GetDistance(userData.latitude, userData.longitude, selectData.latitude, selectData.longitude);

						txt_distance.text = distance.ToString("0.00") + "/米";
						if (distance < 50)
						{
							txt_distance.color = Color.red;
						}
						else
						{
							txt_distance.color = Color.white;
						}
					}
				}

				public void SetParent(Transform parent)
				{
					itemNode.transform.SetParent(parent);
					itemNode.transform.localPosition = Vector3.zero;
					itemNode.transform.localScale = Vector3.one;
					itemNode.SetActive(true);
				}

				public void Destory()
				{
					GameObject.Destroy(itemNode);
				}
			}

			public static GameObject itemNode;
			public static GameObject itemSource;

			public static List<DistanceCompaerItem> dataInfo = new List<DistanceCompaerItem>();

			public static void GetUI(GameObject itemObj)
			{
				itemNode = itemObj;
				itemSource = GenericityTool.GetObjectByPath(itemNode, "ItemSource");
				itemSource.SetActive(false);
			}

			public static void Compaer(UserGpsData compaerData)
			{
				DeleteAll();

				for (int i = 0;i < UserGpsData.gpsData.Count; ++i)
				{
					UserGpsData ugd = UserGpsData.gpsData[i];
					if (ugd.id != compaerData.id)
					{
						GameObject item = GameObject.Instantiate(itemSource);
						DistanceCompaerItem dci = new DistanceCompaerItem();
						dataInfo.Add(dci);

						dci.GetUI(item);
						dci.ShowInfo(ugd, compaerData);
						dci.SetParent(itemSource.transform.parent);
					}
				}
			}

			public static void DeleteAll()
			{
				for (int i = 0; i < dataInfo.Count; ++i)
				{
					dataInfo[i].Destory();
				}
				dataInfo.Clear();
			}
		}

		public Button btn_close;
        public override void OnAwake()
        {
            base.OnAwake();
			SelectInfo.GetUI(GenericityTool.GetObjectByPath(objectInstance, "anchorNode/animationNode/info"));
			UserGpsInfo.GetUI(GenericityTool.GetObjectByPath(objectInstance, "anchorNode/animationNode/usersNode"));
			DistanceCompaer.GetUI(GenericityTool.GetObjectByPath(objectInstance, "anchorNode/animationNode/compareNode"));

			btn_close = GenericityTool.GetComponentByPath<Button>(objectInstance, "anchorNode/animationNode/btn_close");
			btn_close.onClick.AddListener(OnClickCloseUI);
		}

        public override void OnEnable()
        {
            base.OnEnable();
			UserGpsInfo.ShowItems();
		}

		public void OnClickCloseUI()
		{
			FrameWorkDrvice.AudioOutManagerInstance.PlaySound(Rall.ConfigProject.soundName, "btnClick");
			FrameWorkDrvice.UiManagerInstance.CloseUI(Rall.UIDefineName.UIGpsInfo_Rall, eCloseType.None);
		}
    }
}
