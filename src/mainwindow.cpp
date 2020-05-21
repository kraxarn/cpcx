#include "mainwindow.hpp"

MainWindow::MainWindow()
{
	setFixedSize(192, 192);
	auto screenSize = QGuiApplication::screens()[0]->size();
	move(screenSize.width(), screenSize.height() * 0.75);
	background = new QLabel(this);
	setCentralWidget(background);
	setWindowFlags(Qt::FramelessWindowHint);
	setAttribute(Qt::WA_TranslucentBackground);
	speechBubble = new SpeechBubble();
	bot = new Bot(this, speechBubble, Settings());
	new TrayIcon(*bot, this);
}

void MainWindow::setBackground(const QPixmap &background)
{
	this->background->setPixmap(background);
}

void MainWindow::moveWindow(int x, int y)
{
	move(pos().x() + x, pos().y() + y);
}

void MainWindow::setWindowVisible(bool visible)
{
	setVisible(visible);
}
