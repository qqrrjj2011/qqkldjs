//
//  CMPResultsPageManager.h
//  ResultsPageDemo
//
//  Created by KepenJ on 16/6/6.
//  Copyright © 2016年 cmplay. All rights reserved.
//
//  If you have any questions about CMPlayBase.framework,please contact KepenJ(Email: kepenj@gmail.com )


#import <Foundation/Foundation.h>

@interface CMPResultsPageModel : NSObject
//**************这些是解析出来的数据**********
@property (nonatomic,assign) NSInteger pro_id;           //卡片id
@property (nonatomic,copy) NSString *start_time;        //开始时间 （格林时间：配置格式转化为unix时间戳）
@property (nonatomic,copy) NSString *end_time;          //结束时间 （格林时间：配置格式转化为unix时间戳,不配置表示一直生效）
@property (nonatomic,assign) NSInteger show_times;     //展示次数
@property (nonatomic,assign) NSInteger pro_type;        //推广类型 1: 纯推荐2:金币 3: 领钻石
@property (nonatomic,assign) NSInteger reward_counts;   //奖励数量，用于配合2，3
@property (nonatomic,assign) NSInteger pro_priority;    //用于多个app轮循时使用，同场景的卡片优先级，数字越大优先级越高。
@property (nonatomic,assign) NSInteger rotation_times;  //用于多个app轮循时使用，连续show出N次还没点击轮转下一个内容，若无多个，则默认填1
@property (nonatomic,assign) BOOL click_dis;            //点击后是否消失，然后显示下一个（主要考虑ios无白名单情况）
@property (nonatomic,copy) NSString *icon_url;         //icon链接
@property (nonatomic,copy) NSString *title;             //第一行描述文案
@property (nonatomic,copy) NSString *subtitle;          //第二行描述文案
@property (nonatomic,copy) NSString *pkg_name;          //如果没有填包名，则不判断是否已安装作为显示条件
@property (nonatomic,copy) NSString *jump_url;          //跳转url
@property (nonatomic,assign) NSInteger jump_type;       //跳转类型(第一期只做应用市场)
@property (nonatomic, assign) NSInteger day_limit;      // 每天展示的次数限制（只针对爆款游戏）
//**************这些是解析出来的数据**********

//**************这些是后添加用来编写逻辑或者存储信息的数据**********
// warning 如果要添加新的参数，从这里开始⬇️⬇️⬇️，不然会导致生成的 jsonString 解析失败
@property (nonatomic,retain) NSString * icon_image_path;       //icon image 的本地地址
@property (nonatomic,assign) NSInteger showedCount;         //展示了的次数
@property (nonatomic,assign) NSInteger jumpedCount;         //跳转了的次数
@property (nonatomic,assign) NSInteger cancelCount;         //取消了的次数
@property (nonatomic,assign) NSInteger turnCount;           //轮询次数
@property (nonatomic, assign) NSInteger fashionShowedCount;  // 爆款游戏展示的次数，每天都要清零
@property (nonatomic, assign) NSTimeInterval display_timestamp;//   显示的时间戳（现在只针对爆款）
//**************这些是后添加用来编写逻辑或者存储信息的数据**********
@end

@protocol CMPResultCardClickDelegate;
@interface CMPResultsPageManager : NSObject
/**
 *  当前ResultsPageModel
 */
@property (nonatomic,retain,readonly) CMPResultsPageModel * currentResultsPageModel;
/**
 *
 *  回调的代理
 */
@property (strong) id<CMPResultCardClickDelegate> delegate;
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
- (void)initResultPage;
/**
 *  判断是否可以展示结果页banner
 *
 *  @return YES/NO
 */
- (BOOL)canShowResultsPageScreen;
/**
 *  获取当前结果页所展示的数据
 *
 *  @return 该数据的 JSON 字符串(下面是格式！！！！)
 
 {
  "title":xx,
  "subtitle":xx,
  "image_url_file_path":xx,
  "currentIndex":xx,
 }
 
 */
- (NSString *)getJsonStringOfCurrentResultsPageModel;
/**
 *  相应相关的点击事件
 *
 *  @param parameter 数据JsonString (下面是格式！！！！)
 
 {
 "title":xx,
 "subtitle":xx,
 "image_url_file_path":xx,
 "currentIndex":xx,
 }
 */
- (void)clickActionWithParameter:(id)parameter;

/**
 *  infoc 埋点
 */
- (void)reportInfoc:(CMPResultsPageModel*)info action:(int)action remark:(NSString*)remark scenes:(int)scenes;
@end

@protocol CMPResultCardClickDelegate <NSObject>

@optional
/**
 *  SDK内不能处理的点击行为，会回调此Delegate执行
 */
- (BOOL)extraClickResultCard:(CMPResultsPageModel *)info;
- (void)reportResultCardOther:(CMPResultsPageModel*)info action:(int)action remark:(NSString*)remark scenes:(int)scenes sdkVersion:(int)sdkVersion totalInfo:(NSMutableDictionary *)totalInfo;
@end
