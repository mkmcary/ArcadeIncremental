using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonController : MonoBehaviour
{

    public string prevSceneName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // touch handle code
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                Vector3 touchPosWorld = Camera.main.ScreenToWorldPoint(touch.position);
                Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);

                Debug.Log(touchPosWorld2D.x + " " + touchPosWorld.y);

                RaycastHit2D hitInfo = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);

                if(hitInfo.collider != null)
                {
                    GameObject touched = hitInfo.transform.gameObject;
                    if(touched == gameObject)
                    {
                        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                    }
                }
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            Vector3 touchPosWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);

            RaycastHit2D hitInfo = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);

            if (hitInfo.collider != null)
            {
                GameObject touched = hitInfo.transform.gameObject;
                if (touched == gameObject)
                {
                    SceneManager.LoadScene(prevSceneName);
                }
            }
        }
    }
}
