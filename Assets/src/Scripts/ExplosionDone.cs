using System.Collections;
using UnityEngine;

public class ExplosionDone : MonoBehaviour
{
    // Start is called before the first frame update
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(GetComponent<ParticleSystem>().main.duration - 0.02f);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    }
}