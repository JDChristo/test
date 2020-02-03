using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    Vector2 currenttarget;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    private void Update()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Vector2.Distance(transform.position, target.position) > 0.2f)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, -1), speed * Time.deltaTime);
        }
    }
}
