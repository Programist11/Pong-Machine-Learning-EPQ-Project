using UnityEngine;

public class Basic : MonoBehaviour
{
    public float speed = 0f;
    void Update()
    {
        float moveVertical = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(0, 0, moveVertical * speed * Time.deltaTime));
    }
}
