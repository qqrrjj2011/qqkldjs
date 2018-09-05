//
//  CMPDebugManager.h
//  CMPlayBase
//
//  Created by KepenJ on 16/11/16.
//  Copyright © 2016年 cmcm. All rights reserved.
//


#import <Foundation/Foundation.h>
#define OPEN_SCREEN_DEBUG_REASON        (@"OPEN_SCREEN_DEBUG_REASON")
#define SETTING_PAGE_DEBUG_REASON       (@"SETTING_PAGE_DEBUG_REASON")
#define REWARD_VIDEO_DEBUG_REASON       (@"REWARD_VIDEO_DEBUG_REASON")
#define RESULT_PAGE_DEBUG_REASON        (@"RESULT_PAGE_DEBUG_REASON")
#define FAMILY_PAGE_DEBUG_REASON        (@"FAMILY_PAGE_DEBUG_REASON")

@interface CMPDebugManager : NSObject
@property (strong) NSMutableDictionary * canNotShowReasonDic;
@property (nonatomic,assign) BOOL enableDebug;
+ (instancetype)sharedInstance;
+ (void)cmpLog:(NSString *)log;

@end
