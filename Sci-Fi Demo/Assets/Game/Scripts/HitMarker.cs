using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitMarker : MonoBehaviour
{
    [SerializeField]
    private float Life_Time;
    public float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = Life_Time;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0.0f)
        {
            currentTime = Life_Time;
            gameObject.SetActive(false);
        }
    }
}
