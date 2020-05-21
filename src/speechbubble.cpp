#include "speechbubble.hpp"

#define PADDING 8

SpeechBubble::SpeechBubble(QWidget *parent) : QWidget(parent)
{
	setWindowFlags(Qt::FramelessWindowHint);
	setAttribute(Qt::WA_TranslucentBackground);
	auto background = new QLabel(this);
	QPixmap speech(speech_xpm);
	background->setPixmap(speech);
	resize(speech.size());
	auto screenSize = QGuiApplication::screens()[0]->size();
	move(screenSize.width() - 370, (screenSize.height() * 0.75) - 140);

	text = new QLabel(this);
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