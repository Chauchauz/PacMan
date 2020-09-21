using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform player;
    public AudioSource[] soundFX;

    private Tweener tweener;

    private int moveLoop;
    private float startTime;
    public Vector3 endPos;
    
    void Start()
    {
        tweener = GetComponent<Tweener>();
        moveLoop = 0;
        soundFX = GetComponents<AudioSource>();
        
        player = (Transform) Instantiate(player, new Vector3(1.0f, 0.25f, -1.0f), Quaternion.Euler(90.0f, 0.0f, 0.0f));
        endPos = new Vector3(6.0f, 0.01f, -1.0f);

        //Lives
        Instantiate(player, new Vector3(2.0f, 0.0f, -29.0f), Quaternion.Euler(90.0f, 0.0f, 0.0f));
        Instantiate(player, new Vector3(4.0f, 0.0f, -29.0f), Quaternion.Euler(90.0f, 0.0f, 0.0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position == endPos)
        {
            startTime = Time.time;
            switch (moveLoop)
            {
                case 0:
                    endPos = new Vector3(6.0f, 0.25f, -5.0f);
                    break;
                case 1:
                    endPos = new Vector3(1.0f, 0.25f, -5.0f);
                    break;
                case 2:
                    endPos = new Vector3(1.0f, 0.25f, -1.0f);
                    soundFX[1].Play();
                    break;
                case 3:
                    endPos = new Vector3(6.0f, 0.25f, -1.0f);
                    soundFX[1].Play();
                    break;
            }
            moveLoop = ++moveLoop % 4;
            player.transform.Rotate(0.0f, 0.0f, -90.0f);
            soundFX[2].Play();
        }
        else
        {
            tweener.AddTween(player.transform, player.transform.position, endPos, 0.75f);
        }
    }
}
