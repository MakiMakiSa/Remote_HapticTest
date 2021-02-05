using UnityEngine;

#if (UNITY_ANDROID && !UNITY_EDITOR)
using System.Text;
#elif (UNITY_IOS && !UNITY_EDITOR)
using System.Runtime.InteropServices;
#endif

/// <summary>
/// Plays haptic clips.
/// </summary>
///
/// <c>HapticReceiver</c> receives and plays back haptics from <c>HapticSource</c> components.
/// It also provides an API to play haptics directly, but the usual way to play haptics
/// is by using the <c>HapticSource</c> component.
///
/// Your scene should have exactly one <c>HapticReceiver</c> component, similar to how a scene
/// should have exactly one <c>AudioListener</c>.
///
/// Internally, <c>HapticReceiver</c> makes use of the iOS framework or the Android library from
/// the Lofelt SDK, which are added as plugins. On other platforms like a PC or Mac,
/// <c>HapticReceiver</c> does nothing.
///
/// At the moment only one haptic clip at a time can be loaded and played back.
public class HapticReceiver : MonoBehaviour
{
#if (UNITY_ANDROID && !UNITY_EDITOR)
    AndroidJavaObject lofeltHaptics;
#elif (UNITY_IOS && !UNITY_EDITOR)
    // imports of iOS Framework bindings
    // All of the following functions that are dealing with raw pointers have the unsafe keyword.

    [DllImport("__Internal")]
    private static unsafe extern void* lofeltHapticsInitBinding();

    // Use Marshalling to convert string into a pointer to a null-terminated char array.
    [DllImport("__Internal")]
    private static unsafe extern bool lofeltHapticsLoadBinding(void* controller, [MarshalAs(UnmanagedType.LPStr)] string data);

    [DllImport("__Internal")]
    private static unsafe extern bool lofeltHapticsPlayBinding(void* controller);

    [DllImport("__Internal")]
    private static unsafe extern bool lofeltHapticsStopBinding(void* controller);

    [DllImport("__Internal")]
    private static unsafe extern bool lofeltHapticsReleaseBinding(void* controller);

    unsafe void* controller = null;
#endif

    /// <summary>
    /// Initializes the component by creating an instance of the <c>LofeltHaptics</c>
    /// class from the Lofelt SDK.
    /// </summary>
    void Start()
    {
#if (UNITY_ANDROID && !UNITY_EDITOR)
        using (var unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        using (var context = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity"))
        {
            lofeltHaptics = new AndroidJavaObject("com.lofelt.haptics.LofeltHaptics", context);
        }
#elif (UNITY_IOS && !UNITY_EDITOR)
        unsafe
        {
            controller = lofeltHapticsInitBinding();
        }
#endif
    }

    /// <summary>
    /// Loads a haptic clip for later playback.
    /// </summary>
    /// <param name="data">The haptic clip, as a JSON string of the <c>.haptic</c> file content</param>
    public void Load(string data)
    {
#if (UNITY_ANDROID && !UNITY_EDITOR)
        lofeltHaptics.Call("load", Encoding.UTF8.GetBytes(data));
#elif (UNITY_IOS && !UNITY_EDITOR)
        unsafe
        {
            lofeltHapticsLoadBinding(controller, data);
        }
#endif
    }

    /// <summary>
    /// Plays the haptic clip that was previously loaded with <c>Load()</c>.
    /// </summary>
    public void Play()
    {
#if (UNITY_ANDROID && !UNITY_EDITOR)
        lofeltHaptics.Call("play");
#elif (UNITY_IOS && !UNITY_EDITOR)
        unsafe
        {
            lofeltHapticsPlayBinding(controller);
        }
#endif
    }

    /// <summary>
    /// Stops playback of the haptic clip that was previously started with <c>Play()</c>.
    /// </summary>
    public void Stop()
    {
#if (UNITY_ANDROID && !UNITY_EDITOR)
        lofeltHaptics.Call("stop");
#elif (UNITY_IOS && !UNITY_EDITOR)
        unsafe
        {
            lofeltHapticsStopBinding(controller);
        }
#endif
    }

    /// <summary>
    /// Destroys the instance of the <c>LofeltHaptics</c> class from the SDK.
    /// </summary>
    void OnDestroy()
    {
#if (UNITY_ANDROID && !UNITY_EDITOR)
        lofeltHaptics.Dispose();
        lofeltHaptics = null;
#elif (UNITY_IOS && !UNITY_EDITOR)
        unsafe
        {
            lofeltHapticsReleaseBinding(controller);
            controller = null;
        }
#endif
    }
}
