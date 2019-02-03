using System.Collections;
using UnityEngine;

public class BulletDone : MonoBehaviour
{
    // Start is called before the first frame update
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
