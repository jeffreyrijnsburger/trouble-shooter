using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int Health = 100;
    public bool IsUnderAttack;

    public GameObject Bullet;
    public GameObject Grenade;

    // Start is called before the first frame update.
    void Start()
    {
        this.InvokeRepeatStopUnderAttack();
    }

    // Update is called once per frame.
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var obj = Instantiate(Bullet, transform.position + (Vector3.down * 0.2f) + (transform.forward * 0.5f), transform.rotation);
            obj.GetComponent<Rigidbody>().AddForce(transform.forward * 1000f);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            var obj = Instantiate(Grenade, transform.position + (Vector3.down * 0.5f) + (transform.forward * 0.7f), transform.rotation);
            obj.GetComponent<Rigidbody>().AddForce(transform.forward * 500f);
        }
    }

    #region Health-related

    private const string StopUnderAttackInvoke = "StopUnderAttack";
    private const string RegenerateHealthInvoke = "RegenerateHealth";

    /// <summary>
    /// Invokes the check if the player is under attack. Re-invoke / call this if you want to reset the timer. JR
    /// </summary>
    public void InvokeRepeatStopUnderAttack()
    {
        this.InvokeRepeating(StopUnderAttackInvoke, 5, 5);
    }

    /// <summary>
    /// Invokes the check if the player should regenerate health. Re-invoke / call this if you want to reset the timer. JR
    /// </summary>
    public void InvokeRepeatRegenerateHealth()
    {
        this.InvokeRepeating(RegenerateHealthInvoke, 1, 1);
    }

    /// <summary>
    /// Use this to damage the player. JR
    /// </summary>
    public void SetDamage(int damageToHealth)
    {
        if (this.Health - damageToHealth < 0)
        {
            this.Death();
            return;
        }

        this.Health = this.Health - damageToHealth;
        this.IsUnderAttack = true;

        this.CancelInvoke(RegenerateHealthInvoke);
        this.InvokeRepeatStopUnderAttack();
    }

    /// <summary>
    /// Player is no longer being attacked so we can regenerate health. JR
    /// </summary>
    void StopUnderAttack()
    {
        this.IsUnderAttack = false;
        this.CancelInvoke(StopUnderAttackInvoke);
        this.InvokeRepeatRegenerateHealth();
    }

    /// <summary>
    /// Regenerates health over time. Only eligible when the player is not under attack. JR
    /// </summary>
    void RegenerateHealth()
    {
        if (this.Health < 100)
        {
            this.Health += 1;
        }
    }

    void Death()
    {
        this.IsUnderAttack = false;
        this.Health = 0;
    }

    #endregion
}
