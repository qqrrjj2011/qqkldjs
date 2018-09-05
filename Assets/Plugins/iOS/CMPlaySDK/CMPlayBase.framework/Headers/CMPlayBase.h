//
//  CMPlayBase.h
//  CMPlayBase
//
//  Created by wangyufeng on 16/8/2.
//  Copyright © 2016年 cmcm. All rights reserved.
//

#import <UIKit/UIKit.h>

#import <CMPlayBase/CMPCommonHelper.h>
#import <CMPlayBase/CMPDelegateManager.h>
#import <CMPlayBase/CMPCloudConfigHelper.h>
#import <CMPlayBase/CMPReportHelper.h>
#import <CMPlayBase/CMPDelegateManager.h>
#import <CMPlayBase/CMPDownloadManager.h>
#import <CMPlayBase/CMPBOpenUDID.h>
#import <CMPlayBase/CMPBReachability.h>
#import <CMPlayBase/CMPDebugManager.h>
#import <CMPlayBase/CMPNotificationManager.h>
#import <CMPlayBase/CMPZipArchive.h>
#import <CMPlayBase/CMPBase64.h>
#import <CMPlayBase/NSData+CMPBZlib.h>
#import <CMPlayBase/CMPKeyChain.h>
#import <CMPlayBase/NSString+CMPTranscoding.h>
#import <CMPlayBase/UIImage+CMPGif.h>

#define CMPLAY_BASE_VERSION (20304)

/**
 * 此文件供接入方方便引入公共头文件
 * 注意：此文件未引入了提供给C++和C的接口(Utils后缀)文件，要使用相关接口请自行引入相关Utils
 */
@interface CMPlayBase : NSObject
/// 游戏一般只有一个UIViewController，此参数供SDK内用
@property (nonatomic, strong) UIViewController* rootViewController;

/// 实例
+ (CMPlayBase*)sharedInstance;

/// CMPlayBase的版本号
+ (NSString*)getCMPlayBaseVersion;

- (void)cmplaybasePresentLoadingView:(CGFloat)viewWidth red:(CGFloat)red green:(CGFloat)green blue:(CGFloat)blue alpha:(CGFloat)alpha;
- (void)cmplaybaseDismissLoadingView;
//处理common 的app 跳转
- (void)cmplaybaseOpenAppWithAppID:(NSString *)appID isLoadingBlock:(IsLoadingBlock) block;
// APP内评分页面展现
- (BOOL)cmplaybasePresentAPPReview;

/**
 stop tracking user information
 */
- (void)stopTracking;

/**
 is stop tracking user information?

 @return result
 */
- (BOOL)getIsStopTracking;
@end
