using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRemove : MonoBehaviour
{
    public float time = 1.5f;

    void OnEnable()
    {
        Invoke("SetInvisible", time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TimeUpdate()
    {

        
    }

    public void SetInvisible()
    {
        gameObject.SetActive(false);
    }
}
