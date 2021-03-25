using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestUI : MonoBehaviour
{
	public Text frontText;
	public Text rightText;
	public Text leftText;

	public void SetFront(BasicEnemy.EnemyColor color)
	{
		int colornum = (int)color;
		frontText.text = "Front: " + ColorToText(colornum);
		rightText.text = "Right: " + ColorToText(colornum + 1);
		leftText.text = "Left: " + ColorToText(colornum + 2);
	}

	private string ColorToText(int color)
	{
		color = color % 3;
		switch (color)
		{
			default:
			case 0:
				return "red";
			case 1:
				return "green";
			case 2:
				return "blue";
		}
	}
}
