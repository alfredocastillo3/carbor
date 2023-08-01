using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    Animator Animator;
    
    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        
    }


    public void Activate()
    {
        Animator.SetTrigger("Activate");
            }
}
