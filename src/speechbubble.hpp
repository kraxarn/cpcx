#pragma once

#include "res/speech.xpm"

#include <QString>
#include <QLabel>

class SpeechBubble : public QWidget
{
public:
	SpeechBubble(QWidget *parent = nullptr);

	void setDisplayMessage(const QString &message);
	bool isSaying();

private:
	QLabel	*text;
};