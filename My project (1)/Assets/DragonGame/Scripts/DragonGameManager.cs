using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class DragonGameManager : MonoBehaviour
{
    [Header("AR Components")]
    public ARTrackedImageManager trackedImageManager;
    public ARRaycastManager raycastManager;
    
    [Header("Game Objects")]
    public GameObject dragonPrefab;
    private GameObject currentDragon;
    
    [Header("UI")]
    public UnityEngine.UI.Text temperatureText;
    public UnityEngine.UI.Text sizeText;
    public UnityEngine.UI.Text timerText;
    
    private DragonController dragonController;
    private bool dragonPlaced = false;

    void Start()
    {
        // S'abonner aux événements de détection d'images
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void Update()
    {
        // Placement du dragon par tap si pas encore placé
        if (!dragonPlaced && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                TryPlaceDragon(touch.position);
            }
        }
        
        // Mise à jour de l'UI
        UpdateUI();
    }

    void TryPlaceDragon(Vector2 screenPosition)
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        
        if (raycastManager.Raycast(screenPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;
            
            // Créer le dragon
            currentDragon = Instantiate(dragonPrefab, hitPose.position, hitPose.rotation);
            dragonController = currentDragon.GetComponent<DragonController>();
            
            dragonPlaced = true;
            Debug.Log("Dragon placé !");
        }
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        // Traiter les nouvelles images détectées
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            HandleMarkerDetected(trackedImage);
        }
        
        // Traiter les images mises à jour
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                HandleMarkerDetected(trackedImage);
            }
        }
    }

    void HandleMarkerDetected(ARTrackedImage trackedImage)
    {
        if (dragonController == null) return;
        
        string markerName = trackedImage.referenceImage.name;
        Vector3 markerPosition = trackedImage.transform.position;
        
        Debug.Log($"Marqueur détecté : {markerName}");
        
        // Envoyer l'info au dragon
        dragonController.OnMarkerDetected(markerName, markerPosition);
    }

    void UpdateUI()
    {
        if (dragonController == null) return;
        
        if (temperatureText != null)
            temperatureText.text = $"Température: {dragonController.currentTemperature:F1}°C";
            
        if (sizeText != null)
            sizeText.text = $"Taille: {dragonController.currentSize:F1}";
            
        if (timerText != null)
            timerText.text = $"Temps: {dragonController.GetTimeRemaining():F0}s";
    }

    void OnDestroy()
    {
        if (trackedImageManager != null)
            trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }
}
