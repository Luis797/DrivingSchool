using UnityEngine;
using UnityEngine.UI;
public class Manager : MonoBehaviour
{
    public Material[] Materials;
    public Text showInfo;
    public GameObject alertImageObj;
    public GameObject car,OriPosition;
  
    private GameObject carClone;
    private float timer=0;
    private float showInfoTimer = 0;
    private bool isShowing = false;
    private bool isStart = false;
    private bool isNeedIndicator = true;
    private bool acRed, bdRed;
    private int leftIndicator = 0;
    private int rightIndicator = 0;
    private string Info;
    private int speed;
    public Text speedLabel, ScoreLabel,Distance;
    [SerializeField] int level = 1;
    [SerializeField] cameraController cameraControll;
    private int lastMinusScore=0;
    private float scoreTimer=0;
    private bool canMinus=false;
    [SerializeField] Minimap minimap;
    MovementControls movementControls;
    [SerializeField] Timer countDown;
    [SerializeField] GameObject GameOverCanvas;
    [SerializeField] GameObject LevelCompleteCanvas;
    [SerializeField] GameObject AllLevelCompleteCanvas;
    float showTime = 6f;
    float distance;
    // Use this for initialization
    void Start ()
    {
       
        ResetPosition();
       
        timer = 9;
       
    }
	
	// Update is called once per frame
	void Update ()
	{
      if((level == 1 || level == 2) && distance >= 1000)
        {
            LevelCompleteCanvas.SetActive(true);
            movementControls.enabled = false;
            PlayerPrefs.SetInt("Level", level);
            isShowing = false;
        }else if (level == 3 && distance >= 2000)
        {
            LevelCompleteCanvas.SetActive(true);
            movementControls.enabled = false;
            PlayerPrefs.SetInt("Level", level);
            isShowing = false;
        }
        timer += Time.deltaTime; 
        scoreTimer += Time.deltaTime;
        if (scoreTimer>4)
        {
            canMinus = true;
        }
	    
	    if (isShowing)         
	    {
            showInfoTimer += Time.deltaTime;
        }
	    if (showInfoTimer > showTime) 
	    {
            if (showTime == 6)
                showTime = 2.5f;
	        showInfoTimer = 0;
	        isShowing = false;
	        showInfo.color=new Color(1,0,0,0);
            alertImageObj.SetActive(false);
	    }
      
        if (timer>10)                 
        {
	        foreach (var mat in Materials)
	        {
	            mat.color = Color.gray;
	        }

	        Materials[2].color = Color.green;
	        Materials[3].color = Color.red;
	   

            bdRed = true;
            acRed = false;
        }
      
	    if (timer>20)
	    {
            foreach (var mat in Materials)
            {
                mat.color = Color.gray;
            }
	        Materials[1].color = Color.yellow;
            Materials[4].color= Color.yellow;
           

            bdRed = false;
            acRed = false;
        }
       
	    if (timer>23)
	    {
            foreach (var mat in Materials)
            {
                mat.color = Color.gray;
            }
            Materials[5].color = Color.green;
            Materials[0].color = Color.red;
          

            bdRed = false;
            acRed = true;

        }
       
        if (timer>33)
        {
            foreach (var mat in Materials)
            {
                mat.color = Color.gray;
            }
            Materials[1].color = Color.yellow;
            Materials[4].color = Color.yellow;
           

            timer = 7;

            bdRed = false;
            acRed = false;

        }

	    if (Input.GetAxis("Vertical") > 0.1f)
	    {
	        isNeedIndicator = true;
	    }else if (Input.GetAxis("Vertical") <-0.1f)
	    {
            isNeedIndicator = false;
        }

        if (Input.GetAxis("Horizontal") > 0.9f && rightIndicator % 2 == 0 &&carClone != null)
        {
            if (carClone.GetComponent<Rigidbody>().velocity.magnitude > 0.3f)
            {
                if (isNeedIndicator)
                {
                   
                    SetScore(1);
                }
                
                
            }
           
        }
        
        if (Input.GetAxis("Horizontal") < -0.9f && leftIndicator % 2 == 0 && carClone != null)
        {
            if (carClone.GetComponent<Rigidbody>().velocity.magnitude > 0.3f)
            {
                if (isNeedIndicator)
                {
                    
                    SetScore(1);
                }
               
            }

        }





        speed = (int)(movementControls.rbody.velocity.magnitude * 1.5f);
        distance += speed * Time.deltaTime;
        if(level == 1 || level == 2)
        Distance.text = (int)(distance) + "/1000";
        if(level==3)
            Distance.text = (int)(distance) + "/2000";

        speedLabel.text = speed.ToString();
        if (speed > 40)
            {
                
                SetScore(3);

            } 




        if (Info == "25" )
        {

            ShowInfo("Construction Work Speed Limit 25！ \r\n ");
            if (speed > 25)
            {
                ShowInfo("Over Speed！");
                SetScore(2);

            }


        }

       

	    if (Input.GetKeyDown(KeyCode.Q)) 
	    {
	        SetLeftIndicator();
	    }

        if (Input.GetKeyDown(KeyCode.E))
        {
            SetRightIndicator();
        }




    }


   

    public void ShowInfo(string info)  
    {
        Info=info;


  
        if (info == "1")
        {
            if (acRed)
            {
                if (level == 1)
                    ShowInfo("Traffic Light Voilation！ \r\n ");
                else
                    showInfo.text = "Traffic Light Voilation! \r\n -4";
                SetScore(4);
            }
            else
            {
              
                return;
            }
            
        }
        else if (info == "2")
        {
            if (bdRed)
            {
                if (level == 1)
                    ShowInfo("Traffic Light Voilation！ \r\n ");
                else
                    showInfo.text = "Traffic Light Voilation! \r\n -4";
                SetScore(4);
            }
            else
            {
             
                return;
            }
        }
        else if (info == "5")
        {
         
                SetScore(5);

        }
        else if (info == "4")
        {

            SetScore(4);

        }
        else 
        {
          
            showInfoTimer = 0;
            showInfo.text = info;
        }
        showInfo.color = new Color(1, 0, 0, 1);
        alertImageObj.SetActive(true);
        isShowing = true;
    }

    public void SetLeftIndicator()  
    {
        leftIndicator++;
        if (rightIndicator%2 == 1)
        {
            rightIndicator++;
        }
    }
    public void SetRightIndicator()
    {
        if (leftIndicator%2 == 1)
        {
            leftIndicator++;
        }
        
        rightIndicator++;
    }


    public void ResetPosition()  
    {
       Destroy(carClone);
        if (level == 1)
        {
            ShowInfo("Level L (Beginner Level). Try to roam around and follow rules and travel 1000m");
        }
        if (level == 2)
        {
            ShowInfo("Level P (Intermediate Level). Try to roam around and follow rules and travel 1000m and follow rule");
        }
        if (level == 3)
        {
            ShowInfo("Level P (Advance Level). Try to roam around and follow rules and travel 2000m and follow rule");
        }
        Time.timeScale = 1;
        carClone = Instantiate(car, OriPosition.transform.position, OriPosition.transform.rotation) as GameObject;
       isStart = false;
        cameraControll.car =carClone.transform;
        movementControls = carClone.GetComponent<MovementControls>();
        cameraControll.enabled = true;
        cameraControll.rigidbod = carClone.GetComponent<Rigidbody>();
        ScoreLabel.text = "40";
        if (level == 1)
        {
            ScoreLabel.text = "Free Play";

        }
        GameOverCanvas.SetActive(false);
        distance = 0f;
        showInfoTimer = 3.0f;
        movementControls.enabled = false;
        countDown.enabled = true;
        countDown.gameObject.SetActive(true);
        minimap.player = carClone.transform;
        minimap.enabled = true;
        countDown.movementControls = movementControls;
     
      
    }

    private void SetScore(int MinusScore)
    {
       
        if (MinusScore == lastMinusScore && !canMinus)
        {
           
            return;
        
        }

        lastMinusScore = MinusScore;
        if (MinusScore == 3)
        {
            if(level == 1)
            ShowInfo("Speed Limit Cross！ \r\n ");
            else
            ShowInfo("Speed Limit Cross！ \r\n -3");

        }
        if (MinusScore == 1)
        {
            if (level == 1)
                ShowInfo("Turn Indicator On (Q and E for left and right)！ \r\n ");
            else
                ShowInfo("Turn Indicator On (Q and E for left and right)! ( \r\n -1");
            
        }
        if (MinusScore == 2)
        {
            if (level == 1)
                ShowInfo("Construction Work Speed Limit 25！ \r\n ");
            else
                ShowInfo("Construction Work Speed Limit 25 \r\n -2");

        }
        if (MinusScore == 5)
        {
            if (level == 1)
                ShowInfo("Distruction of property! \r\n ");
            else
                ShowInfo("Distruction of property! \r\n -5");

        }
        if (MinusScore == 4)
        {
            if (level == 1)
                ShowInfo("Go Back To Road! \r\n ");
            else
                ShowInfo("Go Back To Road! \r\n -4");

        }
        if (level == 1) return;
        string strs = ScoreLabel.text;
        int oldScore = int.Parse(strs);
        int currentScore ;
        if (oldScore - MinusScore > 0)
        {
            currentScore = oldScore - MinusScore;
        }
        else
        {
            GameOver();
            currentScore = 0;

            carClone.GetComponent<Rigidbody>().drag = 5;


        }
        ScoreLabel.text = "" + currentScore;
        canMinus = false;
        scoreTimer = 0;
    }

    private void GameOver()
    {
        GameOverCanvas.SetActive(true);
        movementControls.enabled = false;
        //ShowInfo("Game Over");
        isShowing = false;
    }




}
