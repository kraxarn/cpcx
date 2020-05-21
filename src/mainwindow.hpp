#pragma once

#include "trayicon.hpp"
#include "speechbubble.hpp"

#include <QMainWindow>

class MainWindow : public QMainWindow, public UserInterface
{
	Q_OBJECT

public:
	MainWindow();
	void setBackground(const QPixmap &background) override;
	void moveWindow(int x, int y) override;
	void setWindowVisible(bool visible) override;

private:
	QLabel			*background;
	SpeechBubble	*speechBubble;
	Bot				*bot;
};