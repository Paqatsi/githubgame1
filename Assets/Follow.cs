using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    private Vector2 offset;
    public Transform player;
    
    // Start is called before the first frame update
    void Start()
    {
        offset = (Vector2)(transform.position - player.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.gameObject.GetComponent<PlayerController>().playerState != PlayerController.PlayerState.DeathState)
        {
            transform.position = new Vector3(player.transform.position.x + offset.x, player.transform.position.y + offset.y, transform.position.z);
        }
        
    }
}
