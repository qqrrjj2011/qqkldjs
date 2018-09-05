//
//  CMPRewardedVideoManager.h
//  Piano
//
//  Created by wangyufeng on 16/7/11.
//
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>


@protocol CMPRewardedVideoDelegate;
@interface CMPRewardedVideoInfo : NSObject

@property (nonatomic, copy) NSString *video_url;
@property (nonatomic, copy) NSString *pro_id;
@property (nonatomic, assign) NSInteger pro_priority;
@property (nonatomic, copy) NSDate *start_time;
@property (nonatomic, copy) NSDate *end_time;
@property (nonatomic, assign) NSInteger show_times;
@property (nonatomic, assign) NSInteger rotation_times;
@property (nonatomic, copy) NSString *bg_img;
@property (nonatomic, copy) NSString *icon_url;
@property (nonatomic, copy) NSString *title;
@property (nonatomic, copy) NSString *subtitle;
@property (nonatomic, assign) NSNumber *comment_star;
@property (nonatomic, copy) NSString *downloads;
@property (nonatomic, copy) NSString *pkg_name;
@property (nonatomic, copy) NSString *jump_url;
@property (nonatomic, assign) NSInteger jump_type;
@property (nonatomic, assign) NSInteger video_width;
@property (nonatomic, assign) NSInteger video_height;
@property (nonatomic, assign) NSInteger display_type;
// 每天展示的次数限制（只针对爆款游戏）
@property (nonatomic, assign) NSInteger day_limit;

// 已经展示过的次数（用于和轮播次数rotation_times做比较）
@property (nonatomic, assign) int displayedCount;
// 已经展示过的总次数（用于和显示次数show_times做比较）
@property (nonatomic, assign) int displayedTotalCount;
// video_url本地路径
@property (nonatomic, copy) NSString *videoPath;
// bg_img本地路径
@property (nonatomic, copy) NSString *bgImgPath;
// icon_url本地路径
@property (nonatomic, copy) NSString *iconPath;
// 是否是本地视频
@property (nonatomic, assign) BOOL isLocalVideo;
// 爆款游戏展示的次数，每天都要清零
@property (nonatomic, assign) int fashionDisplayCount;
// 视频显示的时间戳（现在只针对爆款）
@property (nonatomic, assign) NSTimeInterval display_timestamp;

@end

@interface CMPRewardedVideoManager : NSObject

@property (strong) id<CMPRewardedVideoDelegate> delegate;
@property (nonatomic,assign) NSInteger sectionNum;


//创建单例
+ (CMPRewardedVideoManager*) sharedInstance;

//初始化单例
- (void)initRewardedVideo;

//判断是否可展现
- (BOOL)canShow:(int)scene isClick:(BOOL)isClick;
- (BOOL)canShowHitTopRewardedVideo:(int)scene isClick:(BOOL)isClick;
//展现
- (BOOL)show:(UIViewController*)viewController scene:(int)scene;

//上报埋点
- (void)reportInfoc:(CMPRewardedVideoInfo*)info action:(int)action remark:(NSString*)remark scenes:(int)scenes;
@end

@protocol CMPRewardedVideoDelegate <NSObject>

@optional
- (void)rewardedVideoShow;
- (void)rewardedVideoClick;
- (void)rewardedVideoHide;
- (void)reportRewardVideoOther:(CMPRewardedVideoInfo*)info action:(int)action remark:(NSString*)remark scenes:(int)scenes sdkVersion:(int)sdkVersion totalInfo:(NSMutableDictionary *)totalInfo;
@end
