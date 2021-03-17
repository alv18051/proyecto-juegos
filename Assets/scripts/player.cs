using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField] public gun armaActual;
    private bool shoot;
    private bool estaDisparando = false;
    private bool EstaRecargando;
    private RaycastHit hit;
    public int balas;
    public int maxBalas = 100;

    [SerializeField] Camera cam = null;

    Color debugRayColor = Color.green;
    Color debugShootRayColor = Color.blue;

    // Start is called before the first frame update
    void Start()
    {
        balas = maxBalas;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(cam.ScreenToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f)), cam.transform.forward * 10f, debugRayColor); // Solo para verificar el raycast
        if (Input.GetMouseButton(0))
        {
            debugRayColor = Color.red; // Cambiando el color del rayo debug, para verificar si presiono click
            
            Shoot();
        }
        else // Este else solo es para proposito de prueba, para cambiar el rayo de regreso a verde
        {
            debugRayColor = Color.green;
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
        estaDisparando = true;
        // Rayo azul que se dispara en la direccion del disparo, permanece en pantalla segun la cadencia del arma
        Debug.DrawRay(cam.transform.position, cam.transform.forward * armaActual.alcance, debugShootRayColor, armaActual.cadencia);

        // Disparando el rayo desde el centro de la pantalla en lugar del barril del arma, al hacerlo desde el barril se modifica la direccion
        // del rayo y no pega en el centro de la pantalla
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, armaActual.alcance))
        {
            if (hit.collider.CompareTag("zombie"))
            {
                Destroy(hit.collider.gameObject);
            }
        }
        // Rayo para visualizar hacia donde apuntaria el disparo al salir del barril del arma
        Debug.DrawRay(armaActual.GunBarrel.position, (hit.point - armaActual.GunBarrel.position), Color.yellow, armaActual.cadencia);

        StartCoroutine(CoolDown());
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(armaActual.cadencia);
        estaDisparando = false; 
    }

}
