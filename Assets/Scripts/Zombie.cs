using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] private int HP = 100;
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
            int randomValue = Random.Range(0, 2);

            if (randomValue == 0)
            {
                animator.SetTrigger("DIE1");
            }
            else
            {
                animator.SetTrigger("DIE2");
            }
        }
        else
        {
            animator.SetTrigger("DAMAGE");
        }
    }


    
}
