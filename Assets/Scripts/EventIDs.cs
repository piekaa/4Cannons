public class EventIDs
{
	public const string Test = "test";

	public const string MouseClick = "MouseClick";


//	public enum Facebook
//	{
//		LoggedIn,
//		LoadedScores,
//		LogInFailed,
//		LoadScoreFailed,
//		PictureLoaded,
//		ScoresSaved,
//		LoadingScores,
//		FacebookInitComplete,
//		TokenExpireIn,
//		CheckPermissionsError,
//		PermissionsNotGranted,
//
//	}



	public class Facebook
	{
		public const string LoggedIn = "Facebook.LoggedIn";
		public const string LoadedScores = "Facebook.LoadedScores";
		public const string LogInFailed = "Facebook.LogInFailed";
		public const string LoadScoreFailed = "Facebook.LoadScoreFailed";
		public const string PictureLoaded = "Facebook.PictureLoaded";
		public const string ScoresSaved = "Facebook.ScoresSaved";
		public const string LoadingScores = "Facebook.LoadingScores";
		public const string FacebookInitComplete = "Facebook.FacebookInitComplete";
		public const string TokenExpireIn = "Facebook.TokenExpireIn";
		public const string CheckPermissionsError = "Facebook.CheckPermissionsError";
		public const string PermissionsNotGranted = "Facebook.PermissionsNotGranted";
	}


//	public enum Lifecycle
//	{
//		FirstUpdate,
//		StateChange,
//	}
//
	public class Lifecycle
	{
		public const string FirstUpdate = "Lifecycle.FirstUpdate";
		public const string StateChange = "Lifecycle.StateChange";
	}


//	public enum Game
//	{
//		Reset,
//		Start,
//		StartCountdown,
//		CountdownEnd,
//		End,
//	}

	public class Game
	{
		public const string Reset = "Game.Reset";
		public const string Start = "Game.Start"; 
		public const string StartCountdown = "Game.StartCountdown";	
		public const string CountdownEnd = "Game.CountdownEnd";
		public const string End = "Game.End";
	}


//	public enum Gameplay
//	{
//		ShootComplete,
//	}

	public class Gameplay
	{
		public const string ShootComplete = "Gameplay.ShootComplete";
	}

//	public enum Time
//	{
//		Tick,
//		NextLevel,
//	}


	public class Time
	{
		public const string Tick = "Time.Tick";
		public const string NextLevel = "Time.NextLevel";
	}


//	public enum GameplayUI
//	{
//		StartButton,
//		PlayAgainButton,
//		BackToMenuButton,
//	}

	public class GameplayUI
	{
		public const string StartButton = "GameplayUI.StartButton";
		public const string PlayAgainButton = "GampleyUI.PlayAgainButton";
		public const string BackToMenuButton = "GameplayUI.BackToMenuButton";
	}


//	public enum Menu
//	{
//		Play,
//		RefreshScores,
//		Info,
//	}

	public class Menu
	{
		public const string Play = "Menu.Play";
		public const string RefreshScores = "Menu.RefreshScores";
		public const string Info = "Menu.Info";
	}


//	public enum BallCollision
//	{
//		Regular,
//		Trap,
//		Dolar,
//		Dynamite,
//		Clock,
//	}

	public class BallCollision
	{
		public const string Regular = "BallCollision.Regular";
		public const string Trap = "BallCollision.Trap";
		public const string Dolar = "BallCollision.Dolar";
		public const string Dynamite = "BallCollision.Dynamite";
		public const string Clock = "BallCollision.Clock";
	}

}
 