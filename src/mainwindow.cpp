#include "mainwindow.hpp"

MainWindow::MainWindow()
{
	setFixedSize(192, 192);
	setWindowFlags(Qt::FramelessWindowHint);
	new TrayIcon(this);
	speechBubble = new SpeechBubble();
	speechBubble->show();
}
