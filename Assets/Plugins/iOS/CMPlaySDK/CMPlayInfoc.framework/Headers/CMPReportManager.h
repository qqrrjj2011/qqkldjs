//
//  infocwrap.h
//  demo
//
//  Created by Cupid on 16/5/23.
//  Copyright © 2016年 cmplay. All rights reserved.
//

#ifndef infocwrap_h
#define infocwrap_h
#import <Foundation/Foundation.h>
#import <CMPlayBase/CMPlayBase.h>

@interface ReportClient: NSObject
{
    void*       m_pClient;
}

- (ReportClient*) init;
- (BOOL) InitClient: (void*) pClient;
- (void*) GetClient;


- (void) SetTableEndName: (const char*) szTableName;
- (void) SetTableName: (const char*) szTableName;
- (BOOL) AddInfo: (const char*) szInfo;
- (BOOL) AddInt: (const char*) szName val: (int) nValue;
- (BOOL) AddString: (const char*) szName val: (const char*) szValue;
- (BOOL) AddBinary: (const char*) szName buf: (void*) pBuffer size: (int) nSize;

@end

@interface CMPReportManager : NSObject<CMPReportDelegate>
{
    void*       m_pReportMgr;
    void*       m_sdkReportMgr;
    time_t      m_tGamesStart;
    NSTimer*    m_pDelayStop;
}

- (CMPReportManager*) init;

- (BOOL) InitReportMgr: (void*) pMgr;

- (BOOL) CreateReportClient: (ReportClient**) ppClient isSDKReport:(BOOL)isSDKReport;

- (BOOL) SetPublicTable: (ReportClient*) pClient0;

- (BOOL) Report: (ReportClient*) pClient isSDKReport:(BOOL)isSDKReprot;

- (BOOL) ReportGameShow: (BOOL) bOpen time: (int) nTime firstplay: (int) nFirstPlay isSDKReport:(BOOL)isSDKReport;

- (BOOL) ReportEvent: (int) nAction p1:(int) p1 p2:(int)p2 p3:(int)p3 isSDKReport:(BOOL)isSDKReport;

- (BOOL) onGameResume;

- (BOOL) onGamePause;

- (BOOL) onGameStop;

- (void) delayStop;

- (void) doStop;

- (void) startMonitorNetwork;

- (void) onNetChange: (NSNotification*) note;

+ (CMPReportManager*) getInstance;

+ (CMPReportManager*) ReportInfo: (const char*) pTable info:(const char*) pInfo isSDKReport:(BOOL)isSDKReport;

+ (CMPReportManager*) InitReport: (const char*) pMyFmtPath commonProductID:(int) nCommonID
                                myProductID:(int) nMyID rptAddr:(const char*) rptUrl;


@end


#endif /* infocwrap_h */
