using UnityEngine;
using System.Collections;

public static class PlayerPrefsManager{
	public static void SetLifes(int lifes)
	{
		PlayerPrefs.SetInt ("Lifes", lifes);
	}

	public static int GetLifes()
	{
		if (PlayerPrefs.HasKey ("Lifes"))
			return PlayerPrefs.GetInt ("Lifes");
		else
			return 3;
	}

	public static void SetLastLevel(int level)
	{
		if(level>GetLastLevel())
			PlayerPrefs.SetInt ("Last Level", level);
	}

    public static void SetUnlockedLevel(int level)
    {
        if (level > GetUnlockedLevel())
            PlayerPrefs.SetInt("Unlocked Level", level);
    }
	public static int GetLastLevel()
	{
		if (PlayerPrefs.HasKey ("Last Level"))
			return PlayerPrefs.GetInt ("Last Level");
		else
			return 1;
	}

    public static int GetUnlockedLevel()
    {
        if (PlayerPrefs.HasKey("Unlocked Level"))
            return PlayerPrefs.GetInt("Unlocked Level");
        else
            return 0;
    }
	public static void ResetLife()
	{
		SetLifes (3);
	}

	public static void ResetLastLevel()
	{
		PlayerPrefs.SetInt ("Last Level", 1);
	}

    public static void ResetUnlockedLevel()
    {
        PlayerPrefs.SetInt("Unlocked Level", 0);
    }
}
