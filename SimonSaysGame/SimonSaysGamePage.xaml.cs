using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SimonSaysGame
{
	public partial class SimonSaysGamePage : ContentPage
	{
		public SimonSaysGamePage()
		{
			InitializeComponent();
			loadFunction();
		}

		int numGuess = 0;
		//SoundPlayer[] sounds = new SoundPlayer[5];
		string[] soundStrings = new string[5];
		Random randNum = new Random();
		List<int> playerGuess = new List<int>();
		List<int> buttonPattern = new List<int>();
		int patternLength;

		private void loadFunction()
		{
			soundStrings[0] = "green.mp3";
			soundStrings[1] = "red.mp3";
			soundStrings[2] = "yellow.mp3";
			soundStrings[3] = "blue.mp3";
			soundStrings[4] = "mistake.mp3";
		}

		private async void playButtonClick(object s, EventArgs e)
		{
			greenButton.BackgroundColor = Color.Lime;
			redButton.BackgroundColor = Color.Red;
			yellowButton.BackgroundColor = Color.Yellow;
			blueButton.BackgroundColor = Color.Aqua;

			gameOverLabel.IsVisible = false;
			scoreLabel.IsVisible = false;
			playButton.IsVisible = false;
			playButton.IsEnabled = false;

			await Task.Delay(1500);
			patternLength = 1;
			numGuess = 0;
			patternGenerate();
		}
		private async void greenButtonClick(object s, EventArgs e)
		{
			playerGuess.Add(0);
			greenButton.BackgroundColor = Color.Green;
			playerAction();
			DependencyService.Get<IAudio>().PlayAudioFile("green.mp3");
			await Task.Delay(250);
			greenButton.BackgroundColor = Color.Lime;
		}
		private async void redButtonClick(object s, EventArgs e)
		{
			playerGuess.Add(1);
			redButton.BackgroundColor = Color.Maroon;
			playerAction();
			DependencyService.Get<IAudio>().PlayAudioFile("red.mp3");
			await Task.Delay(250);
			redButton.BackgroundColor = Color.Red;
		}
		private async void yellowButtonClick(object s, EventArgs e)
		{
			playerGuess.Add(2);
			yellowButton.BackgroundColor = Color.Olive;
			playerAction();
			DependencyService.Get<IAudio>().PlayAudioFile("yellow.mp3");
			await Task.Delay(250);

			yellowButton.BackgroundColor = Color.Yellow;
		}
		private async void blueButtonClick(object s, EventArgs e)
		{
			playerGuess.Add(3);
			blueButton.BackgroundColor = Color.Blue;
			playerAction();
			DependencyService.Get<IAudio>().PlayAudioFile("blue.mp3");
			await Task.Delay(250);
			blueButton.BackgroundColor = Color.Aqua;
		}

		public async void patternGenerate()
		{
			//disables buttons during the computer's turn to minimize shenanigans
			greenButton.IsEnabled = false;
			redButton.IsEnabled = false;
			blueButton.IsEnabled = false;
			yellowButton.IsEnabled = false;

			buttonPattern.Add(randNum.Next(0, 4));

			for (int i = 0; i < buttonPattern.Count; i++)
			{
				//changes colours and plays sounds to demonstrate the pattern.
				if (buttonPattern[i] == 0)
				{
					greenButton.BackgroundColor = Color.Green;
					//Refresh();
					DependencyService.Get<IAudio>().PlayAudioFile("green.mp3");
					await Task.Delay(650);
					greenButton.BackgroundColor = Color.Lime;
					//Refresh();
					await Task.Delay(650);
				}
				if (buttonPattern[i] == 1)
				{
					redButton.BackgroundColor = Color.Maroon;
					//Refresh();
					DependencyService.Get<IAudio>().PlayAudioFile("red.mp3");
					await Task.Delay(650);
					redButton.BackgroundColor = Color.Red;
					//Refresh();
					await Task.Delay(650);
				}
				if (buttonPattern[i] == 2)
				{
					yellowButton.BackgroundColor = Color.Olive;
					//Refresh();
					DependencyService.Get<IAudio>().PlayAudioFile("yellow.mp3");
					await Task.Delay(650);
					yellowButton.BackgroundColor = Color.Yellow;
					//Refresh();
					await Task.Delay(650);
				}
				if (buttonPattern[i] == 3)
				{
					blueButton.BackgroundColor = Color.Blue;
					//Refresh();
					DependencyService.Get<IAudio>().PlayAudioFile("blue.mp3");
					await Task.Delay(650);
					blueButton.BackgroundColor = Color.Aqua;
					//Refresh();
					await Task.Delay(650);
				}
			}
			//re-enables buttons so that the player can proceed
			greenButton.IsEnabled = true;
			redButton.IsEnabled = true;
			blueButton.IsEnabled = true;
			yellowButton.IsEnabled = true;

		}

		public async void playerAction()
		{
			//for (int i = 0; i < numGuess; i++)
			//{

				//this will check all of the player's guesses against the actual pattern, if any are incorrect then the player loses
			if (playerGuess[numGuess] != buttonPattern[numGuess])
				{
					// play losing sound effect and go to gameover screen
					DependencyService.Get<IAudio>().PlayAudioFile(soundStrings[4]);
					await Task.Delay(500);
					//gameover stuff
					greenButton.BackgroundColor = Color.Green;
					redButton.BackgroundColor = Color.Maroon;
					yellowButton.BackgroundColor = Color.Olive;
					blueButton.BackgroundColor = Color.Blue;
					greenButton.IsEnabled = false;
					redButton.IsEnabled = false;
					yellowButton.IsEnabled = false;
					blueButton.IsEnabled = false;

					gameOverLabel.IsVisible = true;
					scoreLabel.IsVisible = true;
					scoreLabel.Text = "Final Score: " + patternLength;
					playButton.IsVisible = true;
					playButton.IsEnabled = true;
					playerGuess.Clear();
					buttonPattern.Clear();
				}
				//if the guess is correct, play the necessary sound effect and proceed
				else 
				{
					//DependencyService.Get<IAudio>().PlayAudioFile(soundStrings[buttonPattern[i]]);
					//Refresh();
				}
			numGuess++;

			//}
			//checks if the player has completed the pattern for this round
			if (numGuess == buttonPattern.Count)
			{
				//resets necessary variables
				playerGuess.Clear();
				patternLength++;
				numGuess = 0;
				await Task.Delay(1000);
				//proceeds to next round
				patternGenerate();
			}
		}
	}
}
