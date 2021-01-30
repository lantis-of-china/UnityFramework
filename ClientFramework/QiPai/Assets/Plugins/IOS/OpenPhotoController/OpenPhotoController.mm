#import "OpenPhotoController.h"  
#import "UnityAppController.h"

@interface OpenPhotoController()
@property (nonatomic, strong)UIImagePickerController *picker;
@end

@implementation OpenPhotoController
static int Out_Result;

@synthesize popoverViewController = _popoverViewController;

- (void)viewDidLoad {
    [super viewDidLoad];
    //[self showPicker:UIImagePickerControllerSourceTypePhotoLibrary];
    [self showActionSheet:self.type];
}

- (void)dealloc {
    NSLog(@"dealloc");
    
    UnitySendMessage("AppRun", "OnApplicationPause","false");
    //UnitySendMessage("AppRun", "StopCoroutine","false");
    //UIViewController *vc = UnityGetGLViewController();
    
    //[UIViewController attemptRotationToDeviceOrientation];
    //UIViewController *vc = UnityGetGLViewController();
    //[vc presentViewController:this animated:YES completion:nil];
}

+(int) getResul
{
    return Out_Result;
}

+(void) setResul:(int)value
{
    Out_Result = value;
}

- (UIInterfaceOrientation)preferredInterfaceOrientationForPresentation
{
     return UIInterfaceOrientationLandscapeLeft;
}

-(void)showActionSheet:(int)type
{
    Out_Result = 0;
    /*
    UIActionSheet *sheet = [[UIActionSheet alloc] initWithTitle:nil  
                                                delegate:self  
                                                cancelButtonTitle:NSLocalizedString( @"È¡Ïû", nil )  
                                                destructiveButtonTitle:nil  
                                                otherButtonTitles:NSLocalizedString( @"ÅÄÕÕ", nil ), NSLocalizedString( @"Ïà²á", nil ), nil];  
      
    if( UI_USER_INTERFACE_IDIOM() == UIUserInterfaceIdiomPad )  
        [sheet showFromRect:CGRectMake( 0, 0, 128, 128 ) inView:UnityGetGLViewController().view animated:YES];  
    else  
        [sheet showInView:UnityGetGLViewController().view];  
      */
    //[sheet release];
    if(type == 0)
    {
        [self showPicker:UIImagePickerControllerSourceTypeCamera];
    }
    else
    {
        [self showPicker:UIImagePickerControllerSourceTypePhotoLibrary];
    }
}

- (void)actionSheet:(UIActionSheet*)actionSheet clickedButtonAtIndex:(NSInteger)buttonIndex  
{  
    if(buttonIndex == 0)
    {
        [self showPicker:UIImagePickerControllerSourceTypeCamera];  
    }
    else if(buttonIndex == 1)
    {  
        [self showPicker:UIImagePickerControllerSourceTypePhotoLibrary];  
    }
    else // Cancelled  
    {  
        //UnityPause( false );  
        //UnitySendMessage( "EtceteraManager", "imagePickerDidCancel", "" );  
    }  
}  
  
- (void)showPicker:(UIImagePickerControllerSourceType)type  
{  
    UIImagePickerController *picker = [[UIImagePickerController alloc] init];
    self.picker = picker;
    
    picker.sourceType = type;  
    picker.allowsEditing = YES;  
    picker.delegate = self;
    // We need to display this in a popover on iPad  
    if( UI_USER_INTERFACE_IDIOM() == UIUserInterfaceIdiomPad )  
    {  
        Class popoverClass = NSClassFromString( @"UIPopoverController" );  
        if( !popoverClass )  
            return;  
          
        _popoverViewController = [[popoverClass alloc] initWithContentViewController:picker];  
        [_popoverViewController setDelegate:self];  
        //picker.modalInPopover = YES;  
          
        // Display the popover  
        [_popoverViewController presentPopoverFromRect:CGRectMake( 0, 0, 128, 128 )  
                                                inView:UnityGetGLViewController().view  
                              permittedArrowDirections:UIPopoverArrowDirectionAny  
                                              animated:YES];  
    }  
    else  
    {  
        // wrap and show the modal
        NSString *version = [UIDevice currentDevice].systemVersion;
        if (version.doubleValue >= 10.0)
        {
            [self presentViewController:self.picker animated:YES completion:nil];
        }
        else
        {
            UIViewController *vc = UnityGetGLViewController();
            
            [vc presentViewController:picker animated:YES completion:^{picker.delegate = self;}];
        }
        //[self presentViewController:self.picker animated:YES completion:nil];
    }
}  
- (void)popoverControllerDidDismissPopover:(UIPopoverController*)popoverController  
{  
    self.popoverViewController = nil;  
    //UnityPause( false );  
      
    //UnitySendMessage( "EtceteraManager", "imagePickerDidCancel", "" );  
}



- (void)imagePickerController:(UIImagePickerController *)picker didFinishPickingMediaWithInfo:(NSDictionary *)info
{  
    // Grab the image and write it to disk  
    UIImage *image;  
    UIImage *image2;  
    //  if( _pickerAllowsEditing )  
    image = [info objectForKey:UIImagePickerControllerEditedImage];  
    //        else  
    //            image = [info objectForKey:UIImagePickerControllerOriginalImage];  
      
    //NSLog( @"picker got image with orientation: %i", image.imageOrientation );  
    UIGraphicsBeginImageContext(CGSizeMake(128,128));  
    [image drawInRect:CGRectMake(0, 0, 128, 128)];  
    image2 = UIGraphicsGetImageFromCurrentImageContext();  
    UIGraphicsEndImageContext();  
    
    NSData *imgData;  
    if(UIImagePNGRepresentation(image2) == nil)  
    {  
         imgData= UIImageJPEGRepresentation(image2, 0.5);  
    }  
    else  
    {  
         imgData= UIImageJPEGRepresentation(image2, 0.5);  
    }  
    NSString *imagePath = @"/image.jpg";
    NSArray *paths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);
    NSString *documentsDirectory = [paths objectAtIndex:0];
    //NSString *_encodeImageStr = [imgData base64Encoding];
    [imgData writeToFile:[documentsDirectory stringByAppendingString:imagePath] atomically:YES];
    //UnitySendMessage( "UnityIOSBridge", "PhotoCallBack", _encodeImageStr.UTF8St);
    
    // Dimiss the pickerController
    Out_Result = 1;
    
//    [self dismissWrappedController];
    [self.picker dismissViewControllerAnimated:YES completion:nil];
    [self dismissViewControllerAnimated:YES completion:nil];
}
- (void)imagePickerControllerDidCancel:(UIImagePickerController*)picker  
{
    [self.picker dismissViewControllerAnimated:YES completion:nil];
    
    [self dismissViewControllerAnimated:YES completion:nil];
    // dismiss the wrapper, unpause and notifiy Unity what happened  
//    [self dismissWrappedController];
    //UnityPause( false );
    //UnitySendMessage( "EtceteraManager", "imagePickerDidCancel", "" );  
}  
  
- (void)dismissWrappedController  
{
//    [self presentingViewController];
    //UnityPause( false );
      
//    UIViewController *vc = UnityGetGLViewController();
//
//    // No view controller? Get out of here.
//    if( !vc )
//        return;
//
//    // dismiss the view controller
//    [vc dismissModalViewControllerAnimated:YES];
//
//    // remove the wrapper view controller
//    [self performSelector:@selector(removeAndReleaseViewControllerWrapper) withObject:nil afterDelay:1.0];
    
    
    //UnitySendMessage( "EtceteraManager", "dismissingViewController", "" );
}

- (void)removeAndReleaseViewControllerWrapper  
{  
    // iPad might have a popover  
    if( UI_USER_INTERFACE_IDIOM() == UIUserInterfaceIdiomPad && _popoverViewController )  
    {  
        [_popoverViewController dismissPopoverAnimated:YES];  
        self.popoverViewController = nil;  
    }  
}
@end
  

