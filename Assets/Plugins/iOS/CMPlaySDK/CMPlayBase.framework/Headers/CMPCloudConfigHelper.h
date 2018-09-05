//
//  CMPCloudConfigHelper.h
//  CMPlayBase
//
//  Created by wangyufeng on 16/8/4.
//  Copyright © 2016年 cmcm. All rights reserved.
//

#import <Foundation/Foundation.h>

static NSString *const kCloudConfigRefreshNotify = @"CloudConfigRefreshNotify";

/**
 * cloud config API
 */
@interface CMPCloudConfigHelper : NSObject

/**
 * Pull data from cloud when the player open game or switch language，this method must be called，and use of asynchronous to pull data.
 * @param language APP Language
 */
+ (void)pullCloudConfigDataWithLanguage:(NSString*)language;

/**
 * Back to the key_value of section（Dictionary），access the priority ones
 * @param function According to the func_type in the background to separate the data，the data on the cloud is usually use 2
 * @param section data node
 */
+ (NSDictionary*)getKeyValueInSection:(NSNumber*)function section:(NSString *)section;

/**
 * Access data，The same name of the section may exist，access the priority ones（The lower of the priority value priority, the higher the priority）
 * @param function According to the func_type in the background to separate the data，the data on the cloud is usually use 2
 * @param section data node
 */
+ (NSString*)getCloudData:(int)function section:(NSString*)section;

/**
 * back to the key_value list in the section of the same name
 * @param function According to the func_type in the background to separate the data，the data on the cloud is usually use 2
 * @param section data node
 */
+ (NSArray*) getCloudDatas:(int)function section:(NSString*)section;

/**
 * back to the key_value in the section to access the date（access the priority ones）
 * @param function According to the func_type in the background to separate the data，the data on the cloud is usually use 2
 * @param section data node
 * @param key field name
 * @param defValue default data
 */
+ (NSString*)getCloudStringValue:(int)function section:(NSString*)section key:(NSString*)key defValue:(NSString*)defValue;

/**
 * back to the key_value in the section to access the date（access the priority ones）
 * @param function According to the func_type in the background to separate the data，the data on the cloud is usually use 2
 * @param section data node
 * @param key field name
 * @param defValue default data
 */
+ (int)getCloudIntValue:(int)function section:(NSString*)section key:(NSString*)key defValue:(int)defValue;

/**
 * back to the key_value in the section to access the date（access the priority ones）
 * @param function According to the func_type in the background to separate the data，the data on the cloud is usually use 2
 * @param section data node
 * @param key field name
 * @param defValue default data
 */
+ (long)getCloudLongValue:(int)function section:(NSString*)section key:(NSString*)key defValue:(long)defValue;

/**
 * back to the key_value in the section to access the date（access the priority ones）
 * @param function According to the func_type in the background to separate the data，the data on the cloud is usually use 2
 * @param section data node
 * @param key field name
 * @param defValue default data
 */
+ (BOOL)getCloudBoolValue:(int)function section:(NSString*)section key:(NSString*)key defValue:(BOOL)defValue;

/**
 * back to the key_value in the section to access the date（access the priority ones）
 * @param function According to the func_type in the background to separate the data，the data on the cloud is usually use 2
 * @param section data node
 * @param key field name
 * @param defValue default data
 */
+ (double)getCloudDoubleValue:(int)function section:(NSString*)section key:(NSString*)key defValue:(double)defValue;

//返回当前请求魔方数据的 url
+ (NSString *)getCurrentCloudDataRequest;

//判断当前数据来源，用于上报infoc
+ (BOOL)isCurrentDataFromLocal;

//获取magic 版本
+ (NSString *)getMagicVersion;
@end
