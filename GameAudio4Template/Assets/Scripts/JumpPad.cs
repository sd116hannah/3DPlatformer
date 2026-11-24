using System.Collections;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private float _bounceForce = 12f;
    [SerializeField] private float _bounceCooldown = 0.2f;

    [SerializeField] private float _squishDuration = 0.15f;

    private float _lastBounceTime = -999f;
    private Vector3 _originalScale;

    private void Awake()
    {
        _originalScale = transform.localScale;

        Collider col = GetComponent<Collider>();
        col.isTrigger = true;

        gameObject.tag = "JumpPad";
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        
        if (rb == null) return;
        if (Time.time - _lastBounceTime < _bounceCooldown) return;

        _lastBounceTime = Time.time;

        Vector3 velocity = rb.linearVelocity;
        velocity.y = 0f;
        rb.linearVelocity = velocity;
        rb.AddForce(Vector3.up * _bounceForce, ForceMode.Impulse);


        ComboLandDetector combo = other.GetComponent<ComboLandDetector>();
        if (combo != null)
        {
            combo.RegisterBounce();
        }
        
        StartCoroutine(SquishAnimation());
    }

    IEnumerator SquishAnimation()
    {
        Vector3 squished = _originalScale;
        squished.y *= 0.5f;
        transform.localScale = squished;

        yield return new WaitForSeconds(_squishDuration);

        transform.localScale = _originalScale;
    }
}
