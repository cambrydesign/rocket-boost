using UnityEngine;

public class Oscillator : MonoBehaviour
{

    private Vector3 startingPosition;
    public Vector3 movementVector;
    [Range(0, 1)]
    public float movementFactor;
    public float period = 2f;

    // Start is called before the first frame update
    void Start()
    {
       startingPosition = transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        float cycles = Time.time / period;

        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);

        movementFactor = (rawSinWave + 1f) / 2f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
