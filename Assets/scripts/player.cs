using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    private gun armaActual;
    private bool shoot;
    private bool estaDisparando;
    private bool EstaRecargando;
    private RaycastHit hit;
    public int balas;
    public int maxBalas = 100;


    // Start is called before the first frame update
    void Start()
    {

        balas = maxBalas;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shoot = true;

        }

        if (Input.GetMouseButtonUp(0))
        {
            shoot = false;
        }

        if (shoot && Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (estaDisparando || armaActual == null)
        {
            return;
        }
        if (armaActual.cadencia == 0)
        {
            shoot = false;
        }
        Ray ray = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f));

        ray.origin = armaActual.GunBarrel.position; 

        if (Physics.Raycast(ray, out hit, armaActual.alcance))
        {
            if (hit.collider.CompareTag("zombie"))
            {
                Destroy(hit.collider.gameObject);
            }
        }
        estaDisparando = true;
        StartCoroutine(CoolDown());

        
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(armaActual.cadencia);
        estaDisparando = false; 
    }

}
