using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	// "public" vals
	[SerializeField] private float speed = 10.0f;
	[SerializeField] private float jumpForce = 500.0f;
	[SerializeField] private float groundCheckRadius = 0.1f;
	[SerializeField] private Transform groundCheckPos;
	[SerializeField] private LayerMask whatIsGround;

	// private vals
	private Rigidbody2D rBody;
	private Animator anim;
	private bool isGrounded = false;
	private float isDieByFall = -8.0f;
	private bool isFacingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

	// physics
	private void FixedUpdate() {
		float horiz = Input.GetAxis("Horizontal");
		isGrounded = GroundCheck();

		// jump code here
		if (isGrounded && Input.GetAxis("Jump") > 0) {
			rBody.AddForce(new Vector2(0.0f, jumpForce));
			isGrounded = false;
		}

		rBody.velocity = new Vector2(horiz * speed, rBody.velocity.y);

		// check if the sprite needs to be flipped
		if (isFacingRight && rBody.velocity.x < 0 || !isFacingRight && rBody.velocity.x > 0) {
			Flip();
		}

		// communicate with the animator
		anim.SetFloat("xVelocity", Mathf.Abs(rBody.velocity.x));
		anim.SetFloat("yVelocity", rBody.velocity.y);
		anim.SetBool("isGrounded", isGrounded);

		// animation on moving floor


		// die by fall
		if (transform.position.y < isDieByFall) {
			// reset scene
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	private bool GroundCheck() {
		return Physics2D.OverlapCircle(groundCheckPos.position, groundCheckRadius, whatIsGround);
	}

	// private void OnCollisionEnter2D(Collision2D other) {
	// 	// die when touch hazards
	// 	if (other.collider.tag == "Hazard") {
	// 		// reset scene
	// 		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	// 	}

	// 	// check if player is on the moving floor
	// 	if (transform.parent == null && other.gameObject.name == "Moving Floor") {
	// 		transform.parent = other.gameObject.transform;
	// 	}
	// }

	// private void OnCollisionExit2D(Collision2D other) {
	// 	// check if player is NOT on the moving floor
	// 	if (transform.parent != null && other.gameObject.name == "Moving Floor") {
	// 		transform.parent = null;
	// 	}
	// }

	private void Flip() {
		Vector3 temp = transform.localScale;
		temp.x *= -1;
		transform.localScale = temp;

		isFacingRight = !isFacingRight;
	}
}
