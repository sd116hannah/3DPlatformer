using UnityEngine;

public class InteractiveMusic : MonoBehaviour
{
    public AK.Wwise.Event musicEvent;

    public Light beatLight;

    public float pulseIntensity = 5f;
    public float fadeSpeed = 8f;

    public Color flashRedColor = Color.red;
    public Color flashBlueColor = Color.blue;
    public Color dropColor = Color.yellow;

    private float beatLightTarget = 0f;
    private float cueLightTarget = 0f;

    private Color cueColorTarget = Color.white;

    private uint playingID;

    private void Start()
    {
        playingID = musicEvent.Post(
            gameObject,
            (uint)(AkCallbackType.AK_MusicSyncBeat | AkCallbackType.AK_MusicSyncUserCue),
            OnMusicCallback
            );

        beatLight.intensity = 0f;
    }

    void OnMusicCallback(object in_cookie, AkCallbackType in_type, AkCallbackInfo in_info)
    {
        if (in_type == AkCallbackType.AK_MusicSyncBeat)
        {
            beatLightTarget = pulseIntensity;
        }

        if (in_type == AkCallbackType.AK_MusicSyncUserCue)
        {
            AkMusicSyncCallbackInfo musicInfo = (AkMusicSyncCallbackInfo)in_info;

            string cueName = musicInfo.userCueName;

            switch(cueName)
            {
                case "Jimmy":
                    cueColorTarget = flashRedColor;
                    break;
                case "Jamie":
                    cueColorTarget = flashBlueColor;
                    break;
                default:
                    cueColorTarget = Color.white;
                    break;
            }
        }
    }

    private void Update()
    {
        beatLight.intensity = Mathf.Lerp(beatLight.intensity, beatLightTarget, fadeSpeed * Time.deltaTime );
        beatLightTarget = Mathf.Lerp(beatLightTarget, 0f, fadeSpeed * Time.deltaTime );

        beatLight.color = cueColorTarget;
    }
}