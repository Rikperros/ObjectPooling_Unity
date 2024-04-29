using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [HideInInspector]
    public static BulletManager instance;

    [SerializeField]
    private GameObject _bulletPrefab;
    [SerializeField]
    private int _bulletPoolSize = 10;

    private List<BulletBehaviour> _bulletInstances;

    private void Start()
    {
        InitializeSingleton();
        RetrieveBulletBehaviourFromChildren();
    }

    private void InitializeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void RetrieveBulletBehaviourFromChildren()
    {
        _bulletInstances = new List<BulletBehaviour>();
        foreach (BulletBehaviour l_bullet in transform.GetChild(0).GetComponentsInChildren<BulletBehaviour>(true))
        {
            _bulletInstances.Add(l_bullet);
        }
    }

    public void GetBullet(Vector3 position, Quaternion rotation, Vector3 direction, GameObject instigator)
    {
        BulletBehaviour l_bullet = _bulletInstances.Find((X) => X.gameObject.activeSelf == false);

        if(l_bullet == null)
        {
            Debug.LogError("There are no more available bullets in pool!");
            return;
        }

        l_bullet.InitializeBullet(position, rotation, direction, RetrieveCollisionMaskFromTag(instigator));
        l_bullet.OnBecomeInvisible += SetBulletForReuse;

        l_bullet.gameObject.SetActive(true);
    }

    string RetrieveCollisionMaskFromTag(GameObject instigator)
    {
        return instigator.CompareTag("Player") ? "PlayerBullet" : "EnemyBullet";
    }

    void SetBulletForReuse(GameObject Bullet)
    {
        Bullet.SetActive(false);
    }
    public void ClearBulletPool()
    {
        if(_bulletInstances != null)
            _bulletInstances.Clear();

        if (transform.GetChild(0) != null)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }
    public void GenerateBulletPool()
    {
        GameObject _bulletPool = new GameObject("Bullet Pool");
        _bulletPool.transform.parent = this.transform;
        _bulletPool.transform.position = Vector3.zero;

        for(int i = 0; i < _bulletPoolSize; i++) 
        {
            GameObject l_bullet = Instantiate<GameObject>(_bulletPrefab, _bulletPool.transform);
            l_bullet.SetActive(false);
        }
    }
}