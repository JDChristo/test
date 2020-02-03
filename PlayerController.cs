using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isWin, isLost;

    public GameObject smoke;
    public int starcount;
    public int totalStarCount;
    GameObject [] planets;
    Rigidbody2D rb;
    private Vector3 moveDirection;
    public float moveSpeed;
    public float directionMulti;
    bool buttonDown, onPlanet;
    void Start()
    {
        Time.timeScale = 1f;
        planets = GameObject.FindGameObjectsWithTag("Planet");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Quaternion tarRot = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 5f * directionMulti);
        foreach (GameObject planet in planets)
        {
            float dist = Vector2.Distance(planet.transform.position, transform.position);

            if(dist <= planet.GetComponent<PlanetBehaviour>().maxGravityDist)
            {
                Vector2 v = planet.transform.position - transform.position;
                
                rb.AddForce(v.normalized * /*(1 - dist / planet.GetComponent<PlanetBehaviour>().maxGravityDist) */ planet.GetComponent<PlanetBehaviour>().maxGravity);

                Vector2 gravityUp = v.normalized;
                Quaternion playerRotation = Quaternion.FromToRotation(transform.up, gravityUp) * transform.rotation;
                
                if (buttonDown && !onPlanet)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, tarRot, Time.deltaTime * 5f);
                }
                else
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, playerRotation, 5f * Time.deltaTime);
                }

            }
        }
        if (buttonDown && !onPlanet)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, tarRot, Time.deltaTime * 5f);
        }
        rb.MovePosition(rb.position + (Vector2)transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime);
    }


    private void Update()
    {
        moveDirection = new Vector3(-Input.GetAxisRaw("Horizontal"), -Input.GetAxisRaw("Vertical"), 0).normalized;

        if (Input.GetKey(KeyCode.W))
        {
            smoke.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            smoke.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            buttonDown = true;
            directionMulti = 1;
            smoke.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            buttonDown = false;
            smoke.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            buttonDown = true;
            directionMulti = -1;

            smoke.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            buttonDown = false;

            smoke.SetActive(false);
        }

        if(starcount == totalStarCount)
        {
            isWin = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Planet"))
        {
            onPlanet = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Planet"))
        {
            onPlanet = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Star"))
        {
            starcount++;
            Destroy(other.gameObject);

        }
        if (other.CompareTag("Void"))
        {
            isLost = true;
        }
        if(other.CompareTag("Planet"))
        {
            isLost = true;
        }
    }
}
