using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] int damage;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<IHurt>().NormalDamage(damage);
        }
        Destroy(gameObject);
    }
}
