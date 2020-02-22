using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hyalinize : MonoBehaviour
{
    Image image;
    public float speedOfHyalinize= 0.015f; 

    // Start is called before the first frame update
    void OnEnable()
    {
        image = GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
    }

    // Update is called once per frame
    void Update()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - speedOfHyalinize);
    }
}
