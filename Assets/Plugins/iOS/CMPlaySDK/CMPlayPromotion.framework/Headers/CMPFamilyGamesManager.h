//
//  CMPMoreGameManager.h
//  CMPlayPromotion
//
//  Created by KepenJ on 17/2/8.
//  Copyright © 2017年 cmcm. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface CMPFamilyGamesModel:NSObject
//**************这些是解析出来的数据**********
@property (nonatomic,assign) NSInteger pro_id;          //卡片id
@property (nonatomic,copy) NSString *pkg_name;          //如果没有填包名，则不判断是否已安装作为显示条件
@property (nonatomic,assign) NSInteger pro_priority;        //用于多个app轮循时使用，同场景的卡片优先级，数字越大优先级越高。
@property (nonatomic,copy) NSString *icon_url;         //icon链接
@property (nonatomic,copy) NSString *title;             //第一行描述文案
@property (nonatomic,copy) NSString *subtitle;           //文案
@property (nonatomic,copy) NSString *button_txt;        //未安装按钮文案
@property (nonatomic,copy) NSString *button_txt2;       //已安装按钮文案
@property (nonatomic,copy) NSString *jump_url;               //跳转url
@property (nonatomic,assign) NSInteger jump_type;       //跳转类型（1、gp，）
@property (nonatomic,assign) NSInteger priority_reddot;       //用来区分与设置页红点优先级（0、低于设置页，1、优先设置页）
//**************这些是解析出来的数据**********

//**************这些是后添加用来编写逻辑或者存储信息的数据**********
// warning 如果要添加新的参数，从这里开始⬇️⬇️⬇️，不然会导致生成的 jsonString 解析失败
@property (nonatomic,copy) NSString * icon_image_path;       //icon image 的本地地址
@property (nonatomic,assign) NSInteger showedCount;         //展示了的次数
@property (nonatomic,assign) NSInteger jumpedCount;         //跳转了的次数
@property (nonatomic,copy) NSString* show_card_red_dot;         //跳转了的次数

//**************这些是后添加用来编写逻辑或者存储信息的数据**********
@end

@protocol CMPFamilyGamesClickDelegate;
@interface CMPFamilyGamesManager : NSObject
/**
 *  用来区分与设置页红点优先级（0、低于设置页，1、优先设置页）
 **/
@property (nonatomic,assign,readonly) NSInteger priorityReddot;
/**
 *
 *  回调的代理
 */
@property (strong) id<CMPFamilyGamesClickDelegate> delegate;
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
 *  更新开屏所需要的数据
 *
 *  @return YES/NO
 */
- (void)initFamilyGames;
/**
 *  判断是否可以展示家族页按钮红点
 *
 *  @return YES/NO
 */
- (BOOL)canShowFamilyGamesRedDot;
/**
 *  判断是否可以展示家族页
 *
 *  @return YES/NO
 */
- (BOOL)canShowFamilyGamesPage;
/**
 *  获取当前结果页所展示的数据
 *
 *  @return 该数据的 JSON
 */
- (NSString *)getJsonStringOfCurrentFamilyGames;
/**
 *  相应相关的点击事件
 *
 *  @param parameter 数据JsonString
 */
- (void)clickActionWithParameter:(id)parameter;
/**
 *  infoc 埋点
 */
- (void)reportInfoc:(CMPFamilyGamesModel *)info action:(int)action remark:(NSString *)remark scenes:(int)scenes;
@end

@protocol CMPFamilyGamesClickDelegate <NSObject>
@optional
/**
 *  SDK内不能处理的点击行为，会回调此Delegate执行
 */
- (BOOL)extraClickFamilyGames:(CMPFamilyGamesModel *)info;
- (void)reportFamilyCardOther:(CMPFamilyGamesModel *)info action:(int)action remark:(NSString *)remark scenes:(int)scenes sdkVersion:(int)sdkVersion totalInfo:(NSMutableDictionary *)totalInfo;
@end
