#include "settings.hpp"

void Settings::load()
{
	QFile file("config.txt");
	if (!file.exists())
		return;

	file.open(QIODevice::ReadOnly);
	for (auto &line : QString(file.readAll()).split('\n'))
	{
		auto l = line.split('=');
		auto key = l[0];
		auto value = QVariant(l[1]);

		if (key == "LowerOtherSounds")
			lowerOtherSounds = value.toBool();
		else if (key == "IgnoreWhenFullscreen")
			ignoreWhenFullscreen = value.toBool();
		else if (key == "Personality")
			personality = value.value<Personality>();
		else if (key == "Voice")
			voice = value.value<Voice>();
		else if (key == "Notification")
			notification = value.value<Notification>();
		else if (key == "ComplainAbout")
			complainAbout = value.value<ComplainAbout>();
		qDebug() << "warning: unknown setting:" << key;
	}
	file.close();
}

void Settings::save()
{
	auto lines = QString(
		"LowerOtherSounds=%1\nIgnoreWhenFullscreen=%2\nPersonality=%3\n"
			"Voice=%4\nNotification=%5\nComplainAbout=%6")
				.arg(lowerOtherSounds).arg(ignoreWhenFullscreen).arg(personality)
				.arg(voice).arg(notification).arg(complainAbout);
	QFile file("config.txt");
	file.open(QIODevice::WriteOnly);
	file.write(lines.toUtf8());
	file.close();
}
