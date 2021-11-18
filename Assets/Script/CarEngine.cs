using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngine : MonoBehaviour {

	//private Variables
	Rigidbody rbody;
	private List<Transform> nodes;

	
	private int currentNode = 0;

	//public variables
	[SerializeField]Transform path;
	[SerializeField] Transform[] tyres;
	float mAISteer = 25f;
	float mAITorque = 2500f;
	public WheelCollider[] wheelCol;
	Vector3 cOfMass = new Vector3(0, 0.7f, 0);
	float aiCurrentSpeed;
	float mAISpeed;
	[SerializeField]float mAIMagnitude;
	int wheelColTorqueLength = 4;
	bool aiTurning;
	
	void Start () {
		rbody = gameObject.GetComponent<Rigidbody>();
		rbody.centerOfMass = cOfMass;
		///get all nodes from the path children.
		Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
		///Add all the list of nodes
		nodes = new List<Transform>();
		for(int i = 0; i < pathTransforms.Length; i++){
			if(pathTransforms[i] != path.transform){
				nodes.Add(pathTransforms[i]);
			}
		}
	}

	void Update(){
		RotateTires();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		ApplySteer();
		CarDrive();
		CheckWaypointDistance();
	}

	///<summary>
	///Apply steering to the wheels according to the node informaiton
	///</summary>
	private void ApplySteer(){
		Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
		relativeVector /= relativeVector.magnitude; // same thing as relativeVector = relativeVector / relativeVector.magnitude
		float newSteer = (relativeVector.x / relativeVector.magnitude) * mAISteer;
		wheelCol[0].steerAngle = newSteer;
		wheelCol[1].steerAngle = newSteer;
	}

	///<summary>
	///Determine the next check point
	///</summary>
	private void CheckWaypointDistance(){
		if(Vector3.Distance(transform.position, nodes[currentNode].position) < 1f){
			if(currentNode == nodes.Count - 1){
				currentNode = 0;
			}else{
				currentNode++;
			}
		}
	}

	///<summary>
	///This is used to provide torque to the motors 
	///THis check the ai current speed and provide speed if necessary
	///</summary>
	private void CarDrive(){
		///Identify the current speed of the car
		aiCurrentSpeed = wheelCol[2].radius * wheelCol[2].rpm * 60/1000 * Mathf.PI * 2;
		aiCurrentSpeed = Mathf.Round(aiCurrentSpeed);
		if(aiCurrentSpeed < mAISpeed && rbody.velocity.magnitude <= mAIMagnitude){
			for(int i = 0; i < wheelColTorqueLength; i++){
				wheelCol[i].motorTorque = mAITorque;
			}
		}else{
			for(int i = 0; i < wheelColTorqueLength; i++){
				wheelCol[i].motorTorque = 0;
			}
		}
	}
	///<summary>
	///Rotate the tires according to the AI car rotate info
	///</summary>
	private void RotateTires(){
		//Spinning Tires
		for(int i = 0; i < wheelColTorqueLength; i++){
			tyres[i].Rotate(wheelCol[i].rpm/60 * 360 * Time.deltaTime, 0f, 0f);
		}
		//Steering Tires
		for(int i = 0; i < 2; i++){
			tyres[i].localEulerAngles = new Vector3(tyres[i].localEulerAngles.x, wheelCol[i].steerAngle - tyres[i].localEulerAngles.z, tyres[i].localEulerAngles.z);
		}
	}

	void OnTriggerEnter(Collider other){	
			mAISpeed = 120f;
			mAITorque =Random.Range(100, 500);
			aiCurrentSpeed =Random.Range(100, 150);
			aiTurning = true;	
	}
	
}
