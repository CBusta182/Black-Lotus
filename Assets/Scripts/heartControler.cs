using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartControler : MonoBehaviour
{
    public Animator heartAnimation;
    [SerializeField] float timeUntilShine;
    bool temp; 
    private void Start()
    {
        StartCoroutine(Wait());
    }
    private void Update()
    {
        if (temp)
        {
            StartCoroutine(Wait()); 
        }
    }
    public IEnumerator Wait()
    {
        temp = false;
        heartAnimation.SetBool("Shine", false); 
        yield return new WaitForSeconds(timeUntilShine);
        temp = true;
        heartAnimation.SetBool("Shine", true);
    }
}
