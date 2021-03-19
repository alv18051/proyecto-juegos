using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientoComida : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float rot = 180;
        transform.Rotate(0, rot * Time.deltaTime, 0);
    }
}
