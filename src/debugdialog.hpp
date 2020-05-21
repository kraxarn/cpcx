#pragma once

#include "bot.hpp"

#include <QDialog>
#include <QVBoxLayout>
#include <QHBoxLayout>
#include <QComboBox>

class DebugDialog : public QDialog
{
Q_OBJECT

public:
	DebugDialog(const Bot &bot, QWidget *parent = nullptr);

private:
	QComboBox *pathType, *pathAction, *mood;
};


