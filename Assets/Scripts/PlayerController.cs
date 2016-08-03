using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    private float playerRotateSpeed = 150.0f;
    private float playerMoveSpeed = 3.0f;

    private float bulletSpeed = 6;
    private float bulletDeathTime = 2.0f;

    void Update() {
        if (!isLocalPlayer) {
            return;
        }

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * playerRotateSpeed;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * playerMoveSpeed;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

        if (Input.GetKeyDown(KeyCode.Space)) {
            CmdFire();
        }
    }

    [Command]
    private void CmdFire() {
        var bullet = (GameObject)Instantiate(bulletPrefab,
                                             bulletSpawn.position,
                                             bulletSpawn.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

        // Spawn the bullet on all Clients
        NetworkServer.Spawn(bullet);

        Destroy(bullet, bulletDeathTime);
    }

    public override void OnStartLocalPlayer() {
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }

}
