using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void PlayerAttack();

public abstract class Weapon
{
    protected AudioSource soundSource;
    protected AudioClip soundClip;
    public int ammo { get; set; } = 30;
    protected GameObject projectilePrefab;
    protected Transform firePoint;

    public abstract void Attack();

    public Weapon(AudioSource soundSource, AudioClip fireSound, int initialAmmo, GameObject projectilePrefab, Transform firePoint)
    {
        this.soundSource = soundSource;
        soundClip = fireSound;
        ammo = initialAmmo;
        this.projectilePrefab = projectilePrefab;
        this.firePoint = firePoint;
    }
    
}

public class Rifle : Weapon
{
    public Rifle(AudioSource soundSource, AudioClip fireSound, int initialAmmo, GameObject projectilePrefab, Transform firePoint) : base (soundSource, fireSound, initialAmmo, projectilePrefab, firePoint)
    { }

    public override void Attack()
    {
        if (ammo > 0)
        {
            soundSource.PlayOneShot(soundClip);
            ammo--;
            var bullet = GameObject.Instantiate(projectilePrefab);
            bullet.transform.parent = firePoint.transform.parent.parent;
            bullet.transform.position = firePoint.transform.position;
            bullet.transform.rotation = firePoint.transform.rotation;
            bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.transform.right * 40, ForceMode2D.Impulse);
            GameObject.Destroy(bullet, 3);
        }
    }
}

public class Shotgun : Weapon
{
    public Shotgun(AudioSource soundSource, AudioClip fireSound, int initialAmmo, GameObject projectilePrefab, Transform firePoint) : base(soundSource, fireSound, initialAmmo, projectilePrefab, firePoint)
    { }

    public override void Attack()
    {
        if (ammo > 0)
        {
            soundSource.PlayOneShot(soundClip);
            ammo--;
            for (int i = 0; i < 15; i += 5)
            {
                var pellet = GameObject.Instantiate(projectilePrefab);
                pellet.transform.parent = firePoint.transform.parent.parent;
                pellet.transform.position = firePoint.transform.position;
                pellet.transform.rotation = firePoint.transform.rotation;
                pellet.transform.Rotate(new Vector3(0, 0, i));
                pellet.GetComponent<Rigidbody2D>().AddForce(pellet.transform.right * 40, ForceMode2D.Impulse);
                GameObject.Destroy(pellet, 3);
            }
        }
    }
}
