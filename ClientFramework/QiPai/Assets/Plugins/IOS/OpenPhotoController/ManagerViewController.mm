//
//  ManagerViewController.m
//  Unity-iPhone
//
//  Created by 屈屹 on 2018/3/1.
//

#import "OpenPhotoController.h"

extern "C"
{
    void _TakePhoto(int type)
    {
        OpenPhotoController *app = [[OpenPhotoController alloc] init];
        app.type = type;
        UnitySendMessage("AppRun", "OnApplicationPause","true");
        //[app viewDidLoadCall];
        UIViewController *vc = UnityGetGLViewController();
        //[vc presentViewController:app animated:YES completion:nil];
        [vc presentViewController:app animated:YES completion:nil];
        //[vc dismissViewControllerAnimated:<#(BOOL)#> completion:<#^(void)completion#>:app animated:YES completion:nil];
//        if( ![UIImagePickerController isSourceTypeAvailable:UIImagePickerControllerSourceTypeCamera] )
//        {
////            [app showPicker:UIImagePickerControllerSourceTypeSavedPhotosAlbum];
//            UIViewController *vc = UnityGetGLViewController();
//
//            //        [vc presentViewController:picker animated:YES completion:^{
//            //            picker.delegate = self;
//            //        }];
//            [vc presentViewController:app animated:YES completion:nil];
//            return;
//        }
        
        //[app showActionSheet:type];
    }
    
    int _GetTakePhontResult()
    {
        return [OpenPhotoController getResul];
//        return 1;
    }
}
