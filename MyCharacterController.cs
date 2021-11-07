using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCharacterController : MonoBehaviour {
  public float movementVelocity;
  public float rotationVelocity;
  public int score;
  public float minCylinderDistance;
  public float playerForce;

  // Start is called before the first frame update
  void Start() {
    movementVelocity = 5.0f;
    rotationVelocity = 50.0f;
    score = 0;
    minCylinderDistance = 3.0f;
    playerForce = 10.0f;
  }

  // Update is called once per frame
  void FixedUpdate() {
    float xAxis = Input.GetAxis("xAxis");
    float zAxis = Input.GetAxis("zAxis");
    Vector3 move = new Vector3(xAxis, 0.0f, zAxis) * movementVelocity;
    this.GetComponent<Rigidbody>()
        .MovePosition(this.transform.position + move * Time.fixedDeltaTime);

    float yAxis = Input.GetAxis("yAxis");
    Quaternion rotate = Quaternion.Euler(
        new Vector3(0.0f, yAxis, 0.0f) * rotationVelocity * Time.fixedDeltaTime);
    this.GetComponent<Rigidbody>()
        .MoveRotation(this.GetComponent<Rigidbody>().rotation * rotate);

    if (Input.GetAxis("pushAway") > 0.0f) {
      GameObject[] tipeACylinder = GameObject.FindGameObjectsWithTag("Type A Cylinder");
      foreach (GameObject cylinder in tipeACylinder) {
        float cylinderDistance = Vector3.Distance(cylinder.transform.position, this.transform.position);
        if (cylinderDistance < minCylinderDistance) {
          Vector3 direction = cylinder.transform.position - this.transform.position;
          cylinder.GetComponent<Rigidbody>().AddForce(direction * playerForce);
        }
      }
    }

    GameObject[] tipeBCylinder = GameObject.FindGameObjectsWithTag("Type B Cylinder");
    foreach (GameObject cylinder in tipeBCylinder) {
      float cylinderDistance = Vector3.Distance(cylinder.transform.position, this.transform.position);
      if (cylinderDistance < minCylinderDistance) {
        Vector3 direction = cylinder.transform.position - this.transform.position;
        cylinder.GetComponent<Rigidbody>().AddForce(direction * playerForce);
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
