#include "speechbubble.hpp"

#define PADDING 8

SpeechBubble::SpeechBubble(QWidget *parent) : QWidget(parent)
{
	auto background = new QLabel(this);
	QPixmap speech(speech_xpm);
	background->setPixmap(speech);
	resize(speech.size());

	text = new QLabel("This is some really long sample text in order to properly try out text wrapping.", this);
	text->move(PADDING, 0);
	text->resize(size().width() - (PADDING * 2),
		size().height() - (PADDING * 2));
	text->setStyleSheet("QLabel { color: black; }");
	text->setWordWrap(true);
}

void SpeechBubble::setDisplayMessage(const QString &message)
{
	text->setText(message);
}

bool SpeechBubble::isSaying()
{
	return false;
}