//
//  CMPCloudConfigManager.h
//  CMPlayBase
//
//  Created by KepenJ on 16/5/12.
//  Copyright © 2016年 cmplay. All rights reserved.
//
//  If you have any questions about CMPlayBase.framework,please contact KepenJ(Email: kepenj@gmail.com )


#import <Foundation/Foundation.h>
#import <CMPlayBase/CMPlayBase.h>
@protocol CMPCloudConfigManagerDelegate;
@interface CMPCloudConfigManager : NSObject<CMPCloudConfigDelegate>
@property (nonatomic,weak) id <CMPCloudConfigManagerDelegate>delegate;
@property (nonatomic,assign) BOOL isNewCloud;
@property (nonatomic,strong,readonly) NSString * cloudDataURL;
@property (nonatomic,assign,readonly) BOOL isLocalData;
@property (nonatomic,strong,readonly) NSString * magicVersion;

/**
 *  初始化
 *
 *  @param delegate 接收回调的对象
 *
 *  @return 单例对象
 */
+ (CMPCloudConfigManager*) sharedInstance;

/**
 * 初始化云端配置
 * @param productId 魔方分配的ID
 * @param pkg 包名
 * @param channelId 渠道
 */
- (void)initCloudConfig:(NSString*)productId pkg:(NSString*)pkg channelId:(NSString*)channelId;

@end

@protocol CMPCloudConfigManagerDelegate <NSObject>

- (void)reportCloudMagicInfoc:(NSString*)magicVer action:(int)action remark:(NSString*)remark errorURL:(NSString *)url;

@end
