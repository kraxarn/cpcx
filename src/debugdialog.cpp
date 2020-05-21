#include "debugdialog.hpp"

DebugDialog::DebugDialog(const Bot &bot, QWidget *parent) : QDialog(parent)
{
	auto layout = new QVBoxLayout();
	auto pathLayout = new QHBoxLayout();
	auto path = QString("%1/Voice/Default/%2")
		.arg(bot.dataPath).arg(bot.getMoodModifier());
}