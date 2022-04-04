using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] int damage;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
          IHurt hurt =  collision.collider.GetComponentInParent<IHurt>();
            if (hurt != null)
            {
                hurt.NormalDamage(damage);
            }
        }
        Destroy(gameObject);
    }
}
