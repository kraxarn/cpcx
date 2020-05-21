#include "trayicon.hpp"

TrayIcon::TrayIcon(const Bot &bot, QObject *parent) : QSystemTrayIcon(parent)
{
	setIcon(QPixmap(icon_xpm));
	auto menu = new QMenu();
	menu->addAction("📊 Stats");
	QAction::connect(menu->addAction("⚙️ Settings"), &QAction::triggered, [](bool checked) {
		(new SettingsDialog())->show();
	});
	QAction::connect(menu->addAction("📢 Test voice lines"), &QAction::triggered, [&bot](bool checked) {
		(new DebugDialog(bot))->show();
	});
	menu->addSeparator();
	menu->addAction("Version 2020.05.22")->setEnabled(false);
	QAction::connect(menu->addAction("❌ Close"), &QAction::triggered, [](bool checked) {
		QCoreApplication::quit();
	});
	setContextMenu(menu);
	show();
}