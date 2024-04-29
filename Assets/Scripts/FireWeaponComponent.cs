using UnityEngine;

public class FireWeaponComponent : MonoBehaviour
{
    [SerializeField] private KeyCode ShootInputKey = KeyCode.Space;
    void Update()
    {
        if(Input.GetKeyDown(ShootInputKey))
        {
            BulletManager.instance.GetBullet(transform.position,transform.rotation,transform.up,gameObject);
        }
    }
}
