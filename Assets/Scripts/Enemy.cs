using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public Material hitMat;

    private Rigidbody rb;
    private Renderer rend;
    private Material originalMaterial;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
        originalMaterial = rend.material;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "damage")
        {
            health -= 10;

            if (health <= 0)
            {
                Die();
            }
            else
            {
                StartCoroutine(Blink());
            }
        }
    }

    void Die()
    {
        if (!this.enabled) return;

        rb.freezeRotation = false;

        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + 5);

        this.enabled = false;
    }

    IEnumerator Blink()
    {
        rend.material = hitMat;
        yield return new WaitForSeconds(0.1f);
        rend.material = originalMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
