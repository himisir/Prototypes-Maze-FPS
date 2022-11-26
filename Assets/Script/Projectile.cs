using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    GameObject projectile;
    [SerializeField] float angle;
    Rigidbody rb;
    Vector3 _tarPos;

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Vector3 tarPos = Input.mousePosition;
            tarPos = Camera.main.ScreenToWorldPoint(new Vector3(tarPos.x, tarPos.y, Camera.main.nearClipPlane));
            _tarPos = tarPos;
            Debug.DrawLine(transform.position, _tarPos, Color.red);


        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            projectile = Instantiate(prefab, transform.position, Quaternion.identity);
            rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = speed * this.transform.forward;

        }
        Launch();
        float? angle = Rotate();


    }
    float lAngle = 45;
    void Launch()
    {
        // think of it as top-down view of vectors: 
        //   we don't care about the y-component(height) of the initial and target position.
        Vector3 poPos = transform.position;

        Vector3 diff = _tarPos - poPos;
        Quaternion lookRot = Quaternion.LookRotation(new Vector3(diff.x, 0, diff.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot
        , Time.deltaTime * 5);

    }
    float speed = 5;
    float? CalculateAngle()
    {
        Vector3 dir = _tarPos - transform.position;
        float y = dir.y;
        dir.y = 0f;
        float x = dir.magnitude;
        float g = 9.81f;
        float sSqr = speed * speed;
        float uSqr = (sSqr * sSqr) - g * (g * x * x + 2 * y * sSqr);
        if (uSqr >= 0)
        {
            float root = Mathf.Sqrt(uSqr);
            float hAngle = sSqr * root;
            float lAngle = sSqr - root;
            float low = (Mathf.Atan2(lAngle, g * x) * Mathf.Rad2Deg);
            float high = (Mathf.Atan2(hAngle, g * x) * Mathf.Rad2Deg);

            return low;
        }
        else return null;

    }

    float? Rotate()
    {
        float? angle = CalculateAngle();
        if (angle != null)
        {
            transform.localEulerAngles = new Vector3(360f - (float)angle, 0f, 0f);
        }
        return angle;
    }
}
