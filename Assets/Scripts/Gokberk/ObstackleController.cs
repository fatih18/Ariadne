using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstackleController : MonoBehaviour
{
	public PlayerController m_player;
	public float m_damage;
	


	public void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.GetComponent<PlayerController>() == m_player)
        {
			float phealth = m_player.getHealth();
			phealth -= m_damage;

			m_player.setHealth(phealth);
		}		
	}

}
