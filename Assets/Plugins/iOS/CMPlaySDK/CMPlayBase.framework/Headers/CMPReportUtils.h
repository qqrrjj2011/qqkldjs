//
//  CMPReportUtils.h
//  CMPlayBase
//
//  Created by wangyufeng on 16/8/30.
//  Copyright © 2016年 cmcm. All rights reserved.
//

#ifndef CMPReportUtils_h
#define CMPReportUtils_h

#include <stdio.h>
#include <string>
#include <vector>

/**
 * C++ provided to the statistical report interface
 */
class CMPReportUtils {
    
public:
    
    /**
     * reported data(This interface appends the field network by default, and the caller does not need to stitch this field)
     * @param tableName table
     * @param data data, format：key=value&key=value，e.g.：uptime=1113213&action=3&remark=abc, if key no value: key=&key=value&key=, key can not be missing
     */
    static void reportData(const char* tableName, const char* data);
    
    /**
     * reported action data (report to table: games_businessdata)
     * @param action  action
     */
    static void reportEvent(int action);
    



};

#endif /* CMPReportUtils_h */

#ifdef __cplusplus

/**
 * C provided to the statistical report interface
 */
extern "C" {
    
    /**
     * reported data(This interface appends the field network by default, and the caller does not need to stitch this field)
     * @param tableName table
     * @param data data, format：key=value&key=value，e.g.：uptime=1113213&action=3&remark=abc, if key no value: key=&key=value&key=, key can not be missing
     */
    void CMPReportData(const char* tableName, const char* data);
    
    /**
     * reported action data (report to table: games_businessdata)
     * @param action  action
     */
    void CMPReportEvent(int action);

}

#endif

