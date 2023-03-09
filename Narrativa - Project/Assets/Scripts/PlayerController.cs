using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int movementSpeed = 7;

    // El model original del player té una armadura, dues capes, una espasa i un escut
    // que es poden treure i deixar dins d'aquesta llista, així si volem representar 
    // millores activem l'armadura o les capes, i quan el player hagi de lluitar, activem
    // l'espasa (l'escut jo no el posaria)
    public List<GameObject> addOnsList;

    // Start is called before the first frame update
    void Start()
    {
        // Perque no es vegi el cursor
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        Rotate();
    }

    void Move()
    {
        Vector3 Movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        Movement *= movementSpeed;
        Movement = transform.rotation * Movement;

        transform.position += Movement * Time.deltaTime;
    }

    void Rotate()
    {
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * 2f, 0));
    }
}
