using UnityEngine;

public class ParticleEffect : MonoBehaviour
{
    public float speed = 0.4f;
    public float lifetime = 0.7f;
    float time = 0;
    void Update()
    {
        time += Time.deltaTime;
        transform.Translate(speed * Time.deltaTime * Vector3.up);
        if(time > lifetime) Destroy(gameObject);
    }
}
