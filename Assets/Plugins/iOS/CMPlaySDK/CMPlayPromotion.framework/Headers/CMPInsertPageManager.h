//
//  CMPInsertPageManager.h
//  CMPlayPromotion
//
//  Created by KepenJ on 17/4/24.
//  Copyright © 2017年 cmcm. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>


@interface CMPInsertPageModel : NSObject
//**************这些是解析出来的数据**********
@property (nonatomic,copy) NSString *pro_name;//      推广游戏名
@property (nonatomic,assign) NSInteger pro_id;//         推广app的id，推广时保证每次递增
@property (nonatomic,assign) NSInteger pro_priority;//     section内部app优先级，数字越大优先级越大
@property (nonatomic,copy) NSString *start_time;//       开始时间 （格林时间）
@property (nonatomic,copy) NSString *end_time;//        结束时间 （格林时间）
@property (nonatomic,assign) NSInteger show_times;//     展示总次数
@property (nonatomic,copy) NSString *interval_time;//    间隔时间（小时）
@property (nonatomic,assign) NSInteger rotation_times;//   轮循次数
@property (nonatomic,assign) BOOL click_dis;//        点击后是否消失
@property (nonatomic,assign) BOOL new_player;           //0新用户不展现 1新用户展现
@property (nonatomic,copy) NSString *bg_img;//         背景图片
@property (nonatomic,copy) NSString *button_img;//      按钮图片
@property (nonatomic,copy) NSString *pkg_name;//       如果没有填包名，则不判断是否已安装作为显示条件
@property (nonatomic,copy) NSString *jump_url;//         跳转url
@property (nonatomic,assign) NSInteger jump_type;//       跳转类型(不要配置999的跳转！！！！)
@property (nonatomic,assign) CGFloat percentage;//       appBtn距顶部距离百分比
@property (nonatomic,assign) NSInteger display_type;//       appBtn距顶部距离百分比
@property (nonatomic,assign) NSInteger show_by_startup;//    全局展示次數
@property (nonatomic, assign) NSInteger day_limit;//每天展示的次数限制（只针对爆款游戏）
//**************这些是解析出来的数据**********

//**************这些是后添加用来编写逻辑或者存储信息的数据**********
@property (nonatomic,retain) NSString * button_img_file_path;//       背景图片本地路径
@property (nonatomic,retain) NSString * bg_img_file_path;//       按钮图片本地路径
@property (nonatomic,assign) NSInteger showedCount;//       展示了的次数
@property (nonatomic,assign) NSInteger jumpedCount;//       跳转了的次数
@property (nonatomic,assign) NSInteger cancelCount;//       取消了的次数
@property (nonatomic,assign) NSInteger turnCount;//       轮询次数
@property (nonatomic, assign) NSInteger fashionShowedCount;  // 爆款游戏展示的次数，每天都要清零
@property (nonatomic, assign) NSTimeInterval display_timestamp;//   显示的时间戳（现在只针对爆款）
//**************这些是后添加用来编写逻辑或者存储信息的数据**********
@end

@protocol CMPInsertPageClickDelegate;

@interface CMPInsertPageManager : NSObject
/**
 *  当前openModel
 */
@property (nonatomic,retain,readonly) CMPInsertPageModel * currentInsertPageModel;
/**
 *
 *  回调的代理
 */
@property (strong) id<CMPInsertPageClickDelegate> delegate;
@property (nonatomic,assign) NSInteger sectionNum;

/**
 *  初始化
 *
 *  @param delegate 接收回调的对象
 *
 *  @return 单例对象
 */
+ (instancetype)sharedInstance;
/**
 *  初始化开屏数据
 *
 *  @return YES/NO
 */
- (BOOL)initInserPageModel;
/**
 *  判断是否可以开屏
 *
 *  @return YES/NO
 */
- (BOOL)canShowInsertPage:(BOOL)isNewest;
/**
 *  展示 iOS 源生开平
 *
 *  @param parentView 父视图
 */
- (void)showOnParentViewController:(UIViewController *)paresentVC;
/**
 *  infoc 埋点
 */
- (void)reportInfoc:(CMPInsertPageModel*)info action:(int)action remark:(NSString*)remark scenes:(int)scenes;
/**
 *  放弃之前的代理模式处理这个问题
 **/
- (void)handleInsertPageShow:(CMPInsertPageModel *)model;

- (void)handleInsertPageClick:(CMPInsertPageModel *)model;

- (void)handleInsertPageClose:(CMPInsertPageModel *)model;
@end

@protocol CMPInsertPageClickDelegate <NSObject>
@optional

/**
 *  SDK内不能处理的点击行为，会回调此Delegate执行
 */
- (BOOL)extraClickInsertPage:(CMPInsertPageModel *)info;
- (void)reportInsertPageOther:(CMPInsertPageModel*)info action:(int)action remark:(NSString*)remark scenes:(int)scenes sdkVersion:(int)sdkVersion totalInfo:(NSMutableDictionary *)totalInfo;
@end
