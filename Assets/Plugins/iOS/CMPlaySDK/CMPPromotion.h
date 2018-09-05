//
//  CMPPromotion.h
//  CMPlaySDKSample
//
//  Created by wangyufeng on 16/9/7.
//  Copyright © 2016年 cmcm. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import <CMPlayPromotion/CMPlayPromotion.h>
typedef void (^CMPPromotionCallBack)();

@interface CMPPromotion : NSObject<CMPOpenScreenClickDelegate, CMPRewardedVideoDelegate, CMPResultCardClickDelegate, CMPSettingCardClickDelegate,CMPFamilyGamesClickDelegate>
@property CMPPromotionCallBack onVideoClosed;
@property CMPPromotionCallBack onSettingsPushUpdate;

+ (CMPPromotion*)sharedInstance;

/// Initializes all promotion-related, contains opening-screen, result page, setting page, rewarded video
- (void)initPromotion;

@end

#ifdef __cplusplus
/**
 * C interface
 */
extern "C" {
    
    /// family games red dot from setting page and priority_reddot
    bool CMPCanShowFamilyGamesRedDot();
    bool CMPCanShowSettingPageRedDot();
}
#endif
