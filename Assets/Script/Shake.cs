using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    private bool shaking = false;

    [SerializeField] private float shakeAmt;

    private void FixedUpdate(){
        if(shaking){
            Vector3 newPos;
            newPos.x = transform.position.x + Random.Range(-0.25f,0.25f) * (Time.deltaTime * shakeAmt);
            newPos.y = transform.position.y;
            newPos.z = transform.position.z;
            transform.position = newPos;
        }
    }
    public void ShakeMe(){
        StartCoroutine(nameof(ShakeNow));
    }

    IEnumerator ShakeNow(){
        Vector2 originalPos = transform.position;
        // print("hello");
        if(shaking == false){
            shaking = true;
        }

        yield return new WaitForSeconds(0.75f);
        shaking = false;
        //After finish shake replace the character to original position
        if(GetComponent<Rigidbody2D>().velocity.y < 0)
            transform.position = originalPos;
    }
}
