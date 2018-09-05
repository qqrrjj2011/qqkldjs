//
//  CMPBCommonHelper.h
//  CMPlayBase
//
//  Created by KepenJ on 16/5/5.
//  Copyright © 2016年 cmplay. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import "CMPPageViewModel.h"

#define CMPGetNSDictionaryParam(object) ([object isKindOfClass:[NSNull class]]) ? nil : object
typedef void (^IsLoadingBlock)(BOOL isLoading);
@interface CMPCommonHelper : NSObject
/**
 *  获取广告标识符
 *
 *  @return 广告用标识
 */
+ (NSString*)getIdfa;

/**
 *  获取时区
 *
 *  @return 时区 Number
 */
+ (NSNumber*)getTimeZone;

/**
 *  获取语言
 *
 *  @return 当前系统语言
 */
+ (NSString*)getSystemLanguage;

/**
 *  获取国家码
 *
 *  @return 国家编码
 */
+ (NSString*)getCountryCode;

/**
 * 获取分辨率
 * @param separator 分辨率分割符
 */
+ (NSString*)getResolution:(NSString*)separator;

/**
 *  获取国家
 *
 *  @return 国家
 */
+ (NSString*)getCountry;

/**
 *  获取版本号（构建版本）
 *
 *  @return 版本号
 */
+ (NSString*)getAppVersion;

/**
 *  获取版本名
 *
 *  @return 版本名
 */
+ (NSString*)getAppVersionName;

/**
 *  获取手机型号
 *
 *  @return 手机型号
 */
+ (NSString*)getDeviceModel;

/**
 *  获取手机名称
 *
 *  @return 手机名称
 */
+ (NSString *)getDeviceName;

/**
 *  获取手机唯一标识(如果IDFA不能明文传输，则可使用此接口，此接口返回MD5后IDFA)
 *
 *  @return 手机唯一标识
 */
+ (NSString *)getDeviceId;

/**
 * Base64编码
 * @param data 需要加密的数据
 */
+ (NSString*)getBase64Encode:(NSString*)data;

/**
 * 是否可打开App间通信的URL（一般用于判断是否已安装应用）
 * @param url 目标App内定义的URL，此参值需要字义的Info.plist的白名单里才能正常使用此接口
 */
+ (BOOL)canOpenAppUrl:(NSString*)url;

/**
 * 打开App（用于与其他App通信）
 * @param url 目标App内能接收的URL
 */
+ (BOOL)openAppUrl:(NSString*)url isLoading:(IsLoadingBlock)isLoading;

/**
 *  获取系统版本
 *
 *  @return 系统版本
 */
+ (NSString*)getOSVersion;

/**
 *  是否开启通知栏
 *
 *  @return 0、未开启推送（默认值） 1、已开启推送
 */
+ (int)isAllowedNotification;

/**
 *  获取手机具体型号
 *
 *  @return 手机型号
 */
+ (NSString*)getDevicePlatform;

/**
 *  获取手机总内存(单位：M)
 *
 *  @return 内存大小
 */
+ (unsigned long long)getPhysicalMemory;

/**
 *  获取加密后的IDFA
 *
 *  @return 加密后的IDFA
 */
+ (NSString*)getMuid;

/**
 *  字符串转时间
 *
 *  @param string 字符串
 *
 *  @return 时间
 */
+ (NSDate *)stringToDate:(NSString *)string;

/**
 *  时间转字符串
 *
 *  @param date 时间
 *
 *  @return 字符串
 */
+ (NSString *)dateToString:(NSDate *)date;

/**
 *  时间转字符串
 *
 *  @param date 时间
 *  @param formatter 格式化类型
 *
 *  @return 字符串
 */
+ (NSString *)dateToString:(NSDate *)date dateFormatter:(NSString *)formatter;

/**
 *  比较比较两个时间
 *
 *  @param endTime 时间1
 *  @param currentDate 时间2
 *  @return YES/NO
 */
+ (BOOL)containstartTime:(NSDate*)startTime endTime:(NSDate *)endTime currentTime:(NSDate *)currentDate;

/**
 当前时间是否为今天

 @param date 当前时间
 @return YES/NO
 */
+ (BOOL)isTodayWithDate:(NSDate *)date;

/**
 *  字符串MD5加密
 *
 *  @param str 输入
 *
 *  @return 输出
 */
+ (NSString *)md5:(NSString *)str;

/**
 *  拼接上报数据
 *  @param data 存放拼接数据
 *  @param key 字段
 *  @param value 字段值
 */
+ (void)appendReportData:(NSMutableString*)data key:(NSString*)key value:(id<NSObject>)value;

/**
 *  拼接int类型的上报数据
 *  @param data 存放拼接数据
 *  @param key 字段
 *  @param value 字段值
 */
+ (void)appendReportIntData:(NSMutableString*)data key:(NSString*)key value:(int)value;

/**
 *  获取Bundle中的资源文件
 *
 *  @param assetName 文件名
 *
 *  @return 文件地址string
 */
+ (NSString *)getBundlePath:(NSString *) assetName;

/**
 *  获取设备总存储量
 *
 *  @return 存储量
 */
+ (float)getTotalDiskSpace;

/**
 *  获取设备可用空间
 *
 *  @return 可用空间量
 */
+ (float)getFreeDiskSpace;
/**
 *  获取应用开发商标识符
 *
 *  @return 应用开发商标识
 */
+ (NSString *)getIdfv;
/**
 *  获取设备唯一标识符
 *
 *  @return 唯一标识符
 */
+ (NSString *)getDeviceIdentifier;
/**
 *  获取当前网络状态
 *
 *  @return 状态标识符
 *
 *  NotReachable = 0,
 *  ReachableViaWiFi = 1,
 *  ReachableViaWWAN = 2,
 *  ReachableVia2G = 3,
 *  ReachableVia3G = 4,
 *  ReachableVia4G = 5
 */
+ (int)getCurrentNetStatue;


/**
 压缩文件

 @param path 压缩后的文件路径
 @param paths 被压缩文件的数组集合
 @return 压缩结果
 */
+ (BOOL)zipFileAtPath:(NSString *)path withFilesAtPaths:(NSArray *)paths;

/**
 解压缩文件

 @param path 被解压文件路径
 @param filePath 解压到某个文件路径
 @return 解压结果
 */
+ (BOOL)unZipFileAtPath:(NSString *)path toFilePath:(NSString *)filePath;

+ (BOOL)changeGameLanguageWith:(NSString *)language;

+ (NSString*)getGameLanguage;

+ (void)iPhoneShake;

+ (NSString *)getMacString;

+ (NSString *)getMNCCode;

+ (NSString *)getBundleID;

+ (NSString *)getHardParam;

+ (NSString *)pathOfDocumentDirectory;

+ (NSString *)pathOfResource;

+ (NSString *)isDirectoryExistInternal:(NSString *)file directory:(NSString *)directory;

+ (BOOL)isFileExsit:(NSString *)filePath;

+ (NSString *)getSDKVersion;

+ (int)getIntValueFromUserDefault:(NSString *)key;

+ (UIViewController *)getCurrentVC;

+ (UIViewController *)getCurrentVCFrom:(UIViewController *)rootVC;

+ (NSString *)getInfocType:(NSString *)bundleID;

+ (NSString *)getCurrentTime;

+ (void)presentLoadingView:(CGFloat)viewWidth red:(CGFloat)red green:(CGFloat)green blue:(CGFloat)blue alpha:(CGFloat)alpha;

+ (void)dismissLoadingView;
+ (BOOL)presentAPPReview;

+ (void)setAppsflyerDeviceId:(NSString *)appsflyerDeviceId;
+ (NSString *)getAppsflyerDeviceId;

#pragma mark - GDPR
/**
 *  show a GDPR view
 *
 *  @param  buttonClickBlock    whether the current user agrees with the user privacy agreement
 */
+ (void)showGDPRViewWithButtonClickBlock:(void (^)(BOOL isAgree))buttonClickBlock;

/**
 *  判断是否同意GDPR应用隐私条款更新, 首次调用返回NO
 *
 *  @return        YES为同意，NO为不同意
 */
+ (BOOL)isAgreeGDPRPrivacyPolicy;

/**
 *  判断是否同意GDPR个性化广告推荐, 首次调用返回NO
 *
 *  @return        YES为同意，NO为不同意
 */
+ (BOOL)isAgreeGDPRAdStayInformed;

/**
 *  设置是否同意GDPR个性化广告推荐
 *
 *  @param isAgree       YES为同意，NO为不同意
 */
+ (void)saveGDPRAgreedAdStayInformed:(BOOL)isAgree;

/**
 *  设置是否同意GDPR应用隐私条款更新
 *
 *  @param isAgree        YES为同意，NO为不同意
 */
+ (void)saveGDPRAgreedPrivacyPolicy:(BOOL)isAgree;

/**
 check Is GDPR Enforced Country

 @return result
 */
+ (BOOL)checkIsGDPREnforcedCountry;

/**
 get localized string

 @param key localized String's key
 @return value
 */
+ (NSString *)localizedStringWithKey:(NSString *)key;

/**
 get localized String

 @param bundleName bundle Name(the bundle's suffix name
 should be .bundle)
 @param tableName table Name(the table file's suffix name should be .strings)
 @param key localized String's key
 @param comment error comment
 @return value
 */
+ (NSString *)localizedStringFromBundle:(NSString *)bundleName tableName:(NSString *)tableName key:(NSString *)key comment:(NSString *)comment;

/**
 get localized String

 @param tableName tableName
 @param key localized String's key
 @param comment error comment
 @return value
 */
+ (NSString *)localizedStringFromTable:(NSString *)tableName key:(NSString *)key comment:(NSString *)comment;

/**
 stop tracking user information
 */
+ (void)stopTracking;

/**
 is stop tracking user information?
 
 @return result
 */
+ (BOOL)getIsStopTracking;

+ (NSString *)getLocaleCountryCode;

+ (NSString *)getLocaleLanguageCode;

@end
