using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class DebugScript : MonoBehaviour
{
    [SerializeField] private Text debugText; // Text UI element for debugging
    [SerializeField] private ARHumanBodyManager humanBodyManager; // Reference to ARHumanBodyManager

    private void Start()
    {
        // Validate references
        if (humanBodyManager == null)
        {
            LogToScreen("Error: ARHumanBodyManager is not assigned.");
        }
        if (debugText == null)
        {
            LogToScreen("Error: Debug Text UI is not assigned.");
        }
    }

    private void OnEnable()
    {
        // Subscribe to ARHumanBodyManager's trackablesChanged event
        if (humanBodyManager != null)
        {
            humanBodyManager.trackablesChanged.AddListener(OnTrackablesChanged);
        }
    }

    private void OnDisable()
    {
        // Unsubscribe from ARHumanBodyManager's trackablesChanged event
        if (humanBodyManager != null)
        {
            humanBodyManager.trackablesChanged.RemoveListener(OnTrackablesChanged);
        }
    }

    private void OnTrackablesChanged(ARTrackablesChangedEventArgs<ARHumanBody> args)
    {
        string debugMessage = "";

        // Handle added bodies
        if (args.added.Count > 0)
        {
            debugMessage += $"Bodies Added: {args.added.Count}\n";
            foreach (ARHumanBody body in args.added)
            {
                debugMessage += $"Added Body ID: {body.trackableId}, Tracking State: {body.trackingState}\n";
            }
        }

        // Handle updated bodies
        if (args.updated.Count > 0)
        {
            debugMessage += $"Bodies Updated: {args.updated.Count}\n";
            foreach (ARHumanBody body in args.updated)
            {
                debugMessage += $"Updated Body ID: {body.trackableId}, Tracking State: {body.trackingState}\n";
            }
        }

        // Handle removed bodies
        if (args.removed.Count > 0)
        {
            debugMessage += $"Bodies Removed: {args.removed.Count}\n";
            foreach (var pair in args.removed)
            {
                debugMessage += $"Removed Body ID: {pair.Key}\n";
            }
        }

        // If no changes occurred
        if (string.IsNullOrEmpty(debugMessage))
        {
            debugMessage = "No human body detected.";
        }

        LogToScreen(debugMessage); // Update the UI with debug information
    }

    private void LogToScreen(string message)
    {
        if (debugText != null)
        {
            debugText.text = message; // Display the message on the UI
        }
    }
}
