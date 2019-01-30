using UnityEngine;

// Tip: Followed tutorial on https://www.youtube.com/watch?v=BYL6JtUdEY0 JR
public class Grenade : MonoBehaviour
{
    public float delay = 3f;
    public float blastRadius = 8f;
    public float explosionForce = 50f;
    public float upwardModifier = 1f;

    public GameObject explosionEffect;
    public AudioSource audioSource;

    private float countdown;
    private bool hasExploded;

    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        // Update pitch on the sound JR
        audioSource.pitch += 0.02f;

        countdown -= Time.deltaTime;

        if (countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);

        var colliders = Physics.OverlapSphere(transform.position, blastRadius);
        
        foreach (var nearbyObject in colliders)
        {
            var rb = nearbyObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, blastRadius, upwardModifier);
            }
        }

        Destroy(gameObject);
    }
}