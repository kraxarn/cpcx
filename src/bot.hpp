#pragma once

#include <thread>
#include "userinterface.hpp"
#include "app.hpp"
#include "speechbubble.hpp"
#include "settings.hpp"

#include "res/angry.xpm"
#include "res/annoyed.xpm"
#include "res/excited.xpm"
#include "res/restless.xpm"
#include "res/sleepy.xpm"
#include "res/thankful.xpm"

#include <QString>
#include <QHash>
#include <QRandomGenerator>
#include <QMediaPlayer>
#include <QProcess>
#include <QFile>
#include <QTimer>
#include <QThread>
#include <QElapsedTimer>

#define AUDIO_FORMAT "m4a"

class Bot
{
public:
	Bot(UserInterface *ui, const Settings &settings);

	QString dataPath;

	QVector<App> getRunningApps();
	QString getCurrentMoodString();
	QString getCurrentEnergyString();
	QString getMoodModifier() const;
	void show(const QString &fileName = QString());
	void hide();
	void say(const QString &type, const QString &moodModifier = QString());

private:
	UserInterface *ui = nullptr;
	QHash<QString, App> runningApps;
	QRandomGenerator *rng = QRandomGenerator::global();
	int position = 0;
	SpeechBubble *speechBubble = nullptr;
	QVector<unsigned int> updateTimes;
	Settings cfg;
	QTimer timer;

	/**
	 * Energy 0-100%
	 */
	float energy;

	/**
	 * Current mood, 0 = angry, 100 = happy
	 */
	float mood;

	enum class Mood
	{
		Angry,
		Annoyed,
		Excited,
		Restless,
		Sleepy,
		Thankful
	};
	enum class UsageLevel
	{
		Low,
		Normal,
		High
	};

	Mood getCurrentMood();
	float getAverageUpdateTime();
	float getCpuUsage();
	unsigned int getRamUsage();
	unsigned int getTotalMemory();
	QStringList getRunningProcesses();
	QStringList executePs(QStringList args);
	void timerTimeout();
	QPixmap getMoodBitmap();
	UsageLevel toUsageLevel(unsigned int usage);
	QString usageLevelToString(UsageLevel level);
	void showSpeechBubble(const QString &text = QString());
	void hideSpeechBubble();
	void updateMood(int value);
	void updateAppRuntime(const App &app, int time);
	void updateEnergy(float energy);
};