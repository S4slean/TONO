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
    private Transform _target;
    private GamePawn _thrower;
    private Tile _associatedTile;
    private int _previewId = -1;


    void Update()
    {
        if (_thrown)
        {
            _throwTimeTracker += Time.deltaTime;
            self.position = new Vector3(Mathf.Lerp(_thrower.transform.position.x , _target.position.x, _throwTimeTracker/travelTime),
                Mathf.Lerp(_thrower.transform.position.y + 1, _target.position.y + 1, _throwTimeTracker / travelTime) + trajectory.Evaluate(_throwTimeTracker/ travelTime),
                Mathf.Lerp(_thrower.transform.position.z, _target.position.z, _throwTimeTracker/ travelTime)) ;

            if(TileBelow() != _associatedTile)
            {
                _associatedTile = TileBelow();
                _associatedTile.SetProjectileOnTile(this);
            }


            if(_throwTimeTracker > travelTime)
            {
                FXPlayer.Instance.PlayFX("BottleBreak", transform.position);
                _thrown = false;
                _throwTimeTracker = 0;
                if(_target.TryGetComponent<PlayerCharacter>(out PlayerCharacter player))
                {
                    player.ReceiveDamage(damage);
                }
                else if(_target.TryGetComponent<Barrel>(out Barrel barrel))
                {
                    barrel.Explode();
                }
                else if(_target.TryGetComponent<Projectiles>(out Projectiles projectile))
                {
                    projectile.OnShot();
                }
                _thrower.EndAction();
                Destroy(gameObject);

            }
        }
    }

    public void Throw(Transform target, GamePawn origin, int dmg)
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
        SoundManager.Instance.PlaySound(SoundManager.Instance.bottleBreak);
        SkillManager.instance.CreateAlcoholPool(_associatedTile, createBigPool);
        FXPlayer.Instance.PlayFX("BottleBreak", transform.position);
        Destroy(gameObject);
    }

    public void OnShotPreview()
    {
        if (createBigPool)
        {
            List<Tile> tiles = _associatedTile.GetFreeNeighbours();
            tiles.Add(_associatedTile);
            StartCoroutine(HighLightAtEndFrame(tiles, HighlightMode.ExplosionPreview));
        }
        else
        {
            StartCoroutine(HighLightAtEndFrame(new List<Tile>() { _associatedTile}, HighlightMode.ExplosionPreview));
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

    IEnumerator HighLightAtEndFrame(List<Tile> tilesToHighlight, HighlightMode highlightMode)
    {
        yield return new WaitForEndOfFrame();
        _previewId = Highlight_Manager.instance.ShowHighlight(tilesToHighlight, highlightMode);
        print("SHOW BOTTLE RANGE");
    }

    public int GetPreviewID()
    {
        return _previewId;
    }
}
