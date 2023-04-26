using UnityEngine;
using UnityEngine.AI;

public static class NavMeshUtil
{
	public static Vector3 GetRandomPoint(Vector3 center, float maxDistance)
	{
		NavMeshHit hit;
		NavMesh.SamplePosition(Random.insideUnitSphere * maxDistance + center, out hit, maxDistance, -1);
		return hit.position;
	}
}
