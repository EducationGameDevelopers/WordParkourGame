using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour {

    private Animator animator;

    private void Awake()
    {
        animator = transform.GetComponent<Animator>();
    }
    void Start () {
		
	}
	
	
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            animator.SetTrigger("attack");           
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Game.Instance.a_ObjectPool.Unspawn(collision.gameObject);
        }
    }
}
