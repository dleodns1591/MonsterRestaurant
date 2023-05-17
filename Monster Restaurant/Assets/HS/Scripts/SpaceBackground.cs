using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SpaceBackground : MonoBehaviour
{
    void Update()
    {
        gameObject.transform.Translate(Vector3.left * Time.deltaTime * 0.3f);
        if (gameObject.transform.localPosition.x <= -2716)
        {
            gameObject.transform.localPosition = new Vector3(transform.position.x + 1935, transform.localPosition.y);
        }
    }
}
