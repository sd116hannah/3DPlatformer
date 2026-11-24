using UnityEngine;


[RequireComponent(typeof(Collider))] // acts as a safeguard -> if you drag this to any script, it will add a collider to it
public class ClosestPointEmitter : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Event loopEvent;

    private Collider area;
    private Transform emitter;
    private Transform listener;

    private uint playingID = AkUnitySoundEngine.AK_INVALID_PLAYING_ID;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        area = GetComponent<Collider>();
        AkAudioListener akListener = FindFirstObjectByType<AkAudioListener>();
        listener = akListener.transform;

        emitter = new GameObject("ClosestPoint_Emitter").transform;
        emitter.SetParent(transform, false);
        emitter.position = area.ClosestPoint(listener.position);

        playingID = loopEvent.Post(emitter.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        emitter.position = area.ClosestPoint(listener.position);
    }

    private void OnDrawGizmos() // allows you to see the emitter line around the player
    {
        if (emitter == null || listener == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(listener.position, emitter.position);
        Gizmos.DrawSphere(emitter.position, 0.3f);
    }
}
