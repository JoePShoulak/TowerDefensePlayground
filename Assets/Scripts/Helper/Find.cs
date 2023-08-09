using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Find
{
    public static GameObject ClosestToTransform(List<GameObject> enemies, Transform transform)
    {
        float closestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float dist = (enemy.transform.position - transform.position).magnitude;
            if (dist < closestDistance)
            {
                closestDistance = dist;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }

    public static GameObject ClosestToEnd(List<GameObject> enemies)
    {
        float lowestDistance = Mathf.Infinity;
        int highestWaypoint = 0;
        GameObject closestEnemy = null;

        foreach (GameObject enemyObject in enemies)
        {
            Enemy enemy = enemyObject.GetComponent<Enemy>();
            if (enemy == null)
            {
                break;
            }

            float dist = enemy.DistanceToTarget;
            if (enemy.Waypoint > highestWaypoint)
            {
                highestWaypoint = enemy.Waypoint;
                lowestDistance = dist;
                closestEnemy = enemyObject;
            }
            if (enemy.Waypoint == highestWaypoint && dist < lowestDistance)
            {
                lowestDistance = dist;
                closestEnemy = enemyObject;
            }
        }

        return closestEnemy;
    }

    public static GameObject ClosestToStart(List<GameObject> enemies)
    {
        float greatestDistance = 0f;
        int lowestWaypoint = Waypoints.waypoints.Count;
        GameObject closestEnemy = null;

        foreach (GameObject enemyObject in enemies)
        {
            Enemy enemy = enemyObject.GetComponent<Enemy>();

            float dist = enemy.DistanceToTarget;
            if (enemy.Waypoint < lowestWaypoint)
            {
                lowestWaypoint = enemy.Waypoint;
                greatestDistance = dist;
                closestEnemy = enemyObject;
            }
            if (enemy.Waypoint == lowestWaypoint && dist > greatestDistance)
            {
                greatestDistance = dist;
                closestEnemy = enemyObject;
            }
        }

        return closestEnemy;
    }
}
