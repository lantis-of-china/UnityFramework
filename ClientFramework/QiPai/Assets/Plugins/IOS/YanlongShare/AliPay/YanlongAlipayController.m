#import "YanlongAlipayController.h"

@implementation YanlongAlipayController

static YanlongAlipayController* _Instance;

+(YanlongAlipayController*)InstanceYanlongAlipayController
{
    if(_Instance==nil)
    {
        _Instance=[[YanlongAlipayController alloc] init];
    }
    return _Instance;
}

-(void)AliPay:(char*)orderString appScheme:(char*)appScheme
{
   // NOTE: ����֧�������ʼ֧��
    [[AlipaySDK defaultService] payOrder:[NSString stringWithUTF8String:orderString] fromScheme:[NSString stringWithUTF8String:appScheme] callback:^(NSDictionary *resultDic) {
        NSLog(@"reslut = %@",resultDic);
    }];
}

- (BOOL)application:(UIApplication *)application openURL:(NSURL *)url sourceApplication:(NSString *)sourceApplication annotation:(id)annotation
{
    if ([url.host isEqualToString:@"safepay"]) {
        // ֧����ת֧����Ǯ������֧��������֧�����
        [[AlipaySDK defaultService] processOrderWithPaymentResult:url standbyCallback:^(NSDictionary *resultDic) {
            NSLog(@"result = %@",resultDic);
        }];
        
        // ��Ȩ��ת֧����Ǯ������֧��������֧�����
        [[AlipaySDK defaultService] processAuth_V2Result:url standbyCallback:^(NSDictionary *resultDic) {
            NSLog(@"result = %@",resultDic);
            // ���� auth code
            NSString *result = resultDic[@"result"];
            NSString *authCode = nil;
            if (result.length>0) {
                NSArray *resultArr = [result componentsSeparatedByString:@"&"];
                for (NSString *subResult in resultArr) {
                    if (subResult.length > 10 && [subResult hasPrefix:@"auth_code="]) {
                        authCode = [subResult substringFromIndex:10];
                        break;
                    }
                }
            }
            NSLog(@"��Ȩ��� authCode = %@", authCode?:@"");
        }];
    }
    return YES;
}

// NOTE: 9.0�Ժ�ʹ����API�ӿ�
- (BOOL)application:(UIApplication *)app openURL:(NSURL *)url options:(NSDictionary<NSString*, id> *)options
{
    if ([url.host isEqualToString:@"safepay"]) {
        // ֧����ת֧����Ǯ������֧��������֧�����
        [[AlipaySDK defaultService] processOrderWithPaymentResult:url standbyCallback:^(NSDictionary *resultDic) {
            NSLog(@"result = %@",resultDic);
        }];
        
        // ��Ȩ��ת֧����Ǯ������֧��������֧�����
        [[AlipaySDK defaultService] processAuth_V2Result:url standbyCallback:^(NSDictionary *resultDic) {
            NSLog(@"result = %@",resultDic);
            // ���� auth code
            NSString *result = resultDic[@"result"];
            NSString *authCode = nil;
            if (result.length>0) {
                NSArray *resultArr = [result componentsSeparatedByString:@"&"];
                for (NSString *subResult in resultArr) {
                    if (subResult.length > 10 && [subResult hasPrefix:@"auth_code="]) {
                        authCode = [subResult substringFromIndex:10];
                        break;
                    }
                }
            }
            NSLog(@"��Ȩ��� authCode = %@", authCode?:@"");
        }];
    }
    return YES;
}
@end
