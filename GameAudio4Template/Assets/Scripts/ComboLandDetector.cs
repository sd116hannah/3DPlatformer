using Unity.VisualScripting;
using UnityEngine;

public class ComboLandDetector : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private int _comboCount = 0;
    private bool _isAirborne = false;
    public AK.Wwise.Event bounceEvent;
    public AK.Wwise.Event landEvent;

    public void RegisterBounce()
    {
        _comboCount++;

        _isAirborne = true;

        Debug.Log($"Combo {_comboCount}");

        bounceEvent.Post(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        bool isGroundContact = false;

        foreach(ContactPoint contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                isGroundContact = true;
                break;
            }
        }

        if (!isGroundContact) return;
        if (collision.gameObject.CompareTag("JumpPad")) return;
      
        
        if (_comboCount > 0)
        {
            _comboCount = 0;
            Debug.Log("Landed on gruond, combo reset");

            landEvent.Post(gameObject);
        }

        _isAirborne = false;

        }

        private void OnCollisionExit(Collision collision)
        {
            _isAirborne = true;
        }

        public int GetComboCount()
        {
            return _comboCount;
        }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}