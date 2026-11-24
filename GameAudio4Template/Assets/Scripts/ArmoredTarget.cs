using UnityEngine;

public class ArmoredTarget : Target
{
    [Header("Armor")]
    [Tooltip("Size multiplier per hit")]
    [SerializeField] private float shrinkPerHit = 0.85f;

    [SerializeField] private float darkenPerHit = 0.3f;

    [SerializeField] private float swayAngle = 15f;
    [SerializeField] private float swaySpeed = 1f;

    private Quaternion _startRotation;

    protected override void Awake()
    {
        base.Awake();
        _startRotation = transform.rotation;
    }

    protected override void Move()
    {
        float angle = Mathf.Sin(Time.time * swaySpeed) * swayAngle;

        transform.rotation = _startRotation * Quaternion.Euler(0f, angle, 0f);
    }

    protected override void OnHit(Color paintColor, Vector3 hitPoint)
    {
        base.OnHit(paintColor, hitPoint);

        if (HitsRemaining > 0)
        {
            transform.localScale *= shrinkPerHit;
            BaseColor = Color.Lerp(BaseColor, Color.black, darkenPerHit);
        }
    }
}
