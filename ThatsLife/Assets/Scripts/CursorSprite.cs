using UnityEngine;

public class CursorSprite : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = false;
    }
    

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;

        transform.position = pos;   

        if (Input.GetMouseButtonDown(0) || (Input.GetMouseButtonDown(1)))
        {
            anim.SetTrigger("Click");
        }
    }
}
