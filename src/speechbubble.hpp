#pragma once

#include <QString>

class SpeechBubble
{
public:
	void setDisplayMessage(const QString &text);
	void show();
	void hide();
	bool isSaying();
};