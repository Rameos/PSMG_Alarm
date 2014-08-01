using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour {

    public int damage;

    public void Fire(Vector3 aimPosition)
    {
        transform.Rotate(0, 0, 90);
        rigidbody2D.AddForce(transform.right * 1000);
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
