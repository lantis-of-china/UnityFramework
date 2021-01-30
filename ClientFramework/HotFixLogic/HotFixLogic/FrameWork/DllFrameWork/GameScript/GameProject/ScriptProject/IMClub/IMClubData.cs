using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QiPaiDll
{
    public class IAVIMMessage
    {
        public string ConversationId = "";
        public string FromClientId = "";
    }

    public class AVIMTextMessage : IAVIMMessage
    {
        public AVIMTextMessage() { }
        public AVIMTextMessage(string textContent) 
        {
            TextContent = textContent;
        }

        public int LCType;
        public string TextContent = "";
    }

    public class AVIMMessage : IAVIMMessage
    {
        public AVIMMessage() { }

        public string Content = "";
        public string ConversationId = "";
        public string FromClientId = "";
        public string Id = "";
        public bool MentionAll;
        public IEnumerable<string> MentionList;
        public long RcpTimestamp;
        public long ServerTimestamp;
    }

    public class AVIMBinaryMessage : AVIMMessage
    {
        public AVIMBinaryMessage() { }
        public AVIMBinaryMessage(byte[] data) 
        {
            BinaryData = data;
        }

        public byte[] BinaryData;
    }
}
