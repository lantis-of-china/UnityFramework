#import <UIKit/UIKit.h>
#import <AlipaySDK/AlipaySDK.h>

@interface YanlongAlipayController : UIResponder <UIApplicationDelegate>

@property (strong, nonatomic) UIWindow *window;

+(YanlongAlipayController*)InstanceYanlongAlipayController;

-(void)AliPay:(char*)orderString appScheme:(char*)appScheme;

-(BOOL)application:(UIApplication *)application openURL:(NSURL *)url sourceApplication:(NSString *)sourceApplication annotation:(id)annotation;

-(BOOL)application:(UIApplication *)app openURL:(NSURL *)url options:(NSDictionary<NSString*, id> *)options;
@end
