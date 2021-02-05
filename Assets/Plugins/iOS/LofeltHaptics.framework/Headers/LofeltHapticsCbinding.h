//
//  LofeltHapticsCbinding.h
//  LofeltHaptics
//
//  Created by Tomash Ghz on 24.11.20.
//  Copyright © 2020 Lofelt. All rights reserved.
//

#ifndef LofeltHapticsCbinding_h
#define LofeltHapticsCbinding_h

#import <LofeltHaptics/LofeltHaptics.h>

// C bindings for Lofelt Haptics
/*! @abstract       Wraps LofeltHaptics init function to C binding.
    @retur          Returns a pointer to LofeltHaptics instance
 */
CFTypeRef _Nullable lofeltHapticsInitBinding(void);

/*! @abstract       Wraps LofeltHaptics load function to C binding.
    @param haptics  Pointer to LofeltHaptics instance.
    @param data     Pointer to c string of haptic data.
    @return         Whether the operation succeeded.
 */
BOOL lofeltHapticsLoadBinding(CFTypeRef _Nonnull haptics, const char* _Nonnull data);

/*! @abstract       Wraps LofeltHaptics play function to C binding.
    @param haptics  Pointer to LofeltHaptics instance.
    @return         Whether the operation succeeded
 */
BOOL lofeltHapticsPlayBinding(CFTypeRef _Nonnull haptics);

/*! @abstract       Wraps LofeltHaptics stop function to C binding.
    @param haptics  Pointer to LofeltHaptics instance.
    @return         Whether the operation succeeded
 */
BOOL lofeltHapticsStopBinding(CFTypeRef _Nonnull haptics);

/*! @abstract       Wraps LofeltHaptics dealloc function to C binding.
    @param haptics  Pointer to LofeltHaptics instance.
    @return         Whether the operation succeeded
 */
BOOL lofeltHapticsReleaseBinding(CFTypeRef _Nonnull haptics);

#endif /* LofeltHapticsCbinding_h */
