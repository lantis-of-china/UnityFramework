//
//  YanlongShareWeiChatCall.m
//  SocialFrameworkExample
//
//  Created by 兰志强 on 15/3/17.
//  Copyright (c) 2015年 com.leijing. All rights reserved.
//

#import "YanlongWeiChatController.h"

#define MakeStringCopy( _x_ ) ( _x_ != NULL && [_x_ isKindOfClass:[NSString class]] ) ? strdup( [_x_ UTF8String] ) : NULL
 
extern "C"
{
    bool _WeiChatInstall()
    {
        return [YanlongWeiChatController WeiChatInstall];
    }
    
    void _InstanceWeiChat(char* AppId,char* appSecret,char* Desc,char* MessageTitle,char* MessageContent)
    {
        [YanlongWeiChatController.InstanceYanlongWeiChatController InstanceWeiChar:AppId WeiChatAppSecret:appSecret Description:Desc MessageBoxTitle:MessageTitle BackMessageContent:MessageContent];
    }

	Boolean _WeiChatIsInstall()
	{
		 [YanlongWeiChatController.InstanceYanlongWeiChatController WeiChatIsInstall];
	}

	void _WeiChatAuth()
	{
		 [YanlongWeiChatController.InstanceYanlongWeiChatController WeiChatAuth];
	}

	char* _GetWeiChatOpenId()
	{
		 NSString* nsOpenId = [YanlongWeiChatController.InstanceYanlongWeiChatController GetWeiChatOpenId];
		 return MakeStringCopy(nsOpenId);
	}
    
    void _WeiCharShareImage(char* ImagePath)
    {
        [YanlongWeiChatController.InstanceYanlongWeiChatController ShareImageToWeiCharTimeLine:ImagePath];
    }

	void _WeiCharShareImageToFriend(char* ImagePath)
	{
		[YanlongWeiChatController.InstanceYanlongWeiChatController ShareImageToWeiCharfriend:ImagePath];		
	}
    
    void _WeiCharShareLink(char* Title,char* ContentText,char* URL,char* ImagePath)
    {
        [YanlongWeiChatController.InstanceYanlongWeiChatController ShareLinkContentToWeiCharfriend:Title ContentText:ContentText LinkURL:URL ImageToalPath:ImagePath ];
    }
    
    void _WeiCharShareLinkToTimeLine(char* Title,char* ContentText,char* URL,char* ImagePath)
    {
        [YanlongWeiChatController.InstanceYanlongWeiChatController ShareLinkContentToWeiCharTimeLine:Title ContentText:ContentText LinkURL:URL ImageToalPath:ImagePath ];
    }

	void _WeiChatPay(char* partnerId,char* prepayId,char* nonceStr,char* timeStamp,char* sign)
	{
		[YanlongWeiChatController.InstanceYanlongWeiChatController WeiChatPay:partnerId PrepayId:prepayId NonceStr:nonceStr TimeStamp:timeStamp Sign:sign];
	}
}
