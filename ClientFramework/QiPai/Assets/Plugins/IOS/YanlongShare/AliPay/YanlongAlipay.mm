//  Created by 兰志强 on 15/3/17.
//  Copyright (c) 2015年 com.leijing. All rights reserved.
//

#import "YanlongAlipayController.h"

 
extern "C"
{
	void _AliPay(char* orderString,char* appScheme)
	{
		[YanlongAlipayController.InstanceYanlongAlipayController AliPay:orderString appScheme:appScheme];
	}
}
