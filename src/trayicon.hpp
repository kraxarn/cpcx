#pragma once

#include "res/icon.xpm"
#include "settingsdialog.hpp"

#include <QSystemTrayIcon>
#include <QMenu>
#include <QCoreApplication>

class TrayIcon : public QSystemTrayIcon
{
public:
	TrayIcon(QObject *parent = nullptr);
};