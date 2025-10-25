using UnityEngine;
using TMPro;

public class Cherry : MonoBehaviour
{

    public AudioClip cherryClip;
    private TextMeshProUGUI cherryText;


    private void Start()
    {
        cherryText = GameObject.FindWithTag("CherryText").GetComponent<TextMeshProUGUI>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
   
    {
        if(collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.cherry += 1;
            player.playSFX(cherryClip);
            cherryText.text = player.cherry.ToString();
            Destroy(gameObject);
            
        }
    }
}
