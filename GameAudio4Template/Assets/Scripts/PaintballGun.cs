using System;
using UnityEngine;

public class PaintballGun : MonoBehaviour

{
    public Paintball paintballPrefab;
    public float shootForce = 20f;

    public float minChargeTime = 1f;
    public float maxChargeTime = 2f;
    public float maxChargeMultiplier = 2f;

    public float coolDown = 1f;

    public Transform muzzlePoint;
    public ParticleSystem chargeFX;

    public Transform gunModel;

    [Header("Wwise Gun Events")]
    public AK.Wwise.Event gunBegin;
    public AK.Wwise.Event gunRelease;
    public AK.Wwise.Event gunInterrupt;

    public AK.Wwise.Event gunSequenceB;
    public AK.Wwise.Event gunInterruptB;


    private bool _isCharging = false;
    private float _chargeStartTime = 0f;
    private Camera _playerCamera;

    private Color[] paintColors = new Color[]
    {
        new Color(1f, 0f, 0f),
        new Color(0f, 1f, 0f),
        new Color(0f, 0f, 1f),
        new Color(1f, 1f, 0f),
    };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerCamera = Camera.main;
        chargeFX.Stop();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            BeginCharge();
        }

        if (Input.GetMouseButtonUp(0) && _isCharging)
        {
            Fire();
        }

        if (Input.GetMouseButtonDown(1) && _isCharging)
        {
            Interrupt();
        }

        if (_isCharging)
        {
            UpdateChargeVisuals();
        }
    }

    private void UpdateChargeVisuals()
    {
        float chargeTime = Time.time - _chargeStartTime;
        float chargePercent = Mathf.Clamp01(chargeTime / maxChargeTime);

        float pulse = 1f + Mathf.Sin(Time.time * 10f) * 0.03f * chargePercent;
        float grow = 1f + chargePercent * 0.15f;
        gunModel.localScale = Vector3.one * pulse * grow;

        var emission = chargeFX.emission;
        emission.rateOverTime = Mathf.Lerp(5f, 50f, chargePercent);
        var main = chargeFX.main;
        main.startSpeed = Mathf.Lerp(0.5f, 3f, chargePercent);
    }

    private void Interrupt()
    {
        _isCharging = false;
        chargeFX.Stop();
        gunModel.localScale = Vector3.one; // HERE
        //gunInterrupt.Post(gameObject);
        gunInterruptB.Post(gameObject);
    }

    private void Fire()
    {
        _isCharging = false;
        float chargeTime = Time.time - _chargeStartTime;
        if (chargeTime < minChargeTime)
        {
            Interrupt();
            return;
        }

        float chargePercent = Mathf.Clamp01(chargeTime / maxChargeTime); // "Clamp01" limits between 0 and 1
        float finalForce = shootForce * Mathf.Lerp(1f, maxChargeMultiplier, chargePercent);

        chargeFX.Stop();
        gunModel.localScale = Vector3.one; // HERE
        //gunRelease.Post(gameObject);
        gunSequenceB.Post(gameObject);

        SpawnPaintBall(finalForce);
    }

    private void SpawnPaintBall(float force)
    {
        Color color = paintColors[UnityEngine.Random.Range(0, paintColors.Length)];
        Vector3 spawnPos = muzzlePoint.position;
        Quaternion spawnRot = _playerCamera.transform.rotation;

        Paintball ball = Instantiate(paintballPrefab, spawnPos, spawnRot);
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        Vector3 direction = _playerCamera.transform.forward;
        rb.AddForce(direction * force, ForceMode.Impulse);
    }

    private void BeginCharge()
    {
        _isCharging = true;
        _chargeStartTime = Time.time;

        chargeFX.Play();
        //gunBegin.Post(gameObject);
        gunSequenceB.Post(gameObject);
    }
}
