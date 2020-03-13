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
    private Tile _associatedTile;
    private int _previewId = -1;


    void Update()
    {
        if (_thrown)
        {
            _throwTimeTracker += Time.deltaTime;
            self.position = new Vector3(Mathf.Lerp(_thrower.transform.position.x , _target.transform.position.x, _throwTimeTracker/travelTime),
                Mathf.Lerp(_thrower.transform.position.y + 1, _target.transform.position.y + 1, _throwTimeTracker / travelTime) + trajectory.Evaluate(_throwTimeTracker/ travelTime),
                Mathf.Lerp(_thrower.transform.position.z, _target.transform.position.z, _throwTimeTracker/ travelTime)) ;

            if(TileBelow() != _associatedTile)
            {
                if (PlayerManager.instance.hoverMode == HoverMode.GunShotHover)
                {
                    if(_previewId > -1)
                    {
                        Highlight_Manager.instance.HideHighlight(_previewId);
                    }
                    _associatedTile = TileBelow();
                    OnShotPreview();
                }

            }


            if(_throwTimeTracker > travelTime)
            {
                _thrown = false;
                _throwTimeTracker = 0;
                if(_target is PlayerCharacter)
                {
                    PlayerCharacter player = (PlayerCharacter)_target;
                    player.ReceiveDamage(damage);
                }else if(_target is Barrel)
                {
                    Barrel barrel = _target as Barrel;
                    barrel.Explode();
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
        SkillManager.instance.CreateAlcoholPool(_associatedTile, createBigPool);
    }

    public void OnShotPreview()
    {
        if (createBigPool)
        {
            _previewId = Highlight_Manager.instance.ShowHighlight(_associatedTile.GetFreeNeighbours(), HighlightMode.ExplosionPreview);
        }
        else
        {
            _previewId = Highlight_Manager.instance.ShowHighlight(new List<Tile>() { _associatedTile}, HighlightMode.ExplosionPreview);
        }
    }

    public Tile TileBelow()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit, tileMask);

        if (hit.transform != null && hit.transform.TryGetComponent<Tile>(out Tile tile) && !(tile is Water))
        {
            return tile;
        }
        else
        {
            return null;
        }
    }
}
