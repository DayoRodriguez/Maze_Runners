using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    public Token[] tokens;
    public Token chooseToken;
    public Rigidbody rbToken;
    public GameObject playerViewCamera;
    public GameObject miniMapCamera;
    Transform playerPosition;
    int index = 0;
    public float verticalInput;
    public float horizontalInput;

    // Start is called before the first frame update
    void Awake()
    {
        tokens = Resources.LoadAll<Token>("SOTokens");
    }
    void Start()
    {
        playerViewCamera = GameObject.Find("Player_View");
        miniMapCamera = GameObject.Find("Map_View");
        playerPosition = GameObject.Find(chooseToken.name).transform;
        ChooseToken();
        RotateCamera();
    }

    // Update is called once per frame
    void Update()
    {   
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        ChooseToken();
        MoveToken(chooseToken);
        RotateCamera();
        UpdateMiniMapCam();
    }

    void ChooseToken()
    {
        if(Input.GetKeyDown(KeyCode.T)) index+=1;
        if(index >= tokens.Length) index = 0;
        if(tokens[index] != null)
        {
            chooseToken = tokens[index];

            playerPosition = GameObject.Find(chooseToken.name).transform;

            if(playerViewCamera != null)
            {
                playerViewCamera.transform.SetParent(playerPosition);
                playerViewCamera.transform.position = playerPosition.position;
            }
            MoveToken(chooseToken);
        }
    }
    public void MoveToken(Token token)
    {
        //Get the Forward Camera's direction
        Vector3 cameraForward = playerViewCamera.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        Vector3 force = cameraForward * verticalInput * token.Speed * Time.deltaTime;
        rbToken = GameObject.Find(token.name).GetComponent<Rigidbody>();
        
        if(rbToken != null)
        {
            token.Move(rbToken,force);
        }
    }
    void RotateCamera()
    {
        if(playerViewCamera != null)
        {
            playerViewCamera.transform.Rotate(Vector3.up, horizontalInput * chooseToken.Speed * Time.deltaTime);
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