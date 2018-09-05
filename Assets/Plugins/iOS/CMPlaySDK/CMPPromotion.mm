//
//  CMPPromotion.m
//  CMPlaySDKSample
//
//  Created by wangyufeng on 16/9/7.
//  Copyright © 2016年 cmcm. All rights reserved.
//

#import "CMPPromotion.h"
#import <CMPlayBase/CMPlayBase.h>

@implementation CMPPromotion

static CMPPromotion *defaultInstance = nil;

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
        defaultInstance = [[CMPPromotion alloc] init];
    });
    return defaultInstance;
}

- (id) init {
    if (self = [super init]) {
        [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(refreshPromotion:) name:kCMPSettingCardRefreshNotify object:nil];
        [[NSNotificationCenter defaultCenter] addObserver:self selector:@selector(refreshPromotion:) name:kCMPFamilyGamesRefreshNotify object:nil];

    }
    
    return self;
}

- (id)copyWithZone:(NSZone *)zone {
    return defaultInstance;
}

- (void)initPromotion {
    [CMPPromotionHelper initRewardedVideo];
    [CMPPromotionHelper setRewardedVideoDelegate:self];
    [CMPPromotionHelper initOpenScreen];
    [CMPPromotionHelper setOpenScreenClickDelegate:self];
    [CMPPromotionHelper initResultCard];
    [CMPPromotionHelper setResultCardClickDelegate:self];
    [CMPPromotionHelper initSettingCard];
    [CMPPromotionHelper setSettingCardClickDelegate:self];
    [CMPPromotionHelper initFamilyGames];
    [CMPPromotionHelper setFamilyGamesClickDelegate:self];
}

- (void)refreshPromotion:(NSNotification *)notification{
    if ([notification.name isEqualToString:kCMPSettingCardRefreshNotify]) {
        //        if (self.onSettingsPushUpdate) {
        //            self.onSettingsPushUpdate();
        //        }
        // Setting Card with data refresh, callback to the game's related implementation can be written here
        // Unity
        UnitySendMessage("Nativeutil", "onSettingsPushUpdate", "");
    }
    if ([notification.name isEqualToString:kCMPFamilyGamesRefreshNotify]) {
        //        if (self.onSettingsPushUpdate) {
        //            self.onSettingsPushUpdate();
        //        }
        // Setting Card with data refresh, callback to the game's related implementation can be written here
        // Unity
        UnitySendMessage("Nativeutil", "onFamilyGamesPushUpdate", "");
    }
}

#pragma mark - CMPRewardedVideoDelegate

- (void)rewardedVideoShow {
//    NSLog(@"video is showing");
}

- (void)rewardedVideoClick {
//    NSLog(@"video page is clicking");
}

- (void)rewardedVideoHide {
//    NSLog(@"video page is hidding");
    if (self.onVideoClosed) {
        self.onVideoClosed();
    }
    UnitySendMessage("Nativeutil", "onVideoClosed", "true");
}

#pragma mark - CMPOpenScreenClickDelegate

- (BOOL)extraClickOpenScreen:(CMPOpenScreenModel *)info {
    return true;
}

#pragma mark - CMPResultCardClickDelegate

- (BOOL)extraClickResultCard:(CMPResultsPageModel *)info {
    return true;
}

#pragma mark - CMPSettingCardClickDelegate

- (BOOL)extraClickSettingCard:(CMPSettingPageModel *)info {
    return true;
}
#pragma mark - CMPFamilyGamesClickDelegate

- (BOOL)extraClickFamilyGames:(CMPFamilyGamesModel *)info{
    return true;
}
- (void)dealloc {
    [[NSNotificationCenter defaultCenter] removeObserver:self];
}
extern "C" bool CMPCanShowFamilyGamesRedDot() {
    [CMPDebugManager cmpLog:[NSString stringWithFormat:@"%s",__FUNCTION__]];
    NSInteger priority = [CMPPromotionHelper getFamilyGamesPriorityRedDot];
    [CMPDebugManager cmpLog:[NSString stringWithFormat:@"%ld",priority]];
    
    if (priority == 0) {
        if (![CMPPromotionHelper canShowSettingCardRedDot] && [CMPPromotionHelper canShowFamilyGameRedDot]) {
            return YES;
        }
        else {
            return NO;
        }
    }
    else if (priority == 1) {
        if ([CMPPromotionHelper canShowFamilyGameRedDot]) {
            return YES;
        }
        else {
            return NO;
        }
    }
    else {
        return NO;
    }
}
extern "C" bool CMPCanShowSettingPageRedDot() {
    [CMPDebugManager cmpLog:[NSString stringWithFormat:@"%s",__FUNCTION__]];
    NSInteger priority = [CMPPromotionHelper getFamilyGamesPriorityRedDot];
    [CMPDebugManager cmpLog:[NSString stringWithFormat:@"%ld",priority]];
    if (priority == 0) {
        if ([CMPPromotionHelper canShowSettingCardRedDot]) {
            return YES;
        }
        else {
            return NO;
        }
    }
    else if (priority == 1) {
        if (![CMPPromotionHelper canShowFamilyGameRedDot] && [CMPPromotionHelper canShowSettingCardRedDot]) {
            return YES;
        }
        else {
            return NO;
        }
    }
    else {
        return NO;
    }
}
@end
