using UnityEngine;
using System.Collections;

public class MachineGun : MonoBehaviour
{

    public int damage;

    public void Fire(Vector3 aimPosition)
    {
        rigidbody2D.AddForce(transform.up * 2000);
        Destroy(this.gameObject, 2);
    }

    public void AssignDamage(int damage)
    {
        this.damage = damage;
    }
    public void DoDamage(GameObject enemy)
    {
        enemy.SendMessage("TakeDamage", damage);
        Destroy(this.gameObject);
    }
}
