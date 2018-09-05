//
//  CMPNotificationUtils.h
//  CMPlayBase
//
//  Created by 白利兵 on 17/3/31.
//  Copyright © 2017年 cmcm. All rights reserved.
//

#ifndef CMPNotificationUtils_h
#define CMPNotificationUtils_h
#include <stdio.h>
#include <string>
class CMPNotificationUtils {
public:
    
    /**
     设定本地通知栏

     @param notificationId 通知栏标识符(必须传此参数，后续会通过此参数识别通知栏)
     @param title 标题(ios10以上可用)
     @param content 内容
     @param attachmentFilePath 附件本地路径(ios10以上可用，附件包括图片，音频，视频)
     @param showDate 弹出时间(时间格式：yyyy-MM-dd/HH:mm:ss)或者Unix时间戳字符串(单位s)
     @param intervalTime 间隔时间(单位ms)
     @param notifyInfo 通知栏信息(可根据此信息做操作)
     */
    static void scheduleLocalNotification(int notificationId,const char *title,const char *content,const char *attachmentFilePath,const char *showDate,int intervalTime,const char *notifyInfo);
    
    /**
     取消本地通知栏

     @param notificationId 通知栏标识符
     */
    static void cancelledNotification(int notificationId);
};
#endif

#ifdef __cplusplus

/**
 * 提供给C的魔方接口
 */
extern "C" {
    /**
     设定本地通知栏
     
     @param notificationId 通知栏标识符(必须传此参数，后续会通过此参数识别通知栏)
     @param title 标题(ios10以上可用)
     @param content 内容
     @param attachmentFilePath 附件本地路径(ios10以上可用，附件包括图片，音频，视频)
     @param showDate 弹出时间(时间格式：yyyy-MM-dd/HH:mm:ss)或者Unix时间戳字符串(单位s)
     @param intervalTime 间隔时间(单位ms)
     @param notifyInfo 通知栏信息(可根据此信息做操作)
     */
    void CMPScheduleLocalNotification(int notificationId,const char *title,const char *content,const char *attachmentFilePath,const char *showDate,int intervalTime,const char *notifyInfo);
    
    /**
     取消本地通知栏
     
     @param notificationId 通知栏标识符
     */
    void CMPCancelledNotification(int notificationId);
}

#endif
