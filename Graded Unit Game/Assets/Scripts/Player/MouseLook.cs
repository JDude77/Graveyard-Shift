using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    #region Attributes
    private float mouseSensitivity = 100f;
    private Transform playerBody;
    private float xRotation = 0f;
    private GameObject player;
    private bool swivel;
    #endregion

    #region Getters & Setters
    #region Mouse Sensitivity
    //Getter
    public float getMouseSensitivity()
    {
        return mouseSensitivity;
    }//End mouse sensitivity getter
    
    //Setter
    public void setMouseSensitivity(float newMouseSensitivity)
    {
        mouseSensitivity = newMouseSensitivity;
    }//End mouse sensitivity setter
    #endregion
    #region Player Body
    //Getter
    public Transform getPlayerBody()
    {
        return playerBody;
    }//End player body getter
    //Setter
    public void setPlayerBody(Transform newPlayerBody)
    {
        playerBody = newPlayerBody;
    }//End player body setter
    #endregion
    #region Swivel
    //Swivel Getter
    public bool getSwivel()
    {
        return swivel;
    }//End Swivel Getter
    //Swivel Setter
    public void setSwivel(bool swivel)
    {
        this.swivel = swivel;
    }//End Swivel Setter
    #endregion
    #endregion

    //Start is called before the first frame update
    private void Start()
    {
        playerBody = GetComponentInParent<CharacterController>().transform;
        Cursor.lockState = CursorLockMode.Locked;
        player = playerBody.gameObject;
    }//End Start

    // Update is called once per frame
    private void Update()
    {
        if (!player.GetComponent<PlayerInteraction>().getIsInteracting())
        {
            swivel = false;
            //Get movement of mouse according to mouse sensitivity and make sure it's the same across framerate
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            rotatePlayerHorizontal(mouseX);
            rotateCameraVertical(mouseY);
        }//End if
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }//End else

        if(swivel)
        {
            swivelCamera(player.GetComponent<PlayerInteraction>().getOther());
        }//End if
    }//End Update

    #region Behaviours
    //Handle player left and right rotation
    private void rotatePlayerHorizontal(float mouseX)
    {
        //REMEMBER TO CHANGE TO ALLOW INVERTED X CONTROL
        playerBody.Rotate(Vector3.up * mouseX);
    }//End rotatePlayerHorizontal

    //Handle camera up and down tilting
    private void rotateCameraVertical(float mouseY)
    {
        //REMEMBER TO CHANGE TO ALLOW INVERTED Y CONTROL
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }//End rotateCameraVertical

    //Make player directly face whatever they're interacting with
    public void swivelCamera(GameObject other)
    {
        //Get the camera's transform
        Transform cam = GetComponentInChildren<Camera>().gameObject.transform;
        //Get the other object's transform
        Transform target = other.transform;
        //If the angle isn't facing the target yet
        if (Vector3.Angle(cam.forward, target.position - cam.position) > 1f)
        {
            Transform current = playerBody;
            current.rotation = Quaternion.Lerp(playerBody.rotation, Quaternion.LookRotation(target.position - playerBody.position), 2 * Time.deltaTime);
            Vector3 euler = current.rotation.eulerAngles;
            euler.x = 0;
            euler.z = 0;
            player.transform.rotation = Quaternion.Euler(euler);
            //Lerp between the current camera's transform and the target transform
            current = cam.transform;
            current.rotation = Quaternion.Lerp(cam.rotation, Quaternion.LookRotation(target.position - cam.position), 2 * Time.deltaTime);
            cam.LookAt(current);
        }//End if
        //If the angle is close enough to the target
        else
        {
            //Stop moving the camera
            swivel = false;
        }//End else
    }//End swivelCamera
    #endregion
}
