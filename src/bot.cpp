#include "bot.hpp"

Bot::Bot(UserInterface *ui, const Settings &settings) : ui(ui), cfg(settings),
	// Default mood is restless (50%)
	mood(50.0),
	// Default energy is full (100%)
	energy(100.0)
{
	dataPath = "./Data";
	totalMemory = getTotalMemory();
	QTimer::connect(&timer, &QTimer::timeout, this, &Bot::timerTimeout);
	timer.start(5000);
}

Bot::Mood Bot::getCurrentMood()
{
	// Sleepy if low on energy
	if (energy <= 25)
		return Mood::Sleepy;

	// Angry if very low mood (15%)
	if (mood < 10)
		return Mood::Angry;

	// Excited if high mood (15%)
	if (mood > 75)
		return Mood::Excited;

	// Thankful if very high mood (10%)
	if (mood > 90)
		return Mood::Thankful;

	// Restless if mood is in the middle (20%)
	if (mood > 40 && mood < 60)
		return Mood::Restless;

	// Default annoyed (40%)
	return Mood::Annoyed;
}

float Bot::getAverageUpdateTime()
{
	auto total = 0u;
	for (auto &time : updateTimes)
		total += time;
	return (float) total / updateTimes.count();
}

QVector<App> Bot::getRunningApps()
{
	QVector<App> apps;
	QHashIterator<QString, App> iter(runningApps);
	while (iter.hasNext())
		apps.append(iter.next().value());
	return apps;
}

QString Bot::getCurrentMoodString()
{
	QString currentMood;
	switch (getCurrentMood())
	{
		case Mood::Angry:    currentMood = "Angry";    break;
		case Mood::Annoyed:  currentMood = "Annoyed";  break;
		case Mood::Excited:  currentMood = "Excited";  break;
		case Mood::Restless: currentMood = "Restless"; break;
		case Mood::Sleepy:   currentMood = "Sleepy";   break;
		case Mood::Thankful: currentMood = "Thankful"; break;
	}
	return QString("%1 (%2%)").arg(currentMood).arg(QString::number(mood, 'g', 2));
}

QString Bot::getCurrentEnergyString()
{
	return QString::number(energy, 'g', 2);
}

QString Bot::getMoodModifier()
{
	return mood < 50 ? "Bad" : "Good";
}

float Bot::getCpuUsage()
{
	auto total = 0u;
	for (auto &line : executePs({"-eo", "c"}))
		total += line.toUInt();
	// On error, this will divide by 0
	return (float) total / std::thread::hardware_concurrency();
}

unsigned int Bot::getRamUsage()
{
	auto total = 0u;
	for (auto &line : executePs({"-eo", "size"}))
		total += line.toUInt();
	// ps prints in kb, we want mb
	return total / 1000;
}

unsigned int Bot::getTotalMemory()
{
	QFile memInfo("/proc/meminfo");
	memInfo.open(QIODevice::ReadOnly);
	// Assume total memory is first line in kb
	return memInfo.readAll().split('\n')[0].split(' ')[1].toUInt() / 1000;
}

QStringList Bot::getRunningProcesses()
{
	return executePs({"-eo", "comm"});
}

QStringList Bot::executePs(QStringList args)
{
	QProcess process;
	process.execute("/usr/bin/ps", args);
	return QString(process.readAllStandardOutput()).split('\n');
}

void Bot::timerTimeout()
{
	QElapsedTimer stopwatch;
	stopwatch.start();

	ui->moveWindow(1, 0);
	auto processes = getRunningProcesses();
	QSet<QString> currentApps;

	for (auto &p : processes)
	{
		// Check app type
		auto type = App::getType(p);
		if (type == App::AppType::None)
			continue;
		// Add app to list
		currentApps.insert(p);
	}
	QStringList newApps;
	for (auto &x : currentApps)
		if (!runningApps.contains(x))
			newApps.append(x);
	// Check if they are different
	if (currentApps.count() > runningApps.count())
	{
		auto count = currentApps.count() - runningApps.count();
		qDebug() << count << "new app(s) added";
		for (auto &app : currentApps)
			runningApps[app] = App(app);
		// Lose 4 mood for every new app start
		updateMood(-(count * 4));
		// Say something on start
		// TODO: Should not occur on first start, find another way to check
		if (newApps.count() > 1)
			qDebug() << "warning: more than one app started at once";
		say(QString("%1/Start").arg(App(newApps[0]).getTypeString()));
	}
	else if (runningApps.count() > currentApps.count())
	{
		auto count = runningApps.count() - currentApps.count();
		qDebug() << count << "new app(s) closed";
		QSet<QString> toRemove;
		QHashIterator<QString, App> iter(runningApps);
		while (iter.hasNext())
		{
			auto app = iter.next();
			if (!currentApps.contains(app.key()))
				toRemove.insert(app.key());
		}
		for (auto &rem : toRemove)
			runningApps.remove(rem);
		// Gain 2 mood for every app close
		updateMood(count * 2);
	}
	QHashIterator<QString, App> iter(runningApps);
	while (iter.hasNext())
		updateAppRuntime(iter.next().value(), 5);
	// Check cpu and ram
	auto cpuUsage = getCpuUsage();
	auto ramUsage = getRamUsage() / getTotalMemory();
	auto avgUsage = (cpuUsage + ramUsage) / 2;

	auto cpuLevel = toUsageLevel(cpuUsage);
	auto ramLevel = toUsageLevel(ramUsage);

	// Update mood from cpu/ram
	// Goes from 50% (0% usage) to -50% (100% usage)
	auto m = 50 - (int) avgUsage;
	auto mo = 1 + m / 3000.f; // -2% (100% usage) to +2% (0% usage)
	mood *= mo;
	// TODO: Mood changed event
	qDebug().nospace() << "CPU: " << usageLevelToString(cpuLevel) << " (" << cpuUsage << "%), RAM: "
		<< usageLevelToString(ramLevel) << " (" << ramUsage << "%), Mod: " << m << "%/" << mo << "x"
		<< "(Avg: " << avgUsage << "%)";
	// Reduce energy based on usage
	updateEnergy(-(cpuUsage / 400.f));
	updateTimes.append(stopwatch.elapsed());
	qDebug() << "checked" << processes.length() << "processes in" << stopwatch.elapsed() << "ms (Average"
		<< getAverageUpdateTime() << "ms)";
	currentApps.clear();
	qDebug();
}

QPixmap Bot::getMoodBitmap()
{
	const char **data = nullptr;
	switch (getCurrentMood())
	{
		case Mood::Angry:    data = angry_xpm;    break;
		case Mood::Annoyed:  data = annoyed_xpm;  break;
		case Mood::Excited:  data = excited_xpm;  break;
		case Mood::Restless: data = restless_xpm; break;
		case Mood::Sleepy:   data = sleepy_xpm;   break;
		case Mood::Thankful: data = thankful_xpm; break;
	}
	return QPixmap(data).scaled(192, 192);
}

Bot::UsageLevel Bot::toUsageLevel(unsigned int usage)
{
	return usage <= 25
		? UsageLevel::Low
		: usage >= 75
			? UsageLevel::High
			: UsageLevel::Normal;
}

void Bot::show(const QString &fileName)
{
	// Update mood in ui
	ui->setBackground(getMoodBitmap());
	// Show
	ui->setVisible(true);

	while (true)
	{
		if (position < -192)
		{
			showSpeechBubble();
			if (!fileName.isEmpty())
			{
				// Currently no tts
				QMediaPlayer player;
				QMediaPlayer::connect(&player, &QMediaPlayer::stateChanged, [this](QMediaPlayer::State state) {
					if (state == QMediaPlayer::PlayingState)
						return;
					QThread::sleep(1);
					hide();
				});
				player.setMedia(QUrl::fromLocalFile(QString("%1.%2")
					.arg(fileName).arg(AUDIO_FORMAT)));
				player.setVolume(90);
				player.play();
			}
			return;
		}
		position -= 3;
		ui->moveWindow(-3, 0);
		QThread::msleep(1);
	}
}

void Bot::showSpeechBubble(const QString &text)
{
	speechBubble->setDisplayMessage(text);
	speechBubble->show();
}

void Bot::hideSpeechBubble()
{
	speechBubble->hide();
}

void Bot::hide()
{
	hideSpeechBubble();
	while (true)
	{
		if (position > 0)
		{
			ui->setVisible(false);
			return;
		}
		position += 3;
		ui->moveWindow(3, 0);
		QThread::msleep(1);
	}
}

void Bot::say(const QString &type, const QString &moodModifier)
{
	// Ignore is already saying something
	if (speechBubble->isSaying())
		return;
	// Default value for mood mod
	auto moodMod = moodModifier.isEmpty()
		? getMoodModifier()
		: moodModifier;
	qDebug() << "say:" << type;
	// Get how many voice lines we have
	// Type can be Music/Running
	auto path = QString("%1/Voice/Default/%2/%3/").arg(dataPath).arg(moodMod).arg(type);
	auto lines = QString(QFile(QString("%1/lines.txt").arg(path)).readAll()).split('\n');
	// Get random line
	auto ran = rng->bounded(lines.length());
	// Load message
	auto msg = lines.at(ran);
	speechBubble->setDisplayMessage(msg);
	show(QString("%1%2").arg(path).arg(ran));
}

void Bot::updateMood(int value)
{

}

QString Bot::usageLevelToString(UsageLevel level)
{
	switch (level)
	{
		case UsageLevel::Low:    return "Low";
		case UsageLevel::Normal: return "Normal";
		case UsageLevel::High:   return "High";
	}
	return "Unknown";
}
