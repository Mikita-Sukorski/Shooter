using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour {
    public Weapon _weapon;

    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private LayerMask _mask;

    void Start()
    {
        if(_camera == null)
        {
            Debug.LogError("PlayerShoot: Camera is null");
            this.enabled = false;
        }
    }

    void Update() 
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    [Client]
    void Shoot()
    {
        RaycastHit _hit;
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out _hit, _weapon.Range, _mask))
        {
            if (_hit.collider.tag == "Player")
            { 
                CmdPlayerShoot(_hit.collider.name, _weapon.Damage); 
            }
        }
    }

    [Command]
    void CmdPlayerShoot(string _id, float Damage)
    {
        Debug.Log("В игрока "+_id+" выстрелили");

        Player player = GameManager.GetPlayer( _id );
        player.TakeDamage(Damage);
    }
}
