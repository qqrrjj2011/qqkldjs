//
//  CMPKeyChain.h
//  CMPlayBase
//
//  Created by KepenJ on 17/1/4.
//  Copyright © 2017年 cmcm. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface CMPKeyChain : NSObject
//存储
+ (void)save:(NSString *)service data:(id)data;
//读取
+ (id)load:(NSString *)service;
//删除
+ (void)delete:(NSString *)service;
@end
