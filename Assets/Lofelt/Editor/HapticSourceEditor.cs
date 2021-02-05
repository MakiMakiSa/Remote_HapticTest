using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

[CustomEditor(typeof(HapticSource))]
[CanEditMultipleObjects]
/// <summary>
/// Provides an editor for the <c>HapticSource</c> component for the Inspector view.
/// </summary>
///
/// The editor lets you link a <c>HapticSource</c> to a haptic asset. All haptic assets
/// need to be placed in <c>Assets/StreamingAssets/Haptics/</c>.
public class HapticSourceEditor : Editor
{
    string hapticsDirectory;

    SerializedProperty hapticNameProp;

    void OnEnable()
    {
        hapticsDirectory = Application.streamingAssetsPath + "/Haptics";

        // Pull properties out of HapticSource.
        hapticNameProp = serializedObject.FindProperty("hapticName");
    }

    public override void OnInspectorGUI()
    {
        // Update the serializedProperty.
        serializedObject.Update();

        HapticSource hapticSource = (HapticSource)target;

        try
        {
            // Enumerate the Assets/StreamingAssets/Haptics/ directory for '.haptic' files.
            List<string> hapticPaths = new List<string>(Directory.EnumerateFiles(hapticsDirectory, "*.haptic"));

            if (hapticPaths.Count == 0)
            {
                EditorGUILayout.HelpBox("No haptics found\n\nPlace haptic clips in Assets/StreamingAssets/Haptics and they will show up here.", MessageType.Info);
            }
            else
            {
                // Map haptic paths into names without extension.
                List<string> hapticNames = new List<string>();

                int selectedIndex = 0;
                for (int i = 0; i < hapticPaths.Count; ++i)
                {
                    string name = Path.GetFileNameWithoutExtension(hapticPaths[i]);
                    hapticNames.Add(name);
                    if (hapticNameProp.stringValue == name)
                    {
                        selectedIndex = i;
                    }
                }

                // Draw the dropdown and label.
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("Haptic Clip");
                selectedIndex = EditorGUILayout.Popup(selectedIndex, hapticNames.ToArray());
                EditorGUILayout.EndHorizontal();

                // Set the name of the selected haptic on HapticSource.
                hapticSource.hapticName = hapticNames[selectedIndex];

                StreamReader reader = new StreamReader(hapticsDirectory + "/" + hapticSource.hapticName + ".haptic");
                hapticSource.hapticData = reader.ReadToEnd();
                reader.Close();
            }
        }
        catch (DirectoryNotFoundException)
        {
            // The directory "Assets/StreamingAssets/Haptics" doesn't exist so create it now.
            Directory.CreateDirectory(hapticsDirectory);
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }
}
