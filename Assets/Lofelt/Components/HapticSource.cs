using UnityEngine;

/// <summary>
/// Plays back a haptic clip.
/// </summary>
///
/// Plays back the haptic clip provided in the <c>hapticData</c> property when calling
/// <c>Play()</c>. At the moment, playback of a haptic source is not triggered automatically
/// by e.g. proximity between the <c>HapticReceiver</c> and the <c>HapticSource</c>,
/// so you need to call <c>Play()</c> to trigger playback.
///
/// <c>HapticSourceEditor</c> provides an editor for <c>HapticSource</c> for the Inspector
/// view. The editor sets the <c>hapticData</c> property to the content of the linked
/// haptic asset.
///
/// To make use of <c>HapticSource</c>, you need to place a <c>HapticReceiver</c> component
/// in your scene.
public class HapticSource : MonoBehaviour
{
    HapticReceiver hapticReceiver;

    /// <summary>
    /// The name of the haptic clip without file extension
    /// </summary>
    ///
    /// The haptic clip must sit in <c>Assets/StreamingAssets/Haptics/</c>.
    ///
    /// This is used by <c>HapticSourceEditor</c> to link to the selected haptic asset.
    /// You should not need to use this property directly.
    public string hapticName;

    /// <summary>
    /// The haptic clip, as a JSON string of the <c>.haptic</c> file content
    /// </summary>
    ///
    /// This property is set by <c>HapticSourceEditor</c> when selecting the haptic asset,
    /// you usually don't need to use this property directly.
    public string hapticData;

    /// <summary>
    /// Initializes the <c>HapticSource</c>.
    /// </summary>
    ///
    /// Finds the single <c>HapticReceiver</c> in your scene and remembers it for later
    /// usage.
    void Start()
    {
        var hapticReceivers = FindObjectsOfType(typeof(HapticReceiver)) as HapticReceiver[];
        if (hapticReceivers.Length == 0)
        {
            throw new System.Exception("Unable to find HapticReceiver component");
        }
        if (hapticReceivers.Length > 1)
        {
            throw new System.Exception("There is more than one HapticReceiver component in the scene. Please ensure there is always exactly one HapticReceiver component in the scene.");
        }
        hapticReceiver = hapticReceivers[0];
    }

    /// <summary>
    /// Loads and plays back the haptic clip.
    /// </summary>
    ///
    /// At the moment only one haptic clip at a time can be played.
    public void Play()
    {
#if (UNITY_EDITOR)
        Debug.Log("Playing haptic clip: " + hapticName);
#endif
        if (hapticReceiver != null)
        {
            hapticReceiver.Load(hapticData);
            hapticReceiver.Play();
        }
    }

    /// <summary>
    /// Stops playback that was previously started with <c>Start()</c>.
    /// </summary>
    public void Stop()
    {
        if (hapticReceiver != null)
        {
            hapticReceiver.Stop();
        }
    }
}
