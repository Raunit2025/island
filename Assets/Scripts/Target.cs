using UnityEngine;

public class Target : MonoBehaviour
{
    public float forceAmount = 500f; 
    public float destroyDelay = 2f;

    private Rigidbody rb;
    private bool isHit = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody>(); // ensure rigidbody exists
        rb.isKinematic = true; // keep it still until hit
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isHit) return;

        if (collision.gameObject.CompareTag("Bullet"))
        {
            isHit = true;
            rb.isKinematic = false;

            // apply force in the bullet's direction
            Vector3 hitDir = collision.contacts[0].normal * -1f;
            rb.AddForce(hitDir * forceAmount);

            // destroy cube after delay
            Destroy(gameObject, destroyDelay);
        }
    }
}
