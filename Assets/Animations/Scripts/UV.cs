using UnityEngine;

public class UV : MonoBehaviour
{
    Material mat;
    public float a;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        mat.SetFloat("_Speed",a);
    }

    public void SpeedCan(float speed) {

        a = speed * 10;
    }
}
