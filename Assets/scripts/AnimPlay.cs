using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimaPlay : MonoBehaviour
{
    Animator anim1;
    // Start is called before the first frame update
    void Start()
    {
        anim1 = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void Play()
    {
        anim1.SetTrigger("PlayAnim");
    }
}
