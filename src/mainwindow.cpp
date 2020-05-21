#include "mainwindow.hpp"

MainWindow::MainWindow()
{
	setFixedSize(192, 192);
	background = new QLabel(this);
	setCentralWidget(background);
	//setWindowFlags(Qt::FramelessWindowHint);
	bot = new Bot(this, Settings());
	new TrayIcon(*bot, this);
	speechBubble = new SpeechBubble();
	speechBubble->show();
}

void MainWindow::setBackground(const QPixmap &background)
{
	this->background->setPixmap(background);
}

void MainWindow::moveWindow(int x, int y)
{
	move(x, y);
}

void MainWindow::setWindowVisible(bool visible)
{
	setVisible(visible);
}
