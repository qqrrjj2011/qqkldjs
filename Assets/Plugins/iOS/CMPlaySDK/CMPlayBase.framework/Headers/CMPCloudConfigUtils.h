//
//  CMPCloudConfigUtils.h
//  CMPlayBase
//
//  Created by wangyufeng on 16/8/4.
//  Copyright © 2016年 cmcm. All rights reserved.
//

#ifndef CMPCloudConfigUtils_h
#define CMPCloudConfigUtils_h

#include <stdio.h>
#include <string>
#include <vector>

#define CLOUD_CONFIG_REFRESH_NOTIFY "CloudConfigRefreshNotify"

/**
 * Cloud config API (C++)
 */
class CMPCloudConfigUtils {
    
public:
    
    /**
     * Pull data from cloud when the player open game or switch language，this method must be called，and use of asynchronous to pull data.
     * @param language APP Language
     */
    static void pullCloudConfigDataWithLanguage(const char* language);
    
    /**
     * Access data，The same name of the section may exist，access the priority ones（The lower of the priority value priority, the higher the priority）
     * @param function According to the func_type in the background to separate the data，the data on the cloud is usually use 2
     * @param section data node
     */
    static std::string getCloudData(int function, const char* section);
    
    /**
     * back to the key_value list in the section of the same name
     * @param function According to the func_type in the background to separate the data，the data on the cloud is usually use 2
     * @param section data node
     */
    static std::vector<std::string> getCloudDatas(int function, const char* section);
    /**
     * back to the key_value in the section to access the date（access the priority ones）
     * @param function According to the func_type in the background to separate the data，the data on the cloud is usually use 2
     * @param section data node
     * @param key field name
     * @param defValue default data
     */
    static std::string getCloudStringValue(int function, const char* section, const char* key, const char* defValue);
    
    /**
     * back to the key_value in the section to access the date（access the priority ones）
     * @param function According to the func_type in the background to separate the data，the data on the cloud is usually use 2
     * @param section data node
     * @param key field name
     * @param defValue default data
     */
    static int getCloudIntValue(int function, const char* section, const char* key, int defValue);
    
    /**
     * back to the key_value in the section to access the date（access the priority ones）
     * @param function According to the func_type in the background to separate the data，the data on the cloud is usually use 2
     * @param section data node
     * @param key field name
     * @param defValue default data
     */
    static long getCloudLongValue(int function, const char* section, const char* key, long defValue);
    
    /**
     * back to the key_value in the section to access the date（access the priority ones）
     * @param function According to the func_type in the background to separate the data，the data on the cloud is usually use 2
     * @param section data node
     * @param key field name
     * @param defValue default data
     */
    static bool getCloudBoolValue(int function, const char* section, const char* key, bool defValue);
    
    /**
     * back to the key_value in the section to access the date（access the priority ones）
     * @param function According to the func_type in the background to separate the data，the data on the cloud is usually use 2
     * @param section data node
     * @param key field name
     * @param defValue default data
     */
    static double getCloudDoubleValue(int function, const char* section, const char* key, double defValue);
    
    static std::string getMagicVersion();
};

#endif /* CMPCloudConfigUtils_h */

#ifdef __cplusplus

/**
 * Cloud config API (C)
 */
extern "C" {
    /**
     * Pull data from cloud when the player open game or switch language，this method must be called，and use of asynchronous to pull data.
     * @param language APP Language
     */
    void CMPPullCloudConfigDataWithLanguage(const char* language);
    
    /**
     * Access data，The same name of the section may exist，access the priority ones（The lower of the priority value priority, the higher the priority）
     * @param function According to the func_type in the background to separate the data，the data on the cloud is usually use 2
     * @param section data node
     */
    const char* CMPGetCloudData(int function, const char* section);
    
    /**
     * back to the key_value in the section to access the date（access the priority ones）
     * @param function According to the func_type in the background to separate the data，the data on the cloud is usually use 2
     * @param section data node
     * @param key field name
     * @param defValue default data
     */
    const char* CMPGetCloudStringValue(int function, const char* section, const char* key, const char* defValue);
    
    /**
     * back to the key_value in the section to access the date（access the priority ones）
     * @param function According to the func_type in the background to separate the data，the data on the cloud is usually use 2
     * @param section data node
     * @param key field name
     * @param defValue default data
     */
    int CMPGetCloudIntValue(int function, const char* section, const char* key, int defValue);
    
    /**
     * back to the key_value in the section to access the date（access the priority ones）
     * @param function According to the func_type in the background to separate the data，the data on the cloud is usually use 2
     * @param section data node
     * @param key field name
     * @param defValue default data
     */
    long CMPGetCloudLongValue(int function, const char* section, const char* key, long defValue);
    
    /**
     * back to the key_value in the section to access the date（access the priority ones）
     * @param function According to the func_type in the background to separate the data，the data on the cloud is usually use 2
     * @param section data node
     * @param key field name
     * @param defValue default data
     */
    bool CMPGetCloudBoolValue(int function, const char* section, const char* key, bool defValue);
    
    /**
     * back to the key_value in the section to access the date（access the priority ones）
     * @param function According to the func_type in the background to separate the data，the data on the cloud is usually use 2
     * @param section data node
     * @param key field name
     * @param defValue default data
     */
    double CMPGetCloudDoubleValue(int function, const char* section, const char* key, double defValue);
}

#endif

