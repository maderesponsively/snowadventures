using UnityEngine;

public class SnowBallGenerator : MonoBehaviour
{
	private Camera _cam;

	
	public GameObject snowBall;
	public Trajectory trajectory;

	private SnowBall _snowBallScript;
	private GameObject _player;
	
	[SerializeField]
	private float _pushForce = 4f;
	private bool _isDragging = false;
	private bool _isCreated = false;

	private Vector2 _startPoint;
	private Vector2 _endPoint;
	private Vector2 _direction;
	private Vector2 _force;
	private float _distance;

	void Start()
	{
		_cam = Camera.main;
		_player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update()
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

	void OnDragStart()
	{
        if (!_isCreated)
        {
			GameObject snowBallClone = Instantiate(snowBall, _player.transform.position, Quaternion.identity);

			_snowBallScript = snowBallClone.GetComponent<SnowBall>();

			_isCreated = true;

		}

		_startPoint = _cam.ScreenToWorldPoint(Input.mousePosition);

		trajectory.Show();
	}

	void OnDrag()
	{
        _endPoint = _cam.ScreenToWorldPoint(Input.mousePosition);
        _distance = Vector2.Distance(_startPoint, _endPoint);
        _direction = (_startPoint - _endPoint).normalized;
        _force = _direction * _distance * _pushForce;

        trajectory.UpdateDots(_snowBallScript.Position, _force);
    }

	void OnDragEnd()
	{
		_snowBallScript.ActivateRb();
		_snowBallScript.Push(_force);

        trajectory.Hide();
    }
}