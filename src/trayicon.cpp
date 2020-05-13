#include "trayicon.hpp"

TrayIcon::TrayIcon(QObject *parent) : QSystemTrayIcon(parent)
{
	setIcon(QPixmap(icon_xpm));
	auto menu = new QMenu();
	menu->addAction("📊 Stats");
	QAction::connect(menu->addAction("⚙️ Settings"), &QAction::triggered, [this](bool checked) {
		(new SettingsDialog())->show();
	});
	menu->addAction("📢 Test voice lines");
	menu->addSeparator();
	menu->addAction("Version 2020.05.22")->setEnabled(false);
	QAction::connect(menu->addAction("❌ Close"), &QAction::triggered, [](bool checked) {
		QCoreApplication::quit();
	});
	setContextMenu(menu);
	show();
}