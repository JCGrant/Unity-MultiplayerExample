using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    private float playerRotateSpeed = 150.0f;
    private float playerMoveSpeed = 3.0f;

    void Update() {
        if (!isLocalPlayer) {
            return;
        }

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * playerRotateSpeed;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * playerMoveSpeed;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
    }

    public override void OnStartLocalPlayer() {
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }

}
