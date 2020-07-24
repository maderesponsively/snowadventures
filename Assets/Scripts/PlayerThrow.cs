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

		trajectory.Show();
	}

	private void OnDrag()
	{
		if(_snowBallClone != null && _snowBallScript != null) {
			_snowBallClone.transform.position = _itemPosition.position;
		}

        _endPoint = _cam.ScreenToViewportPoint(Input.mousePosition);
        _distance = Vector2.Distance(_startPoint, _endPoint) * 4;
        _direction = (_startPoint - _endPoint).normalized;

		var _angleRadian = Mathf.Atan2(_direction.y, _direction.x);
		var _angleDegree = _angleRadian * Mathf.Rad2Deg;

		//if(_angleDegree >=-90f && _angleDegree <=90f) {
		if(_snowBallScript != null && (_angleDegree >=-90f && _angleDegree <=90f)) {
			_force = _direction * _distance * _pushForce;
        	trajectory.UpdateDots(_itemPosition.position, _force);
		}
    }

	private void OnDragEnd()
	{
		if(_snowBallScript != null) {
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



		//{
  //          if (angleDegrees < 90f && angleDegrees >= 60f)
  //          {
		//		if(_directionName != "up")
  //              {
		//			OnAimUp.Invoke();

		//			_directionName = "up";
		//		}
  //          }
  //          else if (angleDegrees < 60f && angleDegrees >= 40f)
  //          {
		//		if (_directionName != "diag")
		//		{
		//			OnAimDiag.Invoke();

		//			_directionName = "diag";
		//		}
  //          }
  //          else if (angleDegrees < 40f && angleDegrees >= 0f)
  //          {
		//		if(_directionName != "forward")
  //              {
		//			OnAimForward.Invoke();

		//			_directionName = "forward";
		//		}
  //          }
  //      }
