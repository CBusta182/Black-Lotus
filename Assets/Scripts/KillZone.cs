using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    [SerializeField] private HealthController hc;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("triggered");
        hc.Death(); 
    }
}
