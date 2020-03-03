using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    [Header("References")]
    public Transform self;

    [Header("Stats")]
    public AnimationCurve trajectory;
    public int damage = 1;
    [Range(.1f, 10f)]public float travelTime = 5f;

    private bool _thrown = false;
    private float _throwTimeTracker = 0;
    private Vector3 _target;
    private Vector3 _origin;


    void Update()
    {
        if (_thrown)
        {
            _throwTimeTracker += Time.deltaTime;
            self.position = new Vector3(Mathf.Lerp(_origin.x, _target.x, _throwTimeTracker/travelTime),
                Mathf.Lerp(_origin.y, _target.z, _throwTimeTracker / travelTime) + trajectory.Evaluate(_throwTimeTracker/ travelTime),
                Mathf.Lerp(_origin.z, _target.z, _throwTimeTracker/ travelTime)) ;

            if(_throwTimeTracker > travelTime)
            {
                _thrown = false;
                _throwTimeTracker = 0;
            }
        }
    }

    public void Throw(Transform target, Transform origin)
    {
        self.parent = null;
        _thrown = true;

        _throwTimeTracker = 0;
        _target = target.position;
        _origin = origin.position;
    }
}
