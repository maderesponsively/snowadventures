using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
	private Camera _cam;
	public Animator animator;
	public GameObject _playerItem;

	[SerializeField]
	private Transform _itemPosition;
	private GameObject _snowBallClone;
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


	//private string _directionName;

    private void Awake()
    {

	}

	private void Start()
	{
		_cam = Camera.main;
	}

	private void Update()
	{
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

	private void OnDragStart()
	{
		_snowBallClone = Instantiate(_playerItem, _itemPosition.position, Quaternion.identity);
		_snowBallScript = _snowBallClone.GetComponent<SnowBall>();
		_snowBallScript.DeactivateRb();

		_startPoint = _cam.ScreenToViewportPoint(Input.mousePosition);

		if(_snowBallClone != null) {
			trajectory.Show();
		}
	}

	private void OnDrag()
	{
		if(_snowBallClone != null) {
			_snowBallClone.transform.position = _itemPosition.position;

			_endPoint = _cam.ScreenToViewportPoint(Input.mousePosition);
			_distance = Vector2.Distance(_startPoint, _endPoint) * 4;
			_direction = (_startPoint - _endPoint).normalized;
			_force = new Vector2(Mathf.Clamp(_direction.x, 0, 1), _direction.y) * _distance * _pushForce;

			trajectory.UpdateDots(_itemPosition.position, _force);
		} else {
			trajectory.Hide();
		}
    }

	private void OnDragEnd()
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

        trajectory.Hide();
	}
}

// var _angleRadian = Mathf.Atan2(_direction.y, _direction.x);
// var _angleDegree = _angleRadian * Mathf.Rad2Deg;
// _angleDegree >=-90f && _angleDegree <=90f ?