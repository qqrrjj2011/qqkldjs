//
//  CMPActivityFeedbackManager.h
//  Piano
//
//  Created by wangyufeng on 15/10/16.
//

#import <UIKit/UIKit.h>
@protocol CMPActivityFeedbackManagerDelegate;

@interface CMPActivityFeedbackManager : UIViewController
@property (nonatomic,weak)id <CMPActivityFeedbackManagerDelegate>delegate;
@property (assign, nonatomic) BOOL showCloseBtn;
+ (instancetype)sharedInstance;
- (void)presentSelfWithURL:(NSString *)url appID:(NSString *)appID;
@end

@protocol CMPActivityFeedbackManagerDelegate <NSObject>
- (void)cmpActivityJumpToLevel:(int)index;
- (void)cmpActivitySendPrize:(int)sence id:(int)ID count:(int)count;
@end

