using System;
using UnityEngine;
using UnityEngine.Events;

// 2. Abstraction
public abstract class Target : MonoBehaviour, IHittable
{
    [Header("Target Stats")]
    [Tooltip("How many paintballs it takes to break the target")]
    [SerializeField] private int maxHits = 1;

    [Tooltip("Points awarded to the player")]
    [SerializeField] private int points = 10;

    [Header("Hit Flash")]
    [Tooltip("How long the target flashes when hit")]
    [SerializeField] private float flashDuration = 0.1f;

    // Internal State
    private int _hitsRemaining;
    private Renderer _targetRenderer;
    private Color _baseColor = Color.white;
    private float _flashTimer = 0f;

    // 3. Encapsulation
    // Properties

    public int Points => points;

    public int HitsRemaining => _hitsRemaining;

    public UnityEvent onDown;

    public bool IsDefeated
    {
        get;
        private set;
    }

    protected Color BaseColor
    {
        get { return _baseColor; }
        set
        {
            _baseColor = value;
            if (_flashTimer <= 0f) SetRenderColor(_baseColor);
        }
    }

    protected virtual void Awake()
    {
        _hitsRemaining = Mathf.Max(1, maxHits);

        _targetRenderer = GetComponent<Renderer>();
        if (_targetRenderer != null )
        {
            _baseColor = _targetRenderer.material.color;
        }
    }

    protected virtual void Update()
    {
        if (IsDefeated) return;

        Move();
        UpdateFlash();
    }

    public void OnPaintballHit(Color paintColor, Vector3 hitPoint)
    {
        if (IsDefeated) return;

        _hitsRemaining--;

        OnHit(paintColor, hitPoint);

        if (_hitsRemaining <= 0)
        {
            IsDefeated = true;
            OnDefeated();
        }
    }

    protected abstract void Move();

    private void UpdateFlash()
    {
        if (_flashTimer <= 0f) return;

        _flashTimer -= Time.deltaTime;
        if (_flashTimer <= 0f)
        {
            SetRenderColor(_baseColor);
        }
    }
    
    protected virtual void OnHit(Color paintColor, Vector3 hitPoint)
    {
        Flash(paintColor); 
        AudioManager.Instance.PlayOneShotSound("BulletHit", gameObject);
    }

    protected void Flash(Color colour)
    {
        if (flashDuration <= 0f) return;

        _flashTimer = flashDuration;
        SetRenderColor(colour);
    }

    void SetRenderColor(Color colour) 
    {
        _targetRenderer.material.color = colour;
    }
    
    private void OnDefeated()
    {
        // TODO: Implement score manager call
        onDown.Invoke();
        Destroy(gameObject);
    }
}