#pragma once

#include "app.hpp"
#include "bot.hpp"

#include <QDialog>
#include <QVBoxLayout>
#include <QTreeWidget>
#include <QTreeWidgetItem>

class StatsDialog : public QDialog
{
Q_OBJECT
public:
	StatsDialog(const QVector<App> &apps, const QString &mood,
		const QString &energy, const Bot &bot, QWidget *parent = nullptr);
private:

};