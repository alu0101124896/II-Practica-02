using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCharacterController : MonoBehaviour {
    public float movementVelocity, rotationVelocity;
    public int score;
    float minCylinderDistance;

    // Start is called before the first frame update
    void Start() {
        movementVelocity = 2.5f;
        rotationVelocity = 50.0f;
        score = 0;
        minCylinderDistance = 3f;
    }

    // Update is called once per frame
    void Update() {
        float xAxis = Input.GetAxis("xAxis");
        float zAxis = Input.GetAxis("zAxis");
        Vector3 moveDirection = new Vector3(xAxis, 0.0f, zAxis);
        this.transform.position += moveDirection * movementVelocity * Time.deltaTime;

        float yAxis = Input.GetAxis("yAxis");
        this.transform.Rotate(0.0f, yAxis * rotationVelocity * Time.deltaTime, 0.0f);

        if (Input.GetAxis("pushAway") > 0.0f) {
            GameObject[] tipeACylinder = GameObject.FindGameObjectsWithTag("Type A Cylinder");
            foreach (GameObject cylinder in tipeACylinder) {
                float cylinderDistance = Vector3.Distance(cylinder.transform.position, this.transform.position);
                if (cylinderDistance < minCylinderDistance) {
                    Vector3 direction = cylinder.transform.position - this.transform.position;
                    cylinder.transform.Translate(direction * Time.deltaTime);
                }
            }
        }

        GameObject[] tipeBCylinder = GameObject.FindGameObjectsWithTag("Type B Cylinder");
        foreach (GameObject cylinder in tipeBCylinder) {
            float cylinderDistance = Vector3.Distance(cylinder.transform.position, this.transform.position);
            if (cylinderDistance < minCylinderDistance) {
                Vector3 direction = cylinder.transform.position - this.transform.position;
                cylinder.transform.Translate(direction * Time.deltaTime);
            }
        }
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.name == "Cylinder") {
            score++;
            other.transform.localScale *= 1.2f;
        }
    }
}
