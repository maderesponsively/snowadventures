using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
	private Camera _cam;
	public Animator animator;
	public GameObject playerItem;

	[SerializeField]
	private Transform _itemPosition = null;
	private GameObject _snowBallClone;
	private PlayerSnowCollection _playSnowCollection;
	private SnowBall _snowBallScript;
	public Trajectory trajectory;

	[SerializeField]
	private float _pushForce = 4f;
	private bool _isDragging = false;
	private Vector2 _startPoint;
	private Vector2 _endPoint;
	private Vector2 _direction;
	private Vector2 _force;
	private float _distance;

	private bool facingRight = true; 

	void Start()
	{
		_cam = Camera.main;
		_playSnowCollection = GetComponent<PlayerSnowCollection>();
	}

	void Update()
	{
		if(_playSnowCollection.currentSnow >= 0.5) {
			if (Input.GetMouseButtonDown(0))
			{
				_isDragging = true;
				OnDragStart();
			}
			if (Input.GetMouseButtonUp(0))
			{
				_isDragging = false;
				OnDragEnd();
			}

			if (_isDragging)
			{
				OnDrag();
			}
		}
	}

	void OnDragStart()
	{
		_snowBallClone = Instantiate(playerItem, _itemPosition.position, Quaternion.identity);

		_snowBallScript = _snowBallClone.GetComponent<SnowBall>();
		_snowBallScript.DeactivateRb();

		_startPoint = _cam.ScreenToViewportPoint(Input.mousePosition);

		if(_snowBallClone != null) {
			trajectory.Show();
		}
	}

	void OnDrag()
	{
		if(_snowBallClone != null) {
			_snowBallClone.transform.position = _itemPosition.position;

			_endPoint = _cam.ScreenToViewportPoint(Input.mousePosition);
			_distance = Vector2.Distance(_startPoint, _endPoint) * 4;
			_direction = (_startPoint - _endPoint).normalized;

			var xMin = facingRight ? 0 : -1;
			var xMax = facingRight ? 1 : 0;

			_force = new Vector2(Mathf.Clamp(_direction.x, xMin, xMax), _direction.y) * _distance * _pushForce;

			trajectory.UpdateDots(_itemPosition.position, _force);
		} else {
			trajectory.Hide();
		}
    }

	void OnDragEnd()
	{
		if(_snowBallClone != null) {
			_snowBallScript.ActivateRb();
			_snowBallScript.Push(_force);

			if(_distance > 0.8) {
				animator.SetTrigger("Throw");
				animator.SetFloat("ThrowForce", Mathf.Clamp(_distance, 2, 3));
				animator.SetFloat("ThrowX", _direction.x);
				animator.SetFloat("ThrowY", _direction.y);
			}
		}

		var _snowValue = _playSnowCollection.currentSnow < 1f ? 0.5f : 1;
		_playSnowCollection.DecrementSnow(_snowValue);
		_snowBallScript.SetValue(_snowValue);

        trajectory.Hide();
	}

	public void OnFlip() {
		facingRight = !facingRight;
	}
}