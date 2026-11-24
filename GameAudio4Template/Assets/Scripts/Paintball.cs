using System;
using UnityEngine;

public class Paintball : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Vector3 hitPoint = contact.point;
        Vector3 hitNormal = contact.normal;

        CreateSplat(hitPoint, hitNormal, collision.transform);

        // 5. Polymorphism :)
        IHittable hittable = collision.collider.GetComponent<IHittable>();
        if (hittable != null)
        {
            hittable.OnPaintballHit(Color.red, hitPoint);
        }

        Destroy(gameObject);
    }

    private void CreateSplat(Vector3 hitPoint, Vector3 hitNormal, Transform transform)
    {
        GameObject splat = GameObject.CreatePrimitive(PrimitiveType.Quad);
        splat.name = "PaintSplat";

        splat.transform.position = hitPoint + hitNormal * 0.001f;
        splat.transform.rotation = Quaternion.LookRotation(-hitNormal);

        splat.transform.Rotate(0, 0, UnityEngine.Random.Range(0f, 360f));
        splat.transform.SetParent(transform);

        Renderer renderer = splat.GetComponent<Renderer>();
        Material material = renderer.material;
        material.color = Color.green;

        Destroy(splat.GetComponent<Collider>());

        Destroy(splat, 2f);
    }
}
