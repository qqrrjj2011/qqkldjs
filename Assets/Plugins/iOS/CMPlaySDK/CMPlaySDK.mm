//
//  CMPlaySDK.m
//  CMPlaySDKSample
//
//  Created by wangyufeng on 16/9/12.
//  Copyright © 2016年 cmcm. All rights reserved.
//

#import "CMPlaySDK.h"
#import <CMPlayBase/CMPCommonUtils.h>
#if __has_include("CMPNotification.h")
#import "CMPNotification.h"
#endif
#if __has_include("CMPThirdPlatform.h")
#import "CMPThirdPlatform.h"
#endif
#if __has_include(<AppsFlyerLib/AppsFlyerTracker.h>)
#import <AppsFlyerLib/AppsFlyerTracker.h>
#elif __has_include("AppsFlyerTracker.h")
#import "AppsFlyerTracker.h"
#endif

@interface CMPlaySDK ()
@property (nonatomic, assign) BOOL isInitSDK;
@end

@implementation CMPlaySDK

static CMPlaySDK *defaultInstance = nil;

+ (id)allocWithZone:(struct _NSZone *)zone {
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        defaultInstance = [super allocWithZone:zone];
    });
    return defaultInstance;
}

+ (instancetype)sharedInstance {
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        defaultInstance = [[CMPlaySDK alloc] init];
    });     return defaultInstance;
}

- (id)init {
    if (self = [super init]) {
        [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(refreshCloudConfig:) name:kCloudConfigRefreshNotify object:nil];
        _isInitSDK = NO;
    }
    return self;
}

- (id)copyWithZone:(NSZone *)zone {
    return defaultInstance;
}
- (void)initCMPlaySDK:(UIApplication *)application LaunchOptions:(NSDictionary *)launchOptions RootViewController:(UIViewController *)rootViewController {
    if (!self.isInitSDK) {
#if __has_include(<AppsFlyerLib/AppsFlyerTracker.h>) || __has_include("AppsFlyerTracker.h")
        if ([AppsFlyerTracker respondsToSelector:@selector(sharedTracker)] && [[AppsFlyerTracker sharedTracker] respondsToSelector:@selector(getAppsFlyerUID)]) {
            [CMPCommonHelper setAppsflyerDeviceId:[[AppsFlyerTracker sharedTracker] getAppsFlyerUID]];
        }
#endif
        self.isInitSDK = YES;
        [CMPlayBase sharedInstance].rootViewController = rootViewController;
        // init Kinfoc
        NSString *infocUrl = @"http://helppinballvsblockios1.ksmobile.com/c/v2/";
        if ([CMPDebugManager sharedInstance].enableDebug) {
            infocUrl = @"http://118.89.55.235/c/v2/";
        }
        [CMPReportManager InitReport:"kfmt.dat" commonProductID:277 myProductID:277 rptAddr:[infocUrl UTF8String]];
        // init Cloud config
//        [[CMPCloudConfigManager sharedInstance] initCloudConfig:@"cloudProductId" pkg:@"cloudPkg" channelId:@"as"];
#if __has_include("CMPPromotion.h")
        // init Promotion
//        [[CMPPromotion sharedInstance] initPromotion];
#endif
#if __has_include("CMPPurchase.h")
        // init Purchase
//        [[CMPPurchase sharedInstance] initPurchase];
#endif
#if __has_include("CMPThirdPlatform.h")
        //init Third paltform
//        [[CMPThirdPlatform sharedInstance] initCMPlayThirdPlatformSDK:application LaunchOptions:launchOptions RootViewController:rootViewController];
#endif
    }
}

- (void)onGamePause {
    [[CMPReportManager getInstance] onGamePause];
}

- (void)onGameResume {
    [[CMPReportManager getInstance] onGameResume];
}

- (void)onGameStop {
    [[CMPReportManager getInstance] onGameStop];
}

- (void)setEnableDebug:(BOOL)enabled {
    [[CMPDebugManager sharedInstance] setEnableDebug:enabled];
}

- (void)refreshCloudConfig:(NSNotification *)notification{
    // Cloud config with data refresh, callback to the game's related implementation can be written here
    
    // Cocos
//        __NotificationCenter::getInstance()->postNotification(CLOUD_CONFIG_REFRESH_NOTIFY);
//     Unity
//        UnitySendMessage("CMPlayCallBack", "refreshCloudConfig", "");
}

- (BOOL)application:(UIApplication *)application openURL:(NSURL *)url sourceApplication:(NSString *)sourceApplication annotation:(id)annotation {
#if __has_include(<CMPlayThirdPlatform/CMPThirdPlatformHelper.h>)
    return [CMPThirdPlatformHelper application:application openURL:url sourceApplication:sourceApplication annotation:annotation];
#else
    return YES;
#endif
}

- (BOOL)application:(UIApplication *)app openURL:(NSURL *)url options:(NSDictionary<NSString*, id> *)options {
#if __has_include(<CMPlayThirdPlatform/CMPThirdPlatformHelper.h>)
    return [CMPThirdPlatformHelper application:app openURL:url options:options];
#else
    return YES;
#endif
}

- (void)application:(UIApplication *)application didReceiveLocalNotification:(UILocalNotification *)notification {
    [[CMPNotificationManager sharedInstance] application:application didReceiveLocalNotification:notification];
}

- (void)application:(UIApplication *)application didRegisterUserNotificationSettings:(UIUserNotificationSettings *)notificationSettings {
    [[CMPNotificationManager sharedInstance] application:application didRegisterUserNotificationSettings:notificationSettings];
}

- (void)application:(UIApplication *)application didRegisterForRemoteNotificationsWithDeviceToken:(NSData *)deviceToken {
    [[CMPNotificationManager sharedInstance] application:application didRegisterForRemoteNotificationsWithDeviceToken:deviceToken];
}

- (void)application:(UIApplication *)application didFailToRegisterForRemoteNotificationsWithError:(NSError *)error {
    [[CMPNotificationManager sharedInstance] application:application didFailToRegisterForRemoteNotificationsWithError:error];
}

- (void)application:(UIApplication *)application didReceiveRemoteNotification:(NSDictionary *)userInfo fetchCompletionHandler:(void (^)(UIBackgroundFetchResult))completionHandler {
    [[CMPNotificationManager sharedInstance] application:application didReceiveRemoteNotification:userInfo];
}

- (void)dealloc {
    [[NSNotificationCenter defaultCenter] removeObserver:self];
}

@end

extern "C" void CMPInitCMPlaySDK() {
    [[CMPlaySDK sharedInstance] initCMPlaySDK:[UIApplication sharedApplication] LaunchOptions:[NSDictionary dictionary] RootViewController:[UIApplication sharedApplication].keyWindow.rootViewController];
}

extern "C" void CMPOnGamePause() {
    [[CMPlaySDK sharedInstance] onGamePause];
}

extern "C" void CMPOnGameResume() {
    [[CMPlaySDK sharedInstance] onGameResume];
}

extern "C" void CMPOnGameStop() {
    [[CMPlaySDK sharedInstance] onGameStop];
}

extern "C" void CMPSetEnableDebug(bool enabled) {
    [[CMPlaySDK sharedInstance] setEnableDebug:enabled];
}
