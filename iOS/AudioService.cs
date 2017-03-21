using System;
using Xamarin.Forms;
using SimonSaysGame;      // change this to the name of your project namespace 
using SimonSaysGame.iOS;  // same as above 
using System.IO;
using Foundation;
using AVFoundation;

[assembly: Dependency(typeof(AudioService))]
namespace SimonSaysGame.iOS  //change to match your project namespace 
{
	public class AudioService : IAudio
	{
		public AudioService()
		{
		}
		public void PlayAudioFile(string fileName)
		{
			string sFilePath = NSBundle.MainBundle.PathForResource
			(Path.GetFileNameWithoutExtension(fileName), Path.GetExtension(fileName));
			var url = NSUrl.FromString(sFilePath);
			var _player = AVAudioPlayer.FromUrl(url);
			_player.FinishedPlaying += (object sender, AVStatusEventArgs e) =>
			{
				_player = null;
			};
			_player.Play();
		}
	}
}