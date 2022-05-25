using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyParticle : MonoBehaviour
{
    
    void Start()
    {
        Invoke("DestroyGameObject", 1f);
    }
    void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
