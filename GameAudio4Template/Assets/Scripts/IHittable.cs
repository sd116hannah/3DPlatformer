using UnityEngine;

public interface IHittable
{
    void OnPaintballHit(Color paintColor, Vector3 hitPoint);
}