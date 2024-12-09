using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightGame : MonoBehaviour
{
    public float timedaynight = 1f;
    public Light ligh;
    public Gradient gradient;
    public float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = 360f / (timedaynight * 60f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
        if (gradient != null || ligh != null)
        {
            float time = Mathf.PingPong(Time.time / (timedaynight * 30f), 1f);
            ligh.color = gradient.Evaluate(time);
        }
    }
}
