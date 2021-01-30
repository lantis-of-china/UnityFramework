//
//  Header.h
//  Unity-iPhone
//
//  Created by 兰志强 on 2017/2/24.
//
//

#ifndef Header_h
#define Header_h
#import <UIKit/UIKit.h>
#import "Reachability.h"

@interface CherishSystem

+(int)dataNetworkTypeFromStatusBar;
+(double)getCurrentBatteryLevel;
@end
#endif /* Header_h */
