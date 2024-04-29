using System;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float _movementSpeed = 10f;
    public Vector3 _movementDirection;

    public event Action<GameObject> OnBecomeInvisible;

    public void InitializeBullet(Vector3 position, Quaternion rotation, Vector3 direction, string collisionLayerName = "default", float speed = 10f)
    {
        transform.position = position;
        transform.rotation = rotation;
        gameObject.layer = LayerMask.NameToLayer(collisionLayerName);

        _movementSpeed = speed;
        _movementDirection = direction;
    }

    private void Update()
    {
        transform.position += _movementDirection.normalized * _movementSpeed * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        OnBecomeInvisible?.Invoke(gameObject);
    }
}
