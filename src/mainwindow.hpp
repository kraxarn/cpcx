#pragma once

#include "trayicon.hpp"
#include "speechbubble.hpp"

#include <QMainWindow>

class MainWindow : public QMainWindow
{
public:
	MainWindow();
	SpeechBubble *speechBubble;
};