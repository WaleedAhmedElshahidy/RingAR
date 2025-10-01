using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class BodyTrackingTShirt : MonoBehaviour
{
    public ARHumanBodyManager humanBodyManager; // Reference to ARHumanBodyManager
    public GameObject tShirtPrefab; // Prefab of the T-Shirt model

    private GameObject tShirtInstance;
    public Text text;

    private void Update()
    {
        if (humanBodyManager == null)
        {
            Debug.LogError("ARHumanBodyManager is not assigned!");
            return;
        }

        // Check the tracked human bodies
        foreach (var body in humanBodyManager.trackables)
        {
            text.text = "inside For Cond";
            if (body.trackingState == TrackingState.Tracking)
            {
                // If we don't already have a T-Shirt instance, create one
                if (tShirtInstance == null)
                {
                    tShirtInstance = Instantiate(tShirtPrefab);
                }
                text.text = body.name + "inside if Cond";      
                // Position the T-Shirt on the body
                PositionTShirt(body);
            }
            else
            {
                // If tracking is lost, destroy the T-Shirt instance
                if (tShirtInstance != null)
                {
                    Destroy(tShirtInstance);
                    tShirtInstance = null;
                }
            }
        }
    }

    private void PositionTShirt(ARHumanBody body)
    {
        if (body == null || body.trackingState != TrackingState.Tracking)
            return;

        // Example: Access the chest joint for positioning
        Transform chestJoint = body.transform.Find("SpineChest"); // Adjust to match your joint structure
        
        if (chestJoint != null)
        {
            tShirtInstance.transform.position = chestJoint.position;
            tShirtInstance.transform.rotation = chestJoint.rotation;

            // Adjust the scale of the T-Shirt to fit the body
            tShirtInstance.transform.localScale = Vector3.one * 1.2f;
        }
        else
        {
            Debug.LogWarning("Chest joint not found.");
        }
    }
}
