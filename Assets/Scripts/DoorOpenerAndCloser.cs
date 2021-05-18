using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DoorOpenerAndCloser : MonoBehaviour
{
    Animator animator;
    bool haveSomeTimePast = true;

    void Start()    
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetButton("Interaction") && haveSomeTimePast)
        {
            InteractWithDoor();
            StartCoroutine(Timer());
        }
    }

    IEnumerator Timer()
    {
        haveSomeTimePast = false;
        yield return new WaitForSeconds(1);
        haveSomeTimePast = true;
    }

    void InteractWithDoor()
    {
        animator.SetBool("isDoorOpen", !animator.GetBool("isDoorOpen"));
    }
}
