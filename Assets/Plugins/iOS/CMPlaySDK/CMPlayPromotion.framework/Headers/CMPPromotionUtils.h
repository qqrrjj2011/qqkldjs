//
//  CMPPromotionUtils.h
//  CMPlayPromotion
//
//  Created by wangyufeng on 16/9/6.
//  Copyright © 2016年 cmcm. All rights reserved.
//

#ifndef CMPPromotionUtils_h
#define CMPPromotionUtils_h

#import <stdio.h>
#import <string>
#import <vector>

/**
 * Promotion API (C++)
 */
class CMPPromotionUtils {
    
public:
    
    /// Initialize the data of reward video
    static void initRewardedVideo();
    
    /// determine whether there is any reward video
    static bool canShowRewardedVideo(int scene,bool isClick);
    
    /// determine whether can show hitTop RewardVideo
    static bool canShowHitTopRewardedVideo(int scene,bool isClick);
    
    /// show reward video
    static void showRewardedVideo(int scene);
    
    /// show hitTop reward video
    static void showHitTopRewardedVideo(int scene);
    
    /// Initialize the data of Opening Full Screen AD
    static void initOpenScreen();
    
    /**
     *  determine whether can show the Opening Full Screen AD
     *  @param isNewUser determine whether he is a new user, if it is, based on the policy to choose Whether to show.
     *  @param scence triggering scenarios：1 is open game，2 is back to homepage
     */
    static bool canShowOpenScreen(bool isNewUser, int scene);
    
    /// show the Opening Full Screen AD
    static void showOpenScreen();
    
    static void initInsertPage();
    
    static bool canShowInsertPage(bool isNewUser);
    
    static void showInsertPage();
    
    /// Initialize the data of result page
    static void initResultCard();
    
    /// determine whether can show the ad on result page
    static bool canShowResultCard();
    
    /// access the date of ads, base on it to show
    static std::string getResultCardData();
    
    /**
     *  Click the ads，call this method to handle the Click behavior by SDK
     *  @param data access the data from getResultCardData，pass through it to SDK
     */
    static void clickResultCard(const char* data);
    
    /// Initialize the data of setting page
    static void initSettingCard();
    
    /// determine whether can show the ad on setting page
    static bool canShowSettingCard();
    
    /// determine whether can show red Dot on setting page
    static bool canShowSettingCardRedDot();
    
    /// access the date of ads, base on it to show
    static std::string getSettingCardData();
    
    /**
     *  Click the ads，call this method to handle the Click behavior by SDK
     *  @param data access the data from getResultCardData，passthrough it to SDK
     */
    static void clickSettingCard(const char* data);
    
    
    //family games
    /// Initialize the data of family games page
    static void initFamilyGames();
    
    /// determine whether can show the ad on family games page
    static bool canShowFamilyGames();
    
    /// determine whether can show red Dot on family games page
    static bool canShowFamilyGamesAllRedDot();
    
    /// access the date of ads, base on it to show
    static std::string getFamilyGamesData();
    /**
     *  Click the ads，call this method to handle the Click behavior by SDK
     *  @param data access the data from CMPGetFamilyGamesData，passthrough it to SDK
     */
    static void clickFamilyGames(const char* data);
    
    static void needShowActivityVCCloseBtn(bool canShow);
    static void loadActivity(const char *url);
    static void loadFeedback(const char *url,const char* appID);

};

#endif /* CMPPromotionUtils_h */

#ifdef __cplusplus

/**
 * Promotion API (C)
 */
extern "C" {
    
    /// Initialize the data of reward video
    void CMPInitRewardedVideo();
    
    /// determine whether there is any reward video
    bool CMPCanShowRewardedVideo(int scene,bool isClick);
    
    /// show reward video
    void CMPShowRewardedVideo(int scene);
    
    /// determine whether can show hitTop RewardVideo
    bool CMPCanShowHitTopRewardedVideo(int scene,bool isClick);
    
    /// show hitTop reward video
    void CMPShowHitTopRewardedVideo(int scene);
    
    /// Initialize the data of Opening Full Screen AD
    void CMPInitOpenScreen();
    
    /**
     *  determine whether can show the Opening Full Screen AD
     *  @param isNewUser determine whether he is a new user, if it is, based on the policy to choose Whether to show.
     *  @param scence triggering scenarios：1 is open game，2 is back to homepage
     */
    bool CMPCanShowOpenScreen(bool isNewUser, int scene);
    
    /// show the Opening Full Screen AD
    void CMPShowOpenScreen();
    
    void CMPInitInsertPage();

    bool CMPCanShowInsertPage(bool isNewUser);
    
    void CMPShowInsertPage();
    
    /// Initialize the data of result page
    void CMPInitResultCard();
    
    /// determine whether can show the ad on result page
    bool CMPCanShowResultCard();
    
    /// access the date of ads, base on it to show
    const char* CMPGetResultCardData();
    
    /**
     *  Click the ads，call this method to handle the Click behavior by SDK
     *  @param data access the data from CMPGetResultCardData，pass through it to SDK
     */
    void CMPClickResultCard(const char* data);
    
    /// Initialize the data of setting page
    void CMPInitSettingCard();
    
    /// determine whether can show the ad on setting page
    bool CMPCanShowSettingCard();
    
    /// determine whether can show red Dot on setting page
    bool CMPCanShowSettingCardRedDot();
    
    /// access the date of ads, base on it to show
    const char* CMPGetSettingCardData();
    
    /**
     *  Click the ads，call this method to handle the Click behavior by SDK
     *  @param data access the data from CMPGetSettingCardData，passthrough it to SDK
     */
    void CMPClickSettingCard(const char* data);
    
    
    //family games
    /// Initialize the data of family games page
    void CMPInitFamilyGames();
    
    /// determine whether can show the ad on family games page
    bool CMPCanShowFamilyGames();
    
    /// determine whether can show red Dot on family games page
    bool CMPCanShowFamilyGamesAllRedDot();
    
    /// access the date of ads, base on it to show
    const char* CMPGetFamilyGamesData();
    /**
     *  Click the ads，call this method to handle the Click behavior by SDK
     *  @param data access the data from CMPGetFamilyGamesData，passthrough it to SDK
     */
    void CMPClickFamilyGames(const char* data);
    
    void CMPNeedShowActivityVCCloseBtn(bool canShow);
    void CMPLoadActivity(const char *url);
    void CMPLoadFeedback(const char *url,const char* appID);
    
}

#endif

