using UnityEngine;
using System.Collections;

public class MachineGun : MonoBehaviour
{

    void Update()
    {
        Color color = renderer.material.color;
        color.a -= 0.1f;
        renderer.material.color = color;
    }

    public void Fire(Vector3 aimPosition)
    {
        LineRenderer lr = GetComponent<LineRenderer>();
        lr.SetVertexCount(2);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, aimPosition, 100, 9);
        lr.SetPosition(0, transform.position);

        if (hit.collider != null)
        {
            lr.SetPosition(1, hit.point);

            if(hit.collider.gameObject.tag == "Enemy")
            {
                hit.collider.gameObject.SendMessage("TakeDamage", 100);
            }
        }
        else
        {
            lr.SetPosition(1, aimPosition);
        }
        Destroy(this.gameObject, 1);
    }

}
