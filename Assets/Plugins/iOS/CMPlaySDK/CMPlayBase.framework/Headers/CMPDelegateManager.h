//
//  CMPDelegateManager.h
//  CMPlayBase
//
//  Created by wangyufeng on 16/8/4.
//  Copyright © 2016年 cmcm. All rights reserved.
//

#import <Foundation/Foundation.h>

/**
 * 魔方对外接口能力
 */
@protocol CMPCloudConfigDelegate <NSObject>
@optional

/**
 * 拉取云端数据，启动游戏和切换游戏语言时，务必调用此方法，此方法使用异步线程执行拉取
 * @param language APP语言
 */
- (void)pullCloudConfigDataWithLanguage:(NSString*)language;

/**
 * 返回section中的key_value数据（字典化），取优先级高的
 * @param function 根据后台划分的func_type来区分数据投放，云端数据一般是2
 * @param section 数据节点
 */
- (NSDictionary*)getKeyValueInSection:(NSNumber*)function section:(NSString *)section;

/**
 * 获取数据，若存在同名的section，取优先级高的（priority值越低优先级越高）
 * @param function 根据后台划分的func_type来区分数据投放，云端数据一般是2
 * @param section 数据节点
 */
- (NSString*)getCloudData:(int)function section:(NSString*)section;

/**
 * 返回同名section下的key_value列表
 * @param function 根据后台划分的func_type来区分数据投放，云端数据一般是2
 * @param section 数据节点
 */
- (NSArray*) getCloudDatas:(int)function section:(NSString*)section;

/**
 * 返回section中的key_value数据下的字段里的数据（获取优先级最高的字段数据）
 * @param function 根据后台划分的func_type来区分数据投放，云端数据一般是2
 * @param section 数据节点
 * @param key 字段名
 * @param defValue 默认数据
 */
- (NSString*)getCloudStringValue:(int)function section:(NSString*)section key:(NSString*)key defValue:(NSString*)defValue;

/**
 * 返回section中的key_value数据下的字段里的数据（获取优先级最高的字段数据）
 * @param function 根据后台划分的func_type来区分数据投放，云端数据一般是2
 * @param section 数据节点
 * @param key 字段名
 * @param defValue 默认数据
 */
- (int)getCloudIntValue:(int)function section:(NSString*)section key:(NSString*)key defValue:(int)defValue;

/**
 * 返回section中的key_value数据下的字段里的数据（获取优先级最高的字段数据）
 * @param function 根据后台划分的func_type来区分数据投放，云端数据一般是2
 * @param section 数据节点
 * @param key 字段名
 * @param defValue 默认数据
 */
- (long)getCloudLongValue:(int)function section:(NSString*)section key:(NSString*)key defValue:(long)defValue;

/**
 * 返回section中的key_value数据下的字段里的数据（获取优先级最高的字段数据）
 * @param function 根据后台划分的func_type来区分数据投放，云端数据一般是2
 * @param section 数据节点
 * @param key 字段名
 * @param defValue 默认数据
 */
- (BOOL)getCloudBoolValue:(int)function section:(NSString*)section key:(NSString*)key defValue:(BOOL)defValue;

/**
 * 返回section中的key_value数据下的字段里的数据（获取优先级最高的字段数据）
 * @param function 根据后台划分的func_type来区分数据投放，云端数据一般是2
 * @param section 数据节点
 * @param key 字段名
 * @param defValue 默认数据
 */
- (double)getCloudDoubleValue:(int)function section:(NSString*)section key:(NSString*)key defValue:(double)defValue;

//返回当前请求魔方数据的 url
- (NSString *)getCloudDataRequest;

- (BOOL)isCurrentDataFromLocal;

- (NSString *)getMagicVersion;
@end

/**
 * 埋点上报对外接口能力
 */
@protocol CMPReportDelegate <NSObject>
@optional
/**
 * 上报表数据(此接口默认会追加字段network，调用方不需要拼接此字段)
 * @param tableName 表名
 * @param data 表数据, 格式：key=value&key=value，如：uptime=1113213&action=3&remark=abc
 */
- (void)reportData:(NSString*)tableName data:(NSString*)data isSDKReport:(BOOL)isSDKReport;

/**
 * 上报事件（games_businessdata表）
 * @param action 事件
 */
- (void)reportEvent:(int)action isSDKReport:(BOOL)isSDKReport;

@end

/**
 GDPR对外埋点上报接口
 */
@protocol CMPGDPRReportDelegate <NSObject>
@optional
/**
 上报GDPR埋点
 
 @param tableName 表名字
 @param data 数据
 */
- (void)reportGDPRData:(NSString *)tableName totalInfoDict:(NSDictionary *)totalInfoDict;
@end

/**
 * 此类管理SDK内各framework间互相调用的接口
 */
@interface CMPDelegateManager : NSObject

/// 魔方对外接口，实例初化时会依赖注入此类
@property (strong) id<CMPCloudConfigDelegate> cloudConfigDelegate;

/// 埋点上报对外接口，实例初化时会依赖注入此类
@property (strong) id<CMPReportDelegate> reportDelegate;
@property (strong) id<CMPGDPRReportDelegate> reportGDPRDelegate;
+ (CMPDelegateManager*) sharedInstance;

@end
