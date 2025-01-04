using UnityEngine;

public class LegTemp : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * 5, ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * -5, ForceMode.Impulse);
        }
    }
}
