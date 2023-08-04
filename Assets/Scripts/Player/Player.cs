using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public bool isPaused;

    [SerializeField] private float speed;
    [SerializeField] private float runSpeeed;
    
    private Playeritems playeritems;
    private float initialSpeed;

    private bool _isRunning;
    private bool _isRolling; 
    private bool _isCutting;
    private bool _isDigging;
    private bool _isWatering;

    private Rigidbody2D rig;
    private Vector2 _direction;

    [HideInInspector]
    public int handlingOBJ;

    public Vector2 direction
    {
        get { return _direction; }
        set { _direction = value; }
    }
    
    public bool isRunning
    {
        get { return _isRunning; }
        set { _isRunning = value; }
    }
    
    public bool isRolling
    {
        get { return _isRolling; }
        set { _isRolling = value; }
    }
    
    public bool isCutting
    {
        get { return _isCutting; }
        set { _isCutting = value; }
    }
     
    public bool isDigging
    {
        get { return _isDigging; }
        set { _isDigging = value; }
    }

    public bool isWatering
    {
        get { return _isWatering; }
        set { _isWatering = value; }
    }

    
   

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        playeritems = GetComponent<Playeritems>();

        initialSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                handlingOBJ = 0;
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                handlingOBJ = 1;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                handlingOBJ = 2;
            }

            OnInput();
            OnRun();
            OnRolling();
            OnCutting();
            OnDig();
            OnWater();
        }
    }

    private void FixedUpdate()
    {
        if (!isPaused)
        {
            OnMove();
        }
           
    }

    #region Moviment


    void OnInput()
    {
        _direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void OnMove()
    {
        rig.MovePosition(rig.position + _direction * speed * Time.deltaTime);
        
    }

    void OnRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = runSpeeed;
            isRunning = true;
        } 
        
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = initialSpeed;
            isRunning = false;
        }
    }

    void OnRolling()
    {
        if (Input.GetMouseButtonDown(1))
        {
            speed = runSpeeed;
            isRolling = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            speed = runSpeeed;
            isRolling = false;
        }


    }

    void OnCutting()
    {
        if(handlingOBJ == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isCutting = true;
                speed = 0f;
            }

            if (Input.GetMouseButtonUp(0))
            {
                isCutting = false;
                speed = initialSpeed;
            }
        }
        else
        {
            isCutting = false;
        }
       
    }

    void OnDig()
    {
        if (handlingOBJ == 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isDigging = true;
                speed = 0f;

            }
            if (Input.GetMouseButtonUp(0))
            {
                isDigging = false;
                speed = initialSpeed;
            }
        
        }
        else
        {
            isDigging = false;
        }
       
    }


    void OnWater()
    {
        if (handlingOBJ == 2 )
        {
            if (Input.GetMouseButtonDown(0) && playeritems.currentWater > 0)
            {
                isWatering = true;
                speed = 0f;

            }
            if (Input.GetMouseButtonUp(0) || playeritems.currentWater < 0)
            {
                isWatering = false;
                speed = initialSpeed;
            }

            if (isWatering)
            {
                playeritems.currentWater -= 0.01f;
            }

        }
        else
        {
            isWatering = false;
        }
    }

    #endregion
}

