#pragma once

#include <QString>

class App
{
public:

	enum class AppType
	{
		None,
		Music,
		Productivity,
		TextEditor,
		OtherEditor,
		Code,
		WebBrowser,
		Viewer,
		Communication,
		Utility,
		Game,
		Other
	};

	App(const QString &name, AppType type);
	App(const QString &name);

	QString name;
	AppType type;

	QString getTimeRunning();
	unsigned int getMinutesRemaining();
	void addRuntime(unsigned int seconds);
	static AppType getType(const QString &appName);
	QString toString();
	QString getTypeString();

private:
	unsigned int secondsRunning;
};