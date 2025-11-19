using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] private int HP = 100;
    [SerializeField] private float deathDelay = 2f; // ← added this line

    private Animator animator;
    private UnityEngine.AI.NavMeshAgent navAgent;

    private void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;
        if (HP <= 0)
        {
            navAgent.enabled = false;
            GetComponent<Collider>().enabled = false;

            int randomValue = Random.Range(0, 2);

            if (randomValue == 0)
            {
                animator.SetTrigger("DIE1");
                Destroy(gameObject, deathDelay);
            }
            else
            {
                animator.SetTrigger("DIE2");
                Destroy(gameObject, deathDelay);
            }
        }
        else
        {
            animator.SetTrigger("DAMAGE");
        }
    }
}
