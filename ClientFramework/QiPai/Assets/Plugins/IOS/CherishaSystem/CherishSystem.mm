//
//  CherishSystem.m
//  Unity-iPhone
//
//  Created by 兰志强 on 2017/2/24.
//
//

#import "CherishSystem.h"


extern "C"
{
    int _GetBattlePerValue()
    {
        return [CherishSystem getCurrentBatteryLevel] * 100;
    }
    
    bool _IsNetWorkWifi()
    {
        int typeValue = [CherishSystem dataNetworkTypeFromStatusBar];
        
        if(typeValue == 3)
        {
            return true;
        }
        
        return false;
    }

	void _SetClipboard(char* str)
	{
		UIPasteboard *pasteboard = [UIPasteboard generalPasteboard];
		pasteboard.string = [NSString stringWithUTF8String:str];
	}
}
