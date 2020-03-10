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
    public LayerMask tileMask;
    public bool createBigPool = false;

    private bool _thrown = false;
    private float _throwTimeTracker = 0;
    private GamePawn _target;
    private GamePawn _thrower;


    void Update()
    {
        if (_thrown)
        {
            _throwTimeTracker += Time.deltaTime;
            self.position = new Vector3(Mathf.Lerp(_thrower.transform.position.x, _target.transform.position.x, _throwTimeTracker/travelTime),
                Mathf.Lerp(_thrower.transform.position.y, _target.transform.position.y, _throwTimeTracker / travelTime) + trajectory.Evaluate(_throwTimeTracker/ travelTime),
                Mathf.Lerp(_thrower.transform.position.z, _target.transform.position.z, _throwTimeTracker/ travelTime)) ;

            if(_throwTimeTracker > travelTime)
            {
                _thrown = false;
                _throwTimeTracker = 0;
                if(_target is PlayerCharacter)
                {
                    PlayerCharacter player = (PlayerCharacter)_target;
                    player.ReceiveDamage(damage);
                }
                _thrower.EndAction();
                Destroy(gameObject);

            }
        }
    }

    public void Throw(GamePawn target, GamePawn origin, int dmg)
    {
        self.parent = null;
        _thrown = true;

        _throwTimeTracker = 0;
        _target = target;
        _thrower = origin;
        _thrower = origin;
    }
    public virtual void OnShot()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit, tileMask);
        
        if(hit.transform != null && hit.transform.TryGetComponent<Tile>(out Tile tile))
        {
            SkillManager.instance.CreateAlcoholPool(tile, createBigPool);
        }
    }
}
