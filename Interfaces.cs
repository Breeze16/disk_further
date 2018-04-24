
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserInterface
{
	void ChangeActionManager ();

	void GameOver ();
}

public interface ISceneController
{
	void Restart ();

	void StartGame ();

	void Pause ();

	void Resume ();
}

public interface IGamePlaying
{
	void ShootDisk ();

	void FreeDisk ();

	void NextRound ();

	void addScoreByRound ();

	float getSpeedByRound ();
}

public interface IActionManager
{
	void playDisk ();

	void clearActions ();
}
