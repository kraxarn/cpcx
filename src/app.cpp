#include "app.hpp"

App::App(const QString &name, App::AppType type) : name(name), type(type)
{
	secondsRunning = 0;
}

App::App(const QString &name) : App(name, getType(name))
{
}

QString App::getTimeRunning()
{
	auto s = secondsRunning;
	auto m = 0;
	while (s >= 60)
	{
		m++;
		s -= 60;
	}
	auto mStr = QString(s < 10 ? "0%1" : "%1").arg(s);
	auto hStr = QString(m < 10 ? "0%1" : "%1").arg(m);
	return QString("%1:%2").arg(hStr).arg(mStr);
}

unsigned int App::getMinutesRemaining()
{
	return secondsRunning / 60;
}

void App::addRuntime(unsigned int seconds)
{
	secondsRunning += seconds;
}

App::AppType App::getType(const QString &appName)
{
	auto name = appName.toLower();
	// Music
	if (name == "itunes" || name == "spotify" || name == "wmplayer")
		return AppType::Music;
	// Productivity
	if (name == "audacity" || name == "afterfx" || name == "illustrator" || name == "photoshop"
		|| name == "adobe premiere pro" || name == "gimp-2.10" || name == "vegas140")
		return AppType::Productivity;
	// Text editor
	if (name == "winword" || name == "soffice.bin")
		return AppType::TextEditor;
	// Other editor
	if (name == "powerpnt" || name == "excel")
		return AppType::OtherEditor;
	// Code
	if (name == "codeblocks" || name == "devenv" || name == "processing" || name == "studio64" || name == "idea64")
		return AppType::Code;
	// Web browsers
	if (name == "iexplore" || name == "firefox" || name == "chrome")
		return AppType::WebBrowser;
	// Viewer
	if (name == "vlc")
		return AppType::Viewer;
	// Communication
	if (name == "skype" || name == "discord" || name == "hxoutlook" || name == "ts3client_win64")
		return AppType::Communication;
	// Utility
	if (name == "ccleaner64")
		return AppType::Utility;
	// Game
	if (name == "steam" || name == "minecraft")
		return AppType::Game;
	// Other
	if (name == "virtualbox" || name == "vmplayer")
		return AppType::Other;
	// None
	return AppType::None;
}

QString App::toString()
{
	return QString("%1 %2").arg(name).arg(getTimeRunning());
}
QString App::getTypeString()
{
	switch (type)
	{
		case AppType::None:          return "None";
		case AppType::Music:         return "Music";
		case AppType::Productivity:  return "Productivity";
		case AppType::TextEditor:    return "TextEditor";
		case AppType::OtherEditor:   return "OtherEditor";
		case AppType::Code:          return "Code";
		case AppType::WebBrowser:    return "WebBrowser";
		case AppType::Viewer:        return "Viewer";
		case AppType::Communication: return "Communication";
		case AppType::Utility:       return "Utility";
		case AppType::Game:          return "Game";
		case AppType::Other:         return "Other";
	}
	return QString();
}
