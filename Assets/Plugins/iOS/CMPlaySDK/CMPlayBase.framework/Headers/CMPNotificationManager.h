//
//  CMPNotificationManager.h
//  CMPlayBase
//
//  Created by 白利兵 on 17/3/31.
//  Copyright © 2017年 cmcm. All rights reserved.
//

#import <UIKit/UIKit.h>
@class CMPNotificationManager;
@protocol CMPNotificationManagerDelegate <NSObject>
- (void)CMPNotificationManager:(CMPNotificationManager *)notificationManager didReceiveLocalNotification:(NSDictionary *)userInfo;

- (void)CMPNotificationManager:(CMPNotificationManager *)notificationManager didReceiveRemoteNotification:(NSDictionary *)userInfo;

@end


@interface CMPNotificationManager : NSObject

@property (nonatomic, weak) id <CMPNotificationManagerDelegate> delegate;

+ (instancetype)sharedInstance;
- (void)initNotification:(NSDictionary *)launchOptions;

- (void)application:(UIApplication *)application didRegisterUserNotificationSettings:(UIUserNotificationSettings *)notificationSettings;

- (void)application:(UIApplication *)application didRegisterForRemoteNotificationsWithDeviceToken:(NSData *)deviceToken;

- (void)application:(UIApplication *)application didFailToRegisterForRemoteNotificationsWithError:(NSError *)error;

- (void)application:(UIApplication *)application didReceiveRemoteNotification:(NSDictionary *)userInfo;

- (void)application:(UIApplication *)application didReceiveLocalNotification:(UILocalNotification *)notification;

- (void)applicationDidEnterBackground;

- (void)applicationDidEnterForeground;


/**
 设定本地通知栏

 @param notificationId 通知栏唯一标识符
 @param title 标题（iOS10以上）
 @param content 内容
 @param attachmentFilePath 附件路径
 @param showDate 弹出时间
 @param intervalTime 间隔时间
 @param notifyInfo 通知栏信息
 */
- (void)scheduleLocalNotificationWithNotificationId:(int)notificationId title:(NSString *)title Content:(NSString *)content attachmentFilePath:(NSString *)attachmentFilePath showDate:(NSString *)showDate intervalTime:(int)intervalTime notifyInfo:(NSString *)notifyInfo;

/**
 *  取消本地通知栏
 *
 *  @param notificationId 通知栏标识符
 */
- (void)cancelledNotification:(int)notificationId;
@end
