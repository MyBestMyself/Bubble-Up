using System.Net;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using System.Timers;
using Unity.VisualScripting;
using System;

public class TestPlayerScript : MonoBehaviour
{
	public float speed = 5;
	public float jump_speed = -8;
	public float acceleration = .8f;
	public float friction = 0.20f;
	public float base_gravity = 12;
	public int max_bubble_count = 3;
	//There's something about a global bubble count idk
	public int bubble_count=2;
	bool jumping = false;
	bool dead = false;
	int jump_count = 0;

	/*var gravity = base_gravity*/
	float max_fall_speed = -10;
	int coyote_frames = 5;
	//bool coyote = false;
	bool last_floor = false;

	[SerializeField] Collider2D[] collision_shapes = new Collider2D[3];
	//bool invulnerable = false;
	bool stretch = false;
	bool squash = false;
	//Components added to replace children
	Rigidbody2D rb;
	float CoyoteTimer;
    float DeathTimer = 0f;
    float DamageTimer = 0f;
    [SerializeField] AudioSource PopSound;
    [SerializeField] AudioSource GameOverSound;
	[SerializeField] AudioSource JumpSound;
	[SerializeField] Animator AnimatedSprite2D;
	[SerializeField] SpriteRenderer PlayerSprite;
    [SerializeField] SpriteRenderer BubbleBackSprite2D;
    [SerializeField] SpriteRenderer BubbleFrontSprite2D;
    ParticleSystem PopParticles2D;
    ParticleSystem AddParticles2D;
    [SerializeField] ParticleSystem WalkParticles2D;
    ParticleSystem JumpParticles2D;
	
    
    void Start() {
		CoyoteTimer = coyote_frames / 60.0f;
		handle_bubble_change();
		rb = GetComponent<Rigidbody2D>();
		WalkParticles2D =  gameObject.GetComponent<ParticleSystem>();
        PopParticles2D = transform.GetChild(3).GetComponent<ParticleSystem>();
        AddParticles2D = transform.GetChild(4).GetComponent<ParticleSystem>();
        JumpParticles2D = transform.GetChild(5).GetComponent<ParticleSystem>();
		collision_shapes = new CircleCollider2D[3];
		collision_shapes[0] = transform.GetChild(0).GetComponent<CircleCollider2D>();
        collision_shapes[1] = transform.GetChild(1).GetComponent<CircleCollider2D>();
        collision_shapes[2] = transform.GetChild(2).GetComponent<CircleCollider2D>();
		AnimatedSprite2D = GetComponent<Animator>();
    }
	public void handle_bubble_change() {
		//Global.bubble_count = bubble_count;
		PopSound.Play();
		foreach(Collider2D c in collision_shapes){
			c.enabled = false;
			c.gameObject.GetComponent<SpriteRenderer>().enabled = false;
			// c.gameObject.GetComponent<SpriteRenderer>().enabled = false;	//Replace this with the solution for back/front
		}
		collision_shapes[bubble_count - 1].enabled = true;
		BubbleBackSprite2D = collision_shapes[bubble_count - 1].gameObject.GetComponent<SpriteRenderer>();
		BubbleFrontSprite2D = collision_shapes[bubble_count - 1].gameObject.GetComponent<SpriteRenderer>();
		BubbleBackSprite2D.enabled = true;
		BubbleFrontSprite2D.enabled = true;
	}
    void Update()
    {
		handle_input(Time.deltaTime);
        handle_animation(Time.deltaTime);
		_physics_process(Time.deltaTime);
		//I've decided to do timers manually
		if (Coyote())
		{
			CoyoteTimer -= Time.deltaTime;
			if (CoyoteTimer <= 0)
			{
				_on_coyote_timer_timeout();
			}
		}
		if (DeathTimer > 0) { 
			DeathTimer -= Time.deltaTime;
			if (DeathTimer <= 0)
			{
				_on_death_timer_timeout();
			}
        }
		if (Invulnerable()) {
			DamageTimer -= Time.deltaTime;
			if (DamageTimer <= 0)
			{
				_on_damage_timer_timeout();
			}
        }
    }
    void handle_death() {
		dead = true;
		/*Global.*/bubble_count = 0;
		Time.timeScale = 0.5f;
		foreach(Collider2D c in collision_shapes)
			c.enabled = false;
		BubbleBackSprite2D.enabled = false;
		BubbleFrontSprite2D.enabled = false;
		DeathTimer = 1.5f;
		GameOverSound.Play();
		}

	public void handle_damage() {
		if (Invulnerable()) return;
		squash = true;
		if (bubble_count > 1) {
			pop_bubble();
			//invulnerable = true;
            rb.linearVelocityY = jump_speed * 0.5f;
			if (rb.linearVelocityX > 0) rb.linearVelocityX = -speed * 1f;
			else rb.linearVelocityX = speed * 1f;
			DamageTimer = 1f;
		}
		else {
			rb.linearVelocityY = jump_speed;
			if (rb.linearVelocityX > 0) rb.linearVelocityX = -speed * 1f;
			else rb.linearVelocityX = speed * 1f;
			handle_death();
		}
	}
	public void pop_bubble() {
		Debug.Log("Pop bubble called");
		PopParticles2D.Play();// = true;
		bubble_count -= 1;
		if (bubble_count < 1) {
			bubble_count = 0;
			handle_death();
		}
		handle_bubble_change();
	}

	public void add_bubble() {
		AddParticles2D.Play();// = true;
		bubble_count += 1;
		if (bubble_count > 3) {
			bubble_count = 0;
			handle_death();
		}
		handle_bubble_change();
	}
	void handle_input(float delta) {
		float direction = Input.GetAxis("Horizontal");
		if (direction != 0 && !Invulnerable() && !dead) rb.linearVelocityX = lerp(rb.linearVelocityX, direction * speed, acceleration * delta);
		else rb.linearVelocityX = lerp
				(rb.linearVelocityX, 0.0f, friction * delta);
		if (Input.GetKeyDown(KeyCode.B))
			pop_bubble();
		if (Input.GetKeyDown(KeyCode.Space) && !Invulnerable() && !dead) {
            Debug.Log("JUMP" + jump_count);
            if (is_on_floor() || Coyote()) {
				rb.linearVelocityY = jump_speed;
				jump_count = jump_count + 1;
				jumping = true;
				stretch = true;
				JumpParticles2D.Play(); //= true;
				JumpSound.Play();
			}
			else if (jump_count < 2) {
				rb.linearVelocityY = jump_speed;
				pop_bubble();
				jump_count = jump_count + 1;
				stretch = true;
				JumpParticles2D.Play();// = true;
				JumpSound.Play();
			}
		}
		if (!Input.GetButton("Jump") && jumping) rb.gravityScale = base_gravity * 2.5f;
		else rb.gravityScale = base_gravity;
		if (!Input.GetKeyDown(KeyCode.Space) && is_on_floor() && jumping) {
			jumping = false; 
			jump_count = 0;
			squash = true;
		}
		if (!is_on_floor() && last_floor && !jumping) {
            CoyoteTimer = coyote_frames / 60.0f;
        }
		last_floor = is_on_floor();
	}
void handle_animation(float delta) {
	if (Input.GetAxis("Horizontal") == 0f) {
			WalkParticles2D.Stop();// = true;
		}
	// WalkParticles2D.Stop();// = false;
	if (rb.linearVelocityX > 0 && !dead && !Invulnerable()) { 
		PlayerSprite.flipX = false;
		BubbleBackSprite2D.flipX = false;
		BubbleFrontSprite2D.flipX = false;
	}


	if (rb.linearVelocityX < 0 && !dead && !Invulnerable()) {
		PlayerSprite.flipX = true;
		BubbleBackSprite2D.flipX = true;
		BubbleFrontSprite2D.flipX = true;
	}

	if (Invulnerable()) AnimatedSprite2D.Play("damage");
	else if (dead) AnimatedSprite2D.Play("dead");
	else if (is_on_floor()) {	// && !jumping
		if (Input.GetAxis("Horizontal") != 0f) {
			AnimatedSprite2D.Play("run");
			WalkParticles2D.Play();// = true;
		}
		else AnimatedSprite2D.Play("idle");
	}
	else {
		if (rb.linearVelocityY > 0) {
			if (jump_count < 2)AnimatedSprite2D.Play("up");
			else AnimatedSprite2D.Play("upup");
		}
		else {
			AnimatedSprite2D.Play("down");
			stretch = false;
		}
	}

	if (stretch) {
		PlayerSprite.GetComponent<Transform>().localScale = new Vector3(0.9f, 1.1f, 1);
		BubbleBackSprite2D.GetComponent<Transform>().localScale = new Vector3(1/0.9f, 1/1.1f, 1);
		BubbleFrontSprite2D.GetComponent<Transform>().localScale = new Vector3(1/0.9f, 1/1.1f, 1);
		}


	if (squash) {
		PlayerSprite.GetComponent<Transform>().localScale = new Vector3(1.3f, 0.9f,1f);
		BubbleBackSprite2D.GetComponent<Transform>().localScale = new Vector3(1/1.3f, 1/0.9f,1f);
		BubbleFrontSprite2D.GetComponent<Transform>().localScale = new Vector3(1/1.3f, 1/0.9f,1f);
		JumpParticles2D.Play();// = true;
		squash = false;
		}
        //idk what this does nor how to fix so I commented it out 
        /*
		AnimatedSprite2D.scale.x = move_toward(AnimatedSprite2D.scale.x, 1, delta * 3);
		AnimatedSprite2D.scale.y = move_toward(AnimatedSprite2D.scale.y, 1, delta * 3);
		BubbleBackSprite2D.scale.x = move_toward(BubbleBackSprite2D.scale.x, 1, delta * 3);
		BubbleBackSprite2D.scale.y = move_toward(BubbleBackSprite2D.scale.y, 1, delta * 3);
		BubbleFrontSprite2D.scale.x = move_toward(BubbleBackSprite2D.scale.x, 1, delta * 3);
		BubbleFrontSprite2D.scale.y = move_toward(BubbleFrontSprite2D.scale.y, 1, delta * 3);*/
        // AnimatedSprite2D.size = moveToOne(AnimatedSprite2D.size.x, AnimatedSprite2D.size.y);
		Vector3 BubbleBackSpriteScale = BubbleBackSprite2D.GetComponent<Transform>().localScale;
		Vector3 BubbleFrontSpriteScale = BubbleFrontSprite2D.GetComponent<Transform>().localScale;
		Vector3 PlayerSpriteScale = PlayerSprite.GetComponent<Transform>().localScale;

        BubbleBackSprite2D.GetComponent<Transform>().localScale = moveToOne(BubbleBackSpriteScale.x, BubbleBackSpriteScale.y);
        BubbleFrontSprite2D.GetComponent<Transform>().localScale = moveToOne(BubbleFrontSpriteScale.x, BubbleFrontSpriteScale.y);
		PlayerSprite.GetComponent<Transform>().localScale = moveToOne(PlayerSpriteScale.x, PlayerSpriteScale.y);
    }
	void _physics_process(float delta) {
		/*velocity.y += gravity * delta
	velocity.y = min(velocity.y, max_fall_speed)
	handle_input(delta)
	move_and_slide()
	handle_animation(delta)*/
		rb.linearVelocityY = Mathf.Max(rb.linearVelocityY, max_fall_speed);
	}
void _on_coyote_timer_timeout() {
	
}

void _on_damage_timer_timeout() {
	
}

void _on_death_timer_timeout() { 
	Time.timeScale = 1;
	//get_tree().change_scene_to_file("res://scenes/game_over.tscn");
	}
float lerp(float a, float b, float p)
	{
		return a + (b - a) * p;
	}
	bool is_on_floor()
	{
		return rb.linearVelocityY == 0;
	}
	Vector2 moveToOne(float x, float y)
	{
		if (x - 3 * Time.deltaTime > 1) x -= 3 * Time.deltaTime;
		else if (x + 3 * Time.deltaTime < 1) x += 3 * Time.deltaTime;
		else x = 1;
        if (y - 3 * Time.deltaTime > 1) y -= 3 * Time.deltaTime;
        else if (y + 3 * Time.deltaTime < 1) y += 3 * Time.deltaTime;
        else y = 1;
		return new Vector2(x, y);
    }
	bool Coyote()
	{
		return CoyoteTimer > 0f;
	}
	bool Invulnerable()
	{
		return DamageTimer > 0f;
	}
	bool DeathTimerRunning()
	{
        return DeathTimer > 0f;
    }
}
