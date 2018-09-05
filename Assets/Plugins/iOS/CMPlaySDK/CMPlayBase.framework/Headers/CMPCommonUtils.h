//
//  CMPCommonUtils.h
//  CMPlayBase
//
//  Created by wangyufeng on 16/9/9.
//  Copyright © 2016年 cmcm. All rights reserved.
//


#ifndef CMPCommonUtils_h
#define CMPCommonUtils_h

#include <stdio.h>
#include <string>

/**
 *  CMPCommondUtils C/C++函数统一回调方法类型
 *
 *  @param  num          动态参数类型, 通过该参数遍历返回值个数
 *  @param  ...          动态参数, 类型均为'char *', 接入需确认该参数不会在SDK调用前被释放
 *
 *  接入方需定义一个回调函数，并将函数指针传入该类型参数, 并确保传入的函数指针不会在SDK调用前释放.
 *  ex:
 *
 *  // 调用SDK方法，并传入函数指针等参数
 *  // 以下为C调用方式，C++调用只需要定义一个类，并添加命名空间即可
 *  static void(*callBackMethodPointer)(int, ...) = &CMPCallBackMethod;
 *  CMPCommonUtils::showGDPRViewWithCallBackMethod(callBackMethodPointer);
 *
 *  void CMPCallBackMethod(int num, ...){
 *      // 接入方处理回调方法
 *      va_list ap;
 *      va_start(ap, num);
 *      char *value;
 *      for (int index = 0; index < num; index++) {
 *          // 回调值，接入方可在此处处理逻辑
 *          value = va_arg(ap, char *);
 *      }
 *      va_end(ap);
 *  }
 *
 */

typedef void  (*CMPCommondCallBackMethod)(int num, ...);

class CMPCommonUtils {
    
public:
    
    
    /**
     *  获取广告标识符
     *
     *  @return 广告用标识
     */
    static std::string getIdfa();
    
    /**
     *  获取应用开发商标识符
     *
     *  @return 应用开发商标识
     */
    static std::string getIdfv();
    
    /**
     *  获取时区
     *
     *  @return 时区 Number
     */
    static int getTimeZone();
    
    /**
     *  获取语言
     *
     *  @return 当前系统语言
     */
    static std::string getSystemLanguage();
    
    /**
     *  获取国家码
     *
     *  @return 国家编码
     */
    static std::string getCountryCode();
    
    /**
     * 获取分辨率
     * @param separator 分辨率分割符
     */
    static std::string getResolution(std::string separator);
    
    /**
     *  获取国家
     *
     *  @return 国家
     */
    static std::string getCountry();
    
    /**
     *  获取版本号（构建版本）
     *
     *  @return 版本号
     */
    static std::string getAppVersion();
    
    /**
     *  获取版本名
     *
     *  @return 版本名
     */
    static std::string getAppVersionName();
    
    /**
     获取整形版本名

     @return 版本名的整数值，不带小数点
     */
    static std::string getAppShortVersionName();
    /**
     *  获取手机型号
     *
     *  @return 手机型号
     */
    static std::string getDeviceModel();
    
    /**
     *  获取手机名称
     *
     *  @return 手机名称
     */
    static std::string getDeviceName();
    
    /**
     * 是否可打开App间通信的URL（一般用于判断是否已安装应用）
     * @param url 目标App内定义的URL，此参值需要字义的Info.plist的白名单里才能正常使用此接口
     */
    static bool canOpenAppUrl(std::string url);
    
    /**
     * 打开App（用于与其他App通信）
     * @param url 目标App内能接收的URL
     */
    static bool openAppUrl(std::string url);
    
    /**
     *  获取系统版本名，如：8.1
     *
     *  @return 系统版本
     */
    static std::string getOSVersion();
    
    /**
     *  是否开启通知栏
     *
     *  @return 0、未开启推送（默认值） 1、已开启推送
     */
    static bool isAllowedNotification();
    
    /**
     *  获取手机具体型号
     *
     *  @return 手机型号
     */
    static std::string getDevicePlatform();
    
    /**
     *  获取手机总内存(单位：M)
     *
     *  @return 内存大小
     */
    static unsigned long long getPhysicalMemory();
    
    /**
     *  获取设备总存储量
     *
     *  @return 存储量
     */
    static unsigned long long getTotalDiskSpace();
    
    /**
     *  获取设备可用空间
     *
     *  @return 可用空间量
     */
    static long getFreeDiskSpace();
    /**
     *  获取设备唯一标识符
     *
     *  @return 唯一标识符
     */
    static std::string getDeviceIdentifier();
    /**
     *  获取当前网络状态
     *
     *  @return 网络状态码
     */
    static int getCurrentNetStatue();
    
    /**
     压缩文件成zip文件
     
     @param zipPath zip文件路径
     @param filePath 需要压缩的文件路径
     */
    static bool zipFile(const char *zipPath,const char *filePath);
    
    
    /**
     解压文件
     
     @param zipPath zip文件路径
     @param filePath 解压后的文件路径
     @return 解压结果
     */
    static bool unZipFile(const char *zipPath,const char *filePath);
    
    //获取 网络编码
    static std::string getMNCCode();
    
    static std::string getBundleID();
    
    // 1:x86   2:others
    static std::string getCPUType();
    
    static std::string pathOfDocumentDirectory();
    
    static std::string pathOfResource();

    static std::string isDirectoryExistInternal (const char * file,const char * directory);
    
    static bool isFileExsit(const char *filePath);
    
    static std::string getSDKVersion();
    
    static int getIntValueFromUserDefault(const char*key);
    
    static std::string getInfocType(const char * bundleID);
    
    static std::string getCurrentTime();
    
    static void showLoadingView(float red, float green, float blue, float alpha);
    
    static void dismissLoadingView();
    
    static bool presentAPPReview();
    /**
     设置appsflyer的设备id
     
     @param appsflyerDeviceId 设备id
     */
    static void setAppsflyerDeviceId(const char * appsflyerDeviceId);
    
    /**
     获取appsflyer设备id
     
     @return 设备id
     */
    static std::string getAppsflyerDeviceId();
    
    /**
     *  show a custom rich text alert view
     *
     *  @param  callBackMethod      call back method pointer
     */
    static void showGDPRViewWithCallBackMethod(CMPCommondCallBackMethod callBackMethod);
    
    /**
     *  判断是否同意GDPR应用隐私条款更新, 首次调用返回false
     *
     *  @return         ture为同意，false为不同意
     */
    static bool checkIfGDPRAgreedPolicyUpdates();
    
    /**
     *  判断是否同意GDPR个性化广告推荐, 首次调用返回false
     *
     *  @return         ture为同意，false为不同意
     */
    static bool checkIfGDPRAgreedAdStayInformed();
    
    /**
     *  设置是否同意GDPR个性化广告推荐
     *
     *  @param isAgreed        ture为同意，false为不同意
     */
    static void setGDPRAgreedAdStayInformed(bool isAgreed);
    
    /**
     stop tracking user information
     */
    static void stopTracking();
    
    /**
     is stop tracking user information

     @return result
     */
    static bool isStopTracking();
    
    /**
     check is=s GDPR Enforced Country?

     @return result
     */
    static bool checkIsGDPREnforcedCountry();
};

#endif /* CMPCommonUtils_h */


#ifdef __cplusplus

/**
 * 提供给C的魔方接口
 */
extern "C" {
        
    /**
     *  获取广告标识符
     *
     *  @return 广告用标识
     */
    const char* CMPGetIdfa();
    /**
     *  获取应用开发商标识符
     *
     *  @return 应用开发商标识
     */
    const char* CMPGetIdfv();
    /**
     *  获取时区
     *
     *  @return 时区 Number
     */
    int CMPGetTimeZone();
    
    /**
     *  获取语言
     *
     *  @return 当前系统语言
     */
    const char* CMPGetSystemLanguage();
    
    /**
     *  获取国家码
     *
     *  @return 国家编码
     */
    const char* CMPGetCountryCode();
    
    /**
     * 获取分辨率
     * @param separator 分辨率分割符
     */
    const char* CMPGetResolution(const char* separator);
    
    /**
     *  获取国家
     *
     *  @return 国家
     */
    const char* CMPGetCountry();
    
    /**
     *  获取版本号（构建版本）
     *
     *  @return 版本号
     */
    const char* CMPGetAppVersion();
    
    /**
     *  获取版本名
     *
     *  @return 版本名
     */
    const char* CMPGetAppVersionName();
    
    /**
     *  获取手机型号
     *
     *  @return 手机型号
     */
    const char* CMPGetDeviceModel();
    
    /**
     *  获取手机名称
     *
     *  @return 手机名称
     */
    const char* CMPGetDeviceName();
    
    /**
     * 是否可打开App间通信的URL（一般用于判断是否已安装应用）
     * @param url 目标App内定义的URL，此参值需要字义的Info.plist的白名单里才能正常使用此接口
     */
    bool CMPCanOpenAppUrl(const char* url);
    
    /**
     * 打开App（用于与其他App通信）
     * @param url 目标App内能接收的URL
     */
    bool CMPOpenAppUrl(const char* url);
    
    /**
     *  获取系统版本名，如：8.1
     *
     *  @return 系统版本
     */
    const char* CMPGetOSVersion();
    
    /**
     *  是否开启通知栏
     *
     *  @return 0、未开启推送（默认值） 1、已开启推送
     */
    bool CMPIsAllowedNotification();
    
    /**
     *  获取手机具体型号
     *
     *  @return 手机型号
     */
    const char* CMPGetDevicePlatform();
    
    /**
     *  获取手机总内存(单位：M)
     *
     *  @return 内存大小
     */
    unsigned long long CMPGetPhysicalMemory();
    
    /**
     *  获取设备总存储量
     *
     *  @return 存储量
     */
    long CMPGetTotalDiskSpace();
    
    /**
     *  获取设备可用空间
     *
     *  @return 可用空间量
     */
    long CMPGetFreeDiskSpace();
    
    /// 转换char*
    char * CMPStringCopy(const char* string);
    
    /**
     *  获取设备唯一标识符
     *
     *  @return 唯一标识符
     */
    const char* CMPGetDeviceIdentifier();
    /**
     *  获取当前网络状态
     *
     *  @return 网络状态码
     */
    int CMPGetCurrentNetStatue();
    
    
    /**
     压缩文件成zip文件

     @param zipPath zip文件路径
     @param filePath 需要压缩的文件路径
     */
    bool CMPZipFile(const char *zipPath,const char *filePath);
    
    
    /**
     解压文件

     @param zipPath zip文件路径
     @param filePath 解压后的文件路径
     @return 解压结果
     */
    bool CMPUnZipFile(const char *zipPath,const char *filePath);
    
    //获取 mnc 码
    const char * CMPGetMNCCode();
    
    //获取 mnc 码
    const char * CMPGetBundleID();
    
    const char * CMPGetCPUType();
    
    const char * CMPPathOfDocumentDirectory();
    
    const char * CMPPathOfResource();
    
    const char * CMPIsDirectoryExistInternal (const char * file,const char* directory);
    
    bool CMPIsFileExsit(const char *filePath);
    
    const char * CMPGetSDKVersion();
    
    int CMPGetIntValueFromUserDefault(const char* key);
    
    const char * CMPGetInfocType(const char * bundleID);

    const char * CMPGetCurrentTime();
    
    /**
     展示loading浮层

     @param red 红颜色值
     @param green 绿颜色值
     @param blue 蓝颜色值
     @param alpha 透明度
     */
    void CMPShowLoadingView(float red, float green, float blue, float alpha);
    
    void CMPDismissLoadingView();
    
    bool CMPPresentAPPReview();
    
    /**
     设置appsflyer的设备id

     @param appsflyerDeviceId 设备id
     */
    void CMPSetAppsflyerDeviceId(const char* appsflyerDeviceId);
    
    /**
     获取appsflyer设备id

     @return 设备id
     */
    const char * CMPGetAppsflyerDeviceId();
    
    /**
     *  show a GDPR view
     *
     *  @param  callBackMethod      call back method pointer
     */
    void CMPShowGDPRViewWithCallBackMethod(CMPCommondCallBackMethod callBackMethod);
    
    /**
     *  判断是否同意GDPR应用隐私条款更新, 首次调用返回false
     *
     *  @return            ture为同意，false为不同意
     */
    bool CMPCheckIfGDPRAgreedPolicyUpdates();
    
    /**
     *  判断是否同意GDPR个性化广告推荐, 首次调用返回false
     *
     *  @return            ture为同意，false为不同意
     */
    bool CMPCheckIfGDPRAgreedAdStayInformed();
    
    /**
     *  设置是否同意GDPR个性化广告推荐
     *
     *  @param isAgreed        ture为同意，false为不同意
     */
    void CMPSetGDPRAgreedAdStayInformed(bool isAgreed);
    
    /**
     stop tracking user information
     */
    void CMPStopTracking();
    
    /**
     is stop tracking user information
     
     @return result
     */
    bool CMPIsStopTracking();
    
    /**
     check is=s GDPR Enforced Country?
     
     @return result
     */
    bool CMPCheckIsGDPREnforcedCountry();
}

#endif
