//
//  YanlongShareWeoBo.m
//  SocialFrameworkExample
//
//  Created by 兰志强 on 15/3/17.
//  Copyright (c) 2015年 com.leijing. All rights reserved.
//

//#import "YanlongShareWeiBo.h"


extern "C"
{
    //static ShareViewController* _YanlongShareController;
    
    void InstanceYanlongShareController()
    {
        /*
        if(_YanlongShareController==nil)
        {
            _YanlongShareController=[[ShareViewController alloc] init];
           // [[[UIApplication sharedApplication] keyWindow] addSubview:_YanlongShareController.view];
           UIView* GameView= UnityGetGLView();
            
            [GameView addSubview:_YanlongShareController.view];
        }
         */
    }
    
    Boolean _ShareSinaWeiBo(char* _TextContent,char* _ImagePath,char* _URL,char* _Title,char* _NotSupportMessage,char* _SucessMessage,char* _CancleMessage)
    {
        return false;
        //InstanceYanlongShareController();
        

        //return [_YanlongShareController ShareSinaWeiBo:_TextContent ImageFilePath:_ImagePath LinkURL:_URL WeiBoMessageTitle:_Title NotSupportMessage:_NotSupportMessage SucessMessage:_SucessMessage CancleMessage:_CancleMessage];
    }
    
    
    
    
    Boolean _ShareTencentWeiBo(char* _TextContent,char* _ImagePath,char* _URL,char* _Title,char* _NotSupportMessage,char* _SucessMessage,char* _CancleMessage)
    {
        return false;
        //InstanceYanlongShareController();
        //return [_YanlongShareController ShareTencentWeiBo:_TextContent ImageFilePath:_ImagePath LinkURL:_URL WeiBoMessageTitle:_Title NotSupportMessage:_NotSupportMessage SucessMessage:_SucessMessage CancleMessage:_CancleMessage];
    }
    
    
    Boolean _ShareFaceBookWeiBo(char* _TextContent,char* _ImagePath,char* _URL,char* _Title,char* _NotSupportMessage,char* _SucessMessage,char* _CancleMessage)
    {
        return  false;
        //InstanceYanlongShareController();
        ////return [_YanlongShareController ShareFaceBookWeiBo:_TextContent ImageFilePath:_ImagePath LinkURL:_URL WeiBoMessageTitle:_Title NotSupportMessage:_NotSupportMessage SucessMessage:_SucessMessage CancleMessage:_CancleMessage];
    }
    
    
    Boolean _ShareTwitterWeiBo(char* _TextContent,char* _ImagePath,char* _URL,char* _Title,char* _NotSupportMessage,char* _SucessMessage,char* _CancleMessage)
    {
        return false;
        //InstanceYanlongShareController();
        
        //return [_YanlongShareController ShareTwitterWeiBo:_TextContent ImageFilePath:_ImagePath LinkURL:_URL WeiBoMessageTitle:_Title NotSupportMessage:_NotSupportMessage SucessMessage:_SucessMessage CancleMessage:_CancleMessage];
    }
    
    void _OpenSystemShareActivity(char* ImagePath ,char* Content)
    {
        //InstanceYanlongShareController();
        //[_YanlongShareController OpenSysShareActivy:ImagePath ContentText:Content];
    }
    
}
