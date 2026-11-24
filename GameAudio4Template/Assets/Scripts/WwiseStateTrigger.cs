using UnityEngine;

public class WwiseStateTrigger : MonoBehaviour
{
    public AK.Wwise.State state;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FPSController>() != null)
            state.SetValue();
    }
}
