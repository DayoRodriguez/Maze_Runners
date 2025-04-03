using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    public Token[] playerTokens;
    public Token chooseToken;
    public Rigidbody rbToken;
    public GameObject playerViewCamera;
    public GameObject miniMapCamera;
    Transform playerPosition;
    int index = 0;
    float mouseSensivity = 100f;//Sensivilidad del mouse
    float pichLimit = 80f;//Limite del angulo Vertical
    float pich = 0f;//Rotacion Vertical
    float yaw = 0f;
    public float mouseX;
    public float mouseY;
    public float verticalInput;
    public float horizontalInput;
    public bool HasMoveToken{get;set;}
    public int counter = 0;
    // Start is called before the first frame update
    void Awake()
    {
        GetTokens(5);
    }
    void Start()
    {        
        Utils.DisableCursor();
        playerViewCamera = GameObject.Find("Player_View");
        miniMapCamera = GameObject.Find("Map_View");
        ChooseToken();
        RotateCamera();
    }

    // Update is called once per frame
    void Update()
    {   
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        mouseX=Input.GetAxis("Mouse X")*mouseSensivity*Time.deltaTime;
        mouseY=Input.GetAxis("Mouse Y")*mouseSensivity*Time.deltaTime;
        ChooseToken();
        if(!HasMoveToken)
        {
            MoveToken(chooseToken);
        }
        RotateCamera();
        UpdateMiniMapCam();
    }
    void GetTokens(int amout)
    {   
        Token[] tokens = Resources.LoadAll<Token>("SOTokens");

        playerTokens = new Token[amout];

        for (int i = 0; i < amout; i++)
        {
            int index = UnityEngine.Random.Range(0, tokens.Length);
            playerTokens[i] = tokens[index];    
        }
    }

    void ChooseToken()
    {
        if(Input.GetKeyDown(KeyCode.T)) index+=1;
        if(index >= playerTokens.Length) index = 0;
        if(playerTokens[index] != null)
        {
            chooseToken = playerTokens[index];

            playerPosition = GameObject.Find(chooseToken.name).transform;

            if(playerViewCamera != null)
            {
                playerViewCamera.transform.SetParent(playerPosition);
                playerViewCamera.transform.position = playerPosition.position;
                playerViewCamera.transform.rotation = playerPosition.rotation;
            }
            MoveToken(chooseToken);
        }
    }
    public void MoveToken(Token token)
    {
        Vector3 movementDirection = new Vector3(horizontalInput, 0,verticalInput).normalized;

        Vector3 displacement = movementDirection * 1 * Time.deltaTime;
            
        playerPosition.Translate(displacement, Space.Self);
        playerViewCamera.transform.position = playerPosition.transform.position;
        playerViewCamera.transform.rotation = playerPosition.transform.rotation;
        
        rbToken = GameObject.Find(token.name).GetComponent<Rigidbody>();
    }
    void RotateCamera()
    {
        if(playerViewCamera != null)
        {
            //Actualiza la rotacion vertical de la camera
            pich -= mouseY;
            pich = Mathf.Clamp(pich, -pichLimit,pichLimit);

            //Acumula la rotacion en el eje horizontal
            yaw += mouseX;

            //Aplica Rotacion a la Camera
            playerViewCamera.transform.localRotation = Quaternion.Euler(pich,yaw,0);
        }
    }
    void UpdateMiniMapCam()
    {
        if(chooseToken != null)
        {
            miniMapCamera.transform.position = new Vector3(playerPosition.position.x, miniMapCamera.transform.position.y, playerPosition.transform.position.z);
        }
    }
}