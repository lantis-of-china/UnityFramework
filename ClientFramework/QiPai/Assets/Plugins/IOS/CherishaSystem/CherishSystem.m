//
//  CherishSystem.m
//  Unity-iPhone
//
//  Created by 兰志强 on 2017/2/24.
//
//

#import "CherishSystem.h"

@implementation CherishSystem


+ (int)dataNetworkTypeFromStatusBar {
    
        NSString* result;
        Reachability *r = [Reachability reachabilityWithHostName:@"www.baidu.com"];
        switch ([r currentReachabilityStatus]) {
            caseNotReachable:// 没有网络连接
                return 0;
            caseReachableViaWWAN:// 使用3G网络
                return 2;
            caseReachableViaWiFi:// 使用WiFi网络
            return 3;        }
        return 0;
    
    
}





+(double)getCurrentBatteryLevel
{
    [UIDevice currentDevice].batteryMonitoringEnabled = YES;
    
    double deviceLevel = [UIDevice currentDevice].batteryLevel;
    
   return deviceLevel;
}


@end
