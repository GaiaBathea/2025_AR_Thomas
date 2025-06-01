using System.Collections;
using UnityEngine;

public class DragonController : MonoBehaviour
{
    [Header("Dragon Properties")]
    public float currentSize = 1f;
    public float minSize = 0.3f;
    public float maxSize = 2f;
    public float currentTemperature = 20f;
    
    [Header("Game Settings")]
    public float gameTimer = 90f; // 1.5 minutes
    public float winSize = 1.8f;
    
    [Header("Visual Effects")]
    public GameObject fireEffect;
    
    private bool gameActive = true;
    private float coolingRate = -1f; // Refroidissement par seconde

    void Start()
    {
        // Démarrer le refroidissement constant
        StartCoroutine(AmbientCooling());
        StartCoroutine(GameTimer());
        
        // Taille initiale
        transform.localScale = Vector3.one * currentSize;
        
        // Cacher l'effet de feu au début
        if (fireEffect != null)
            fireEffect.SetActive(false);
    }

    void Update()
    {
        if (!gameActive) return;
        
        // Ajuster la taille selon la température
        UpdateSize();
        
        // Vérifier les conditions de fin
        CheckGameConditions();
    }

    void UpdateSize()
    {
        float targetSize = currentSize;
        
        // Grandir si chaud, rétrécir si froid
        if (currentTemperature > 25f)
        {
            targetSize += 0.5f * Time.deltaTime;
        }
        else if (currentTemperature < 15f)
        {
            targetSize -= 0.3f * Time.deltaTime;
        }
        
        // Appliquer les limites
        targetSize = Mathf.Clamp(targetSize, minSize, maxSize);
        
        // Changer progressivement
        currentSize = Mathf.Lerp(currentSize, targetSize, Time.deltaTime * 2f);
        transform.localScale = Vector3.one * currentSize;
        
        // Effet visuel selon la température
        if (fireEffect != null)
        {
            fireEffect.SetActive(currentTemperature > 30f);
        }
    }

    public void OnMarkerDetected(string markerName, Vector3 position)
    {
        Debug.Log($"Dragon a détecté : {markerName}");
        
        // Appliquer l'effet de température
        switch (markerName.ToLower())
        {
            case "fire":
            case "heater":
                AddTemperature(20f);
                break;
                
            case "ice":
            case "freezer":
                AddTemperature(-15f);
                break;
        }
        
        // Optionnel : déplacer le dragon vers le marqueur
        StartCoroutine(MoveToPosition(position));
    }

    void AddTemperature(float deltaTemp)
    {
        currentTemperature += deltaTemp;
        currentTemperature = Mathf.Clamp(currentTemperature, -10f, 80f);
        
        Debug.Log($"Nouvelle température : {currentTemperature}°C");
    }

    IEnumerator MoveToPosition(Vector3 targetPos)
    {
        Vector3 startPos = transform.position;
        float journey = 0f;
        
        while (journey <= 1f)
        {
            journey += Time.deltaTime * 0.5f; // Vitesse de mouvement
            transform.position = Vector3.Lerp(startPos, targetPos, journey);
            yield return null;
        }
    }

    IEnumerator AmbientCooling()
    {
        while (gameActive)
        {
            yield return new WaitForSeconds(1f);
            AddTemperature(coolingRate);
        }
    }

    IEnumerator GameTimer()
    {
        while (gameTimer > 0 && gameActive)
        {
            yield return new WaitForSeconds(1f);
            gameTimer--;
        }
        
        CheckGameConditions();
    }

    void CheckGameConditions()
    {
        if (currentSize <= minSize)
        {
            GameOver(false, "Le dragon a disparu dans le froid !");
        }
        else if (currentSize >= winSize)
        {
            GameOver(true, "Le dragon a grandi suffisamment ! Victoire !");
        }
        else if (gameTimer <= 0)
        {
            GameOver(false, "Temps écoulé ! Le dragon n'est pas assez grand.");
        }
    }

    void GameOver(bool won, string message)
    {
        gameActive = false;
        Debug.Log(message);
        
        // Ici tu peux ajouter des effets de fin de jeu
    }

    public float GetTimeRemaining()
    {
        return gameTimer;
    }
}
