//
//  InAppPurchaseManager.h
//  plus2048
//
//  Created by 兰志强 on 14-11-20.
//
//


#import <StoreKit/StoreKit.h>

#define kInAppPurchaseManagerProductsFetchedNotification @"kInAppPurchaseManagerProductsFetchedNotification"
#define kInAppPurchaseManagerTransactionFailedNotification @"kInAppPurchaseManagerTransactionFailedNotification"
#define kInAppPurchaseManagerTransactionSucceededNotification @"kInAppPurchaseManagerTransactionSucceededNotification"


@interface InAppPurchaseManager : NSObject <SKProductsRequestDelegate,SKPaymentTransactionObserver>
{
    SKProduct *proUpgradeProduct;
    SKProductsRequest *productsRequest;
}

- (void)loadStore:(char*)CallBackObjName BackFunName:(char*)CallBackFunName;
- (void)purchaseProUpgrade:(NSString *)prounctId;
@end



