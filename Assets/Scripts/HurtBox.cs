using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HurtBox : Box {

    public int initialHealth = 100;

    Animator animator;
    private int health;
    private Image healthBar = null;

    private float timeLastHurt = 0;
       
    // Use this for initialization
    void Awake () {
		animator = gameObject.GetComponent<Animator>();
        health = initialHealth;

        Transform healthBarTransform = transform.Find("Information/HealthBackground/HealthBar");
        if (healthBarTransform != null)
        {
            healthBar = healthBarTransform.gameObject.GetComponent<Image>();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    protected new void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        // Debug.Log("Hit from: " + collision.collider.name + ", to: " + gameObject.name);

        HitBox hitBox = collision.gameObject.GetComponent<HitBox>();
        if (hitBox == null) return;
        // Debug.Log("I'm " + gameObject.name + ", and I have a ControllerColliderHit with: " + collision.gameObject.name);

        // Filter small collision, they are not punches!!
        float collisionMagnitude = collision.relativeVelocity.magnitude + collision.impulse.magnitude;
        if (collisionMagnitude < 2.5)
        {
            Debug.Log("Magnitude: " + collisionMagnitude + ", from: " + collision.gameObject.name);
            return;
        }

        // Avoid multiple collisions
        if (timeLastHurt + 0.2f > Time.realtimeSinceStartup)
        {
            return;
        }
        timeLastHurt = Time.realtimeSinceStartup;

        
        updateHealth(collision, collisionMagnitude);
        animator.SetTrigger("TakingPunch");
    }

    protected new void OnCollisionExit(Collision collision)
    {
        base.OnCollisionExit(collision);
        // Debug.Log("Exit from: " + collision.collider.name + ", to: " + gameObject.name);
    }

    protected void OnControllerColliderHit(ControllerColliderHit controllerColliderHit)
    {
        if (controllerColliderHit.gameObject.name == "Terrain") return;
    }

    private void updateHealth(Collision collision, float collisionMagnitude)
    {
        if (gameObject.tag == "Player")
        {
            health = Mathf.Max(0, (int)(health - collisionMagnitude * 0.2f));

            GameState.getSingleton().playerHealth = health;
        } else
        {
            health = Mathf.Max(0, (int)(health - collisionMagnitude));
        }

        if (healthBar != null)
        {
            healthBar.fillAmount = ((float) health) / initialHealth;
        }
        

        if (health == 0)
        {
            explode();

            if (gameObject.tag == "Player")
            {
                EventManager.TriggerEvent("PlayerDead");
            }
        }
        
    }

    void explode()
    {
        GameObject explosion = GameObject.Find("Explosion");
        explosion.transform.position = transform.position;
        var exp = explosion.GetComponent<ParticleSystem>();
        exp.Play();

        GameObject objectGameManager = GameObject.Find("GameManager");
        GameManager gameManager = objectGameManager.GetComponent<GameManager>();
        gameManager.destroyEnemy(gameObject);
    }

}
