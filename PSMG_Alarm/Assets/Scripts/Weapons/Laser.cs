using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

    public int damage;
    void Start()
    {
        AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, new Vector3(transform.position.x, transform.position.y, -10), 0.5f);
    }

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
    }
}
