using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HamsterController : MonoBehaviour
{

    private const string MoveEars = "MoveEars";
    private const string Blink = "Blink";
    private const string DieTrigger = "Die";

    public AudioClip explosion;
    public float AnimationSecondsCooldownLower = 5f;
    public float AnimationSecondsCooldownHigher = 10f;
    public Animator Animator;
    private float timePassed;
    private float currentCooldown = 6f;

    private AudioSource _Source;

	// Use this for initialization
	void Start ()
	{
	    _Source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    timePassed += Time.deltaTime;
	    if (timePassed > currentCooldown)
	    {
	        timePassed = 0f;
	        Animator.SetTrigger(Random.Range(0, 2) != 0 ? MoveEars : Blink);
	        currentCooldown = Random.Range(AnimationSecondsCooldownLower, AnimationSecondsCooldownHigher);
	    }
	}

    public void Explode()
    {
        Animator.SetTrigger(DieTrigger);
    }

    public void PlayExplosionSound()
    {
        _Source.PlayOneShot(explosion);
    }

}
