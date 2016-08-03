using UnityEngine;

public class Bullet : MonoBehaviour {

    private int bulletDamage = 10;

    void OnCollisionEnter(Collision collision) {
        var hit = collision.gameObject;
        var health = hit.GetComponent<Health>();

        // Check for null since not all gameObjects have the Health script
        if (health != null) {
            health.TakeDamage(bulletDamage);
        }

        Destroy(gameObject);
    }

}
