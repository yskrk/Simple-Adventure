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
	[SerializeField] private float attackRadius;
	[SerializeField] private float chargeAtkRadius;
	[SerializeField] private Transform groundCheckPos;
	[SerializeField] private Transform attackArea;
	[SerializeField] private Transform chargeAtkArea;
	[SerializeField] private LayerMask whatIsGround;
	[SerializeField] private LayerMask objectsLayer;

	// private vals
	private Rigidbody2D rBody;
	private Animator anim;
	private bool isGrounded = false;
	private bool isFacingRight = true;
	[Range(0.0f, 301.0f)]private float charge = 0.0f;
	private const float CONST_BORDER = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
    }

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

		// attack
		if (Input.GetMouseButtonDown(0)) {
			anim.SetTrigger("isAttack");
			Collider2D[] hitObject = Physics2D.OverlapCircleAll(attackArea.position, attackRadius, objectsLayer);

			foreach (Collider2D obj in hitObject) {
				// break object
				if (obj.gameObject.tag == "BarrelSmall") {
					Destroy(obj.gameObject);
				}
			}
		}

		// charge attack
		if (Input.GetMouseButton(0)) {
			charge++;
			// Debug.Log("charge: " + charge);
		}

		if (Input.GetMouseButtonUp(0) && (charge >= CONST_BORDER)) {
			anim.SetTrigger("isChargeAttack");
			Collider2D[] hitObject = Physics2D.OverlapCircleAll(chargeAtkArea.position, chargeAtkRadius, objectsLayer);

			foreach (Collider2D obj in hitObject) {
				// break object
				if (obj.gameObject.tag == "BarrelLarge") {
					Destroy(obj.gameObject);
				}
			}

			charge = 0.0f;
		} else if (Input.GetMouseButtonUp(0) && (charge <= CONST_BORDER)) {
			charge = 0.0f;
		}
	}

	private bool GroundCheck() {
		return Physics2D.OverlapCircle(groundCheckPos.position, groundCheckRadius, whatIsGround);
	}

	private void Flip() {
		Vector3 temp = transform.localScale;
		temp.x *= -1;
		transform.localScale = temp;

		isFacingRight = !isFacingRight;
	}

	// show attack areas in scene
	private void OnDrawGizmosSelected() {
		// attack
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(attackArea.position, attackRadius);

		// charge attack
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(chargeAtkArea.position, chargeAtkRadius);
	}

	private void OnCollisionEnter2D(Collision2D other) {
		// check if player is on the moving floor
		if (transform.parent == null && other.gameObject.tag == "Moving Platform") {
			transform.parent = other.gameObject.transform;
		}
	}

	private void OnCollisionExit2D(Collision2D other) {
		// check if player is NOT on the moving floor
		if (transform.parent != null && other.gameObject.tag == "Moving Platform") {
			transform.parent = null;
		}
	}

}
