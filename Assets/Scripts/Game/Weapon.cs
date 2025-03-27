using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class Weapon : Gun
{
    [SerializeField] private GameObject _bulletHole;

    private PhotonView _photonView;
    private Animator _animator;
    private Transform _muzzle;
    private GameObject _shootFX;

    public override void Use()
    {
        Shoot();
    }

    void Start()
    {
        _photonView = GetComponent<PhotonView>();
        _animator = _objectInHand.GetComponent<Animator>();
    }

    private void Shoot()
    {
        _animator.Play("Shot");
        Ray ray = _camera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(((GunData)_itemData).damage);
            }
            _photonView.RPC("RemoteShoot", RpcTarget.All, hitInfo.point, hitInfo.normal);
        }
    }

    [PunRPC]
    private void RemoteShoot(Vector3 impactPoint, Vector3 impactNormal)
    {
        Collider[] colliders = Physics.OverlapSphere(impactPoint, 0.01f);

        if (colliders.Length > 0)
        {
            Quaternion bulletHoleRotation = Quaternion.LookRotation(impactNormal);
            GameObject bulletHole = Instantiate(_bulletHole, impactPoint, bulletHoleRotation);
            bulletHole.transform.SetParent(colliders[0].transform);
        }
    }
}
