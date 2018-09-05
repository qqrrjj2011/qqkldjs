//
//  TWRDownloadManager.h
//  DownloadManager
//
//  Created by Michelangelo Chasseur on 25/07/14.
//  Copyright (c) 2014 Touchware. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <CoreGraphics/CGBase.h>

@interface CMPDownloadManager : NSObject

@property (nonatomic, copy) void(^backgroundTransferCompletionHandler)();

+ (instancetype)sharedManager;

- (void)downloadFileForURL:(NSString *)urlString
                  withName:(NSString *)fileName
          inDirectoryNamed:(NSString *)directory
              friendlyName:(NSString *)friendlyName
             progressBlock:(void(^)(CGFloat progress))progressBlock
             remainingTime:(void(^)(NSUInteger seconds))remainingTimeBlock
           completionBlock:(void(^)(BOOL completed, NSString* url, NSError* error))completionBlock
      enableBackgroundMode:(BOOL)backgroundMode;

- (void)downloadFileForURL:(NSString *)url
                  withName:(NSString *)fileName
          inDirectoryNamed:(NSString *)directory
             progressBlock:(void(^)(CGFloat progress))progressBlock
           completionBlock:(void(^)(BOOL completed, NSString* url, NSError* error))completionBlock
      enableBackgroundMode:(BOOL)backgroundMode;

- (void)downloadFileForURL:(NSString *)url
          inDirectoryNamed:(NSString *)directory
             progressBlock:(void(^)(CGFloat progress))progressBlock
           completionBlock:(void(^)(BOOL completed, NSString* url, NSError* error))completionBlock
      enableBackgroundMode:(BOOL)backgroundMode;

- (void)downloadFileForURL:(NSString *)url
             progressBlock:(void(^)(CGFloat progress))progressBlock
           completionBlock:(void(^)(BOOL completed, NSString* url, NSError* error))completionBlock
      enableBackgroundMode:(BOOL)backgroundMode;

#pragma mark - Download with estimated time

- (void)downloadFileForURL:(NSString *)url
                  withName:(NSString *)fileName
          inDirectoryNamed:(NSString *)directory
             progressBlock:(void(^)(CGFloat progress))progressBlock
             remainingTime:(void(^)(NSUInteger seconds))remainingTimeBlock
           completionBlock:(void(^)(BOOL completed, NSString* url, NSError* error))completionBlock
      enableBackgroundMode:(BOOL)backgroundMode;

- (void)downloadFileForURL:(NSString *)url
          inDirectoryNamed:(NSString *)directory
             progressBlock:(void(^)(CGFloat progress))progressBlock
             remainingTime:(void(^)(NSUInteger seconds))remainingTimeBlock
           completionBlock:(void(^)(BOOL completed, NSString* url, NSError* error))completionBlock
      enableBackgroundMode:(BOOL)backgroundMode;

- (void)downloadFileForURL:(NSString *)url
             progressBlock:(void(^)(CGFloat progress))progressBlock
             remainingTime:(void(^)(NSUInteger seconds))remainingTimeBlock
           completionBlock:(void(^)(BOOL completed, NSString* url, NSError* error))completionBlock
      enableBackgroundMode:(BOOL)backgroundMode;

- (void)downloadFileForURL:(NSString *)url
          inDirectoryNamed:(NSString *)directory
           completionBlock:(void(^)(BOOL completed, NSString* url, NSError* error))completionBlock;

- (void)downloadFileForURL:(NSString *)url
           completionBlock:(void(^)(BOOL completed, NSString* url, NSError* error))completionBlock;

- (void)cancelAllDownloads;
- (void)cancelDownloadForUrl:(NSString *)fileIdentifier;

- (void)suspendAllDownloads;
- (void)resumeAllDownloads;

- (void)cleanDirectoryNamed:(NSString *)directory;


- (NSString *)localPathForFile:(NSString *)fileIdentifier;
- (NSString *)localPathForFile:(NSString *)fileIdentifier inDirectory:(NSString *)directoryName;

- (BOOL)fileExistsForUrl:(NSString *)urlString;
- (BOOL)fileExistsForUrl:(NSString *)urlString inDirectory:(NSString *)directoryName;
- (BOOL)fileExistsWithName:(NSString *)fileName;
- (BOOL)fileExistsWithName:(NSString *)fileName inDirectory:(NSString *)directoryName;

- (BOOL)directoryExistsWithName:(NSString *)directoryName;

- (BOOL)deleteFileForUrl:(NSString *)urlString;
- (BOOL)deleteFileForUrl:(NSString *)urlString inDirectory:(NSString *)directoryName;
- (BOOL)deleteFileWithName:(NSString *)fileName;
- (BOOL)deleteFileWithName:(NSString *)fileName inDirectory:(NSString *)directoryName;

- (void)deleteFileWithExceptName:(NSArray *)urlArray inDirectory:(NSString *)directoryName;

/**
 *  This method helps checking which downloads are currently ongoing.
 *
 *  @return an NSArray of NSString with the URLs of the currently downloading files.
 */
- (NSArray *)currentDownloads;

@end
