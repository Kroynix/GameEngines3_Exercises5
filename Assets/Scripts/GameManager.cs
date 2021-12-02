using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        float playerPosX = PlayerPrefs.GetFloat("playerPosX");
        float playerPosY = PlayerPrefs.GetFloat("playerPosY");
        float playerPosZ = PlayerPrefs.GetFloat("playerPosZ");
        //float playerRotationY = PlayerPrefs.GetFloat("playerRotationY");

        player.transform.position = new Vector3(playerPosX,playerPosY,playerPosZ);
        //player.transform.rotation = Quaternion.Euler(0, playerRotationY,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SavePlayerPos()
    {
        //Debug.Log("playerPosX" + player.transform.position.x);
        //Debug.Log("playerPosY" + player.transform.position.y);
        //Debug.Log("playerPosZ" + player.transform.position.z);

        PlayerPrefs.SetFloat("playerPosX", player.transform.position.x);
        PlayerPrefs.SetFloat("playerPosY", player.transform.position.y);
        PlayerPrefs.SetFloat("playerPosZ", player.transform.position.z);
        //PlayerPrefs.SetFloat("playerRotationY", player.transform.rotation.y);
        PlayerPrefs.Save();

    }

    public void LoadPlayerPos()
    {
        float playerPosX = PlayerPrefs.GetFloat("playerPosX");
        float playerPosY = PlayerPrefs.GetFloat("playerPosY");
        float playerPosZ = PlayerPrefs.GetFloat("playerPosZ");
        float playerRotationY = PlayerPrefs.GetFloat("playerRotationY");

        player.transform.position = new Vector3(playerPosX,playerPosY,playerPosZ);
        //player.transform.rotation = Quaternion.Euler(0, playerRotationY,0);
    }
}
