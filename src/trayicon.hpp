#pragma once

#include "res/icon.xpm"
#include "settingsdialog.hpp"
#include "debugdialog.hpp"

#include <QSystemTrayIcon>
#include <QMenu>
#include <QCoreApplication>

class TrayIcon : public QSystemTrayIcon
{
public:
	TrayIcon(const Bot &bot, QObject *parent = nullptr);
};