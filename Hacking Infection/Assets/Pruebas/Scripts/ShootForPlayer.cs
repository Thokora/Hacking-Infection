using UnityEngine;

public class ShootForPlayer : MonoBehaviour
{
	[SerializeField, Range(0, 10)]
	float m_launchIntensity;

	[SerializeField]
	GameObject m_projectile;

    public void Fire()
	{
		GameObject newProjectile = Instantiate(m_projectile,transform.position,transform.rotation) as GameObject;

		if (newProjectile.GetComponent<Rigidbody2D>())
			newProjectile.GetComponent<Rigidbody2D>().AddForce(transform.forward * m_launchIntensity,ForceMode2D.Impulse);
		else if (newProjectile.GetComponent<Rigidbody>())
			newProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * m_launchIntensity, ForceMode.Impulse);
	}
}
