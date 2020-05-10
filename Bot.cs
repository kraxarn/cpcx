using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Speech.Synthesis;
using System.Threading;
using System.Windows.Forms;
using CPC.Properties;
using Microsoft.Win32;
using NAudio.Wave;
using Timer = System.Windows.Forms.Timer;

namespace CPC
{
	public sealed class Bot
	{
		public  readonly string DataPath;
		private readonly IUserInterface ui;
		private readonly Dictionary<string, App> runningApps;
		private readonly Random rng;
		private int position;
		private readonly FormSpeechBubble speechBubble;

		private enum Mood
		{
			Angry,
			Annoyed,
			Excited,
			Restless,
			Sleepy,
			Thankful
		}

		private enum UsageLevel
		{
			Low,
			Normal,
			High
		}

		private Mood CurrentMood
		{
			get
			{
				// Sleepy if low on energy
				if (energy <= 25)
					return Mood.Sleepy;
				
				// Angry if very low mood (15%)
				if (mood < 10)
					return Mood.Angry;
				
				// Excited if high mood (15%)
				if (mood > 75)
					return Mood.Excited;

				// Thankful if very high mood (10%)
				if (mood > 90)
					return Mood.Thankful;

				// Restless if mood is in the middle (20%)
				if (mood > 40 && mood < 60)
					return Mood.Restless;
				
				// Default annoyed (40%)
				return Mood.Annoyed;
			}
		}

		/// <summary>
		/// Energy 0-100%
		/// </summary>
		private float energy;

		/// <summary>
		/// Current mood, 0 = angry, 100 = happy
		/// </summary>
		private float mood;

		private readonly List<uint> updateTimes;

		private float AverageUpdateTime
		{
			get
			{
				uint total = 0;
				foreach (var time in updateTimes)
					total += time;

				return (float) total / updateTimes.Count;
			}
		}

		public IEnumerable<App> RunningApps => from app in runningApps select app.Value;
		public string CurrentMoodString   => $"{CurrentMood} ({mood:F}%)";
		public string CurrentEnergyString => $"{energy:F}";
		private readonly Settings cfg;

		public string MoodModifier 
			=> mood < 50 ? "Bad" : "Good";

		// NAudio
		private WaveOutEvent    outputDevice;
		private AudioFileReader audioFile;

		// For checking performance
		private readonly PerformanceCounter cpu, ram;
		private readonly uint totalMemory;

		// For text to speech
		private readonly SpeechSynthesizer speech;

		// Event tests for mood and energy
		public delegate void EnergyEvent(string energy);
		public delegate void MoodEvent(string mood);
		public delegate void AppEvent(IEnumerable<App> apps);

		public event EnergyEvent EnergyChanged;
		public event MoodEvent   MoodChanged;
		public event AppEvent    AppsChanged;

		public Bot(IUserInterface ui, Settings settings)
		{
			this.ui = ui;
			cfg = settings;

			DataPath = "./Data";

			updateTimes  = new List<uint>();
			runningApps  = new Dictionary<string, App>();
			speechBubble = new FormSpeechBubble();
			rng = new Random();

			speech = new SpeechSynthesizer();
			speech.SetOutputToDefaultAudioDevice();
			speech.SpeakCompleted += (o, eventArgs) => Hide();

			// Default mood to restless (50%)
			mood = 50;
			// Default energy to full (100)
			energy = 100;

			// Just reset energy when going from sleep mode
			SystemEvents.PowerModeChanged += (sender, args) =>
			{
				if (args.Mode == PowerModes.Resume)
					energy = 100;
			};
			
			var timer = new Timer();
			timer.Tick += TimerOnTick;
			timer.Interval = 5000;
			timer.Enabled = true;

			cpu = new PerformanceCounter("Processor", "% Processor Time", "_Total");
			ram = new PerformanceCounter("Memory", "Available MBytes");

			// Get total memory
			var query = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
			var searcher = new ManagementObjectSearcher(query);
			foreach (var s in searcher.Get())
				totalMemory = Convert.ToUInt32(s["TotalVisibleMemorySize"]) / 1024;

			TimerOnTick(this, null);
		}

		private void UpdateEnergy(float change)
		{
			energy += change;
			EnergyChanged?.Invoke(CurrentEnergyString);
		}

		private void UpdateMood(float change)
		{
			mood += change;
			MoodChanged?.Invoke(CurrentMoodString);
		}

		private void UpdateAppRuntime(App app, uint seconds)
		{
			app.AddRuntime(seconds);
			AppsChanged?.Invoke(RunningApps);
		}

		private Bitmap GetMoodBitmap()
		{
			switch (CurrentMood)
			{
				case Mood.Angry: return Resources.Angry;
				case Mood.Annoyed: return Resources.Annoyed;
				case Mood.Excited: return Resources.Excited;
				case Mood.Restless: return Resources.Restless;
				case Mood.Sleepy: return Resources.Sleepy;
				case Mood.Thankful: return Resources.Thankful;

				default:
					throw new InvalidOperationException("Invalid mood");
			}
		}

		private UsageLevel ToUsageLevel(uint usage)
		{
			if (usage <= 25)
				return UsageLevel.Low;
			if (usage >= 75)
				return UsageLevel.High;
			return UsageLevel.Normal;
		}

		public void Show(string fileName = null)
		{
			// Update mood in ui
			ui.Background = GetMoodBitmap();

			// Show
			ui.IsVisible = true;

			var timer = new Timer
			{
				Interval = 1
			};

			timer.Tick += (sender, args) =>
			{
				if (position < -192)
				{
					timer.Stop();
					ShowSpeechBubble();

					if (fileName != null)
					{
						if (cfg.Voice == Settings.Voices.Computer)
							speech.SpeakAsync(fileName);
						else
						{
							outputDevice = new WaveOutEvent();
							outputDevice.PlaybackStopped += (o, eventArgs) =>
							{
								Thread.Sleep(1000);
								Hide();
							};

							try
							{
								audioFile = new AudioFileReader(fileName);
							}
							catch (FileNotFoundException e)
							{
								MessageBox.Show($@"{e.Message}{Environment.NewLine}The application will now (sadly) terminate.", @"File not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
								Environment.Exit(1);
							}

							outputDevice.Init(audioFile);

							outputDevice.Play();
						}
					}
				}

				position -= 3;
				ui.MoveWindow(-3, 0);
			};

			timer.Start();
		}

		public void Hide()
		{
			var timer = new Timer
			{
				Interval = 1
			};

			HideSpeechBubble();

			timer.Tick += (sender, args) =>
			{
				if (position > 0)
				{
					timer.Stop();
					// Hide when done
					ui.IsVisible = false;
				}

				position += 3;
				ui.MoveWindow(3, 0);
			};

			timer.Start();
		}

		public void ShowSpeechBubble(string text = null)
		{
			/*
			if (text != null)
				speechBubble.DisplayMessage = text;
			*/

			Console.WriteLine($@"Speech bubble invoke required: {speechBubble.InvokeRequired}");

			if (speechBubble.InvokeRequired)
			{
				speechBubble.Invoke((MethodInvoker) delegate
				{
					speechBubble.DisplayMessage = text;
					speechBubble.Show();
				});
			}
			else
				speechBubble.Show();
		}

		private void HideSpeechBubble()
		{
			if (speechBubble.InvokeRequired)
			{
				speechBubble.Invoke(new MethodInvoker(delegate
				{
					speechBubble.Hide();
				}));
			}
			else
				speechBubble.Hide();
		}

		public void Say(string type, string moodMod = null)
		{
			// Skip if we're already saying something
			if (speechBubble.Saying)
				return;

			// If moodMod is null, set it
			if (moodMod == null)
				moodMod = MoodModifier;

			// Debugging
			Console.WriteLine($@"Say: {type}");

			// Get how many voice lines we have
			// Type can be Music/Running
			// TODO: Don't assume 'Bad'
			var path = $@"{DataPath}/Voice/Default/{moodMod}/{type}/";
			var lines = File.ReadAllLines($"{path}lines.txt");

			// Get random line
			var ran = rng.Next(lines.Length);

			// Load message
			var msg = lines[ran];
			speechBubble.DisplayMessage = msg;

			// Say it
			Show(cfg.Voice == Settings.Voices.Computer ? msg : $"{path}{ran}.wav");
		}

		private void TimerOnTick(object sender, EventArgs e)
		{
			var stopwatch = new Stopwatch();
			stopwatch.Start();

			ui.MoveWindow(1, 0);

			var processes   = Process.GetProcesses();
			var currentApps = new HashSet<string>();
			var firstLaunch = sender == this;

			foreach (var p in processes)
			{
				// Common Windows services always running
				switch (p.ProcessName)
				{
					case "conhost":
					case "csrss":
					case "explorer":
					case "RuntimeBroker":
					case "svchost":
						continue;
				}

				// Check app type
				var type = App.GetType(p.ProcessName);
				if (type == App.AppType.None)
					continue;

				// Add app to list
				currentApps.Add(p.ProcessName);
				
				// Add app to dictionary
				/*
				if (!runningApps.ContainsKey(p.ProcessName))
				{
					runningApps[p.ProcessName] = new App(p.ProcessName, type);
					newApps.Add(p.ProcessName);
				}
				*/
			}
			
			var newApps = currentApps.Where(x => runningApps.All(y => y.Key != x)).ToList();

			// Check if they are different
			if (currentApps.Count > runningApps.Count)
			{
				var count = currentApps.Count - runningApps.Count;
				Console.WriteLine(count == 1 ? "1 new app added" : $@"{currentApps.Count - runningApps.Count} new apps added");

				foreach (var app in currentApps)
				{
					if (!runningApps.ContainsKey(app))
						runningApps[app] = new App(app);
				}

				// Lose 4 mood for every app start
				UpdateMood(-(count * 4));

				// Say something if not first time
				if (!firstLaunch)
				{
					// TODO: Check if multiple apps started at once?
					if (newApps.Count > 1)
						Console.WriteLine(@"Warning: More than one app started at once");

					var a = new App(newApps[0]);
					Say($"{a.Type}/Start");
				}
			}
			else if (runningApps.Count > currentApps.Count)
			{
				var count = runningApps.Count - currentApps.Count;
				Console.WriteLine(count == 1 ? "1 new app closed" : $@"{runningApps.Count - currentApps.Count} new apps closed");

				var toRemove = new HashSet<string>();

				foreach (var app in runningApps)
				{
					if (!currentApps.Contains(app.Key))
						toRemove.Add(app.Key);
				}

				foreach (var rem in toRemove)
					runningApps.Remove(rem);

				// Gain 2 mood for every app close
				UpdateMood(count * 2);
			}

			// TODO: Change this when final update time is decided
			foreach (var app in runningApps)
				UpdateAppRuntime(app.Value, 5);

			// Check CPU and RAM
			var cpuUsage = (uint) cpu.NextValue();
			var ramUsage = (uint) (ram.NextValue() / totalMemory * 100);
			var avgUsage = (cpuUsage + ramUsage) / 2;

			var cpuLevel = ToUsageLevel(cpuUsage);
			var ramLevel = ToUsageLevel(ramUsage);

			// Update mood based on cpu/ram usage
			// Goes from 50% (0% usage) to -50% (100% usage)
			var m = 50 - (int) avgUsage;
			var mo = 1 + m / 3000f;	// -2% (100% usage) to +2% (0% usage)
			mood *= mo;
			MoodChanged?.Invoke(CurrentMoodString);

			Console.WriteLine($@"CPU: {cpuLevel} ({cpuUsage}%), RAM: {ramLevel} ({ramUsage}%), Mod: {m:F}%/{mo:f}x (Avg: {avgUsage}%)");

			// Reduce energy based on cpu usage
			UpdateEnergy(-(cpuUsage / 400f));

			updateTimes.Add(Convert.ToUInt32(stopwatch.ElapsedMilliseconds));
			Console.WriteLine($@"Checked {processes.Length} processes in {stopwatch.ElapsedMilliseconds} ms (Average {AverageUpdateTime:F} ms)");

			if (firstLaunch)
				Console.WriteLine(@"Note: Ignoring first launch");

			currentApps.Clear();

			Console.WriteLine();
		}
	}
}