using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

// Review #1 (How to Access Wwise from Unity Script)
public class WwiseReview1 : MonoBehaviour
{
    //AK.Wwise.Bank("123456789"); // Method 1

    AK.Wwise.Event nameOfEvent1; // Method #2
    public AK.Wwise.Event nameOfEvent2; // Method #3

    // Most common Wwise Events in Unity:
    public AK.Wwise.Switch nameOfSwitch;
    public AK.Wwise.State nameOfState;
    public AK.Wwise.RTPC nameOfRTPC;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //nameOfEvent1.Name = "xyz"; // Method #2
        //AkSoundEngine.PostEvent(1394719417249, gameObject); // Method #3
    }
}



// Review #2

public class WwiseReview2 : MonoBehaviour
{
    // Wwise Events:
    public AK.Wwise.Event nameOfEvent;
    public AK.Wwise.Switch nameOfSwitch;
    public AK.Wwise.State nameOfState;
    public AK.Wwise.RTPC nameOfRTPC;
    public AK.Wwise.RTPC nameOfLocalRTPC;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nameOfEvent.Post(gameObject); // will post event from "Wwise Events:"
        nameOfSwitch.SetValue(gameObject);
        nameOfState.SetValue();

        nameOfRTPC.SetGlobalValue(5f); // Idea #1: If multiple objects are listening to the same RTPC
        nameOfRTPC.SetValue(gameObject, 5f); // Idea #2: Things like wind - if you run faster, the wind speed increases

        nameOfLocalRTPC.SetValue(gameObject, 5f);

        AkSoundEngine.SetSwitch("Footsteps", "Grass", gameObject); // 
        AkSoundEngine.SetRTPCValue("Health", 5f); //
    }
}