// InAppPurchaseManager.m
#import "InAppPurchaseManager.h"

@implementation InAppPurchaseManager



- (void)requestProUpgradeProductData:(NSString*)PayIdentifiers
{
    NSSet *productIdentifiers = [NSSet setWithObject:PayIdentifiers/*@"com.runmonster.runmonsterfree.upgradetopro" */];
    productsRequest = [[SKProductsRequest alloc] initWithProductIdentifiers:productIdentifiers];
    productsRequest.delegate = self;
    [productsRequest start];
    
    // we will release the request object in the delegate callback
}



- (void)productsRequest:(SKProductsRequest *)request didReceiveResponse:(SKProductsResponse *)response
{
    NSArray *products = response.products;
    proUpgradeProduct = [products count] == 1 ? [products firstObject] : nil;
    if (proUpgradeProduct)
    {
        NSLog(@"Product title: %@" , proUpgradeProduct.localizedTitle);
        NSLog(@"Product description: %@" , proUpgradeProduct.localizedDescription);
        NSLog(@"Product price: %@" , proUpgradeProduct.price);
        NSLog(@"Product id: %@" , proUpgradeProduct.productIdentifier);
    }
    
    for (NSString *invalidProductId in response.invalidProductIdentifiers)
    {
        NSLog(@"Invalid product id: %@" , invalidProductId);
    }
    
    // finally release the reqest we alloc/init’ed in requestProUpgradeProductData
    //[productsRequest release];
    
    [[NSNotificationCenter defaultCenter] postNotificationName:kInAppPurchaseManagerProductsFetchedNotification object:self userInfo:nil];
}




// InAppPurchaseManager.m
//#define kInAppPurchaseProUpgradeProductId @"com.runmonster.runmonsterfree.upgradetopro"
NSString* kInAppPurchaseProUpgradeProductId=@"";
NSString* UnityObjName=@"";
NSString* UnityFunName=@"";
// 第一次启动 执行
//
- (void)loadStore:(char*)CallBackObjName BackFunName:(char*)CallBackFunName
{
    UnityObjName=[[NSString alloc] initWithUTF8String:CallBackObjName];
    UnityFunName=[[NSString alloc] initWithUTF8String:CallBackFunName];
    // restarts any purchases if they were interrupted last time the app was open
    [[SKPaymentQueue defaultQueue] addTransactionObserver:self];
   
}

//
// 用户是否设置支付
//
- (BOOL)canMakePurchases
{
    return [SKPaymentQueue canMakePayments];
}

//
// 购买信息
//
- (void)purchaseProUpgrade:(NSString *)prounctId
{
    kInAppPurchaseProUpgradeProductId=prounctId;

    [self requestProUpgradeProductData:prounctId];
    
    //商品id
    SKPayment *payment = [SKPayment paymentWithProductIdentifier:prounctId];
    
    [[SKPaymentQueue defaultQueue] addPayment:payment];
}

//
// saves a record of the transaction by storing the receipt to disk  保存记录的事务存储收据到磁盘
//
- (void)recordTransaction:(SKPaymentTransaction *)transaction
{
    if ([transaction.payment.productIdentifier isEqualToString:kInAppPurchaseProUpgradeProductId])
    {
        // save the transaction receipt to disk
        [[NSUserDefaults standardUserDefaults] setValue:transaction.transactionReceipt forKey:@"proUpgradeTransactionReceipt" ];
        [[NSUserDefaults standardUserDefaults] synchronize];
    }
}

// 从队列中删除事务和帖子对交易结果通知
//
- (void)finishTransaction:(SKPaymentTransaction *)transaction wasSuccessful:(BOOL)wasSuccessful
{
    // remove the transaction from the payment queue.
    [[SKPaymentQueue defaultQueue] finishTransaction:transaction];
    
    NSDictionary *userInfo = [NSDictionary dictionaryWithObjectsAndKeys:transaction, @"transaction" , nil];
    if (wasSuccessful)
    {
        // send out a notification that we’ve finished the transaction
        [[NSNotificationCenter defaultCenter] postNotificationName:kInAppPurchaseManagerTransactionSucceededNotification object:self userInfo:userInfo];
    }
    else
    {
        // send out a notification for the failed transaction
        [[NSNotificationCenter defaultCenter] postNotificationName:kInAppPurchaseManagerTransactionFailedNotification object:self userInfo:userInfo];
    }
}

// 购买完成了的 向服务器发送凭证
- (void)completeTransaction:(SKPaymentTransaction *)transaction
{
    [self recordTransaction:transaction];
    
    //SKPaymentTransaction* skt=[[SKPaymentTransaction alloc] i]
    
    NSString* temptransactionReceipt=[[NSString alloc] initWithData:transaction.transactionReceipt  encoding:NSUTF8StringEncoding];
    
    temptransactionReceipt=[@"Sucess/" stringByAppendingString:temptransactionReceipt];
    
    
    UnitySendMessage(UnityObjName.UTF8String,UnityFunName.UTF8String,temptransactionReceipt.UTF8String);

    [self finishTransaction:transaction wasSuccessful:YES];
}

//已经购买过  恢复
- (void)restoreTransaction:(SKPaymentTransaction *)transaction
{
    [self recordTransaction:transaction.originalTransaction];
    
    
    NSString *temptransactionReceipt=[[NSString alloc] initWithData:transaction.transactionReceipt encoding:NSUTF8StringEncoding];
    
    temptransactionReceipt=[@"Restore/" stringByAppendingString:temptransactionReceipt];
    
    UnitySendMessage(UnityObjName.UTF8String,UnityFunName.UTF8String,temptransactionReceipt.UTF8String);
    
    [self finishTransaction:transaction wasSuccessful:YES];
}
//
// called when a transaction has failed当一个事务失败了
//
- (void)failedTransaction:(SKPaymentTransaction *)transaction
{
    NSString *temptransactionReceipt=[[NSString alloc] initWithData:transaction.transactionReceipt encoding:NSUTF8StringEncoding];
    
    temptransactionReceipt=[@"Failed/" stringByAppendingString:temptransactionReceipt];
    
    UnitySendMessage(UnityObjName.UTF8String,UnityFunName.UTF8String,temptransactionReceipt.UTF8String);
    
    if (transaction.error.code != SKErrorPaymentCancelled)
    {
        // error!
        [self finishTransaction:transaction wasSuccessful:NO];
    }
    else
    {
        // this is fine, the user just cancelled, so don’t notify
        [[SKPaymentQueue defaultQueue] finishTransaction:transaction];
    }
}

//
// called when the transaction status is updated更新交易状态时调用
//
- (void)paymentQueue:(SKPaymentQueue *)queue updatedTransactions:(NSArray *)transactions
{
    for (SKPaymentTransaction *transaction in transactions)
    {
        switch (transaction.transactionState)
        {
            case SKPaymentTransactionStatePurchased:
                [self completeTransaction:transaction];
                break;
            case SKPaymentTransactionStateFailed:
                [self failedTransaction:transaction];
                break;
            case SKPaymentTransactionStateRestored:
                [self restoreTransaction:transaction];
                break;
            case SKPaymentTransactionStateDeferred:
                break;
            default:
                break;
        }
    }
}
@end
