using System.Collections;

using System.Collections.Generic;

using UnityEngine;

public class Scene1Events : MonoBehaviour

{

	public GameObject fadeScreenIn;

	public GameObject MainChaFBrother;

	void Start()

	{

		StartCoroutine(EventStarter());

	}

	IEnumerator EventStarter()

	{

		yield return new WaitForSeconds(2);

		fadeScreenIn.SetActive(false);

		MainChaFBrother.SetActive(true);

		yield return new WaitForSeconds(2);

		yield return new WaitForSeconds(2);

		MainChaFBrother.SetActive(true);

	}

}

