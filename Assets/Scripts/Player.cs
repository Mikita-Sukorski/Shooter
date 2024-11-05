using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

	[SerializeField]
	private float _maxHealth = 100f;

	[SyncVar]
	private float _currHealth;

	void Awake()
	{
		_currHealth = _maxHealth;
	}

	public void TakeDamage(float damage)
	{
		_currHealth -= damage;

		Debug.Log(transform.name + " здоровье: " + _currHealth);
	}
}
