//
//  CMPReportHelper.h
//  CMPlayBase
//
//  Created by wangyufeng on 16/8/30.
//  Copyright © 2016年 cmcm. All rights reserved.
//

#import <Foundation/Foundation.h>

/**
 * API of data report
 */
@interface CMPReportHelper : NSObject

/**
 * reported data(This interface appends the field network by default, and the caller does not need to stitch this field)
 * @param tableName table
 * @param data date, format：key=value&key=value，e.g.：uptime=1113213&action=3&remark=abc, if key no value: key=&key=value&key=, key can not be missing
 */
+ (void)reportData:(NSString*)tableName data:(NSString*)data isSDKReport:(BOOL)isSDKReport;

/**
 * reported action data (report to table: games_businessdata)
 * @param action  action
 */
+ (void)reportEvent:(int)action isSDKReport:(BOOL)isSDKReport;
@end
