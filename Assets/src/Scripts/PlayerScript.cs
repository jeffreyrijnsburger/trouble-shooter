using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerScript : MonoBehaviour
{
    #region Stats
    public int Health = 100;
    public bool IsUnderAttack;
    public bool IsFiring;
    #endregion

    #region Ammo
    public GameObject AkBullet;
    public GameObject Grenade;
    #endregion

    // Start is called before the first frame update.
    void Start()
    {
        this.InvokeRepeatStopUnderAttack();
    }

    // Update is called once per frame.
    void Update()
    {
        // If AK is equipped.
        if (Input.GetMouseButtonDown(0) || IsFiring)
        {
            var akBulletRotation = Quaternion.AngleAxis(90, Vector3.left);
            akBulletRotation.y = transform.rotation.y;
            akBulletRotation.z = transform.rotation.z;
            akBulletRotation.w = transform.rotation.w;

            var akBullet = Instantiate(AkBullet, transform.position + (Vector3.down * 0.1f) + (transform.forward * 0.5f), akBulletRotation);

            akBullet.GetComponent<Rigidbody>().AddForce(transform.forward * 2000f);

            // Play shooting audio JR
            if (!this.IsFiring)
            {
                var akAudio = GameObject.FindWithTag("Weapon_AKM_main").GetComponents<AudioSource>();
                akAudio[0].loop = true;
                akAudio[0].Play();
            }

            this.IsFiring = true;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            var grenade = Instantiate(Grenade,
                transform.position + (Vector3.down * 0.5f) + (transform.forward * 0.7f),
                transform.rotation);

            grenade.GetComponent<Rigidbody>().AddForce(transform.forward * 500f);
        }

        if (Input.GetMouseButtonUp(0))
        {
            // Stop shooting audio JR
            if (this.IsFiring)
            {
                var akAudio = GameObject.FindWithTag("Weapon_AKM_main").GetComponents<AudioSource>();
                akAudio[0].loop = false;
                akAudio[0].Stop();
            }


            this.IsFiring = false;
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
