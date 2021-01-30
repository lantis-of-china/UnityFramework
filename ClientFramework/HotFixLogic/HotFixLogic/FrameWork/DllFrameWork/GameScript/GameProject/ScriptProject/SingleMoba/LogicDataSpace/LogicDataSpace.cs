using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace SingleMoba
{
    public class LogicDataSpace : GoableData.GoableDataItem
    {
        public override void LodingDate()
        {
            LocalConfigLoader.Load();
            RegistEvent();
        }

        /// <summary>
        /// 清理数据
        /// </summary>
        public override void ClearData()
        {
            gameRoomInfo = null;
            UnRegistEvent();
        }

        private void RegistEvent()
        {
            LocalEventNotices.Regist(EventNoticesDefine.ChactarPropyteType, NoticesPropyteChange);
        }

        private void UnRegistEvent()
        {
            LocalEventNotices.UnRegist(EventNoticesDefine.ChactarPropyteType, NoticesPropyteChange);
        }

        /// <summary>
        /// 房间信息
        /// </summary>
        public static SC_EntryRoom gameRoomInfo;

        public static P_PlayerInfo GetSelfData()
        {
            var characterBase = CharacterManager.Instance.GetMainCharacter();

            if (characterBase != null)
            {
                var character = characterBase as SingleMoba.CharacterBase;

                if (character != null)
                {
                    return character.GetPlayerData();
                }
            }

            return null;
        }

        public static P_PlayerInfo GetPlayerInfo(int playerId)
        {
            var characterBase = CharacterManager.Instance.GetCharacter(playerId);

            if (characterBase != null)
            {
                var character = characterBase as SingleMoba.CharacterBase;

                if (character != null)
                {
                    return character.GetPlayerData();
                }
            }

            return null;
        }

        public static void SetPlayerChangeState(P_GamerStateChange gamerStateChange)
        {
            if (gamerStateChange != null)
            {
                var playerInfo = GetPlayerInfo(gamerStateChange.playerId);

                if (playerInfo != null)
                {
                    playerInfo.hp = gamerStateChange.hp;
                    playerInfo.power = gamerStateChange.power;
                    playerInfo.score = gamerStateChange.score;
                    playerInfo.boom = gamerStateChange.boom;
                    playerInfo.canMove = gamerStateChange.canMove;

                    LocalEventNotices.Notices(EventNoticesDefine.PlayerStateChange, playerInfo);
                }
                else
                {
                    DebugLoger.LogError("playerInfo null");
                }
            }
            else
            {
                DebugLoger.LogError("gamerStateChange null");
            }
        }

        public static void NoticesPropyteChange(object paramar)
        {
            var paramarList = paramar as object[];
            var playerId = (int)paramarList[0];
            var propyteType = (ChactarPropyteType)paramarList[1];
            var currentValue = (int)paramarList[2];
            var changeValue = (int)paramarList[3];
            var playerInfo = GetPlayerInfo(playerId);

            var character = CharacterManager.Instance.GetCharacter(playerId);

            if (playerInfo != null)
            {
                switch (propyteType)
                {
                    case ChactarPropyteType.Hp:
                    {
                        playerInfo.hp = currentValue;

                        if (character != null && playerInfo.hp <= 0)
                        {
                            character.PlayDead();
                        }
                        break;
                    }
                }

                LocalEventNotices.Notices(EventNoticesDefine.PlayerStateChange, playerInfo);
            }
            else
            {
                DebugLoger.LogError("playerInfo null");
            }
        }
    }
}
