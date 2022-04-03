using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] float damageRadius = 5;

    private void Start()
    {
        DealDamage();
    }
    void DealDamage()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, damageRadius, Vector3.up);

        for (int i = 0; i < hits.Length; i++)
        {
            IHurt hurt = hits[i].collider.GetComponent<IHurt>();
            if (hurt != null)
            {
                float dist = Vector3.Distance(transform.position, hits[i].point);
                float modifier = 1 - (dist / damageRadius);
                int damageByDist = Mathf.RoundToInt(damage * modifier);
                hurt.NormalDamage(damageByDist);
            }
                
        }
    }
}
