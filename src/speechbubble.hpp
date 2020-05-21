#pragma once

#include "res/speech.xpm"

#include <QString>
#include <QLabel>
#include <QGuiApplication>
#include <QScreen>

class SpeechBubble : public QWidget
{
public:
	SpeechBubble(QWidget *parent = nullptr);

	void setDisplayMessage(const QString &message);
	bool isSaying();

private:
	QLabel	*text;
};