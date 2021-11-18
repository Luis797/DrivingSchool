using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MovementControls : MonoBehaviour {
	 //Private variables//
	public Rigidbody rbody;

	//Public Variables
	public WheelCollider[] wc;
	public Transform[] tires;
	[SerializeField] float mTorque = 2500f;
	[SerializeField] float mSteer = 25f;
	[SerializeField] float mBrake = 10000f;
	[SerializeField] float mDecelerationSpeed = 1000f;
	[SerializeField] int wcTorqueLength;
	public int wcDecelerationSpeedLength;
	public Vector3 cOfMass = new Vector3(0, 0.7f, 0);
	public bool brakeAllowed;
	bool turning;
	[SerializeField] float currentSpeed;
	[SerializeField] float mSpeed;
	public float mMagnitude;
	[SerializeField] Light leftSpotLight;
	[SerializeField] Light leftPointLight;
	[SerializeField] Light rightSpotLight;
	[SerializeField] Light rightPointLight;
	[SerializeField] IndicatorLight leftIndicatorLight;
	public IndicatorLight rightIndicatorLight;
	AudioSource audioSource;
	public int[] gearRatio;

	// Use this for initialization
	void Start () {
		audioSource = gameObject.GetComponent<AudioSource>();
		audioSource.Play();
		rbody.centerOfMass = cOfMass;
	}
	void Update(){
		VehicleSound();
		HandBrake();
		RotatingTires();
		if (Input.GetKeyDown(KeyCode.Q))
		{
			SetLeftIndicator();
		}

		if (Input.GetKeyDown(KeyCode.E))
		{
			SetRightIndicator();
		}
	}
	float horizontal, vertical;
	// Update is called once per frame
	void FixedUpdate () {
		horizontal = Input.GetAxis("Horizontal");
		vertical = Input.GetAxis("Vertical");
		Movement();
		DecelerationSpeed();

	}
	private int leftIndicator = 0;
	private int rightIndicator = 0;
    private void OnDisable()
    {
		pitch = 0f;
		audioSource.pitch = pitch;
		for (int i = 0; i < wcTorqueLength; i++)
		{
			wc[i].motorTorque = 0f;
			wc[i].brakeTorque = mBrake;
		}
	}
    public void SetLeftIndicator()
	{
		leftIndicator++;
		if(leftIndicator%2 == 1)
			leftIndicatorLight.enabled = true;

		else
			leftIndicatorLight.enabled = false;

		if (rightIndicator % 2 == 1)
		{
			rightIndicator++;
			rightIndicatorLight.enabled = false ;
		}
	}
	public void SetRightIndicator()
	{
		if (leftIndicator % 2 == 1)
		{
			leftIndicator++;
			leftIndicatorLight.enabled = false;
		}
		rightIndicator++;
		if(rightIndicator%2== 1)
			rightIndicatorLight.enabled = true;
			else
			rightIndicatorLight.enabled = false;

	}
	void Movement(){
		currentSpeed = wc[2].radius * wc[2].rpm * 60/1000 * Mathf.PI;
		currentSpeed = Mathf.Round(currentSpeed);
		if(Mathf.Abs(currentSpeed) < mSpeed && rbody.velocity.magnitude <= mMagnitude){
			for(int i = 0; i < wcTorqueLength; i++){
				wc[i].motorTorque = vertical * mTorque;
				leftPointLight.intensity = 0f;
				rightPointLight.intensity = 0f;
			}
		}else{
			currentSpeed = mSpeed;
		}
		wc[0].steerAngle =horizontal * mSteer;
		wc[1].steerAngle = horizontal * mSteer;
	
		if(wc[0].steerAngle < 0 && wc[1].steerAngle < 0 && horizontal == -1){
			rightPointLight.intensity = 5f;
		}else if(wc[0].steerAngle > 0 && wc[1].steerAngle > 0 && horizontal == 1){
			leftPointLight.intensity = 5f;
		}
	}

	void DecelerationSpeed(){
		if(!brakeAllowed){
			if((currentSpeed>0 && vertical == -1) ||( currentSpeed<0 && vertical == 1) || vertical == 0)
            {
				for (int i = 0; i < wcDecelerationSpeedLength; i++)
				{
					wc[i].brakeTorque = mDecelerationSpeed;
					wc[i].motorTorque = 0;
					leftPointLight.intensity = 5f;
					rightPointLight.intensity = 5f;
				}
			}
			
		}
		if(horizontal!= 0){
			turning = true;
			float floor = 0f;
			float ceil = 20f;
			float emission = floor + Mathf.PingPong(Time.deltaTime * 2f, ceil - floor);
			if (horizontal == -1){
				
				rightPointLight.intensity = emission;
			}else if(horizontal == 1){
				
				leftPointLight.intensity = emission;
			}
		}else{
			turning = false;
			leftPointLight.intensity = 20f;
			rightPointLight.intensity = 20f;
		}
	}

	void HandBrake(){
		if(Input.GetKey(KeyCode.B)){
			brakeAllowed = true;
		}else{
			brakeAllowed = false;
		}

		if(rbody.velocity.magnitude <= 10f && brakeAllowed && turning){
			mBrake = 20f;
		}else if(brakeAllowed){
			for(int i = 0; i < wcTorqueLength; i++){
				wc[i].brakeTorque = mBrake;
				wc[i].motorTorque = 0f;
				leftPointLight.intensity = 20f;
				rightPointLight.intensity = 20f;
			}
			rbody.drag = 0.4f;
		}else if(!brakeAllowed && Mathf.Abs(vertical) == 1){
			
			for(int i = 0; i < wcTorqueLength; i++){
				wc[i].brakeTorque = 0;
				wc[i].motorTorque = mTorque;
				leftPointLight.intensity = 0f;
				rightPointLight.intensity = 0f;
			}
			rbody.drag = 0.1f;
		}
	}

	void RotatingTires(){
		//Spinning Tires
		for(int i = 0; i < wcTorqueLength; i++){
			tires[i].Rotate(wc[i].rpm/60 * 360 * Time.deltaTime, 0f, 0f);
		}
		//Steering Tires
		for(int i = 0; i < 2; i++){
			tires[i].localEulerAngles = new Vector3(tires[i].localEulerAngles.x, wc[i].steerAngle - tires[i].localEulerAngles.z, tires[i].localEulerAngles.z);
		}
	}
	float pitch = 0f;
	void VehicleSound(){
		

		pitch = (float)(Mathf.Abs(currentSpeed) / Mathf.Abs(mSpeed)) ;
		if(pitch >= 1f){
			pitch = 0.9f;
		}

		if (pitch <	 0.4f)
			pitch = 0.4f;
		audioSource.pitch = pitch;
	}
}
