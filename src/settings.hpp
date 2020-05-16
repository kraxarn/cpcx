#pragma once

#include <QFile>
#include <QVariant>
#include <QDebug>

class Settings
{
public:
	Settings()
	{
	}

	enum Voice
	{
		VoiceDefault,
		VoiceComputer
	};

	enum Personality
	{
		PersonalityFriendly,
		PersonalitySnarky,
		PersonalityHomicidal,
		PersonalityOverkill
	};

	enum Notification
	{
		NotificationBoth,
		NotificationSound,
		NotificationSpeechBubble
	};

	enum ComplainAbout
	{
		ComplainAboutBoth,
		ComplainAboutApps,
		ComplainAboutResources
	};

	bool lowerOtherSounds = false;
	bool ignoreWhenFullscreen = false;
	Personality personality = PersonalityHomicidal;
	Voice voice = VoiceDefault;
	Notification notification = NotificationBoth;
	ComplainAbout complainAbout = ComplainAboutBoth;

	void load();
	void save();
};