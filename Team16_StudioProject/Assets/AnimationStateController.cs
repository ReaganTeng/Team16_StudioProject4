using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponentInParent<GuardStateManager>().returnState() == gameObject.GetComponentInParent<GuardStateManager>().PatrolState)
        {
            animator.SetBool("isWalking", true);
        }

        if (gameObject.GetComponentInParent<GuardStateManager>().returnState() == gameObject.GetComponentInParent<GuardStateManager>().StationState)
        {
            animator.SetBool("isWalking", false);
        }


        //animator.SetBool("Shootmode", true);



        if (gameObject.GetComponentInParent<GuardStateManager>().returnState() == gameObject.GetComponentInParent<GuardStateManager>().SearchState
            || gameObject.GetComponentInParent<GuardStateManager>().returnState() == gameObject.GetComponentInParent<GuardStateManager>().ChaseState
            || gameObject.GetComponentInParent<GuardStateManager>().returnState() == gameObject.GetComponentInParent<GuardStateManager>().SecurityState
            || gameObject.GetComponentInParent<GuardStateManager>().returnState() == gameObject.GetComponentInParent<GuardStateManager>().AlarmedState
            || gameObject.GetComponentInParent<GuardStateManager>().returnState() == gameObject.GetComponentInParent<GuardStateManager>().GunshotSoundState
            || gameObject.GetComponentInParent<GuardStateManager>().returnState() == gameObject.GetComponentInParent<GuardStateManager>().CoinState)
        {
            animator.SetBool("isSearching", true);
            animator.SetBool("isWalking", false);

            if (gameObject.GetComponentInParent<GuardStateManager>().returnState() == gameObject.GetComponentInParent<GuardStateManager>().ChaseState)
            {
                if (Vector3.Distance(gameObject.GetComponentInParent<GuardStateManager>().getplayerPos().position,
                   gameObject.GetComponentInParent<GuardStateManager>().getgenemyPos().position) <= 4.5f
                   )
                {
                    //Debug.Log("#1 STOP");
                    animator.SetBool("StoppingDistance", true);
                }
                else
                {
                    //Debug.Log("#1 RUN");
                    animator.SetBool("StoppingDistance", false);
                }
            }
            else
            {
                //Debug.Log("#1 WHERE IS HE");
                animator.SetBool("StoppingDistance", false);
            }
        }


        if (animator.GetBool("Shootmode") == true
            && animator.GetBool("isWalking") == false
            && animator.GetBool("StoppingDistance") == true
            && animator.GetBool("isSearching") == true
            )
        {
            Debug.Log("HIT FROM IDLE");
        }


        if (animator.GetBool("Shootmode") == true
           && animator.GetBool("isWalking") == false
           && animator.GetBool("StoppingDistance") == false
           && animator.GetBool("isSearching") == true
           )

        {
            Debug.Log("HIT FROM RUN");
        }


        
    }
}
