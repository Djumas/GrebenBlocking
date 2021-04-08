using UnityEngine;


public class EnemyMovement: MonoBehaviour
{
	private Animator anim;
	private UnityEngine.AI.NavMeshAgent agent;
	private Vector2 smoothDeltaPosition = Vector2.zero;
	private Vector2 velocity = Vector2.zero;

	void Start()
	{
		anim = GetComponent<Animator>();
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		agent.updatePosition = false;
	}

	void Update()
	{
		Vector3 worldDeltaPosition = agent.nextPosition - transform.position;

		float dx = Vector3.Dot(transform.right, worldDeltaPosition);
		float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
		Vector2 deltaPosition = new Vector2(dx, dy);

		float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
		smoothDeltaPosition = Vector2.Lerp(smoothDeltaPosition, deltaPosition, smooth);

		if (Time.deltaTime > 1e-5f)
			velocity = smoothDeltaPosition / Time.deltaTime;

		bool shouldMove = velocity.magnitude > 0.5f && agent.remainingDistance > agent.radius;

		anim.SetBool("isWalking", shouldMove);
		anim.SetFloat("X", velocity.x);
		anim.SetFloat("Z", velocity.y);

		LookAt lookAt = GetComponent<LookAt>();
		if (lookAt)
			lookAt.lookAtTargetPosition = agent.steeringTarget + transform.forward;
	}

	void OnAnimatorMove()
	{
		transform.position = agent.nextPosition;
	}
}