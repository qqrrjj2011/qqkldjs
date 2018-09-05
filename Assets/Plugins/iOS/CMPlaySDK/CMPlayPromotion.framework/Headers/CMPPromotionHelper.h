//
//  CMPPromotionHelper.h
//  CMPlayPromotion
//
//  Created by wangyufeng on 16/8/31.
//  Copyright © 2016年 cmcm. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "CMPOpenScreenManager.h"
#import "CMPResultsPageManager.h"
#import "CMPSettingPageManager.h"
#import "CMPRewardedVideoManager.h"
#import "CMPFamilyGamesManager.h"
#import "CMPInsertPageManager.h"

#import "CMPActivityFeedbackManager.h"

#define CMPPROMOTIONS_NOTIFICATION_OBJC_NO_DATA   (@"cmppromotion_notification_objc_no_data")
/**
 * Refresh notification: resultcard data has changed, need to call canShowFamilyGames: again
 */
static NSString *const kCMPFamilyGamesRefreshNotify = @"CMPFamilyGamesRefreshNotify";

/**
 * Refresh notification: resultcard data has changed, need to call canShowResultCard: again
 */
static NSString *const kCMPResultCardRefreshNotify = @"CMPResultCardRefreshNotify";

/**
 * Refresh notification: settingcard data has changed, need to call canShowSettingCard: again
 */
static NSString *const kCMPSettingCardRefreshNotify = @"CMPSettingCardRefreshNotify";

/**
 * Promotion API
 */
@interface CMPPromotionHelper : NSObject

#pragma mark - RewardedVideo

/// Initialize the data of reward video
+ (void)initRewardedVideo;

/// determine whether there is any reward video
+ (BOOL)canShowRewardedVideo:(int)scene isClick:(BOOL)isClick;
/**
 determine whether can show hitTop RewardVideo
 
 @return result
 */
+ (BOOL)canShowHitTopRewardedVideo:(int)scene isClick:(BOOL)isClick;
/// show reward video
+ (void)showRewardedVideo:(UIViewController*)viewController scene:(int)scene;



/// show hitTop rewardVideo
+ (void)showHitTopRewardedVideo:(UIViewController*)viewController scene:(int)scene;

/**
 *  set callback of reward video
 *  @param delegate call this delegate when the reward video is closed
 */
+ (void)setRewardedVideoDelegate:(id<CMPRewardedVideoDelegate>)delegate;

#pragma mark - OpenScreen

/// Initialize the data of Opening Full Screen AD
+ (void)initOpenScreen;

/**
 *  determine whether can show the Opening Full Screen AD
 *  @param isNewUser determine whether he is a new user, if it is, based on the policy to choose Whether to show.
 *  @param scence triggering scenarios：1 is open game，2 is back to homepage
 */
+ (BOOL)canShowOpenScreen:(BOOL)isNewUser scene:(int)scene;

/// show the Opening Full Screen AD
+ (void)showOpenScreen:(UIViewController*)viewController;

/**
 *  set callback of click on Opening Full Screen AD
 *  @param delegate  call this delegate when the Click behavior is unable to handle in the SDK
 */
+ (void)setOpenScreenClickDelegate:(id<CMPOpenScreenClickDelegate>)delegate;


#pragma mark - InsertPage

/// Initialize the data of insert page AD
+ (void)initInsertPage;

/**
 *  determine whether can show the insert pageAD
 *  @param isNewUser determine whether he is a new user, if it is, based on the policy to choose Whether to show.
 */
+ (BOOL)canShowInsertPage:(BOOL)isNewUser;

/// show the insert page AD
+ (void)showInsertPage:(UIViewController*)viewController;

/**
 *  set callback of click on insert page AD
 *  @param delegate  call this delegate when the Click behavior is unable to handle in the SDK
 */
+ (void)setInsertPageClickDelegate:(id<CMPInsertPageClickDelegate>)delegate;

#pragma mark - ResultCard

/// Initialize the data of result page
+ (void)initResultCard;

/// determine whether can show the ad on result page
+ (BOOL)canShowResultCard;

/// access the date of ads, base on it to show
+ (NSString*)getResultCardData;

/**
 *  Click the ads，call this method to handle the Click behavior by SDK
 *  @param data access the data from getResultCardData，pass through it to SDK
 */
+ (void)clickResultCard:(NSString*)data;

/**
 *  set callback of click on result page
 *  @param delegate call this delegate when the Click behavior is unable to handle in the SDK
 */
+ (void)setResultCardClickDelegate:(id<CMPResultCardClickDelegate>)delegate;

#pragma mark - SettingCard

/// Initialize the data of setting page
+ (void)initSettingCard;

/// determine whether can show the ad on setting page
+ (BOOL)canShowSettingCard;

/// determine whether can show red Dot on setting page 
+ (BOOL)canShowSettingCardRedDot;

/// access the date of ads, base on it to show
+ (NSString*)getSettingCardData;

/// access the date of ads, base on it to show
+ (NSDictionary *)getSettingCardDataDictionary;

/**
 *  Click the ads，call this method to handle the Click behavior by SDK
 *  @param data access the data from getSettingCardData，passthrough it to SDK
 */
+ (void)clickSettingCard:(NSString*)data;

/**
 *  set callback of click on setting page
 *  @param delegate call this delegate when the Click behavior is unable to handle in the SDK
 */
+ (void)setSettingCardClickDelegate:(id<CMPSettingCardClickDelegate>)delegate;

#pragma mark - FamilyGames
/// Initialize the data of family games page
+ (void)initFamilyGames;

/// determine whether can show the ad on family games page
+ (BOOL)canShowFamilyGames;

/// determine whether can show red Dot on family games page
+ (BOOL)canShowFamilyGameRedDot;

/// access the date of ads, base on it to show
+ (NSString *)getFamilyGamesData;
// get priority of red dots
+ (NSInteger)getFamilyGamesPriorityRedDot;

/**
 *  Click the ads，call this method to handle the Click behavior by SDK
 *  @param data access the data from getFamilyGamesData，passthrough it to SDK
 */
+ (void)clickFamilyGames:(NSString *)data;

/**
 *  set callback of click on family games page
 *  @param delegate call this delegate when the Click behavior is unable to handle in the SDK
 */
+ (void)setFamilyGamesClickDelegate:(id<CMPFamilyGamesClickDelegate>)delegate;

#pragma mark - ActivityWebView
+ (void)setActivityFeedbackWebViewDelegate:(id<CMPActivityFeedbackManagerDelegate>)delegate;
+ (void)setShowCloseBtnShow:(BOOL)isSHow;
+ (void)loadActivityURL:(NSString *)url;
+ (void)loadFeedbackURL:(NSString *)url appID:(NSString *)appID;

@end
