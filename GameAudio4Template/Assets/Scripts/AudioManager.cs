using UnityEngine;

public class AudioManager : MonoBehaviour
{

    //////////////////////////////////////////////////////////////////////////////////////////////////////

    // Singleton System

    public static AudioManager Instance {  get ; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) // If there is an instance, AND it's not me...
        {
            Destroy(this); // .. I will immediately destroy myself
        }
        else // Otherwise...
        {
            Instance = this; // ... Don't do anything
        }

        DontDestroyOnLoad(gameObject);
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////

    public AK.Wwise.Event ambientEvent;
    public AK.Wwise.State defaultState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        defaultState.SetValue();
        ambientEvent.Post(gameObject);
    }

    public void PlayOneShotSound(string eventName, GameObject gameObject)
    {
        AkSoundEngine.PostEvent(eventName, gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}