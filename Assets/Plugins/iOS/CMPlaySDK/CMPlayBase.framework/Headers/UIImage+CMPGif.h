//
//  UIImage+CMPGif.h
//  CMPlayBase
//
//  Created by KepenJ on 2017/6/19.
//  Copyright © 2017年 cmcm. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface UIImage (CMPGif)
+ (UIImage *)cmpAnimatedGIFWithData:(NSData *)data;
- (BOOL)isGIF;
@end
