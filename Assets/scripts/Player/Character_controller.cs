using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Character_controller : MonoBehaviour
{
    struct animation_stats
    {
        public string name;
        public bool is_continuous;
        public float anim_time;
    }
    private Item current_Item;
    public Animator right_animator;

    private Player_controls controls;
    void Start()
    {
        controls = new Player_controls();

        controls.Enable();
    }
 
    private void punch()
    {
        if (animation_playing)
            return;
        animation_stats stats = new animation_stats
        {
            name = "punch",
            is_continuous = true,
            anim_time = 0
        };

        animation_stats[] animation_Stats = {stats};
        StartCoroutine(play_animation(animation_Stats));
        
    }
    
    
    private bool animation_playing = false;
    IEnumerator play_animation(animation_stats[] animations)
    {
        animation_playing = true;
        foreach (animation_stats a in animations)
        {
            right_animator.Play(a.name);
            yield return new WaitForSeconds(a.anim_time + right_animator.GetCurrentAnimatorStateInfo(0).length);
            while( a.is_continuous && Input.GetMouseButton(0))
            {
                yield return new WaitForEndOfFrame();
            }
        }
        right_animator.Play("Idle");
        animation_playing = false;
    }

}
