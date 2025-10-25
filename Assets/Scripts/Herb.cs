using UnityEngine;
using TMPro;

public class Herb : MonoBehaviour
{

    public AudioClip cherryClip;
    private TextMeshProUGUI herbText;


    private void Start()
    {
        herbText = GameObject.FindWithTag("HerbText").GetComponent<TextMeshProUGUI>();
    }

    private void OnTriggerEnter2D(Collider2D collision)

    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.herb += 1;
            player.playSFX(cherryClip);
            herbText.text = player.herb.ToString();
            Destroy(gameObject);

        }
    }
}
