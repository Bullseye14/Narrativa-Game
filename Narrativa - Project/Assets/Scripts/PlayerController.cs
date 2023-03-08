using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;

    private int speed = 7;

    // El model original del player té una armadura, dues capes, una espasa i un escut
    // que es poden treure i deixar dins d'aquesta llista, així si volem representar 
    // millores activem l'armadura o les capes, i quan el player hagi de lluitar, activem
    // l'espasa (l'escut jo no el posaria)
    public List<GameObject> addOnsList;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        Vector3 Movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        player.transform.position += Movement * speed * Time.deltaTime;
    }
}
