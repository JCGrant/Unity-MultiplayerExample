using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {

    public RectTransform healthBar;

    public const int maxHealth = 100;
    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = maxHealth;

    public bool destroyOnDeath;

    private Vector3 playerStartPosition = Vector3.zero;

    public void TakeDamage(int amount) {
        if (!isServer) {
            return;
        }
        currentHealth -= amount;
        if (currentHealth <= 0) {
            Die();
        }
    }

    private void Die() {
        if (destroyOnDeath) {
            Destroy(gameObject);
        } else {
            currentHealth = maxHealth;
            RpcRespawn();
        }
    }

    void OnChangeHealth(int health) {
        healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
    }

    // Called on the Server, but invoked on the Client
    [ClientRpc]
    void RpcRespawn() {
        if (isLocalPlayer) {
            transform.position = playerStartPosition;
        }
    }
}