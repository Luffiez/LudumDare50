using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] int damage;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            Debug.Log($"{gameObject.name} dealt '{damage}' damage to {collision.gameObject.name}");
            //collision.collider.GetComponent<PlayerHealth>().
        }
        Destroy(gameObject);
    }
}
