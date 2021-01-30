@interface OpenPhotoController : UIViewController<UIApplicationDelegate,UIImagePickerControllerDelegate, UIActionSheetDelegate,UINavigationControllerDelegate>
{
    UIView*                _rootView;
    UIViewController*    _rootController;
@private
    id _popoverViewController;
}

@property (nonatomic, retain) id popoverViewController;
@property (nonatomic, assign) int type;

+(int) getResul;
-(void)showActionSheet:(int)type;
- (UIInterfaceOrientation)preferredInterfaceOrientationForPresentation;
@end
