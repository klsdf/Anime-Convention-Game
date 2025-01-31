using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 3;

    private Animator anim;

    private Vector2 direction = Vector2.zero;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
       

        if (direction.magnitude > 0)
        {
            anim.SetBool("isWalking", true);
            anim.SetFloat("Horizontal", direction.x);
            anim.SetFloat("Vertical", direction.y);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

       
    }
    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        direction = new Vector2(x, y);
        transform.Translate(direction * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Pickable")
        {
            InventoryManager.Instance.AddToBag(collision.GetComponent<Pickable>().type);
            Destroy(collision.gameObject);
        }
        
    }
}
