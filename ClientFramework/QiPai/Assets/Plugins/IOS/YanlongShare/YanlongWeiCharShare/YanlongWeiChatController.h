//
//  YanlongWeiChatController.h
//  SDKSample
//
//  Created by 鍏板織寮� on 15/3/17.
//
//
/*
 @interface AppDelegate : UIResponder <UIApplicationDelegate,WXApiDelegate,YanlongWeChatViewDelegate>
 AppDelegate add
 */
#import <UIKit/UIKit.h>
#import "WXApiObject.h"
#import "WXApi.h"

@protocol YanlongWeChatViewDelegate <NSObject>
@end

@interface YanlongWeiChatController : UIRegion<UIApplicationDelegate,WXApiDelegate>

@property (nonatomic, assign) id<YanlongWeChatViewDelegate,NSObject> delegate;

+(YanlongWeiChatController*)InstanceYanlongWeiChatController;
+(BOOL) WeiChatInstall;
-(NSString*) GetWeiChatOpenId;
-(BOOL) WeiChatIsInstall;
-(void)InstanceWeiChar:(char*)AppId WeiChatAppSecret:(char*)AppSecret Description : (char*)Desc MessageBoxTitle : (char*)_BackMessageTitle BackMessageContent : (char*)_BackMessageContent;
-(void) WeiChatAuth;
-(void) ShareImageToWeiCharTimeLine:(char*)ImagePath;
-(void) ShareImageToWeiCharfriend:(char*)ImagePath;
-(void) ShareLinkContentToWeiCharTimeLine:(char*)Title ContentText:(char*) ContentInfor LinkURL:(char*) URL ImageToalPath:(char*)ImagePath;
-(void) ShareLinkContentToWeiCharfriend:(char*)Title ContentText:(char*) ContentInfor LinkURL:(char*) URL ImageToalPath:(char*)ImagePath;
-(void) WeiChatPay:(char*)PartnerId PrepayId:(char*)prepayId NonceStr:(char*)nonceStr TimeStamp:(char*)timeStamp Sign:(char*)sign;
-(BOOL)application:(UIApplication *)application openURL:(NSURL *)url sourceApplication:(NSString *)sourceApplication annotation:(id)annotation;
-(BOOL)application:(UIApplication *)application handleOpenURL:(NSURL *)url;
@end
