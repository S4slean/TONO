using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyBehaviour_Kamikaze : EnemieBehaviour
{

    public override void SetDestination(Tile destination, bool showHighlight = false, bool movedByPlayer = false)
    {
        //print("Destination : " + destination.transform.position);
        List<Tile> path = Pathfinder_AStar.instance.SearchForShortestPath(associatedTile, destination);
        if (path.Count == 0 || (path.Count == 1 && path[0] == _player.GetTile()))
        {
            _isDoingSomething = false;
            return;
        }


        anim.SetBool("Moving", true);
        Sequence s = DOTween.Sequence();
        foreach (Tile tile in path)
        {
            s.Append(transform.DOMove(tile.transform.position + new Vector3(0, tile.transform.localScale.y, 0), 0.3f)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    Free f = (Free)GetTile();
                    f.SetAlcoolized(true);

                    SoundManager.Instance.PlaySound(SoundManager.Instance.step);
                    associatedTile.SetPawnOnTile(null);
                    associatedTile = tile;
                    associatedTile.SetPawnOnTile(this);
                    associatedTile.rend.material = associatedTile.defaultMaterial;
                    associatedTile.highlighted = false;

                    movementPoints--;
                }));

        }

        s.OnComplete(() =>
        {
            if (movedByPlayer) PlayerManager.instance.playerCharacter.EndAction();
            _isDoingSomething = false;
            anim.SetBool("Moving", false);
        });
    }
    public override void SetRangedDestination(Tile destination, bool showHighlight = false)
    {
        //print("Destination : " + destination.transform.position);
        List<Tile> path = Pathfinder_AStar.instance.SearchForShortestPath(associatedTile, destination);

        anim.SetBool("Moving", true);
        Sequence s = DOTween.Sequence();
        foreach (Tile tile in path)
        {
            s.Append(transform.DOMove(tile.transform.position + new Vector3(0, tile.transform.localScale.y, 0), 0.3f)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    Free f = (Free)GetTile();
                    f.SetAlcoolized(true);

                    SoundManager.Instance.PlaySound(SoundManager.Instance.step);
                    associatedTile.SetPawnOnTile(null);
                    associatedTile = tile;
                    associatedTile.SetPawnOnTile(this);
                    associatedTile.rend.material = associatedTile.defaultMaterial;
                    associatedTile.highlighted = false;
                    movementPoints--;

                    if (IsInLineSight(enemyStats.rangedAttack.range))
                    {
                        s.Kill();
                    }
                }));
        }

        s.OnComplete(() =>
        {
            _isDoingSomething = false;
            anim.SetBool("Moving", false);

        });

        s.OnKill(() =>
        {
            _isDoingSomething = false;
            anim.SetBool("Moving", false);
        });
    }

}
