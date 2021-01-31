using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEntrySpace
{
    public class SingleMoba_GameEntry : GameEntryItem
    {
        /// <summary>
        /// 反射调用的注册方法
        /// </summary>
        /// <param name="_className"></param>
        public static int RegistSystem()
        {
            FrameWorkDrvice.GameEntryManagerInstanece.RegistFunctionCallFun(
                new SingleMoba_GameEntry()
                {
                    gameType = (int)eRoomType.AskDao,
                    gameName = "最简王者",
                    gameServerId = SingleMoba.ConfigProject.serverId,
                    assetFloder = SingleMoba.ConfigProject.projectFloderName,
                    uiName = SingleMoba.UIDefineName.UIRall,
                    isGernerlRall = false,
                    callSendEntryRoomCall = SendEntryRoom
                });
            return 1;
        }

        public static void ReleseGame()
        {
            AssetsParkManager.RelesePark(SingleMoba.ConfigProject.iconsName);
            AssetsParkManager.RelesePark(SingleMoba.ConfigProject.soundName);
            AssetsItemLoader.Dispose(SingleMoba.ConfigProject.projectFloderName);

            FrameWorkDrvice.UiManagerInstance.CloseUI(SingleMoba.UIDefineName.UIFight, eCloseType.None);
        }

        /// <summary>
        /// 加入房间
        /// </summary>
        /// <param name="entryType"></param>
        /// <param name="roomId"></param>
        /// <param name="conditionId"></param>
        public static void SendEntryRoom(int entryType,int roomId,int conditionId)
        {
            SingleMoba.MessageSend.EntryRoom((byte)entryType, roomId);
        }
    }
}
