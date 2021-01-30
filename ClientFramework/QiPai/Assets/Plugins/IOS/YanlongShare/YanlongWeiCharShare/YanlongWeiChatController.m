//
//  YanlongWeiChatController.m
//  SDKSample
//
//  Created by 閸忔澘绻斿锟� on 15/3/17.
//
//

#import "YanlongWeiChatController.h"


@implementation YanlongWeiChatController

static NSString* BackMessageTitle=nil;
static NSString* BackMessageContent=nil;
static NSString* appId;
static NSString* appSecret;
//瀵邦喕淇婇惂濠氭鐟曚胶鏁�
static NSString* openId;

static YanlongWeiChatController* _Instance;


+(YanlongWeiChatController*)InstanceYanlongWeiChatController
{
    if(_Instance==nil)
    {
        _Instance=[[YanlongWeiChatController alloc] init];
    }
    return _Instance;
}

+(BOOL) WeiChatInstall
{
    return [WXApi isWXAppInstalled];
}

//鏉╂柨娲栭幒鍫熸綀ID
-(NSString*) GetWeiChatOpenId
{
	return openId;
}

//瀵邦喕淇婇弰顖氭儊鐎瑰顥�
-(BOOL) WeiChatIsInstall
{
	return [WXApi isWXAppInstalled];
}

//濞夈劌鍞藉顔讳繆 鐎圭偘绶ュ顔讳繆缁狅紕鎮婄�电钖�
-(void) InstanceWeiChar:(char*)AppId WeiChatAppSecret:(char*)AppSecret Description:(char*) Desc MessageBoxTitle:(char*) _BackMessageTitle BackMessageContent:(char*) _BackMessageContent
{
	appId = [[NSString alloc] initWithUTF8String:AppId];
	appSecret = [[NSString alloc] initWithUTF8String:AppSecret];
    BackMessageTitle=[[NSString alloc] initWithUTF8String:_BackMessageTitle];
    BackMessageContent=[[NSString alloc] initWithUTF8String:_BackMessageContent];
    //閸氭垵浜曟穱鈩冩暈閸愶拷
    [WXApi registerApp:[[NSString alloc] initWithUTF8String:AppId]];
}


//瀵邦喕淇婇惂濠氭
-(void) WeiChatAuth
{
	SendAuthReq *req = [[SendAuthReq alloc] init];
    req.scope = @"snsapi_userinfo";
    req.state = @"App";
    [WXApi sendReq:req];
}


-(void) ShareImageToWeiCharTimeLine:(char*)ImagePath
{
        WXMediaMessage *message = [WXMediaMessage message];
    
        NSString* ImageNsPath=[[NSString alloc] initWithUTF8String:ImagePath];
    
        //[message setThumbImage:[UIImage imageWithContentsOfFile:ImageNsPath]];
    
        WXImageObject *ext = [WXImageObject object];

        //ext.imageData = [NSData dataWithContentsOfFile:ImageNsPath];
        
        //UIImage* image = [UIImage imageWithContentsOfFile:filePath];
        UIImage* image = [UIImage imageWithContentsOfFile:ImageNsPath];//[UIImage imageWithData:ext.imageData];
    
        ext.imageData = UIImagePNGRepresentation(image);
        
        //    UIImage* image = [UIImage imageNamed:@"res5thumb.png"];
        //    ext.imageData = UIImagePNGRepresentation(image);
        
        message.mediaObject = ext;
        
        SendMessageToWXReq* req = [[SendMessageToWXReq alloc] init];
    
        req.bText = NO;
    
        req.message = message;
    
        req.scene = WXSceneTimeline;
        
        [WXApi sendReq:req];
    
}

-(void) ShareImageToWeiCharfriend:(char*)ImagePath
{
        WXMediaMessage *message = [WXMediaMessage message];
    
        NSString* ImageNsPath=[[NSString alloc] initWithUTF8String:ImagePath];
    
        //[message setThumbImage:[UIImage imageWithContentsOfFile:ImageNsPath]];
    
        WXImageObject *ext = [WXImageObject object];

        //ext.imageData = [NSData dataWithContentsOfFile:ImageNsPath];
        
        //UIImage* image = [UIImage imageWithContentsOfFile:filePath];
        UIImage* image = [UIImage imageWithContentsOfFile:ImageNsPath];//[UIImage imageWithData:ext.imageData];
    
        ext.imageData = UIImagePNGRepresentation(image);
        
        //UIImage* image = [UIImage imageNamed:@"res5thumb.png"];
        //ext.imageData = UIImagePNGRepresentation(image);
        
        message.mediaObject = ext;
        
        SendMessageToWXReq* req = [[SendMessageToWXReq alloc] init];
    
        req.bText = NO;
    
        req.message = message;
    
        req.scene = WXSceneSession;
        
        [WXApi sendReq:req];    
}

-(void) ShareLinkContentToWeiCharfriend:(char*)Title ContentText:(char*) ContentInfor LinkURL:(char*) URL ImageToalPath:(char*)ImagePath
{
    WXMediaMessage *message = [WXMediaMessage message];
    
    message.title = [[NSString alloc]initWithUTF8String:Title];
    
    message.description = [[NSString alloc]initWithUTF8String:ContentInfor];
    
    [message setThumbImage:[UIImage imageWithContentsOfFile:[[NSString alloc] initWithUTF8String:ImagePath]]];
    
    WXWebpageObject *ext = [WXWebpageObject object];
    
    ext.webpageUrl = [[NSString alloc]initWithUTF8String:URL];
    
    message.mediaObject = ext;
    
    SendMessageToWXReq* req = [[SendMessageToWXReq alloc] init];
    req.bText = NO;
    req.message = message;
    req.scene = WXSceneSession;
    
    [WXApi sendReq:req];
}

- (void) ShareLinkContentToWeiCharTimeLine:(char*)Title ContentText:(char*) ContentInfor LinkURL:(char*) URL ImageToalPath:(char*)ImagePath
{
    WXMediaMessage *message = [WXMediaMessage message];
    
    message.title = [[NSString alloc]initWithUTF8String:Title];
    
    message.description = [[NSString alloc]initWithUTF8String:ContentInfor];
    
    [message setThumbImage:[UIImage imageWithContentsOfFile:[[NSString alloc] initWithUTF8String:ImagePath]]];
    
    WXWebpageObject *ext = [WXWebpageObject object];
    
    ext.webpageUrl = [[NSString alloc]initWithUTF8String:URL];
    
    message.mediaObject = ext;
    
    SendMessageToWXReq* req = [[SendMessageToWXReq alloc] init];
    req.bText = NO;
    req.message = message;
    req.scene = WXSceneTimeline;
    
    [WXApi sendReq:req];
}

-(void) sendVideoContent
{
    WXMediaMessage *message = [WXMediaMessage message];
    message.title = @"娑旀柨绔烽弬顖濐問鐠嬶拷";
    message.description = @"妤楄法娼冮懖姘辨瘖閿涘苯鍋﹂柅鑲╂絻閵嗭拷";
    [message setThumbImage:[UIImage imageNamed:@"res7.jpg"]];
    
    WXVideoObject *ext = [WXVideoObject object];
    ext.videoUrl = @"http://v.youku.com/v_show/id_XNTUxNDY1NDY4.html";
    
    message.mediaObject = ext;
    
    SendMessageToWXReq* req = [[SendMessageToWXReq alloc] init];
    req.bText = NO;
    req.message = message;
    req.scene = WXSceneTimeline;
    
    [WXApi sendReq:req];
}


-(void) WeiChatPay:(char*)partnerId PrepayId:(char*)prepayId NonceStr:(char*)nonceStr TimeStamp:(char*)timeStamp Sign:(char*)sign;
{
	PayReq *request = [[PayReq alloc] init];
	request.partnerId = [NSString stringWithUTF8String:partnerId];
	request.prepayId= [NSString stringWithUTF8String:prepayId];
	request.package = @"Sign=WXPay";
	request.nonceStr= [NSString stringWithUTF8String:nonceStr];
    UInt32 intTime = [[NSString stringWithUTF8String:timeStamp] intValue];
    request.timeStamp= intTime;
	request.sign= [NSString stringWithUTF8String:sign];
	[WXApi sendReq:request];
}




-(void) onResp:(BaseResp*)resp
{
    if([resp isKindOfClass:[SendMessageToWXResp class]])
    {
        //if(!resp.errCode)
        //{
          //  UIAlertView *alert = [[UIAlertView alloc] initWithTitle:BackMessageTitle message:BackMessageContent delegate:self.delegate/*[[UIApplication sharedApplication] delegate]*/ cancelButtonTitle:@"OK" otherButtonTitles:nil, nil];
        //[alert show];
        //}
    }
	if([resp isKindOfClass:[SendAuthResp  class]])
	{
		SendAuthResp *temp = (SendAuthResp *)resp;
		if (resp.errCode == 0) 
		{
            openId = [[NSString alloc] initWithString:temp.code];
		}
	}
	
    if ([resp isKindOfClass:[PayResp class]])
	{
	      PayResp*response=(PayResp*)resp;
	      switch(response.errCode)
	      {
              case WXSuccess:
	          //鏈嶅姟鍣ㄧ鏌ヨ鏀粯閫氱煡鎴栨煡璇PI杩斿洖鐨勭粨鏋滃啀鎻愮ず鎴愬姛
	          //NSlog(@"鏀粯鎴愬姛");
	          	break;
              default:
	          	//NSlog(@"鏀粯澶辫触锛宺etcode=%d",resp.errCode);
	          	break;
	      }
     }
}



- (BOOL)application:(UIApplication *)application handleOpenURL:(NSURL *)url
{
    return  [WXApi handleOpenURL:url delegate:self];
}

- (BOOL)application:(UIApplication *)application openURL:(NSURL *)url sourceApplication:(NSString *)sourceApplication annotation:(id)annotation
{
    BOOL isSuc = [WXApi handleOpenURL:url delegate:self];
    NSLog(@"url %@ isSuc %d",url,isSuc == YES ? 1 : 0);
    return  isSuc;
}
@end
