//
//  NSData+CMPBZlib.h
//  Unity-iPhone
//
//  Created by KepenJ on 16/9/26.
//
//

#import <Foundation/Foundation.h>

@interface NSData (CMPBZlib)
//zlib 压缩
- (NSData *)dataUseZLibCompress;
//zlib 解压缩
- (NSData *)dataUseZLibDecompress;
@end
