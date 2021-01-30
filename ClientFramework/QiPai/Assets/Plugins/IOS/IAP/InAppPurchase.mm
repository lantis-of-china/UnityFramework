//
//  InAppPurchase.mm
//  plus2048
//
//  Created by 兰志强 on 14-11-20.
//
//

//InAppPurchase.mm



#import <Foundation/Foundation.h>

#import <StoreKit/StoreKit.h>

#import "InAppPurchaseManager.h"



extern "C"
{
   
    
    InAppPurchaseManager *sharedMgr = nil;
    
    void _InstancePurchase(char* CallBackObjName,char* CallFunName)
    {
        if (sharedMgr == nil)
        {
            sharedMgr = [[InAppPurchaseManager alloc]init];
            
            [sharedMgr loadStore:CallBackObjName BackFunName:CallFunName];
            
        }
    }
    
    //是否能购买
    bool _CanPurchase()
    {
        return ([SKPaymentQueue canMakePayments]);
    }
    
    
    //UnitySendMessage
    
    //调用购买
    void _BuyPay(char* prounctId)
    {
        NSString *_prounctId=@"1";//[[NSString alloc] initWithUTF8String:prounctId];
        
        if(sharedMgr!=nil)
        {
        
            [sharedMgr purchaseProUpgrade:_prounctId];
        }
        
    }
    
}
