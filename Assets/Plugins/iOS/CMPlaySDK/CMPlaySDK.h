//
//  CMPlaySDK.h
//  CMPlaySDKSample
//
//  Created by wangyufeng on 16/9/12.
//  Copyright © 2016年 cmcm. All rights reserved.
//

#import <CMPlayBase/CMPlayBase.h>
#import <CMPlayInfoc/CMPlayInfoc.h>
#import <CMPlayCloudConfig/CMPlayCloudConfig.h>
#if __has_include("CMPPromotion.h")
#import "CMPPromotion.h"
#endif
#if __has_include("CMPPurchase.h")
#import "CMPPurchase.h"
#endif

@interface CMPlaySDK : NSObject

+ (CMPlaySDK*) sharedInstance;

/**
 * Initialization CMPlay SDK, SDK contains Infoc, CloudConfig, Promotion, etc.
 * @param rootViewController UIViewController
 */
- (void)initCMPlaySDK:(UIApplication *)application LaunchOptions:(NSDictionary *)launchOptions RootViewController:(UIViewController*)rootViewController;

/**
 * Game Pause (Application life cycle call, mainly used for Infoc report processing)
 */
- (void)onGamePause;

/**
 * Game Resume (Application life cycle call, mainly used for Infoc report processing)
 */
- (void)onGameResume;

/**
 * Game Stop (Application life cycle call, mainly used for Infoc report processing)
 */
- (void)onGameStop;

/**
 * Enable debug
 */
- (void)setEnableDebug:(BOOL)enabled;

/**
 open url
 
 @param application
 @param url
 @param sourceApplication
 @param annotation
 @return result
 */
- (BOOL)application:(UIApplication *)application openURL:(NSURL *)url sourceApplication:(NSString *)sourceApplication annotation:(id)annotation;

/**
 open application
 
 @param app
 @param url
 @param options
 @return
 */
- (BOOL)application:(UIApplication *)app openURL:(NSURL *)url options:(NSDictionary<NSString*, id> *)options;

- (void)application:(UIApplication *)application didReceiveLocalNotification:(UILocalNotification *)notification;

- (void)application:(UIApplication *)application didRegisterUserNotificationSettings:(UIUserNotificationSettings *)notificationSettings;

- (void)application:(UIApplication *)application didRegisterForRemoteNotificationsWithDeviceToken:(NSData *)deviceToken;

- (void)application:(UIApplication *)application didFailToRegisterForRemoteNotificationsWithError:(NSError *)error;

- (void)application:(UIApplication *)application didReceiveRemoteNotification:(NSDictionary *)userInfo fetchCompletionHandler:(void (^)(UIBackgroundFetchResult))completionHandler;
@end

#ifdef __cplusplus

/**
 * C interface
 */
extern "C" {
    
    /// init CMPlaySDK
    void CMPInitCMPlaySDK();
    
    /// Game Pause
    void CMPOnGamePause();
    
    /// Game Resume
    void CMPOnGameResume();
    
    /// Game Stop
    void CMPOnGameStop();
    
    /// Enable debug
    void CMPSetEnableDebug(bool enabled);
}

#endif
